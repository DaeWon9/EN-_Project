using System;

namespace Library.View
{
    class Label
    {
        public void PrintLibraryLabel(bool isClear = false)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *             ");
            Console.WriteLine("           *                                                                     *            ");
            Console.WriteLine("          *     ■     ■■■  ■■■   ■■■      ■■    ■■■   ■    ■     *           ");
            Console.WriteLine("         *      ■       ■    ■   ■  ■   ■    ■  ■   ■   ■   ■  ■       *          ");
            Console.WriteLine("        *       ■       ■    ■■■   ■■■    ■■■■  ■■■      ■          *         ");
            Console.WriteLine("         *      ■       ■    ■   ■  ■   ■   ■    ■  ■   ■     ■         *          ");
            Console.WriteLine("          *     ■■■ ■■■  ■■■   ■    ■  ■    ■  ■    ■    ■        *           ");
            Console.WriteLine("           *                                                                     *            ");
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *             ");
        }

        public void PrintExplainMemberLoginLabel(bool isClear = false)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *             ");
            Console.WriteLine("           *                         < 회 원 로 그 인 >                          *            ");
            Console.WriteLine("          *                                                                       *           ");
            Console.WriteLine("         *                ● 가입하신 ID 와 PASSWORD를 적어주세요                  *          ");
            Console.WriteLine("        *                 ● ID       : (영어 & 숫자 6 ~ 10 글자)                   *         ");
            Console.WriteLine("         *                ● PASSWORD : (영어 & 숫자 6 ~ 10 글자)                  *          ");
            Console.WriteLine("          *                                                                       *           ");
            Console.WriteLine("           *                                                                     *            ");
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *             ");
        }


        public void PrintExplainAdimistratorLoginLabel(bool isClear = false)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *             ");
            Console.WriteLine("           *                        < 관 리 자 로 그 인 >                        *            ");
            Console.WriteLine("          *                                                                       *           ");
            Console.WriteLine("         *                ● 관리자 ID 와 PASSWORD를 적어주세요                    *          ");
            Console.WriteLine("        *                 ● ID       : (영어 & 숫자 6 ~ 10 글자)                   *         ");
            Console.WriteLine("         *                ● PASSWORD : (영어 & 숫자 6 ~ 10 글자)                  *          ");
            Console.WriteLine("          *                                                                       *           ");
            Console.WriteLine("           *                                                                     *            ");
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *             ");
        }

        public void PrintExplainMemberSignUpLabel(bool isClear = false)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *             ");
            Console.WriteLine("           *                           < 회 원 가 입 >                           *            ");
            Console.WriteLine("          *                                                                       *           ");
            Console.WriteLine("         *     ● 해당칸에 정보를 입력해주세요                                     *          ");
            Console.WriteLine("        *      ● ID, PASSWORD : (영어 & 숫자 6 ~ 10 글자)                          *         ");
            Console.WriteLine("         *     ● 주소 : (시|도) + (시|군|구) + (읍|면|동) 까지는 꼭 적어주세요    *          ");
            Console.WriteLine("          *                                                                       *           ");
            Console.WriteLine("           *                                                                     *            ");
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *             ");
        }

        public void PrintExplainBookSearchLabel(bool isClear = false)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *             ");
            Console.WriteLine("           *                           < 도 서 찾 기 >                           *            ");
            Console.WriteLine("          *                                                                       *           ");
            Console.WriteLine("         *     ● 검색하고자하는 정보를 입력해주세요 (원하는 옵션만 입력가능)      *          ");
            Console.WriteLine("        *      ● ↑↓ 화살표로 옵션 이동 후 ENTER를 누르면 입력 가능합니다         *         ");
            Console.WriteLine("         *     ● 검색 도중 ESC를 누르면 입력을 취소할 수 있습니다                 *          "); 
            Console.WriteLine("          *    ● 입력을 완료한 뒤에 <검색하기> 에서 ENTER 눌러주세요             *           ");
            Console.WriteLine("           *                                                                     *            ");
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *             ");
        }

        public void PrintResultBookSearchLabel(bool isClear = false)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *             ");
            Console.WriteLine("           *                       < 도 서 찾 기 결 과 >                         *            ");
            Console.WriteLine("          *                                                                       *           ");
            Console.WriteLine("         *                ● 입력하신 조건에 따른 검색 결과 입니다.                *          ");
            Console.WriteLine("        *                 ● 다시검색 : ENTER                                       *         ");
            Console.WriteLine("         *                ● 뒤로가기 : ESC                                        *          ");
            Console.WriteLine("          *                                                                       *           ");
            Console.WriteLine("          *                                                                       *           ");
            Console.WriteLine("           *                                                                     *            ");
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *             ");
        }
    }
}
