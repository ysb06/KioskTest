using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KioskTest.Experiment {

    //해킹 급 스크립트
    //척결 대상
    public class JJECustomizer : MonoBehaviour
    {
        public Button[] Targets;
        // Start is called before the first frame update
        void Start()
        {
            ExperimentControllerData data = GetComponent<ExperimentController>().data;
            foreach (ExperimentState state in data.States)
            {
                List<string> answers = new List<string>(state.AnswerSet);
                while(answers.Count < 16)
                {
                    answers.Add(" ");
                }
                state.AnswerSet = answers.ToArray();
            }
        }
    }
}