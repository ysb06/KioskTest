using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KioskTest.UI
{
    public class AnswerGuideText : MonoBehaviour
    {
        public float ShowTime = 3;
        public Text Content;

        public void Initialize(string answer, Action callback)
        {
            Content.text = answer;
            StartCoroutine(Wait(callback));
        }

        public IEnumerator Wait(Action callback)
        {
            yield return new WaitForSeconds(ShowTime);
            gameObject.SetActive(false);
            callback.Invoke();
        }
    }
}