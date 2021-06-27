using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KioskTest.Experiment.Shuffler
{
    public class MC_Shuffler : MonoBehaviour
    {
        public ExperimentEventLogger EventLogger;
        public List<Button> Buttons = new List<Button>();
        public List<HorizontalLayoutGroup> ButtonHolders = new List<HorizontalLayoutGroup>();
        public int Width = 2;
        public int Height = 4;
        public int MaxShuffleLength = 8;
        public Button[] ShuffledButtonList;

        public void SortOrder()
        {
            for (int i = 0; i < ButtonHolders.Count; i++)
            {
                Transform targetRow = ButtonHolders[i].transform;
                for (int j = 0; j < Width; j++)
                {
                    int cursor = i * Width + j;
                    Buttons[cursor].transform.SetParent(targetRow);
                    Buttons[cursor].transform.SetSiblingIndex(cursor);
                }
            }
        }

        public void Shuffle()
        {
            int[] shuffleOrder = GetShuffleOrder();
            SetShuffledButtonList(shuffleOrder);

            if(EventLogger != null)
            {
                string logText = string.Empty;
                foreach(int value in shuffleOrder)
                {
                    logText += value.ToString("X");
                }
                EventLogger.LogTest(UnitTestEvent.Undefined, logText);
            }

            for (int i = 0; i < Height; i++)
            {
                Transform targetRow = ButtonHolders[i].transform;
                for (int j = 0; j < Width; j++)
                {
                    int cursor = i * Width + j;
                    ShuffledButtonList[cursor].transform.SetParent(targetRow);
                    ShuffledButtonList[cursor].transform.SetSiblingIndex(cursor);
                    // print(ShuffledButtonList[cursor].name + ", " + ShuffledButtonList[cursor].transform.GetSiblingIndex());
                }
            }
        }

        private int[] GetShuffleOrder()
        {
            HashSet<int> shuffleOrder = new HashSet<int>();

            while (shuffleOrder.Count < MaxShuffleLength)
            {
                int num = Random.Range(0, MaxShuffleLength);
                shuffleOrder.Add(num);
            }
            int[] shuffledbuttenindexs = new int[MaxShuffleLength];
            shuffleOrder.CopyTo(shuffledbuttenindexs);

            return shuffledbuttenindexs;
        }

        private void SetShuffledButtonList(int[] shuffleOrder)
        {
            ShuffledButtonList = new Button[Buttons.Count];
            for (int i = 0; i < ShuffledButtonList.Length; i++)
            {
                if (i < shuffleOrder.Length)
                {
                    ShuffledButtonList[i] = Buttons[shuffleOrder[i]];
                }
                else
                {
                    ShuffledButtonList[i] = Buttons[i];
                }
            }
        }

        private void PrintList(int[] list)
        {
            string str = "";
            foreach (int ord in list)
            {
                str += ord + ", ";
            }
            print(str);
        }

        private void PrintList(Button[] list)
        {
            string str = "";
            foreach (Button button in ShuffledButtonList)
            {
                str += button.name + ", ";
            }
            print(str);
        }
    }
}