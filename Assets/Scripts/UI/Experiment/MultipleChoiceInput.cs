using KioskTest.Experiment;
using KioskTest.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KioskTest.UI.Experiment
{
    public class MultipleChoiceInput : MonoBehaviour
    {
        public const int TRANSITION_FRAME_INTERVAL = 6; //6 Frame == 0.1초

        public ExperimentEventLogger EventLogger;

        [Header("페이지 이동 관련")]
        public int CurrentPage = 0;
        public List<RectTransform> Pages = new List<RectTransform>();
        public PageIndicator indicatorSwitcher;
        private bool IsMoving = false;
        private bool NeedToTrasitRight = true;

        [Header("답안과 답안 버튼")]
        public int MaxAnswer = 1;
        public Button[] AnswerButtons;
        private List<long> Answers = new List<long>();

        [Header("사용자 최종 답안 구성 이벤트")]
        public ExperimentActionEvent SelectEvent;

        private void Start()
        {
            InitializePagePosition();   //모양 초기화
            if (indicatorSwitcher == null)
            {
                indicatorSwitcher = GetComponentInChildren<PageIndicator>();
            }
        }

        public void Initialize(string[] answerSet, int answerCount)
        {
            MaxAnswer = answerCount;
            for(int i = 0; i < AnswerButtons.Length; i++)
            {
                if(i < answerSet.Length)
                {
                    AnswerButtons[i].gameObject.SetActive(true);

                    //최적화 문제 있음
                    AnswerButtons[i].GetComponentInChildren<Text>().text = answerSet[i];
                }
                else
                {
                    AnswerButtons[i].gameObject.SetActive(false);
                }
            }

            //선택 및 모양 초기화
            Answers.Clear();
            foreach(Button target in AnswerButtons)
            {
                target.colors = new ColorBlock()
                {
                    normalColor = Color.white,
                    highlightedColor = Color.white,
                    pressedColor = Color.white,
                    selectedColor = Color.white,
                    disabledColor = Color.red,
                    colorMultiplier = 1,
                    fadeDuration = 0.1f
                };
            }

            CurrentPage = 0;
            IsMoving = false;
            InitializePagePosition();   //currentPage 값이 이상할 수 있으니 움직임과 상관 없이 위치 결정
        }

        public void OnSwipe(GameObject sender, InputEvent.EventArgs args)
        {
            if(args.value >= 0)
            {
                NeedToTrasitRight = false;
            }
            else
            {
                NeedToTrasitRight = true;
            }
        }

        public void OnSwipeEnd(GameObject sender, InputEvent.EventArgs args)
        {
            if (gameObject.activeInHierarchy)
            {
                if (NeedToTrasitRight)
                {
                    SetPage(CurrentPage + 1);
                    EventLogger.LogTest(UnitTestEvent.Swipe, 1);
                }
                else
                {
                    SetPage(CurrentPage - 1);
                    EventLogger.LogTest(UnitTestEvent.Swipe, -1);
                }
            }
        }

        public void OnChoiceSelected(int id)
        {
            int listId = id - 1;

            if(Answers.Contains(listId))
            {   //있는 답안은 제거
                Answers.Remove(listId);
                AnswerButtons[listId].colors = new ColorBlock()
                {
                    normalColor = Color.white,
                    highlightedColor = Color.white,
                    pressedColor = Color.white,
                    selectedColor = Color.white,
                    disabledColor = Color.red,
                    colorMultiplier = 1,
                    fadeDuration = 0.1f
                };
                EventLogger.LogTest(UnitTestEvent.Cancel, id);
            }
            else
            {
                EventLogger.LogTest(UnitTestEvent.Click, id);
                //없는 답안은 추가
                if (Answers.Count < MaxAnswer)
                {
                    Answers.Add(listId);
                    AnswerButtons[listId].colors = new ColorBlock()
                    {
                        normalColor = Color.grey,
                        highlightedColor = Color.grey,
                        pressedColor = Color.grey,
                        selectedColor = Color.grey,
                        disabledColor = Color.red,
                        colorMultiplier = 1,
                        fadeDuration = 0.1f
                    };

                    if(Answers.Count >= MaxAnswer)
                    {
                        EventLogger.LogTest(UnitTestEvent.Answer, id);
                    }
                }
            }

            SelectEvent.Invoke(gameObject, new ExperimentActionEvent.EventArgs()
            {
                Answers = Answers.ToArray()
            });
        }

        public void SetPage(int page)
        {
            if (IsMoving == false && page >= 0 && page < Pages.Count)
            {
                indicatorSwitcher.SetCurrentPage(page);
                StartCoroutine(ChangePage(page));
            }
        }

        private IEnumerator ChangePage(int page)
        {
            IsMoving = true;
            //프레임 별 움직여야 할 거리
            Vector2 movingUnit = new Vector2((CurrentPage - page) * Screen.width / TRANSITION_FRAME_INTERVAL, 0);

            for(int i = 0; i < TRANSITION_FRAME_INTERVAL; i++)
            {
                foreach(RectTransform target in Pages)
                {
                    target.offsetMin += movingUnit;
                    target.offsetMax += movingUnit;
                }
                yield return new WaitForEndOfFrame();
            }
            CurrentPage = page;
            IsMoving = false;
            InitializePagePosition();   //currentPage 값이 이상할 수 있으니 움직임과 상관 없이 위치 결정
        }

        /// <summary>
        /// 현재 스크린에 맞추어 모양 재구성
        /// </summary> 만약 플레이 도중 모양 이상하게 나오는 경우 본 메서드를 호출할 것
        private void InitializePagePosition()
        {
            for (int i = 0; i < Pages.Count; i++)
            {
                Pages[i].offsetMin = new Vector2(Screen.width * (-CurrentPage + i), 100);
                Pages[i].offsetMax = new Vector2(Screen.width * (-CurrentPage + i), 0);
            }
        }
    }
}