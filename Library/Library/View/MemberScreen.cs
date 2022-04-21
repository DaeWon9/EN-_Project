using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.View
{
    class MemberScreen : Label
    {

        public void MainScreenPrint(bool isClear = false)
        {
            LibraryLabelPrint(isClear);
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                                                              뒤로가기 : ESC    ");
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                      ▶  로그인                                                ");
            Console.WriteLine("                                      ▶  회원가입                                              ");
            Console.WriteLine("                                                                                                ");
        }
        public void LoginScreenPrint(bool isClear = false)
        {
            ExplainMemberLoginLabelPrint(isClear);
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                                                              뒤로가기 : ESC    ");
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                 ID :                                                           ");
            Console.WriteLine("                                 PW :                                                           ");
            Console.WriteLine("                                                                                                ");
        }
        public void MenuScreenPrint(bool isClear = false)
        {
            LibraryLabelPrint(isClear);
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                                                              뒤로가기 : ESC    ");
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                      ▶  도서찾기                                              ");
            Console.WriteLine("                                      ▶  도서대여                                              ");
            Console.WriteLine("                                      ▶  대여도서확인                                          ");
            Console.WriteLine("                                      ▶  회원정보수정                                          ");
            Console.WriteLine("                                                                                                ");
        }
    }
}
