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

            while (reader.Read())
            {
                Console.WriteLine(reader["id"]);
                Console.WriteLine(reader["name"]);
                Console.WriteLine(reader["publisher"]);
                Console.WriteLine(reader["author"]);
                Console.WriteLine(reader["price"]);
                Console.WriteLine(reader["quantity"]);
                Console.WriteLine("-----------------------------------------------------");
                //Console.WriteLine("{0} {1} {2} {3} {4} {5}", reader["id"], reader["name"], reader["publisher"], reader["author"], reader["price"], reader["quantity"]);
            }
            reader.Close();
        }

    }
}
