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
            if (!connection.Ping())
                connection.Open();

            if (conditionalString == "") // 조건문이 없는경우
                sqlString = string.Format(Constant.QUERY_STRING_SELECT, filed, tableName);
            else
                sqlString = string.Format(Constant.QUERY_STRING_CONDITIONAL_SELECT, filed, tableName, conditionalString);

            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();

            return reader; // 다른곳에서 reader 닫아주는중
        }

        public string GetSelectedElement(string filed, string tableName, string conditionalString)
        {
            string selectedElement = "";

            if (!connection.Ping())
                connection.Open();
            sqlString =string.Format(Constant.QUERY_STRING_CONDITIONAL_SELECT, filed, tableName, conditionalString);


            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
                selectedElement = reader[string.Format("{0}", filed)].ToString();

            reader.Close();
            connection.Close();
            return selectedElement;
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

        public void InsertMember(string tableName, string name, string id, string password, int age, string address, string phonenumber) //오버라이드로 or 그냥 다른이름
        {
            if (!connection.Ping())
                connection.Open();
            sqlString = string.Format(Constant.QUERY_STRING_INSERT_MEMBER, tableName, name, id, password, age, address, phonenumber);
            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
            connection.Close();
        }

        private bool IsBorrowBookDuplicate(string tableName, int id)
        {
            List<string> borrowedBookList = GetSelectedElements(Constant.BOOK_FILED_ID, tableName);
            for (int repeat = 0; repeat < borrowedBookList.Count; repeat++)
            {
                if (borrowedBookList[repeat] == id.ToString())
                    return true;
            }
            return false;
        }

        private bool IsExistBookInLibrary(int bookid, List<string> searchedBookIdList)
        {
            if (searchedBookIdList.Count < 1)
                searchedBookIdList = GetSelectedElements(Constant.BOOK_FILED_ID, Constant.TABLE_NAME_BOOK);
            for (int repeat = 0; repeat < searchedBookIdList.Count; repeat++)
            {
                if (searchedBookIdList[repeat] == bookid.ToString())
                    return true;
            }
            return false;
        }


        public void MINUS_BOOK_QUANTITY(int id)
        {
            if (!connection.Ping())
                connection.Open();
            sqlString = string.Format(Constant.QUERY_STRING_UPDATE_BOOK_QUANTITY_BY_BORROWED, id); // 도서관에 보관중인 책에서 개수 -1
            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
            connection.Close();
        }

        public void InsertBorrowedBook(string tableName, int id, string bookName, string bookPublisher, string bookAuthor, int bookPrice)
        {
            DateTime borrowDate = DateTime.Now;
            DateTime returnDate = borrowDate.AddDays(7);

            if (!connection.Ping())
                connection.Open();
            sqlString = string.Format(Constant.QUERY_STRING_INSERT_BORROW_BOOK, tableName, id, bookName, bookPublisher, bookAuthor, bookPrice, /*bookQuantity*/1, borrowDate.ToString("yyyy-MM-dd-HH-mm-ss"), returnDate.ToString("yyyy-MM-dd-HH-mm-ss"));
            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
            connection.Close();
        }
        public int IsInsertBorrowedBook(string tableName, int id, List<string> searchedBookIdList)
        {
            string bookName = "", bookPublisher = "", bookAuthor = "";
            int bookPrice = 0, bookQuantity = 0;

            if (!connection.Ping())
                connection.Open();

            // 사용자가 대여하고자하는 도서의 정보 불러오기 (도서관에 보유중인)
            sqlString = string.Format(Constant.QUERY_STRING_CONDITIONAL_SELECT, Constant.FILED_ALL, Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_INT, Constant.BOOK_FILED_ID, id));
            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                bookName = reader[Constant.BOOK_FILED_NAME].ToString();
                bookPublisher = reader[Constant.BOOK_FILED_PUBLISHER].ToString();
                bookAuthor = reader[Constant.BOOK_FILED_AUTHOR].ToString();
                bookPrice = (int)reader[Constant.BOOK_FILED_PRICE];
                bookQuantity = (int)reader[Constant.BOOK_FILED_QUANTITY];
            }
            reader.Close();
            connection.Close();

            if (!IsExistBookInLibrary(id, searchedBookIdList)) // 도서관에 없는책이면 대여 불가능
                return (int)Constant.CheckInsertBorrowedBook.NOT_EXIST_BOOK;

            if (bookQuantity < 1) // 도서관에 책 보유수량이 없으면 대여 불가능
                return (int)Constant.CheckInsertBorrowedBook.SHORTAGE_BOOK_QUANTITY;

            if (IsBorrowBookDuplicate(tableName, id)) // 이미 대여중인 도서면 대여 불가능
                return (int)Constant.CheckInsertBorrowedBook.DUPLICATE_BOOK_ID;

            MINUS_BOOK_QUANTITY(id); // 도서관에서 보유중인 도서수량 1개 뺴주고
            InsertBorrowedBook(tableName, id, bookName, bookPublisher, bookAuthor, bookPrice); // 사용자별 개별테이블에 대여도서 정보 넣기
            return (int)Constant.CheckInsertBorrowedBook.SUCCESS;
        }

        public void CreateTable(string tableName)
        {
            if (!connection.Ping())
                connection.Open();
            sqlString = string.Format(Constant.QUERY_STRING_CREATE_TABLE_BY_USER_ID, tableName);
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
            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
            connection.Close();
        }
    }
}
