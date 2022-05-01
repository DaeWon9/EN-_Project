using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Library.View
{
    internal class LogScreen
    {
        public void PrintLog(MySqlDataReader reader)
        {
            Console.WriteLine("              시간              |        회원정보       |                    활동                    ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");

            while (reader.Read())
            {
                Console.WriteLine(" " + reader[Constant.LOG_FILED_DATE] + "\t| " +  reader[Constant.LOG_FILED_MEMBER] + "\t\t| " + reader[Constant.LOG_FILED_ACTIVITY]);
                Console.WriteLine("----------------------------------------------------------------------------------------------------");
            }
            reader.Close();
        }
    }
}
