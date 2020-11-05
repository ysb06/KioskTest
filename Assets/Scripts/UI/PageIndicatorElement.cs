using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KioskTest.UI
{
    public class PageIndicatorElement : MonoBehaviour
    {
        public GameObject ActiveDotImage;
        public GameObject InactiveDotImage;

        private void Start()
        {
            if(ActiveDotImage == null)
            {
                ActiveDotImage = transform.GetChild(0).gameObject;
            }
            if(InactiveDotImage == null)
            {
                InactiveDotImage = transform.GetChild(0).gameObject;
            }
        }

        public void Activate()
        {
            ActiveDotImage.SetActive(true);
            InactiveDotImage.SetActive(false);
        }

        public void Inactivate()
        {
            ActiveDotImage.SetActive(false);
            InactiveDotImage.SetActive(true);
        }
    }
}