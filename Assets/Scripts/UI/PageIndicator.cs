using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KioskTest.UI
{
    public class PageIndicator : MonoBehaviour
    {
        public List<PageIndicatorElement> PageDotList = new List<PageIndicatorElement>();

        public void SetCurrentPage(int page)
        {
            for(int i = 0; i < PageDotList.Count; i++)
            {
                if(i == page)
                {
                    PageDotList[i].Activate();
                }
                else
                {
                    PageDotList[i].Inactivate();
                }
            }
        }
    }
}