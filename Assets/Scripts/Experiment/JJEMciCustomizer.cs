using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KioskTest.UI.Experiment;

namespace KioskTest.Experiment {
    public class JJEMciCustomizer : MonoBehaviour
    {
        private MultipleChoiceInput target;
        // Start is called before the first frame update
        void Start()
        {
            target = GetComponent<MultipleChoiceInput>();
        }

        private void LateUpdate()
        {
            target.CurrentPage = -10;
        }
    }
}