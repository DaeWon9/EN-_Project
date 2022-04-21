using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Library.Model
{
    internal class DataBaseTestSingleton
    {
        private static DataBaseTestSingleton instance;
        private string sqlstring;

        MySqlConnection connection = new MySqlConnection(Constant.DATABASE_CONNECTION_INFORMATION);

        private DataBaseTestSingleton() { }
        public static DataBaseTestSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataBaseTestSingleton();
                }
                return instance;
            }
        }

        public void Select(string filed, string tableName, string conditionalString = "")
        {
            connection.Open();

            if (conditionalString == "") // 조건문이 없는경우
                sqlstring = string.Format(Constant.QUERY_STRING_SELECT, filed, tableName);
            else
                sqlstring =string.Format(Constant.QUERY_STRING_CONDITIONAL_SELECT, filed, tableName, conditionalString);

            MySqlCommand command = new MySqlCommand(sqlstring, connection);
            MySqlDataReader reader = command.ExecuteReader();


            if (filed == Constant.FILED_ALL) // 전체
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader[Constant.BOOK_FILED_ID]);
                    Console.WriteLine(reader[Constant.BOOK_FILED_NAME]);
                    Console.WriteLine(reader[Constant.BOOK_FILED_PUBLISHER]);
                    Console.WriteLine(reader[Constant.BOOK_FILED_AUTHOR]);
                    Console.WriteLine(reader[Constant.BOOK_FILED_PRICE]);
                    Console.WriteLine(reader[Constant.BOOK_FILED_QUANTITY]);
                }
            }
            else
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader[string.Format("{0}", filed)]);
                }
            }
            reader.Close();
            connection.Close();
        }

        public List<string> GetSelectedElements(string filed, string tableName, string conditionalString = "")
        {
            List<string> selectedElements = new List<string>();

            connection.Open();
            if (conditionalString == "") // 조건문 없을때
                sqlstring = string.Format(Constant.QUERY_STRING_SELECT, filed, tableName);
            else
                sqlstring =string.Format(Constant.QUERY_STRING_CONDITIONAL_SELECT, filed, tableName, conditionalString);

            MySqlCommand command = new MySqlCommand(sqlstring, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                selectedElements.Add(reader[string.Format("{0}", filed)].ToString());
            }
            reader.Close();
            connection.Close();
            return selectedElements;
        }

        public void MemberInsert(string tableName, string name, string id, string password, int age, string address, string phonenumber)
        {
            connection.Open();
            sqlstring = string.Format(Constant.QUERY_STRING_INSERT, tableName, name, id, password, age, address, phonenumber);
            MySqlCommand command = new MySqlCommand(sqlstring, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
            connection.Close();
        }
    }
}
