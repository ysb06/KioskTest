using KioskTest;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gender { None, Male, Female }

/// <summary>
/// 테스트 단위 이벤트
/// </summary>
public enum UnitTestEvent { 
    /// <summary>
    /// 테스트 시작 지점, 정답을 보여준 경우는 정답 보여주고 난 후 시점
    /// </summary>
    Start, 
    /// <summary>
    /// 사용자가 입력을 위해 클릭한 시점
    /// </summary>
    Click, 
    /// <summary>
    /// 사용자가 잘못 눌렀음을 인지하고 선택을 취소한 시점
    /// </summary>
    Cancel, 
    /// <summary>
    /// 스와이프 동작
    /// </summary>
    Swipe,
    /// <summary>
    /// 사용자가 답안 제출을 할 준비가 완료된 시점
    /// </summary>
    Answer, 
    /// <summary>
    /// 사용자가 실제로 Confirm 버튼을 누른 시점
    /// </summary>
    End 
}

public class ExperimentEventLogger : MonoBehaviour
{
    public List<TestUnit> TestList = new List<TestUnit>();
    public TestUnit CurrentTest = new TestUnit();

    public void LogTestStart(int currentStep, ExperimentContentType testType)
    {
        CurrentTest = new TestUnit()
        {
            TestStep = currentStep,
            TestType = testType,
            EventList = new List<TestEvent>()
        };
        CurrentTest.EventList.Add(new TestEvent()
        {
            Event = UnitTestEvent.Start,
            Time = Time.realtimeSinceStartup
        });
    }

    public void LogTest(UnitTestEvent eventType, int value)
    {
        CurrentTest.EventList.Add(new TestEvent()
        {
            Event = eventType,
            Time = Time.realtimeSinceStartup,
            Value = value
        });
    }

    public void LogGender(Gender gender)
    {
        CurrentTest.Gender = gender;
    }

    public void LogBirth(int birth)
    {
        CurrentTest.BirthDate = birth;
    }

    public void LogTestEnd(int currentState)
    {
        CurrentTest.EventList.Add(new TestEvent()
        {
            Event = UnitTestEvent.End,
            Time = Time.realtimeSinceStartup
        });

        TestList.Add(CurrentTest);
    }

    public void ShowCurrent()
    {
        string str = "Current Test\n";
        float start = 0;
        float end = Time.realtimeSinceStartup;
        foreach(TestEvent e in CurrentTest.EventList)
        {
            str += e.Event + ": " + e.Time + "\n";
            if(e.Event == UnitTestEvent.Start)
            {
                start = e.Time;
            }
            if(e.Event == UnitTestEvent.End)
            {
                end = e.Time;
            }
        }
        str += "\nTotal: " + (end - start);
        print(str);
    }
}

[Serializable]
public struct TestUnit
{
    //개인정보
    public int TesterId;
    public Gender Gender;
    public int BirthDate;

    //실험정보
    public int TestStep;
    public ExperimentContentType TestType;
    public List<TestEvent> EventList;

}

[Serializable]
public struct TestEvent
{
    public UnitTestEvent Event;
    public float Time;
    public int Value;
}