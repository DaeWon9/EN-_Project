using System;

namespace Library.View
{
    class Label
    {
        public void LibraryLabelDraw(bool isClear = false)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *               ");
            Console.WriteLine("           *                                                                     *              ");
            Console.WriteLine("          *     ■     ■■■  ■■■   ■■■      ■■    ■■■   ■    ■     *             ");
            Console.WriteLine("         *      ■       ■    ■   ■  ■   ■    ■  ■   ■   ■   ■  ■       *            ");
            Console.WriteLine("        *       ■       ■    ■■■   ■■■    ■■■■  ■■■      ■          *           ");
            Console.WriteLine("         *      ■       ■    ■   ■  ■   ■   ■    ■  ■   ■     ■         *            ");
            Console.WriteLine("          *     ■■■ ■■■  ■■■   ■    ■  ■    ■  ■    ■    ■        *             ");
            Console.WriteLine("           *                                                                     *              ");
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *               ");
        }

        public void ExplainMemberLoginLabelDraw(bool isClear = false)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *               ");
            Console.WriteLine("           *                         < 회 원 로 그 인 >                          *              ");
            Console.WriteLine("          *                                                                       *             ");
            Console.WriteLine("         *                ● 가입하신 ID 와 PASSWORD를 적어주세요                  *            ");
            Console.WriteLine("        *                 ● ID       : (영어 & 숫자 6 ~ 10 글자)                   *           ");
            Console.WriteLine("         *                ● PASSWORD : (영어 & 숫자 6 ~ 10 글자)                  *            ");
            Console.WriteLine("          *                                                                       *             ");
            Console.WriteLine("           *                                                                     *              ");
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *               ");
        }


        public void ExplainAdimistratorLoginLabelDraw(bool isClear = false)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *               ");
            Console.WriteLine("           *                        < 관 리 자 로 그 인 >                        *              ");
            Console.WriteLine("          *                                                                       *             ");
            Console.WriteLine("         *                ● 관리자 ID 와 PASSWORD를 적어주세요                    *            ");
            Console.WriteLine("        *                 ● ID       : (영어 & 숫자 6 ~ 10 글자)                   *           ");
            Console.WriteLine("         *                ● PASSWORD : (영어 & 숫자 6 ~ 10 글자)                  *            ");
            Console.WriteLine("          *                                                                       *             ");
            Console.WriteLine("           *                                                                     *              ");
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *               ");
        }
    }
}
