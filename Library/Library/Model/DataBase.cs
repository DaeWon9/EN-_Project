﻿using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Library.Model
{
    internal class DataBase
    {
        private static DataBase database;
        private string sqlString;
        MySqlConnection connection = new MySqlConnection(Constant.DATABASE_CONNECTION_INFORMATION);

        public static DataBase GetDataBase()
        {
            if (database == null)
                database = new DataBase();
            return database;
        }

        public MySqlDataReader Select(string filed, string tableName, string conditionalString = "", string orderByString = "") //분할해주는 게 좋을듯 구체적으로
        {
            if (!connection.Ping())
                connection.Open();

            if (conditionalString == "" && orderByString == "") // 조건문이 없는경우
                sqlString = string.Format(Constant.QUERY_STRING_SELECT, filed, tableName);
            if (conditionalString != "" && orderByString == "")
                sqlString = string.Format(Constant.QUERY_STRING_CONDITIONAL_SELECT, filed, tableName, conditionalString);
            if (conditionalString == "" && orderByString != "")
                sqlString = string.Format(Constant.QUERY_STRING_ORDER_BY_SELECT, filed, tableName, orderByString);

            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();
            command.Dispose();
            return reader; // 다른곳에서 reader 닫아주는중
        }

        public void CreateTable(string tableName)
        {
            if (!connection.Ping())
                connection.Open();
            sqlString = string.Format(Constant.QUERY_STRING_CREATE_TABLE_BY_USER_ID, tableName);
            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        public void Drop(string tableName)
        {
            if (!connection.Ping())
                connection.Open();

            sqlString = string.Format(Constant.QUERY_STRING_DROP, tableName);
            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
            command.Dispose();
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
            command.Dispose();
            connection.Close();
        }

        public void Update(string tableName, string setString, string conditionalString = "")//UPDATE 테이블이름 SET 필드이름1 = 데이터값1, 필드이름2 = 데이터값2, ... WHERE 필드이름 = 데이터값
        {
            if (!connection.Ping())
                connection.Open();

            if (conditionalString == "") // 조건문이 없는경우
                sqlString = string.Format(Constant.QUERY_STRING_UPDATE, tableName, setString);
            else
                sqlString = string.Format(Constant.QUERY_STRING_CONDITIONAL_UPDATE, tableName, setString, conditionalString);

            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
            command.Dispose();
            connection.Close(); //소멸자로 객체 삭제해주기
        }

        public string GetSelectedElement(string filed, string tableName, string conditionalString)
        {
            string selectedElement = "";

            if (!connection.Ping())
                connection.Open();

            if (conditionalString == "")
                sqlString = string.Format(Constant.QUERY_STRING_SELECT, filed, tableName);
            else
                sqlString = string.Format(Constant.QUERY_STRING_CONDITIONAL_SELECT, filed, tableName, conditionalString);


            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
                selectedElement = reader[string.Format("{0}", filed)].ToString();

            reader.Close();
            command.Dispose();
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
            command.Dispose();
            connection.Close();
            return selectedElements;
        }

        public List<string> GetAllTablesName()
        {
            List<string> selectedElements = new List<string>();

            if (!connection.Ping())
                connection.Open();

            sqlString = Constant.QUERY_STRING_GET_ALL_TABLES;

            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                selectedElements.Add(reader[string.Format("Tables_in_daewonLibrary")].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return selectedElements;

        }

        public void InsertMember(string tableName, string name, string id, string password, string birthDate, string address, string phonenumber)
        {
            if (!connection.Ping())
                connection.Open();
            sqlString = string.Format(Constant.QUERY_STRING_INSERT_MEMBER, tableName, name, id, password, birthDate, address, phonenumber);
            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
            command.Dispose();
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

        public bool IsRegisteredMemberId(string memberId)
        {
            List<string> memberIdList = GetSelectedElements(Constant.MEMBER_FILED_ID, Constant.TABLE_NAME_MEMBER);
            for (int repeat = 0; repeat < memberIdList.Count; repeat++)
            {
                if (memberIdList[repeat] == memberId)
                    return true;
            }
            return false;
        }

        public void MinusBookQuantity(int id)
        {
            if (!connection.Ping())
                connection.Open();
            sqlString = string.Format(Constant.QUERY_STRING_UPDATE_BOOK_QUANTITY_BY_BORROWED, id); // 도서관에 보관중인 책에서 개수 -1
            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        public void PlusBookQuantity(int id)
        {
            if (!connection.Ping())
                connection.Open();
            sqlString = string.Format(Constant.QUERY_STRING_UPDATE_BOOK_QUANTITY_BY_RETURN, id); // 도서관에 보관중인 책에서 개수 +1
            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        public void InsertBorrowedBook(string tableName, int bookId, string bookName, string bookPublisher, string bookAuthor, int bookPrice, string bookPublicationDate, string bookISBN)
        {
            DateTime borrowDate = DateTime.Now;
            DateTime returnDate = borrowDate.AddDays(7);

            if (!connection.Ping())
                connection.Open();
            sqlString = string.Format(Constant.QUERY_STRING_INSERT_BORROW_BOOK, tableName, bookId, bookName, bookPublisher, bookAuthor, bookPrice, /*bookQuantity*/1, bookPublicationDate, bookISBN, borrowDate.ToString("yyyy-MM-dd-HH-mm-ss"), returnDate.ToString("yyyy-MM-dd-HH-mm-ss"));
            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        public void InsertAddBook(string tableName, string bookName, string bookPublisher, string bookAuthor, int bookPrice, int bookQuantity, string bookPublicationDate, string bookISBN)
        {
            if (!connection.Ping())
                connection.Open();
            sqlString = string.Format(Constant.QUERY_STRING_INSERT_ADD_BOOK, tableName, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity, bookPublicationDate, bookISBN);
            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        public int GetBookBorrowResult(string tableName, int id, List<string> searchedBookIdList)
        {
            string bookName = "", bookPublisher = "", bookAuthor = "", bookPublicationDate = "", bookISBN = "";
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
                bookPublicationDate = string.Format("{0:yyyy-MM-dd}", reader[Constant.BOOK_FILED_PUBLICATION_DATE]);
                bookISBN = reader[Constant.BOOK_FILED_ISBN].ToString();
            }
            reader.Close();
            command.Dispose();
            connection.Close();

            if (!IsExistBookInLibrary(id, searchedBookIdList)) // 도서관에 없는책이면 대여 불가능
                return (int)Constant.CheckInsertBorrowedBook.NOT_EXIST_BOOK;

            if (bookQuantity < 1) // 도서관에 책 보유수량이 없으면 대여 불가능
                return (int)Constant.CheckInsertBorrowedBook.SHORTAGE_BOOK_QUANTITY;

            if (IsBorrowBookDuplicate(tableName, id)) // 이미 대여중인 도서면 대여 불가능
                return (int)Constant.CheckInsertBorrowedBook.DUPLICATE_BOOK_ID;

            MinusBookQuantity(id); // 도서관에서 보유중인 도서수량 1개 뺴주고
            InsertBorrowedBook(tableName, id, bookName, bookPublisher, bookAuthor, bookPrice, bookPublicationDate, bookISBN); // 사용자별 개별테이블에 대여도서 정보 넣기
            return (int)Constant.CheckInsertBorrowedBook.SUCCESS;
        }
    
        public void AddLog(string member, string activity) // 로그를 db에 추가
        {
            if (!connection.Ping())
                connection.Open();
            
            sqlString = string.Format(Constant.QUERY_STRING_INSERT_LOG, member, activity);
            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();
            
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        public void ResetLog()
        {
            if (!connection.Ping())
                connection.Open();

            sqlString = Constant.QUERY_STRING_LOG_RESET;

            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();

            reader.Close();
            command.Dispose();
            connection.Close();
        }

        public MySqlDataReader GetLog(string conditionalString)
        {
            if (!connection.Ping())
                connection.Open();

            if (conditionalString == "") // 조건문이 없는경우
                sqlString = string.Format(Constant.QUERY_STRING_SELECT, Constant.FILED_ALL, Constant.TABLE_NAME_LOG);
            else
                sqlString = string.Format(Constant.QUERY_STRING_CONDITIONAL_SELECT, Constant.FILED_ALL, Constant.TABLE_NAME_LOG, conditionalString);

            MySqlCommand command = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = command.ExecuteReader();
            command.Dispose();
            return reader; // 다른곳에서 reader 닫아주는중
        }


    }
}
