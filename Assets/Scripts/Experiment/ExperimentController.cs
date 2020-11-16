using KioskTest.UI;
using KioskTest.UI.Experiment;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace KioskTest.Experiment
{
    public class ExperimentController : MonoBehaviour
    {
        public ExperimentControllerData data = new ExperimentControllerData();

        public ExperimentEventLogger EventLogger;
        public int currentState = -1;

        public AudioSource DingSound;
        public AudioSource BeepSound;

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
        private bool isShowingGuideText = false;

        private void Start()
        {
            DoTest();   //대체로 시작화면 보여줌
        }

        private void DoTest()
        {
            currentState++;
            isShowingGuideText = false;
            AnswerGuideText.isOkToProceed = false;

            if (currentState >= 0 && currentState < data.States.Length)
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
                        DingSound.Play();

                        AnswerGuideText.gameObject.SetActive(false);
                        NumberAnswerIndicator.gameObject.SetActive(true);
                        NumberInputPanel.gameObject.SetActive(true);
                        MultipleChoiceInput.gameObject.SetActive(false);

                        NumberAnswerIndicator.Initialize(currentStateData.AnswerTitle, currentStateData.AnswerCount, currentStateData.AnswerMaxLength);
                        EventLogger.LogTestStart(currentState, currentStateData.ContentType);
                        break;
                    case ExperimentContentType.MultipleSelection:
                        DingSound.Play();

                        AnswerGuideText.gameObject.SetActive(false);
                        NumberAnswerIndicator.gameObject.SetActive(false);
                        NumberInputPanel.gameObject.SetActive(false);
                        MultipleChoiceInput.gameObject.SetActive(true);

                        MultipleChoiceInput.Initialize(currentStateData.AnswerSet, currentStateData.AnswerCount);
                        EventLogger.LogTestStart(currentState, currentStateData.ContentType);
                        break;

                    //랜덤 정답을 생성해야 하는 경우
                    case ExperimentContentType.NumberWithRandom:
                        DingSound.Play();

                        AnswerGuideText.gameObject.SetActive(true);
                        NumberAnswerIndicator.gameObject.SetActive(false);
                        NumberInputPanel.gameObject.SetActive(false);
                        MultipleChoiceInput.gameObject.SetActive(false);

                        //정답 랜덤 생성
                        answerRange = 1;
                        for (int i = 0; i < currentStateData.AnswerMaxLength; i++)
                        {
                            answerRange *= 10;
                        }
                        correctAnswers = GenerateRandomAnswer(1, answerRange, currentStateData.AnswerCount);
                        answerGuideText = GenerateAnswerText(correctAnswers);


                        isShowingGuideText = true;  //위험한 코드
                        ConfirmButton.interactable = false;
                        AnswerGuideText.Initialize(answerGuideText, DoTestAfterShowAnswer);
                        break;
                    case ExperimentContentType.MultipleSelectionWithRandom:
                        DingSound.Play();

                        AnswerGuideText.gameObject.SetActive(true);
                        NumberAnswerIndicator.gameObject.SetActive(false);
                        NumberInputPanel.gameObject.SetActive(false);
                        MultipleChoiceInput.gameObject.SetActive(false);

                        //정답 랜덤 생성
                        answerRange = currentStateData.AnswerMaxLength;
                        correctAnswers = GenerateRandomAnswer(0, answerRange, currentStateData.AnswerCount);
                        answerGuideText = GenerateAnswerText(correctAnswers, currentStateData.AnswerSet);

                        isShowingGuideText = true;  //위험한 코드
                        ConfirmButton.interactable = false;
                        AnswerGuideText.Initialize(answerGuideText, DoTestAfterShowAnswer);
                        break;
                }
            }
        }

        private int[] GenerateRandomAnswer(int start, int end, int size)
        {
            int[] result = new int[size];
            HashSet<int> resultSet = new HashSet<int>();

            while(resultSet.Count < size)
            {
                resultSet.Add(Random.Range(start, end));
            }

            resultSet.CopyTo(result);
            return result;
        }

        private string GenerateAnswerText(int[] answers)
        {
            string result = string.Empty;
            for (int i = 0; i < correctAnswers.Length; i++)
            {
                if (i != correctAnswers.Length - 1)
                {
                    result += correctAnswers[i] + ", ";
                }
                else
                {
                    result += correctAnswers[i].ToString();
                }
            }

            return result;
        }

        private string GenerateAnswerText(int[] answers, string[] answerTexts)
        {
            string result = string.Empty;
            for (int i = 0; i < correctAnswers.Length; i++)
            {
                if (i != correctAnswers.Length - 1)
                {
                    result += answerTexts[correctAnswers[i]] + ", ";
                }
                else
                {
                    result += answerTexts[correctAnswers[i]];
                }
            }

            return result;
        }

        public void OnReadyToStartExperiment(GameObject sender, ExperimentActionEvent.EventArgs args)
        {
            ConfirmButton.interactable = true;
        }

        private void DoTestAfterShowAnswer()
        {   
            ExperimentState currentStateData = data.States[currentState];

            switch(currentStateData.ContentType)
            {
                case ExperimentContentType.Number:
                    break;
                case ExperimentContentType.MultipleSelection:
                    break;
                case ExperimentContentType.NumberWithRandom:
                    NumberAnswerIndicator.gameObject.SetActive(true);
                    NumberInputPanel.gameObject.SetActive(true);

                    NumberAnswerIndicator.Initialize(currentStateData.AnswerTitle, currentStateData.AnswerCount, currentStateData.AnswerMaxLength);
                    EventLogger.LogTestStart(currentState, currentStateData.ContentType);

                    BeepSound.Play();
                    break;
                case ExperimentContentType.MultipleSelectionWithRandom:
                    MultipleChoiceInput.gameObject.SetActive(true);

                    MultipleChoiceInput.Initialize(currentStateData.AnswerSet, currentStateData.AnswerCount);
                    EventLogger.LogTestStart(currentState, currentStateData.ContentType);
                    BeepSound.Play();
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
                            case 12:
                                EventLogger.LogPhoneNumber(args.Answers[0]);
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

        public void OnAnswerNotCompleted(GameObject sender, ExperimentActionEvent.EventArgs args)
        {
            ConfirmButton.interactable = false;
        }

        /// <summary>
        /// 테스트 케이스 종료 시 실행, 사용자가 확인 버튼 누름
        /// </summary>
        public void OnAnswerConfirmed()
        {
            if(isShowingGuideText)
            {
                AnswerGuideText.isOkToProceed = true;   //이거 위험한 코드
                //실험 진행...코드가 꼬일 수 있음
                //이래서 실험 계획서를 제대로 써서 줬으면 좋겠는데....
                //기획서, 계획서 안 주면 이렇게 땜빵되는 코드가 점점 늘어남.
                //영향 받는 쪽: 확인 버튼, AnswerGuideText, 여기 DoTest함수
                //여기 isShowingGuideText 변수

                isShowingGuideText = false;
                ConfirmButton.interactable = false;
            }
            else
            {
                EventLogger.LogTestEnd(currentState);
                EventLogger.ShowCurrent();
                EventLogger.BackupCSV();
                DoTest();
            }
        }

        public void ForceTest(int state)
        {
            currentState = state - 1;
            DoTest();
        }

        public void ForceTest()
        {
            currentState -= 1;
            DoTest();
        }
    }
}
