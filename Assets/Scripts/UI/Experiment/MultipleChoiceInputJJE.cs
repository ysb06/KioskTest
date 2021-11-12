using KioskTest.Experiment;
using KioskTest.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KioskTest.UI.Experiment
{
    public class MultipleChoiceInputJJE : MultipleChoiceInput
    {

        public override void Initialize(string[] answerSet, int answerCount)
        {
            MaxAnswer = answerCount;
            for(int i = 0; i < AnswerButtons.Length; i++)
            {
                int maxNumber = answerSet.Length;
                if (maxNumber % 4 != 0)
                {
                    maxNumber += 2;
                }

                if(i < answerSet.Length)
                {
                    AnswerButtons[i].gameObject.SetActive(true);

                    //최적화 문제 있음
                    AnswerButtons[i].GetComponentInChildren<Text>().text = answerSet[i];
                }
                else
                {
                    if (i < maxNumber)
                    {
                        AnswerButtons[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        AnswerButtons[i].gameObject.SetActive(false);
                    }
                }
            }

            //선택 및 모양 초기화
            Answers.Clear();
            foreach(Button target in AnswerButtons)
            {
                target.colors = new ColorBlock()
                {
                    normalColor = Color.black,
                    highlightedColor = Color.black,
                    pressedColor = Color.gray,
                    selectedColor = Color.black,
                    disabledColor = Color.gray,
                    colorMultiplier = 1,
                    fadeDuration = 0.1f
                };
            }

            CurrentPage = 0;
            IsMoving = false;
            InitializePagePosition();   //currentPage 값이 이상할 수 있으니 움직임과 상관 없이 위치 결정
        }
    }
}