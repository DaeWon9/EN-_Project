using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Library.Controller
{
    class TableFuntions
    {
        private string connectString;

        public TableFuntions(string connectString)
        {
            this.connectString = connectString;
        }

        public void BookSelect(string filed, string tableName, string conditionalString = "")
        {
            string sql;
            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();

                if (conditionalString == "") // 조건문이 없는경우
                    sql = "SELECT " + filed + " FROM " + tableName;
                else
                    sql = "SELECT " + filed + " FROM " + tableName + " WHERE " + conditionalString;

         
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("{0} {1} {2} {3} {4} {5}", reader["id"], reader["name"], reader["publisher"], reader["author"], reader["price"], reader["quantity"]);
                }
                reader.Close();
            }
        }
    }
}
