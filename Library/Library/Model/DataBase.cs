using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Library.Model
{
    internal class DataBase
    {
        private static DataBase instance;
        private string sqlString;
        MySqlConnection connection = new MySqlConnection(Constant.DATABASE_CONNECTION_INFORMATION);

        private DataBase() { }
        public static DataBase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataBase();
                }
                return instance;
            }
        }

        public MySqlDataReader Select(string filed, string tableName, string conditionalString = "")
        {
            if(!connection.Ping())
                connection.Open();

            if (conditionalString == "") // 조건문이 없는경우
                sqlString = string.Format(Constant.QUERY_STRING_SELECT, filed, tableName);
            else
                sqlString =string.Format(Constant.QUERY_STRING_CONDITIONAL_SELECT, filed, tableName, conditionalString);

            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();

            return reader; // 다른곳에서 reader 닫아주는중
        }

        public List<string> GetSelectedElements(string filed, string tableName, string conditionalString = "")
        {
            List<string> selectedElements = new List<string>();

            if (!connection.Ping())
                connection.Open();
            if (conditionalString == "") // 조건문 없을때
                sqlString = string.Format(Constant.QUERY_STRING_SELECT, filed, tableName);
            else
                sqlString =string.Format(Constant.QUERY_STRING_CONDITIONAL_SELECT, filed, tableName, conditionalString);

            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                selectedElements.Add(reader[string.Format("{0}", filed)].ToString());
            }
            reader.Close();
            connection.Close();
            return selectedElements;
        }

        public void InsertMember(string tableName, string name, string id, string password, int age, string address, string phonenumber) //오버라이드로 insertbook이랑 묶기
        {
            if (!connection.Ping())
                connection.Open();
            sqlString = string.Format(Constant.QUERY_STRING_INSERT, tableName, name, id, password, age, address, phonenumber);
            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
            connection.Close();
        }

        public void Delete(string tableName, string conditionalString)
        {
            if (!connection.Ping())
                connection.Open();

            sqlString = string.Format(Constant.QUERY_STRING_CONDITIONAL_DELETE, tableName, conditionalString);


            //sqlString = "DELETE FROM " + tableName + " WHERE id = " + id;
            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
            connection.Close();
        }
    }
}
