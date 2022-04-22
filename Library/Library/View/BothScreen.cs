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
        public void PrintSelectUserModeScreen(bool isClear = false)
        {
            PrintLibraryLabel(isClear);
            Console.WriteLine("                                                                                              ");
            Console.WriteLine("                                                            선택 : ENTER        종료 : ESC    ");
            Console.WriteLine("                                                                                              ");
            Console.WriteLine("                                      ▶  회원모드                                            ");
            Console.WriteLine("                                      ▶  관리자모드                                          ");
            Console.WriteLine("                                                                                              ");
        }

        public void PrintSelectedValues(MySqlDataReader reader, string tableName)
        {
            if (tableName == Constant.TABLE_NAME_BOOK)
            {
                while (reader.Read())
                {
                    Console.WriteLine("도서아이디 : " + reader[Constant.BOOK_FILED_ID]);
                    Console.WriteLine("도서명     : " + reader[Constant.BOOK_FILED_NAME]);
                    Console.WriteLine("출판사     : " + reader[Constant.BOOK_FILED_PUBLISHER]);
                    Console.WriteLine("저자       : " + reader[Constant.BOOK_FILED_AUTHOR]);
                    Console.WriteLine("도서가격   : " + reader[Constant.BOOK_FILED_PRICE]);
                    Console.WriteLine("도서수량   : " + reader[Constant.BOOK_FILED_QUANTITY]);
                    Console.WriteLine("----------------------------------------------------------------------------------------------");
                }
            }
            if (tableName == Constant.TABLE_NAME_MEMBER)
            {
                while (reader.Read())
                {
                    Console.WriteLine("이름       : " + reader[Constant.MEMBER_FILED_NAME]);
                    Console.WriteLine("아이디     : " + reader[Constant.MEMBER_FILED_ID]);
                    Console.WriteLine("비밀번호   : " + reader[Constant.MEMBER_FILED_PASSWORD]);
                    Console.WriteLine("나이       : " + reader[Constant.MEMBER_FILED_AGE]);
                    Console.WriteLine("주소       : " + reader[Constant.MEMBER_FILED_ADDRESS]);
                    Console.WriteLine("핸드폰번호 : " + reader[Constant.MEMBER_FILED_PHONE_NUMBER]);
                    Console.WriteLine("----------------------------------------------------------------------------------------------");
                }
            }
            reader.Close();
        }
    }
}
