using KioskTest.Experiment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KioskTest.UI
{
    public class SwitchTestButton : MonoBehaviour
    {
        public ExperimentController controller;
        public InputField NumberField;

        public void SwitchTest()
        {
            controller.ForceTest(int.Parse(NumberField.text));
        }
    }
}