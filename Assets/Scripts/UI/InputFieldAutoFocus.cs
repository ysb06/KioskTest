using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KioskTest.UI
{
    [RequireComponent(typeof(InputField))]
    public class InputFieldAutoFocus : MonoBehaviour
    {
        public NumberAnswerIndicator AnswerIndicator;
        public bool InitallySelected = false;
        public NumberInput InputPanel;
        private InputField currentField;
        public InputField NextField;

        private void Start()
        {
            if(currentField == null)
            {
                currentField = GetComponent<InputField>();
            }
            if (InitallySelected)
            {
                currentField.Select();
            }
        }
        public void OnValueChanged(string value)
        {
            if (currentField.characterLimit > 0 && value.Length >= currentField.characterLimit)
            {
                if (NextField != null)
                {
                    NextField.Select();
                    if (InputPanel != null)
                    {
                        InputPanel.Target = NextField;
                    }
                }
            }

            if (value != string.Empty)
            {
                AnswerIndicator.InvokeAnswerSelectedEvent(currentField, value);
            }
        }
    }
}