using Library.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.View
{
    class BothScreen : Label
    {
        public void PrintSelectUserModeScreen(bool isClear = true)
        {
            PrintLibraryLabel(isClear);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                   선택 : ENTER        종료 : ESC   ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                         ▶  회원모드                                               ");
            Console.WriteLine("                                         ▶  관리자모드                                             ");
            Console.WriteLine("                                                                                                    ");
        }

        public void PrintBookSearchScreen(bool isClear = true)
        {
            PrintBookSearchLabel(isClear);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                   뒤로가기 : ESC   ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                  도서아이디    :                                                   ");
            Console.WriteLine("                                  도서명        :                                                   ");
            Console.WriteLine("                                  출판사        :                                                   ");
            Console.WriteLine("                                  저자          :                                                   ");
            Console.WriteLine("                                  도서가격(MAX) :                                                   ");
            Console.WriteLine("                                  도서수량(MIN) :                                                   ");
            Console.WriteLine("                                  < 검색하기 >                                                      ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }

        public void PrintSearchResultScreen(bool isClear = true)
        {
            PrintLibraryLabel(isClear);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                               다시검색 : ENTER    뒤로가기 : ESC   ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }

        public void PrintBorrowedBookListScreen(bool isClear = true)
        {
            PrintCheckBorrowedBookLabel(isClear);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                   뒤로가기 : ESC   ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }

        public void PrintModifyMemberInformationScreen(bool isClear = true)
        {
            Console.SetCursorPosition(Constant.MODIFY_MEMBER_MODE_POS_X, (int)Constant.MemberModifyModePosY.NAME);
            Console.WriteLine("|  이름 변경       :");
            Console.SetCursorPosition(Constant.MODIFY_MEMBER_MODE_POS_X, (int)Constant.MemberModifyModePosY.PASSWORD);
            Console.WriteLine("|  비밀번호 변경   :");
            Console.SetCursorPosition(Constant.MODIFY_MEMBER_MODE_POS_X, (int)Constant.MemberModifyModePosY.AGE);
            Console.WriteLine("|  나이 변경       :");
            Console.SetCursorPosition(Constant.MODIFY_MEMBER_MODE_POS_X, (int)Constant.MemberModifyModePosY.ADDRESS);
            Console.WriteLine("|  주소 변경       :");
            Console.SetCursorPosition(Constant.MODIFY_MEMBER_MODE_POS_X, (int)Constant.MemberModifyModePosY.PHONE_NUMBER);
            Console.WriteLine("|  핸드폰번호 변경 :");
            Console.SetCursorPosition(Constant.MODIFY_MEMBER_MODE_POS_X, (int)Constant.MemberModifyModePosY.WITHDRAWAL);
            Console.WriteLine("|  < 회원탈퇴 >");
        }

        public void PrintSelectedValues(MySqlDataReader reader, string tableName, string memeberName, bool isAdministator = false)
        {
            if (tableName == Constant.TABLE_NAME_BOOK)
            {
                while (reader.Read())
                {
                    Console.WriteLine("  도서아이디 : " + reader[Constant.BOOK_FILED_ID]);
                    Console.WriteLine("  도서명     : " + reader[Constant.BOOK_FILED_NAME]);
                    Console.WriteLine("  출판사     : " + reader[Constant.BOOK_FILED_PUBLISHER]);
                    Console.WriteLine("  저자       : " + reader[Constant.BOOK_FILED_AUTHOR]);
                    Console.WriteLine("  도서가격   : " + reader[Constant.BOOK_FILED_PRICE]);
                    Console.WriteLine("  도서수량   : " + reader[Constant.BOOK_FILED_QUANTITY]);
                    Console.WriteLine("----------------------------------------------------------------------------------------------------");
                }
            }
            else if (tableName == Constant.TABLE_NAME_MEMBER)
            {
                while (reader.Read())
                {
                    Console.WriteLine("  이름       : " + reader[Constant.MEMBER_FILED_NAME]);
                    Console.WriteLine("  아이디     : " + reader[Constant.MEMBER_FILED_ID]);
                    Console.WriteLine("  비밀번호   : " + reader[Constant.MEMBER_FILED_PASSWORD]);
                    Console.WriteLine("  나이       : " + reader[Constant.MEMBER_FILED_AGE]);
                    Console.WriteLine("  주소       : " + reader[Constant.MEMBER_FILED_ADDRESS]);
                    Console.WriteLine("  핸드폰번호 : " + reader[Constant.MEMBER_FILED_PHONE_NUMBER]);
                    Console.WriteLine("----------------------------------------------------------------------------------------------------");
                }
            }
            else
            {
                if (isAdministator)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(" < {0}({1}) 님의 대여도서 목록 >", memeberName, tableName);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("----------------------------------------------------------------------------------------------------");
                }

                while (reader.Read())
                {
                    TimeSpan remainDate = Convert.ToDateTime(reader[Constant.BORROWED_BOOK_FILED_RETURN_DATE]) - DateTime.Now;

                    if (remainDate.Days  < 1)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (remainDate.Days < 4)
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    else
                        Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine("  도서아이디 : " + reader[Constant.BORROWED_BOOK_FILED_ID]);
                    Console.WriteLine("  도서명     : " + reader[Constant.BORROWED_BOOK_FILED_NAME]);
                    Console.WriteLine("  출판사     : " + reader[Constant.BORROWED_BOOK_FILED_PUBLISHER]);
                    Console.WriteLine("  저자       : " + reader[Constant.BORROWED_BOOK_FILED_AUTHOR]);
                    Console.WriteLine("  대여일자   : " + reader[Constant.BORROWED_BOOK_FILED_BORROW_DATE] + "\t\t\t남은기간 : " + remainDate.Days + "일 " + remainDate.Hours + "시간");
                    Console.WriteLine("  반납일자   : " + reader[Constant.BORROWED_BOOK_FILED_RETURN_DATE]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("----------------------------------------------------------------------------------------------------");
                }
            }
            reader.Close();
        }
    }
}
