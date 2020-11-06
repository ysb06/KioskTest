using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentEventLogger : MonoBehaviour
{
    public ExperimentEvent CurrentTest = new ExperimentEvent();

    public void LogTestStart(int currentState)
    {
        CurrentTest.ExperimentState = currentState;
        CurrentTest.StartTime = Time.realtimeSinceStartup;
    }

    public void LogTestEnd(int currentState)
    {
        if (CurrentTest.ExperimentState == currentState)
        {
            CurrentTest.EndTime = Time.realtimeSinceStartup;
        }
        else
        {
            Debug.LogError("Something Wrong!");
        }
    }

    public void ShowResult()
    {
        Debug.Log("Unit Test Time: " + (CurrentTest.EndTime - CurrentTest.StartTime));
    }
}

[Serializable]
public struct ExperimentEvent
{
    public int ExperimentState;
    public float StartTime;
    public float EndTime;
}