using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KioskTest.UI
{
    public class NumberInput : MonoBehaviour
    {
        public ExperimentEventLogger EventLogger;
        public InputField Target;

        public void OnNumberButtonClicked(int value)
        {
            if (Target != null)
            {
                if (value != -1)
                {
                    EventLogger.LogTest(UnitTestEvent.Click, value);
                    Target.text += value;
                }
                else
                {
                    if (Target.text.Length > 0)
                    {
                        Target.text = Target.text.Substring(0, Target.text.Length - 1);
                    }
                    else
                    {
                        //최적화 필요
                        Target.GetComponent<InputFieldAutoFocus>().SetCursorPrevField();
                        if (Target.text.Length > 0)
                        {
                            Target.text = Target.text.Substring(0, Target.text.Length - 1);
                        }
                    }

                    EventLogger.LogTest(UnitTestEvent.Cancel, value);
                }
            }
            else
            {
                Debug.LogWarning("No target field");
            }        }

        public void OnNumberButtonClicked(string value)
        {
            if (Target != null)
            {
                print("Input(string): " + value);
            }
            else
            {
                Debug.LogWarning("No target field");
            }
        }
    }
}