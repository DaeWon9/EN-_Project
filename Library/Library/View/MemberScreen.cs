using System;

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
            Console.WriteLine("                                             로그인                                                 ");
            Console.WriteLine("                                             회원가입                                               ");
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
            Console.WriteLine("                                             도서찾기                                               ");
            Console.WriteLine("                                             도서대여                                               ");
            Console.WriteLine("                                             도서반납                                               ");
            Console.WriteLine("                                             대여도서확인                                           ");
            Console.WriteLine("                                             회원정보수정                                           ");
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
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("         ----------------------------------------------------------------------------------         ");
            Console.WriteLine("        |            이름 : (한글 : 2~5 글자 | 영어 : 3~20 글자)                           |        ");
            Console.WriteLine("        |            아이디, 비밀번호 : (영어 & 숫자 6~10 글자)                            |        ");
            Console.WriteLine("        |            생년월일 : (yyyymmdd)  <ex : 20001230>                                |        ");
            Console.WriteLine("        |            주소 : 도로명주소 OR 지번주소                                         |        ");
            Console.WriteLine("        |             <ex : 서울특별시 광진구 아차산로31길 OR 서울특별시 광진구 자양1동>   |        ");
            Console.WriteLine("        |            핸드폰번호 : 0xxxxxxxxxx <ex : 01012345678, 0413334444>                           |        ");
            Console.WriteLine("         ----------------------------------------------------------------------------------         ");
        }

        public void PrintSelectBorrowBookModeScreen(bool isClear = true)
        {
            PrintBorrowBookModeLabel(isClear);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                   선택 : ENTER    뒤로가기 : ESC   ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                             바로 대여                                              ");
            Console.WriteLine("                                             검색 후 대여                                           ");
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
