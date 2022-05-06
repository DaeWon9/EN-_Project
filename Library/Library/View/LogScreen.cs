using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Library.View
{
    internal class LogScreen : Label
    {
        public void PrintLog(MySqlDataReader reader)
        {
            Console.WriteLine("                                          < 로 그 현 황 >                                           ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
            while (reader.Read())
            {
                Console.WriteLine(" < {0}번 >", reader[Constant.LOG_FILED_NUMBER]);
                Console.WriteLine("----------------------------------------------------------------------------------------------------");
                Console.WriteLine(" 활동시간 : {0}", reader[Constant.LOG_FILED_DATE]);
                Console.WriteLine(" 회원정보 : {0}", reader[Constant.LOG_FILED_MEMBER]);
                Console.WriteLine(" 활동내역 : {0}", reader[Constant.LOG_FILED_ACTIVITY]);
                Console.WriteLine("----------------------------------------------------------------------------------------------------");
            }
            Console.WriteLine("                                                                                뒤로가기 : ESC      ");
            reader.Close();
        }

        public void PrintDeleteLogScreen(bool isClear = true)
        {
            PrintDeleteLogLabel(isClear);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                뒤로가기 : ESC      ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                     로그번호 :                                                     ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                                     < 삭제하기 >                                                   ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }

    }
}
