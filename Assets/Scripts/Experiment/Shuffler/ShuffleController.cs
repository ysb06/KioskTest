using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KioskTest.Experiment.Shuffler
{
    public class ShuffleController : MonoBehaviour
    {
        public List<MC_Shuffler> Shufflers = new List<MC_Shuffler>();

        public void SetShuffle(int state)
        {
            /* 전체 실험 섞는 코드
            switch(state)
            {
                case 0:
                    foreach(MC_Shuffler shuffler in Shufflers)
                    {
                        shuffler.SortOrder();
                    }
                    break;
                case 1:
                    Shufflers[1].Width = 2;
                    Shufflers[1].Height = 1;
                    Shufflers[1].MaxShuffleLength = 2;
                    Shufflers[1].Shuffle();
                    break;
                case 2:
                    Shufflers[1].Width = 2;
                    Shufflers[1].Height = 2;
                    Shufflers[1].MaxShuffleLength = 4;
                    Shufflers[1].Shuffle();
                    break;
                case 3:
                    Shufflers[1].Width = 2;
                    Shufflers[1].Height = 4;
                    Shufflers[1].MaxShuffleLength = 8;
                    Shufflers[1].Shuffle();
                    break;
                case 4:
                case 5:
                    Shufflers[0].Shuffle();
                    break;
                case 6:
                    Shufflers[1].Width = 2;
                    Shufflers[1].Height = 1;
                    Shufflers[1].MaxShuffleLength = 2;
                    Shufflers[1].Shuffle();
                    break;
                case 7:
                case 8:
                    Shufflers[1].Width = 2;
                    Shufflers[1].Height = 2;
                    Shufflers[1].MaxShuffleLength = 4;
                    Shufflers[1].Shuffle();
                    break;
                case 9:
                case 10:
                case 11:
                    Shufflers[1].Width = 2;
                    Shufflers[1].Height = 8;
                    Shufflers[1].MaxShuffleLength = 16;
                    Shufflers[1].Shuffle();
                    break;
                case 12:
                    Shufflers[0].Shuffle();
                    break;
                case 13:
                    foreach (MC_Shuffler shuffler in Shufflers)
                    {
                        shuffler.SortOrder();
                    }
                    break;
                default:
                    print("Error in: " + state);
                    foreach (MC_Shuffler shuffler in Shufflers)
                    {
                        shuffler.SortOrder();
                    }
                    break;
            }
            */

            // 성대용
            switch (state)
            {
                case 4:
                case 12:
                case 15:
                    Shufflers[0].Shuffle();
                    break;
                default:
                    foreach (MC_Shuffler shuffler in Shufflers)
                    {
                        shuffler.SortOrder();
                    }
                    break;
            }

            // 인천대용
            //switch (state)
            //{
            //    case 13:
            //    case 14:
            //    case 16:
            //        Shufflers[0].Shuffle();
            //        break;
            //    default:
            //        foreach (MC_Shuffler shuffler in Shufflers)
            //        {
            //            shuffler.SortOrder();
            //        }
            //        break;
            //}
        }
    }
}