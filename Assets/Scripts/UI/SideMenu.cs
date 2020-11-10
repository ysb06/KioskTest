using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KioskTest.UI
{
    public class SideMenu : MonoBehaviour
    {
        public const int MOVING_FRAME = 15;
        public bool Visible = false;
        public RectTransform TargetTransform;

        private void Start()
        {
            TargetTransform = GetComponent<RectTransform>();
        }

        public void Show()
        {
            Visible = true;
            StartCoroutine(Animate(640));
        }

        public void Hide()
        {
            Visible = false;
            StartCoroutine(Animate(-640));
        }

        public void Switch()
        {
            if(Visible)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        public IEnumerator Animate(float distance)
        {
            float moveUnit = distance / MOVING_FRAME;
            for (int i = 0; i < MOVING_FRAME; i++)
            {
                TargetTransform.position += new Vector3(moveUnit, 0, 0);
                yield return new WaitForEndOfFrame();
            }

            if(distance > 0)
            {
                TargetTransform.position = new Vector3(0, 0, 0);
                TargetTransform.offsetMin = new Vector2(TargetTransform.offsetMin.x, 0);
                TargetTransform.offsetMax = new Vector2(TargetTransform.offsetMax.x, -90);
            }
            else
            {
                TargetTransform.position = new Vector3(-640, 0, 0);
                TargetTransform.offsetMin = new Vector2(TargetTransform.offsetMin.x, 0);
                TargetTransform.offsetMax = new Vector2(TargetTransform.offsetMax.x, -90);
            }
        }
    }
}