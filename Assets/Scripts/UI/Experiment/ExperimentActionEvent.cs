using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace KioskTest.UI.Experiment
{
    [Serializable]
    public class ExperimentActionEvent : UnityEvent<GameObject, ExperimentActionEvent.EventArgs>
    {
        public struct EventArgs
        {
            public long[] Answers;
            public int AnswerCount { get { return Answers.Length; } }
        }
    }
}
