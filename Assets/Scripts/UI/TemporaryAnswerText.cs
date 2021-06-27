using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemporaryAnswerText : MonoBehaviour
{
    public Text Content;

    public void ShowText(string text)
    {
        Content.gameObject.SetActive(true);
        Content.text = text;
    }

    public void HideText()
    {
        Content.gameObject.SetActive(false);
    }
}
