using KioskTest.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace KioskTest
{
    public class ExperimentController : MonoBehaviour
    {
        private ExperimentControllerData data = new ExperimentControllerData();

        public ExperimentEventLogger EventLogger;
        public int currentState = -1;

        [Space(20)]
        public Text MainGuideTextUI;

        [Space(20)]
        public AnswerGuideText AnswerGuideText;

        [Space(20)]
        public NumberAnswerIndicator NumberAnswerIndicator;
        public NumberInput NumberInputPanel;

        [Space(20)]
        public MultipleChoiceInput MultipleChoiceInput;

        [Space(20)]
        public Button ConfirmButton;

        //실험 private 변수
        private int[] correctAnswers;

        private void Start()
        {
            DoTest();   //대체로 시작화면 보여줌
        }

        private void DoTest()
        {
            currentState++;

            if(currentState >= 0 && currentState < data.States.Length)
            {
                ExperimentState currentStateData = data.States[currentState];

                MainGuideTextUI.text = currentStateData.MainGuideText;  //제목 설정
                ConfirmButton.interactable = false;
                ConfirmButton.GetComponentInChildren<Text>().text = "확인";


                int answerRange;
                string answerGuideText;
                switch (currentStateData.ContentType)
                {
                    case ExperimentContentType.None:
                        AnswerGuideText.gameObject.SetActive(false);
                        NumberAnswerIndicator.gameObject.SetActive(false);
                        NumberInputPanel.gameObject.SetActive(false);
                        MultipleChoiceInput.gameObject.SetActive(false);

                        ConfirmButton.interactable = true;
                        ConfirmButton.GetComponentInChildren<Text>().text = "실험 재시작";
                        currentState = -1;
                        break;
                    case ExperimentContentType.Number:
                        AnswerGuideText.gameObject.SetActive(false);
                        NumberAnswerIndicator.gameObject.SetActive(true);
                        NumberInputPanel.gameObject.SetActive(true);
                        MultipleChoiceInput.gameObject.SetActive(false);

                        NumberAnswerIndicator.Initialize(currentStateData.AnswerTitle, currentStateData.AnswerCount, currentStateData.AnswerMaxLength);
                        EventLogger.LogTestStart(currentState, currentStateData.ContentType);
                        break;
                    case ExperimentContentType.MultipleSelection:
                        AnswerGuideText.gameObject.SetActive(false);
                        NumberAnswerIndicator.gameObject.SetActive(false);
                        NumberInputPanel.gameObject.SetActive(false);
                        MultipleChoiceInput.gameObject.SetActive(true);

                        MultipleChoiceInput.Initialize(currentStateData.AnswerSet, currentStateData.AnswerCount);
                        EventLogger.LogTestStart(currentState, currentStateData.ContentType);
                        break;

                    //랜덤 정답을 생성해야 하는 경우
                    case ExperimentContentType.NumberWithRandom:
                        AnswerGuideText.gameObject.SetActive(true);
                        NumberAnswerIndicator.gameObject.SetActive(false);
                        NumberInputPanel.gameObject.SetActive(false);
                        MultipleChoiceInput.gameObject.SetActive(false);

                        //정답 랜덤 생성
                        answerRange = currentStateData.AnswerMaxLength * 10;
                        correctAnswers = new int[currentStateData.AnswerCount];
                        answerGuideText = "";
                        for (int i = 0; i < correctAnswers.Length; i++)
                        {
                            bool isNotOk = true;
                            while (isNotOk)
                            {
                                correctAnswers[i] = Random.Range(1, answerRange - 1);
                                isNotOk = false;
                                for (int j = 0; j < i; j++)
                                {
                                    if (correctAnswers[i] == correctAnswers[j])
                                    {
                                        isNotOk = false;
                                        i--;
                                    }
                                }
                            }
                            answerGuideText += correctAnswers[i] + ", ";
                        }

                        AnswerGuideText.Initialize(answerGuideText, DoTestAfterShowAnswer);
                        break;
                    case ExperimentContentType.MultipleSelectionWithRandom:
                        AnswerGuideText.gameObject.SetActive(true);
                        NumberAnswerIndicator.gameObject.SetActive(false);
                        NumberInputPanel.gameObject.SetActive(false);
                        MultipleChoiceInput.gameObject.SetActive(false);

                        //정답 랜덤 생성
                        answerRange = currentStateData.AnswerSet.Length;
                        correctAnswers = new int[currentStateData.AnswerCount];
                        answerGuideText = "";
                        for (int i = 0; i < correctAnswers.Length; i++)
                        {
                            bool isNotOk = true;
                            while(isNotOk)
                            {
                                correctAnswers[i] = Random.Range(1, answerRange - 1);
                                isNotOk = false;
                                for(int j = 0; j < i; j++)
                                {
                                    if (correctAnswers[i] == correctAnswers[j])
                                    {
                                        isNotOk = false;
                                        i--;
                                    }
                                }
                            }
                            answerGuideText += currentStateData.AnswerSet[correctAnswers[i]] + ", ";
                        }

                        AnswerGuideText.Initialize(answerGuideText, DoTestAfterShowAnswer);
                        break;
                }
            }
        }

        private void DoTestAfterShowAnswer()
        {   
            ExperimentState currentStateData = data.States[currentState];

            switch(currentStateData.ContentType)
            {
                case ExperimentContentType.NumberWithRandom:
                    NumberAnswerIndicator.gameObject.SetActive(true);
                    NumberInputPanel.gameObject.SetActive(true);

                    NumberAnswerIndicator.Initialize(currentStateData.AnswerTitle, currentStateData.AnswerCount, currentStateData.AnswerMaxLength);
                    EventLogger.LogTestStart(currentState, currentStateData.ContentType);
                    break;
                case ExperimentContentType.MultipleSelectionWithRandom:
                    MultipleChoiceInput.gameObject.SetActive(true);

                    MultipleChoiceInput.Initialize(currentStateData.AnswerSet, currentStateData.AnswerCount);
                    EventLogger.LogTestStart(currentState, currentStateData.ContentType);
                    break;
            }
        }

        /// <summary>
        /// 사용자가 Confrim 누르기 전 입력을 완료했다고 판단 되었을 때 실행되는 이벤트 핸들러
        /// </summary>
        /// <param name="sender">이벤트 발생 객체</param>
        /// <param name="args">이벤트 발생 정보</param>
        public void OnAnswerSelected(GameObject sender, ExperimentActionEvent.EventArgs args)
        {
            ExperimentState currentStateData = data.States[currentState];
            switch (currentStateData.ContentType)
            {
                case ExperimentContentType.Number:
                case ExperimentContentType.MultipleSelection:
                    if (args.Answers.Length >= currentStateData.AnswerCount)
                    {
                        ConfirmButton.interactable = true;

                        //주의: State List가 바뀌면 실제 성별이나 생일을 묻는 질문인지 검토 필요
                        switch(currentState)
                        {
                            case 0:
                                EventLogger.SetID(args.Answers[0]);
                                break;
                            case 1:
                                EventLogger.LogGender((Gender)args.Answers[0] + 1);
                                break;
                            case 4:
                                EventLogger.LogBirth(args.Answers[0]);
                                break;
                        }
                    }
                    else
                    {
                        ConfirmButton.interactable = false;
                    }
                    break;
                case ExperimentContentType.NumberWithRandom:
                case ExperimentContentType.MultipleSelectionWithRandom:
                    if (args.Answers.Length < currentStateData.AnswerCount)
                    {
                        ConfirmButton.interactable = false;
                        break;
                    }
                    bool isAllCorrect = true;
                    foreach(int answer in args.Answers)
                    {
                        bool isCorrect = false;
                        foreach(int correctAnswer in correctAnswers)
                        {
                            if(correctAnswer == answer)
                            {
                                isCorrect = true;
                            }
                        }

                        isAllCorrect = isAllCorrect && isCorrect;
                    }
                    ConfirmButton.interactable = isAllCorrect;
                    break;
            }
        }

        /// <summary>
        /// 테스트 케이스 종료 시 실행, 사용자가 확인 버튼 누름
        /// </summary>
        public void OnAnswerConfirmed()
        {
            EventLogger.LogTestEnd(currentState);
            EventLogger.ShowCurrent();
            DoTest();
        }
    }
}
