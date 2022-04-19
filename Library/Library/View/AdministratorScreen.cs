using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.View
{
    class AdministratorScreen : Label
    {
        public void LoginScreenDraw(bool isClear = false)
        {
            ExplainAdimistratorLoginLabelDraw(isClear);
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                                                              뒤로가기 : ESC    ");
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                             ID :                                                               ");
            Console.WriteLine("                             PW :                                                               ");
            Console.WriteLine("                                                                                                ");
        }

        public void MenuScreenDraw(bool isClear = false)
        {
            LibraryLabelDraw(isClear);
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                                                              뒤로가기 : ESC    ");
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                      ▶  도서찾기                                              ");
            Console.WriteLine("                                      ▶  도서추가                                              ");
            Console.WriteLine("                                      ▶  도서삭제                                              ");
            Console.WriteLine("                                      ▶  도서수정                                              ");
            Console.WriteLine("                                      ▶  회원관리                                              ");
            Console.WriteLine("                                      ▶  대여상황                                              ");
            Console.WriteLine("                                                                                                ");
        }
    }
}
