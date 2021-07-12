using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskTest.Experiment
{
    public class ExperimentControllerData
    {
        private static string[] AnswerSetGender = { "남자", "여자" };
        private static string[] AnswerSetSchool = { "초등학교", "중학교", "고등학교", "대학교" };
        private static string[] AnswerSetAddress = { "서울시", "수원시", "인천시", "화성시", "부천시", "분당", "송도", "기타" };
        private static string[] AnswerSetYesNo = { "있다", "없다" };
        private static string[] AnswerSetATM = { "계좌조회", "현금인출", "송금이체", "지로공과금" };
        private static string[] AnswerSetBank = { "국민은행", "기업은행", "신한은행", "하나은행", "우리은행", "외환은행", "농협", "새마을금고", "부산은행", "광주은행", "제주은행", "카카오뱅크", "제일은행", "산업은행", "수협", "씨티" };

        public ExperimentState[] States =
        {
            new ExperimentState("_0_시작화면",
                "기표력 향상사업 실험", ExperimentContentType.Number,
                "실험자 번호", new string[0], 1, 2),

            new ExperimentState("_1_실험 1-1",
                "귀하의 성별은 무엇입니까?", ExperimentContentType.MultipleSelection,
                "", AnswerSetGender, 1, AnswerSetGender.Length),
            new ExperimentState("_2_실험 1-2",
                "귀하의 최종학력은 무엇입니까?", ExperimentContentType.MultipleSelection,
                "", AnswerSetSchool, 1, AnswerSetSchool.Length),
            new ExperimentState("_3_실험 1-3",
                "지금 살고 계시는 주거지는 어디입니까?", ExperimentContentType.MultipleSelectionWithRandom,
                "", AnswerSetAddress, 1, AnswerSetAddress.Length),
            new ExperimentState("_4_실험 1-4",
                "귀하의 주민등록번호 앞 6자리 생년월일을 입력해 주세요.", ExperimentContentType.Number,
                "생년월일", new string[0], 1, 6),
            new ExperimentState("_5_실험 1-5",
                "귀하가 주로 사용하시는 지하철 또는 전철 호선 번호를 두 개 입력해 주세요", ExperimentContentType.NumberWithRandom,
                "지하철 노선", new string[0], 2, 1),

            new ExperimentState("_6_실험 2-1",
                "최근 한 달 사이에 은행에 설치된 현금자동인출기를 사용하신 적이 있습니까?", ExperimentContentType.MultipleSelection,
                "", AnswerSetYesNo, 1, AnswerSetYesNo.Length),
            new ExperimentState("_7_실험 2-2-A",
                "현금자동인출기에서 주로 사용하시는 서비스 한가지만 골라주세요", ExperimentContentType.MultipleSelectionWithRandom,
                "", AnswerSetATM, 1, AnswerSetATM.Length),
            new ExperimentState("_8_실험 2-2-B",
                "현금자동인출기에서 주로 사용하시는 서비스 두가지를 차례대로 골라주세요", ExperimentContentType.MultipleSelectionWithRandom,
                "", AnswerSetATM, 2, AnswerSetATM.Length),
            new ExperimentState("_9_실험 2-3-A",
                "다음중 주거래 은행을 하나만 골라 주세요", ExperimentContentType.MultipleSelectionWithRandom,
                "", AnswerSetBank, 1, AnswerSetBank.Length),
            new ExperimentState("_10_실험 2-3-B",
                "다음중 주로 거래하시는 은행을 두 개 골라 주세요", ExperimentContentType.MultipleSelectionWithRandom,
                "", AnswerSetBank, 2, AnswerSetBank.Length),
            new ExperimentState("_11_실험 2-3-C",
                "다음중 거래하셨던 경험이 있는 은행을 세 개 골라 주세요", ExperimentContentType.MultipleSelectionWithRandom,
                "", AnswerSetBank, 3, AnswerSetBank.Length),
            new ExperimentState("_12_실험 2-3-D",
                "귀하의 휴대폰 번호 11자리를 입력해주세요", ExperimentContentType.Number,
                "전화번호", new string[0], 1, 11),

            new ExperimentState("_13_실험 Add-1",
                "귀하의 주민등록번호 앞 6자리 생년월일을 다시 입력해 주세요.", ExperimentContentType.Number,
                "생년월일", new string[0], 1, 6),
            new ExperimentState("_14_실험 Add-2",
                "귀하의 휴대폰 번호 11자리를 다시 입력해주세요", ExperimentContentType.Number,
                "전화번호", new string[0], 1, 11),
            
            new ExperimentState("_15_실험 Add-3",
                "다음의 인증번호를 입력해 주세요", ExperimentContentType.NumberWithRandomAndNoGuide,
                "인증번호", new string[0], 1, 6),
            new ExperimentState("_16_실험 Add-4",
                "다음의 인증번호를 다시 입력해 주세요", ExperimentContentType.NumberWithRandomAndNoGuide,
                "인증번호", new string[0], 1, 6),

            new ExperimentState("_15_실험종료",
                "수고하셨습니다", ExperimentContentType.None,
                "", new string[0], 1, 1),
        };
    }
}
