using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace KioskTest.Input
{
    [Serializable]
    public class InputEvent : UnityEvent<GameObject, InputEvent.EventArgs>
    {
        public struct EventArgs
        {
            public float value;
        }
    }
}
