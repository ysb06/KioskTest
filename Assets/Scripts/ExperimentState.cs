using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskTest
{
    public enum ExperimentContentType
    {
        None, Number, NumberWithRandom, MultipleSelection, MultipleSelectionWithRandom
    }

    [Serializable]
    public class ExperimentState
    {
        /// <summary>
        /// 단위 실험 이름, 실험 로직에서는 사용되지 않음.
        /// </summary>
        public string TestName { get; private set; }

        /// <summary>
        /// 상단 큰 박스에 표시할 문자열
        /// </summary>
        public string MainGuideText { get; private set; }

        /// <summary>
        /// 실험 종류, 콘텐츠 부분을 어떻게 표시할 지를 정의
        /// </summary>
        public ExperimentContentType ContentType { get; private set; }

        /// <summary>
        /// 응답을 설명하는 제목, 실제 사용은 숫자에서만 사용
        /// </summary>
        public string AnswerTitle { get; private set; }

        /// <summary>
        /// 다중 선택 실험에서 응답 리스트
        /// </summary>
        public string[] AnswerSet { get; private set; }

        /// <summary>
        /// 응답 수, 숫자 타입의 실험에서는 2이상의 수에서 Input Field가 2개 표시, 다중 선택 실험에서는 무시됨
        /// </summary>
        public int AnswerCount { get; private set; }

        /// <summary>
        /// 응답 길이, 숫자 타입의 실험에서만 적용되며 Input Field 내 텍스트 최대 길이
        /// </summary>
        public int AnswerMaxLength { get; private set; }

        /// <summary>
        /// ExperimentState의 기본 Constructor
        /// </summary>
        /// <param name="testName">테스트 이름</param>
        /// <param name="titleText">제목 표시부에 표시할 내용</param>
        /// <param name="type">선택지 종류</param>
        /// <param name="answerTitle">정답 제목</param>
        /// <param name="answerCount">실험자가 선택할 정답 갯수</param>
        /// <param name="answerMaxLength">숫자 타입 입력에서 입력 텍스트 최대 길이</param>
        public ExperimentState(string testName, string titleText, ExperimentContentType type, string answerTitle, string[] answerSet, int answerCount, int answerMaxLength)
        {
            TestName = testName;
            MainGuideText = titleText;
            ContentType = type;
            AnswerTitle = answerTitle;
            AnswerSet = answerSet;
            AnswerCount = answerCount;
            AnswerMaxLength = answerMaxLength;
        }
    }
}
