using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.View
{
    class MemberScreen : Label
    {

        public void PrintMainScreen(bool isClear = false)
        {
            PrintLibraryLabel(isClear);
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                                                              뒤로가기 : ESC    ");
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                      ▶  로그인                                                ");
            Console.WriteLine("                                      ▶  회원가입                                              ");
            Console.WriteLine("                                                                                                ");
        }
        public void PrintLoginScreen(bool isClear = false)
        {
            PrintExplainMemberLoginLabel(isClear);
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                                                              뒤로가기 : ESC    ");
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                    ID :                                                        ");
            Console.WriteLine("                                    PW :                                                        ");
            Console.WriteLine("                                                                                                ");
        }
        public void PrintMenuScreen(bool isClear = false)
        {
            PrintLibraryLabel(isClear);
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                                                              뒤로가기 : ESC    ");
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                      ▶  도서찾기                                              ");
            Console.WriteLine("                                      ▶  도서대여                                              ");
            Console.WriteLine("                                      ▶  대여도서확인                                          ");
            Console.WriteLine("                                      ▶  회원정보수정                                          ");
            Console.WriteLine("                                                                                                ");
        }

        public void PrintSignUpScreen(bool isClear = false)
        {
            PrintExplainMemberSignUpLabel(isClear);
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                                                              뒤로가기 : ESC    ");
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                               이름          :                                                  ");
            Console.WriteLine("                               아이디        :                                                  ");
            Console.WriteLine("                               비밀번호      :                                                  ");
            Console.WriteLine("                               비밀번호 확인 :                                                  ");
            Console.WriteLine("                               나이          :                                                  ");
            Console.WriteLine("                               주소          :                                                  ");
            Console.WriteLine("                               핸드폰번호    :                                                  ");
            Console.WriteLine("                                                                                                ");
        }

        public void PrintBookSearchScreen(bool isClear = false)
        {
            PrintExplainBookSearchLabel(isClear);
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                                                              뒤로가기 : ESC    ");
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                               도서아이디    :                                                  ");
            Console.WriteLine("                               도서명        :                                                  ");
            Console.WriteLine("                               출판사        :                                                  ");
            Console.WriteLine("                               저자          :                                                  ");
            Console.WriteLine("                               도서가격      :                                                  ");
            Console.WriteLine("                               도서수량      :                                                  ");
            Console.WriteLine("                                                                                                ");
        }
    }
}
