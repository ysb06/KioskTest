using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KioskTest.UI
{
    public class AnswerGuideText : MonoBehaviour
    {
        public float WaitTime = 3;
        public float ShowTime = 3;
        public Text Content;

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
            yield return new WaitForSeconds(ShowTime);
            gameObject.SetActive(false);
            callback.Invoke();
        }
    }
}