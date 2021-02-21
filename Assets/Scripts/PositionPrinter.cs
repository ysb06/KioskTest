using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PositionPrinter : MonoBehaviour
{
    public List<RectTransform> targets = new List<RectTransform>();

    private void Start()
    {

    }

    private void Update()
    {
        if(Keyboard.current.eKey.wasPressedThisFrame)
        {
            print("Start");
            foreach (RectTransform target in targets)
            {
                print(target.gameObject.name + " : " +  RectTransformUtility.WorldToScreenPoint(null, target.position));
            }
        }
    }
}
