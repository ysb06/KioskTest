using KioskTest.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberAnswerIndicator : MonoBehaviour
{
    public Text Header;
    public InputField InputField1;
    public InputFieldAutoFocus InputField1Controller;
    public InputField InputField2;
    public NumberInput NumberInputPanel;
    [Header("사용자 최종 답안 구성 이벤트")]
    public ExperimentActionEvent SelectEvent;
    private int[] answers = new int[2];

    public void Initialize(string headerText, int fieldCount, int maxLength)
    {
        Header.text = headerText;
        InputField1.characterLimit = maxLength;
        InputField2.characterLimit = maxLength;
        InputField1.text = string.Empty;
        InputField2.text = string.Empty;

        answers = new int[2];

        if (fieldCount == 1)
        {
            InputField2.gameObject.SetActive(false);
            InputField1Controller.NextField = null;
        }
        else
        {
            InputField2.gameObject.SetActive(true);
            InputField1Controller.NextField = InputField2;
        }

        NumberInputPanel.Target = InputField1;
    }

    public void InvokeAnswerSelectedEvent(InputField sender, string value)
    {
        print(value);
        if(sender == InputField1)
        {
            answers[0] = int.Parse(value);
        }
        else
        {
            answers[1] = int.Parse(value);
        }

        SelectEvent.Invoke(gameObject, new ExperimentActionEvent.EventArgs()
        {
            Answers = answers
        });
    }
}

