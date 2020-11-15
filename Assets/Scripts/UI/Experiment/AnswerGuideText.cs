using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KioskTest.UI.Experiment
{
    public class AnswerGuideText : MonoBehaviour
    {
        public float WaitTime = 3;
        public bool isOkToProceed = false;
        public Text Content;
        public ExperimentActionEvent TextShowedEvent = new ExperimentActionEvent();

        public void Initialize(string answer, Action callback)
        {
            Content.text = answer;
            StartCoroutine(Wait(callback));
        }

        public IEnumerator Wait(Action callback)
        {
            //다시 숨김
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(WaitTime);
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            TextShowedEvent.Invoke(gameObject, new ExperimentActionEvent.EventArgs()
            {
                // No Result
            });
            yield return new WaitUntil(() => isOkToProceed);
            isOkToProceed = false;
            gameObject.SetActive(false);
            callback.Invoke();
        }
    }
}