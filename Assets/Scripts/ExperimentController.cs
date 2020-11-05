using KioskTest.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KioskTest
{
    public class ExperimentController : MonoBehaviour
    {
        public ExperimentState[] States =
        {
            new ExperimentState(
                "",
                "",
                ExperimentContentType.Number, 1, 1)
        };

        /// <summary>
        /// 사용자가 Confrim 누르기 전 입력을 완료했다고 판단 되었을 때 실행되는 이벤트 핸들러
        /// </summary>
        /// <param name="sender">이벤트 발생 객체</param>
        /// <param name="args">이벤트 발생 정보</param>
        public void OnAnswerSelected(GameObject sender, ExperimentActionEvent.EventArgs args)
        {

        }
    }
}