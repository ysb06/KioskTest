using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KioskTest.Experiment;

namespace KioskTest.UI {
    public class ConfrimButton : MonoBehaviour
    {
        public ExperimentEventLogger Logger;

        public void OnConfirmButtonClick()
        {
            Logger.LogTest(UnitTestEvent.Click, -1);
        }
    }
}