using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Library.Model
{
    class DataBase // 싱글톤으로 하기
    {
        private string connectString;
        private string sqlString;
        public DataBase(string connectString)
        {
            this.connectString = connectString;
        }

        public void BookSelect(string filed, string tableName, string conditionalString = "")
        {
            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();

                if (conditionalString == "") // 조건문이 없는경우
                    sqlString = "SELECT " + filed + " FROM " + tableName;
                else
                    sqlString = "SELECT " + filed + " FROM " + tableName + " WHERE " + conditionalString;

         
                MySqlCommand command = new MySqlCommand(sqlString, connection);
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
                    sqlString = "SELECT " + filed + " FROM " + tableName;
                else
                    sqlString = "SELECT " + filed + " FROM " + tableName + " WHERE " + conditionalString;


                MySqlCommand command = new MySqlCommand(sqlString, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("{0} {1} {2} {3} {4} {5}", reader["name"], reader["id"], reader["pw"], reader["age"], reader["address"], reader["PHONE_NUMBER"]);
                }
                reader.Close();
            }
        }

        public void BookInsert(string tableName, int id, string name, string publisher, string author, int price, int quantity)
        {
            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();
                sqlString = "INSERT INTO " + tableName + "(id, name, publisher, author, price, quantity) VALUES " + "('" + id + "','" + name + "','" + publisher + "','" + author + "','" + price + "','" + quantity +"')";
                MySqlCommand command = new MySqlCommand(sqlString, connection);
                MySqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
        }

        public void BookDelete(string tableName, int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();
                sqlString = "DELETE FROM " + tableName + " WHERE id = " + id;
                MySqlCommand command = new MySqlCommand(sqlString, connection);
                MySqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
        }

    }
}
