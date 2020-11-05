using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskTest
{
    public enum ExperimentContentType
    {
        None, Answer, Number, MultipleSelection
    }

    public class ExperimentState
    {
        /// <summary>
        /// 단위 실험 이름, 실험 로직에서는 사용되지 않음.
        /// </summary>
        public string TestName { get; private set; } = string.Empty;

        /// <summary>
        /// 상단 큰 박스에 표시할 문자열
        /// </summary>
        public string MainGuideText { get; private set; } = string.Empty;

        /// <summary>
        /// 실험 종류, 콘텐츠 부분을 어떻게 표시할 지를 정의
        /// </summary>
        public ExperimentContentType ContentType { get; private set; } = ExperimentContentType.None;

        /// <summary>
        /// 응답 수, 숫자 타입의 실험에서는 2이상의 수에서 Input Field가 2개 표시, 다중 선택 실험에서는 
        /// </summary>
        public int AnswerCount { get; private set; } = 1;
        public int AnswerLength { get; private set; }
    }
}
