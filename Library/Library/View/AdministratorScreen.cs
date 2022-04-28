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
            Console.WriteLine("                                     회원아이디 :                                                   ");
            Console.WriteLine("                                     < 수정하기 >                                                   ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }

        public void PrintAddBookScreen(bool isClear = true)
        {
            PrintAddBookLabel(isClear);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                   뒤로가기 : ESC   ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                  도서아이디 :                                                      ");
            Console.WriteLine("                                  도서명     :                                                      ");
            Console.WriteLine("                                  출판사     :                                                      ");
            Console.WriteLine("                                  저자       :                                                      ");
            Console.WriteLine("                                  가격       :                                                      ");
            Console.WriteLine("                                  수량       :                                                      ");
            Console.WriteLine("                                  < 추가하기 >                                                      ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }

        public void PrintModifyBookScreen(bool isClear = true)
        {
            Console.WriteLine("                                         < 수 정 할 정 보 >                                         ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  도서아이디  : 변경불가                                                                            ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  도서명 변경 :                                                                                     ");
            Console.WriteLine("  출판사 변경 :                                                                                     ");
            Console.WriteLine("  저자 변경   :                                                                                     ");
            Console.WriteLine("  가격 변경   :                                                                                     ");
            Console.WriteLine("  수량 변경   :                                                                                     ");
            Console.WriteLine("  < 도서삭제 >                                                                                      ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }

        public void PrintSelectModifyBookScreen(bool isClear = true)
        {
            PrintSelectModifyBookIDLabel(isClear);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                   뒤로가기 : ESC   ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                     도서아이디 :                                                   ");
            Console.WriteLine("                                     < 수정하기 >                                                   ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }

        public void PrintSelectModifyBookModeScreen(bool isClear = true)
        {
            PrintSelectModifyBookModeLabel(isClear);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                   선택 : ENTER    뒤로가기 : ESC   ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                         ▶  바로 수정                                              ");
            Console.WriteLine("                                         ▶  검색 후 수정                                           ");
            Console.WriteLine("                                                                                                    ");
        }

        public void PrintSelectManagementMemberModeScreen(bool isClear = true)
        {
            PrintSelectManagementMemberModeLabel(isClear);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                   선택 : ENTER    뒤로가기 : ESC   ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                         ▶  회원정보 검색                                          ");
            Console.WriteLine("                                         ▶  회원정보 수정                                          ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }

        public void PrintMemberSearchScreen(bool isClear = true)
        {
            PrintMemberSearchLabel(isClear);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                   뒤로가기 : ESC   ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                  이름          :                                                   ");
            Console.WriteLine("                                  아이디        :                                                   ");
            Console.WriteLine("                                  나이          :                                                   ");
            Console.WriteLine("                                  주소          :                                                   ");
            Console.WriteLine("                                  핸드폰번호    :                                                   ");
            Console.WriteLine("                                  < 검색하기 >                                                      ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }

        public void PrintSelectCheckBorrowedBookModeScreen(bool isClear = true)
        {
            PrintAdministratorCheckBorrowedBookModeLabel(isClear);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                   뒤로가기 : ESC   ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                     도서아이디 :                                                   ");
            Console.WriteLine("                                     회원아이디 :                                                   ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }
    }
}
