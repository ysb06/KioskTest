using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KioskTest.Experiment;
using KioskTest.UI.Experiment;

namespace KioskTest.UI {
    public class ConfrimButton : MonoBehaviour
    {
        public ExperimentEventLogger Logger;
        public Button target;
        public ExperimentActionEvent OnConfirm = new ExperimentActionEvent();

        public void OnConfirmButtonClick()
        {
            if(target.interactable)
            {
                Logger.LogTest(UnitTestEvent.Click, -1);
                OnConfirm.Invoke(gameObject, new ExperimentActionEvent.EventArgs()
                {
                    
                });
            }
        }
    }
}