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
            Console.WriteLine("              시간              |        회원정보        |                    활동                    ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
            //Console.Write("{0}", lectureList[row][column] + "".PadRight(GetPadLength(GetSortLength(column), lectureList[row][column])));
            while (reader.Read())
            {
                Console.Write(" {0}", reader[Constant.LOG_FILED_DATE] + "".PadRight(Constant.LOG_LENGTH_DATE - Encoding.Default.GetBytes(reader[Constant.LOG_FILED_DATE].ToString()).Length) + "|");
                Console.Write(" {0}", reader[Constant.LOG_FILED_MEMBER] + "".PadRight(Constant.LOG_LENGTH_MEMBER - Encoding.Default.GetBytes(reader[Constant.LOG_FILED_MEMBER].ToString()).Length) + "|");
                Console.WriteLine(" {0}", reader[Constant.LOG_FILED_ACTIVITY]);
                Console.WriteLine("----------------------------------------------------------------------------------------------------");
            }
            reader.Close();
        }

    }
}
