using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.View
{
    class AdministratorScreen : BothScreen
    {
        public void PrintLoginScreen(bool isClear = true)
        {
            PrintAdimistratorLoginLabel(isClear);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                   뒤로가기 : ESC   ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                       ID :                                                         ");
            Console.WriteLine("                                       PW :                                                         ");
            Console.WriteLine("                                                                                                    ");
        }

        public void PrintMenuScreen(bool isClear = true)
        {
            PrintLibraryLabel(isClear);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                   선택 : ENTER    로그아웃 : ESC   ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                         ▶  도서찾기                                               ");
            Console.WriteLine("                                         ▶  도서추가                                               ");
            Console.WriteLine("                                         ▶  도서삭제                                               ");
            Console.WriteLine("                                         ▶  도서수정                                               ");
            Console.WriteLine("                                         ▶  회원관리                                               ");
            Console.WriteLine("                                         ▶  대여상황                                               ");
            Console.WriteLine("                                                                                                    ");
        }

        public void PrintManagementMemberScreen(bool isClear = true)
        {
            PrintMemberManagementLabel(isClear);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                   뒤로가기 : ESC   ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                  회원아이디    :                                                   ");
            Console.WriteLine("                                  < 관리하기 >                                                      ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }
    }
}
