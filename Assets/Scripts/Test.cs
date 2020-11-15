using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour
{
    public Button target;

    public void OnButtonDown(BaseEventData e)
    {
        e.Use();
    }
}
