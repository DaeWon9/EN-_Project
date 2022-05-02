using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.View
{
    class MemberScreen : BothScreen
    {

        public void PrintMainScreen(bool isClear = true)
        {
            PrintLibraryLabel(isClear);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                   선택 : ENTER        종료 : ESC   ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                         ▶  로그인                                                 ");
            Console.WriteLine("                                         ▶  회원가입                                               ");
            Console.WriteLine("                                                                                                    ");
        }
        public void PrintLoginScreen(bool isClear = true)
        {
            PrintMemberLoginLabel(isClear);
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
            Console.WriteLine("                                         ▶  도서대여                                               ");
            Console.WriteLine("                                         ▶  도서반납                                               ");
            Console.WriteLine("                                         ▶  대여도서확인                                           ");
            Console.WriteLine("                                         ▶  회원정보수정                                           ");
            Console.WriteLine("                                                                                                    ");
        }

        public void PrintSignUpScreen(bool isClear = true)
        {
            PrintMemberSignUpLabel(isClear);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                   뒤로가기 : ESC   ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                  이름          :                                                   ");
            Console.WriteLine("                                  아이디        :                                                   ");
            Console.WriteLine("                                  비밀번호      :                                                   ");
            Console.WriteLine("                                  비밀번호 확인 :                                                   ");
            Console.WriteLine("                                  생년월일      :                                                   ");
            Console.WriteLine("                                  주소          :                                                   ");
            Console.WriteLine("                                  핸드폰번호    :                                                   ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                                  < 회원가입 >                                                      ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                                                                                                    ");
        }

        public void PrintSelectBorrowBookModeScreen(bool isClear = true)
        {
            PrintBorrowBookModeLabel(isClear);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                   선택 : ENTER    뒤로가기 : ESC   ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                         ▶  바로 대여                                              ");
            Console.WriteLine("                                         ▶  검색 후 대여                                           ");
            Console.WriteLine("                                                                                                    ");
        }

        public void PrintBorrowBookScreen(bool isClear = true)
        {
            PrintBorrowBookLabel(isClear);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                   뒤로가기 : ESC   ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                  도서아이디    :                                                   ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                                  < 대여하기 >                                                      ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }

        public void PrintReturnBookScreen(bool isClear = true)
        {
            PrintReturnBookLabel(isClear);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                   뒤로가기 : ESC   ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                  도서아이디    :                                                   ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                                  < 반납하기 >                                                      ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }
    }
}
