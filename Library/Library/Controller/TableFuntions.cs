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
        private string sqlstring;
        public TableFuntions(string connectString)
        {
            this.connectString = connectString;
        }

        public void BookSelect(string filed, string tableName, string conditionalString = "")
        {
            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();

                if (conditionalString == "") // 조건문이 없는경우
                    sqlstring = "SELECT " + filed + " FROM " + tableName;
                else
                    sqlstring = "SELECT " + filed + " FROM " + tableName + " WHERE " + conditionalString;

         
                MySqlCommand command = new MySqlCommand(sqlstring, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("{0} {1} {2} {3} {4} {5}", reader["id"], reader["name"], reader["publisher"], reader["author"], reader["price"], reader["quantity"]);
                }
                reader.Close();
            }
        }
        public void MemberSelect(string filed, string tableName, string conditionalString = "")
        {
            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();

                if (conditionalString == "") // 조건문이 없는경우
                    sqlstring = "SELECT " + filed + " FROM " + tableName;
                else
                    sqlstring = "SELECT " + filed + " FROM " + tableName + " WHERE " + conditionalString;


                MySqlCommand command = new MySqlCommand(sqlstring, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("{0} {1} {2} {3} {4} {5}", reader["name"], reader["id"], reader["pw"], reader["age"], reader["address"], reader["phonenumber"]);
                }
                reader.Close();
            }
        }

        public void BookInsert(string tableName, int id, string name, string publisher, string author, int price, int quantity)
        {
            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();
                sqlstring = "INSERT INTO " + tableName + "(id, name, publisher, author, price, quantity) VALUES " + "('" + id + "','" + name + "','" + publisher + "','" + author + "','" + price + "','" + quantity +"')";
                MySqlCommand command = new MySqlCommand(sqlstring, connection);
                MySqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
        }

        public void BookDelete(string tableName, int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();
                sqlstring = "DELETE FROM " + tableName + " WHERE id = " + id;
                MySqlCommand command = new MySqlCommand(sqlstring, connection);
                MySqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
        }

    }
}
