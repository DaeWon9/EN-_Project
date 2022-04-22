using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.View
{
    class AdministratorScreen : BothScreen
    {
        public void PrintLoginScreen(bool isClear = false)
        {
            PrintExplainAdimistratorLoginLabel(isClear);
            Console.WriteLine("                                                                                              ");
            Console.WriteLine("                                                                            뒤로가기 : ESC    ");
            Console.WriteLine("                                                                                              ");
            Console.WriteLine("                                    ID :                                                      ");
            Console.WriteLine("                                    PW :                                                      ");
            Console.WriteLine("                                                                                              ");
        }

        public void PrintMenuScreen(bool isClear = false)
        {
            PrintLibraryLabel(isClear);
            Console.WriteLine("                                                                                              ");
            Console.WriteLine("                                                                            뒤로가기 : ESC    ");
            Console.WriteLine("                                                                                              ");
            Console.WriteLine("                                      ▶  도서찾기                                            ");
            Console.WriteLine("                                      ▶  도서추가                                            ");
            Console.WriteLine("                                      ▶  도서삭제                                            ");
            Console.WriteLine("                                      ▶  도서수정                                            ");
            Console.WriteLine("                                      ▶  회원관리                                            ");
            Console.WriteLine("                                      ▶  대여상황                                            ");
            Console.WriteLine("                                                                                              ");
        }

        public void PrintBookSearchScreen(bool isClear = false)
        {
            PrintExplainBookSearchLabel(isClear);
            Console.WriteLine("                                                                                              ");
            Console.WriteLine("                                                                            뒤로가기 : ESC    ");
            Console.WriteLine("                                                                                              ");
            Console.WriteLine("                               도서아이디    :                                                ");
            Console.WriteLine("                               도서명        :                                                ");
            Console.WriteLine("                               출판사        :                                                ");
            Console.WriteLine("                               저자          :                                                ");
            Console.WriteLine("                               도서가격      :                                                ");
            Console.WriteLine("                               도서수량      :                                                ");
            Console.WriteLine("                               <검색하기>                                                     ");
            Console.WriteLine("                                                                                              ");
            Console.WriteLine("----------------------------------------------------------------------------------------------");
        }

        public void PrintSearchResultScreen(bool isClear = false)
        {
            PrintLibraryLabel(isClear);
            Console.WriteLine("                                                                                              ");
            Console.WriteLine("                                                      다시검색 : ENTER      뒤로가기 : ESC    ");
            Console.WriteLine("----------------------------------------------------------------------------------------------");
        }
    }
}
