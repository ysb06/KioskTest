using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySwitcher : MonoBehaviour
{
    public Camera CurrentCamera;
    private void Start()
    {
        CurrentCamera = GetComponent<Camera>();
    }
    public void SwitchToOne()
    {
        print("Switch To One");
        CurrentCamera.targetDisplay = 1;
    }

    public void SwitchToTwo()
    {
        print("Switch To Two");
        CurrentCamera.targetDisplay = 2;
    }
}
