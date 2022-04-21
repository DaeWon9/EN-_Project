﻿using System;

namespace Library.View
{
    class Label
    {
        public void LibraryLabelPrint(bool isClear = false)
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

        public void ExplainMemberLoginLabelPrint(bool isClear = false)
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


        public void ExplainAdimistratorLoginLabelPrint(bool isClear = false)
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

        public void ExplainMemberSignUpLabelPrint(bool isClear = false)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *               ");
            Console.WriteLine("           *                           < 회 원 가 입 >                           *              ");
            Console.WriteLine("          *                                                                       *             ");
            Console.WriteLine("         *     ● 해당칸에 정보를 입력해주세요                                     *            ");
            Console.WriteLine("        *      ● ID, PASSWORD : (영어 & 숫자 6 ~ 10 글자)                          *           ");
            Console.WriteLine("         *     ● 주소 : (시|도) + (시|군|구) + (읍|면|동) 까지는 꼭 적어주세요    *            ");
            Console.WriteLine("          *                                                                       *             ");
            Console.WriteLine("           *                                                                     *              ");
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *               ");
        }
    }
}
