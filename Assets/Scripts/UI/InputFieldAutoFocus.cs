using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KioskTest.UI
{
    [RequireComponent(typeof(InputField))]
    public class InputFieldAutoFocus : MonoBehaviour
    {
        public bool Selected = false;
        public NumberInput InputPanel;
        private InputField currentField;
        public InputField NextField;

        private void Start()
        {
            if(currentField == null)
            {
                currentField = GetComponent<InputField>();
            }
            if (Selected)
            {
                currentField.Select();
            }
        }
        public void OnValueChanged(string value)
        {
            if (currentField.characterLimit > 0 && value.Length >= currentField.characterLimit)
            {
                NextField.Select();
                if (InputPanel != null)
                {
                    InputPanel.Target = NextField;
                }
            }
        }
    }
}