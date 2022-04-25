﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Library.Utility;
using Library.Model;
using Library.View;

namespace Library.Controller
{
    class Administrator : MenuSelection
    {
        private bool isInputEscape = false;
        private bool isBookDeleteCompleted = false;
        private int modificateBookId = 0;
        public void Login(AdministratorScreen administratorScreen) // id : admin1    pw: admin1
        {
            bool isLogin = false;
            string id = "", password = "";
            administratorScreen.PrintLoginScreen();
            while (!isLogin)
            {
                id = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.LOGIN_POS_X, Constant.LOGIN_ID_POS_Y, Constant.MAX_LENGTH_MEMBER_ID, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_ID);
                if (id == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 뒤로가기
                    break;
                password = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.LOGIN_POS_X, Constant.LOGIN_PASSWORD_POS_Y, Constant.MAX_LENGTH_MEMBER_PASSWORD, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_PASSWORD, Constant.IS_PASSWORD);
                if (password == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 뒤로가기
                    break;
                isLogin = CheckLogin(administratorScreen, id, password);
            }
            if (id == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString() || password == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 처음 회원 or 관리자모드 선택으로 돌아가야함
                return;
            SelectMenu(administratorScreen);
        }

        private bool CheckLogin(AdministratorScreen administratorScreen, string id, string password)
        {
            List<string> administratorId = DataBase.Instance.GetSelectedElements(Constant.ADMINISTRATOR_FILED_ID, Constant.TABLE_NAME_ADMINISTRATOR);
            List<string> administratorPassword = DataBase.Instance.GetSelectedElements(Constant.ADMINISTRATOR_FILED_PASSWORD, Constant.TABLE_NAME_ADMINISTRATOR);

            for (int repeat = 0; repeat < administratorId.Count; repeat++)
            {
                if (id == administratorId[repeat] && password == administratorPassword[repeat])
                    return true;
            }

            administratorScreen.PrintMessage("ID & PASSWORD 가 틀립니다", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
            DataProcessing.Instance.ClearConsoleLine(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_MAX_POS_X, Constant.LOGIN_ID_POS_Y);
            DataProcessing.Instance.ClearConsoleLine(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_MAX_POS_X, Constant.LOGIN_PASSWORD_POS_Y);
            return false;
        }

        private void InputBookSearchOption(AdministratorScreen administratorScreen)
        {
            string bookId = "", bookName = "", bookPublisher = "", bookAuthor = "", bookPrice = "", bookQuantity = "";
            int currentConsoleCursorPosY;
            bool isSearchBookCompleted = false;
            isInputEscape = false;
            Console.CursorVisible = true;

            administratorScreen.PrintBookSearchScreen();
            administratorScreen.PrintSelectedValues(DataBase.Instance.Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
            Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.BookSearchPosY.ID); //좌표조정

            while (!isInputEscape && !isSearchBookCompleted)
            {
                currentConsoleCursorPosY = DataProcessing.Instance.CursorMove(Constant.SEARCH_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.BookSearchPosY.ID, (int)Constant.BookSearchPosY.SEARCH);
                isInputEscape = DataProcessing.Instance.IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.BookSearchPosY.ID:
                        bookId = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.ID, Constant.MAX_LENGTH_BOOK_ID, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_ID);
                        break;
                    case (int)Constant.BookSearchPosY.NAME:
                        bookName = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.NAME, Constant.MAX_LENGTH_BOOK_NAME, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_NAME);
                        break;
                    case (int)Constant.BookSearchPosY.PUBLISHER:
                        bookPublisher = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.PUBLISHER, Constant.MAX_LENGTH_BOOK_PUBLISHER, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_PUBLISHER);
                        break;
                    case (int)Constant.BookSearchPosY.AUTHOR:
                        bookAuthor = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.AUTHOR, Constant.MAX_LENGTH_BOOK_AUTHOR, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER_KOREA, Constant.EXCEPTION_TYPE_BOOK_AUTHOR);
                        break;
                    case (int)Constant.BookSearchPosY.PRICE:
                        bookPrice = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.PRICE, Constant.MAX_LENGTH_BOOK_PRICE, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_PRICE);
                        break;
                    case (int)Constant.BookSearchPosY.QUANTITY:
                        bookQuantity = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.QUANTITY, Constant.MAX_LENGTH_BOOK_QUANTITY, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_QUANTITY);
                        break;
                    case (int)Constant.BookSearchPosY.SEARCH:
                        isSearchBookCompleted = IsSearchBookCompleted(administratorScreen, bookId, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity);
                        break;
                    default:
                        break;
                }
            }
        }

        private bool IsSearchBookCompleted(AdministratorScreen administratorScreen, string bookId, string bookName, string bookPublisher, string bookAuthor, string bookPrice, string bookQuantity)
        {
            int getYesOrNoBySearching, getYesOrNoByResearching;
            // ↓ 옵션입력시 모두 공백일경우 체크하는 조건문
            if ((bookId == "" || bookId == Constant.INPUT_ESCAPE.ToString()) && (bookName == "" || bookName == Constant.INPUT_ESCAPE.ToString()) && (bookPublisher == "" || bookPublisher == Constant.INPUT_ESCAPE.ToString()) && (bookAuthor == "" || bookAuthor == Constant.INPUT_ESCAPE.ToString()) && (bookPrice == "" || bookPrice == Constant.INPUT_ESCAPE.ToString()) && (bookQuantity == "" || bookQuantity == Constant.INPUT_ESCAPE.ToString()))
            {
                administratorScreen.PrintMessage(Constant.TEXT_PLEASE_INPUT_OPTION, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.BookSearchPosY.ID); //좌표조정
                return false;
            }

            administratorScreen.PrintMessage(Constant.TEXT_IS_SEARCH, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            getYesOrNoBySearching = DataProcessing.Instance.GetEnterOrEscape();

            if (getYesOrNoBySearching == Constant.INPUT_ENTER)
            {
                administratorScreen.PrintSearchResultScreen();
                administratorScreen.PrintSelectedValues(DataBase.Instance.Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK, DataProcessing.Instance.GetConditionalStringBySearchBook(bookId, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity)), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
                Console.SetCursorPosition(0, 0); // 출력되는 자료가 많아서 화면이 내려갈 수 있어 최상단으로 커서 옮기기
                Console.CursorVisible = false;
                getYesOrNoByResearching = DataProcessing.Instance.GetEnterOrEscape();
                if (getYesOrNoByResearching == Constant.INPUT_ENTER)
                    InputBookSearchOption(administratorScreen);
            }
            if (getYesOrNoBySearching == Constant.INPUT_ESCAPE)
            {
                administratorScreen.PrintBookSearchScreen();
                administratorScreen.PrintSelectedValues(DataBase.Instance.Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
                Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
                Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.BookSearchPosY.ID); //좌표조정
            }
            return true;
        }
  
        private bool IsAlreadyRegisteredBookInLibrary(string bookId)
        {
            List<string> memberIdList = DataBase.Instance.GetSelectedElements(Constant.BOOK_FILED_ID, Constant.TABLE_NAME_BOOK);
            for (int repeat = 0; repeat < memberIdList.Count; repeat++)
            {
                if (memberIdList[repeat] == bookId)
                    return true;
            }
            return false;
        }

        private bool IsAddBookCompleted(AdministratorScreen administratorScreen, string bookId, string bookName, string bookPublisher, string bookAuthor, string bookPrice, string bookQuantity)
        {
            int GetYesOrNoByAdd, GetYesOrNoByReAdd;
            // 모든 값이 입력됐는지 체크
            if ((bookId == "" || bookId == Constant.INPUT_ESCAPE.ToString()) || (bookName == "" || bookName == Constant.INPUT_ESCAPE.ToString()) || (bookPublisher == "" || bookPublisher == Constant.INPUT_ESCAPE.ToString()) || (bookAuthor == "" || bookAuthor == Constant.INPUT_ESCAPE.ToString()) || (bookPrice == "" || bookPrice == Constant.INPUT_ESCAPE.ToString()) || (bookQuantity == "" || bookQuantity == Constant.INPUT_ESCAPE.ToString()))
            {
                administratorScreen.PrintMessage(Constant.TEXT_PLEASE_INPUT_OPTION, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.ADD_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookAddPosY.ID); //좌표조정
                return false;
            }
            administratorScreen.PrintMessage(Constant.TEXT_IS_ADD, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            GetYesOrNoByAdd = DataProcessing.Instance.GetEnterOrEscape();

            if (GetYesOrNoByAdd == Constant.INPUT_ESCAPE) // 추가하시겠습니까?? -> ESC
            {
                DataProcessing.Instance.ClearErrorMessage();
                Console.SetCursorPosition(Constant.ADD_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookAddPosY.ID); //좌표조정
                return false;
            }
            if (GetYesOrNoByAdd == Constant.INPUT_ENTER) // 추가하시겠습니까?? -> ENTER
            {
                DataBase.Instance.InsertAddBook(Constant.TABLE_NAME_BOOK, int.Parse(bookId), bookName, bookPublisher, bookAuthor, int.Parse(bookPrice), int.Parse(bookQuantity));
                Console.CursorVisible = false;
                administratorScreen.PrintMessage(Constant.TEXT_SUCCESS_ADD_BOOK, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
                administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);

                GetYesOrNoByReAdd = DataProcessing.Instance.GetEnterOrEscape();
                if (GetYesOrNoByReAdd == Constant.INPUT_ENTER) // 계속해서 추가할것임.
                    AddBook(administratorScreen);
            }
            return true;
        }

        private void AddBook(AdministratorScreen administratorScreen)
        {
            string bookId = "", bookName = "", bookPublisher = "", bookAuthor = "", bookPrice = "", bookQuantity = "";
            int currentConsoleCursorPosY;
            bool isAddBookCompleted = false;
            isInputEscape = false;
            Console.CursorVisible = true;

            administratorScreen.PrintAddBookScreen();
            administratorScreen.PrintSelectedValues(DataBase.Instance.Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
            Console.SetCursorPosition(0, 0);      //입력창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.ADD_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookAddPosY.ID); //좌표조정

            while (!isInputEscape && !isAddBookCompleted)
            {
                if (IsAlreadyRegisteredBookInLibrary(bookId)) // 이미 등록되어있는 책
                {
                    administratorScreen.PrintMessage(Constant.TEXT_ALREADY_REGISTERED_BOOK, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    Console.SetCursorPosition(Constant.ADD_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookAddPosY.ID); //좌표조정
                    DataProcessing.Instance.ClearConsoleLine(Constant.ADD_BOOK_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.BookAddPosY.ID);
                    bookId = "";
                }

                currentConsoleCursorPosY = DataProcessing.Instance.CursorMove(Constant.ADD_BOOK_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.BookAddPosY.ID, (int)Constant.BookSearchPosY.SEARCH);
                isInputEscape = DataProcessing.Instance.IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.BookAddPosY.ID:
                        bookId = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.ID, Constant.MAX_LENGTH_BOOK_ID, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_ID);
                        break;
                    case (int)Constant.BookAddPosY.NAME:
                        bookName = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.NAME, Constant.MAX_LENGTH_BOOK_NAME, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_NAME);
                        break;
                    case (int)Constant.BookAddPosY.PUBLISHER:
                        bookPublisher = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.PUBLISHER, Constant.MAX_LENGTH_BOOK_PUBLISHER, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_PUBLISHER);
                        break;
                    case (int)Constant.BookAddPosY.AUTHOR:
                        bookAuthor = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.AUTHOR, Constant.MAX_LENGTH_BOOK_AUTHOR, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER_KOREA, Constant.EXCEPTION_TYPE_BOOK_AUTHOR);
                        break;
                    case (int)Constant.BookAddPosY.PRICE:
                        bookPrice = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.PRICE, Constant.MAX_LENGTH_BOOK_PRICE, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_PRICE);
                        break;
                    case (int)Constant.BookAddPosY.QUANTITY:
                        bookQuantity = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.QUANTITY, Constant.MAX_LENGTH_BOOK_QUANTITY, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_QUANTITY);
                        break;
                    case (int)Constant.BookAddPosY.ADD:
                        isAddBookCompleted = IsAddBookCompleted(administratorScreen, bookId, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity);
                        break;
                    default:
                        break;
                }
            }
        }

        private void ManagementMember(AdministratorScreen administratorScreen)
        {
            isInputEscape = false;
            administratorScreen.PrintManagementMemberScreen();
            administratorScreen.PrintSelectedValues(DataBase.Instance.Select(Constant.FILED_ALL, Constant.TABLE_NAME_MEMBER), Constant.TABLE_NAME_MEMBER, Constant.TEXT_NONE); 
            Console.CursorVisible = false;
            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.CURSOR_POS_TOP);
            while (!isInputEscape)
            {
                isInputEscape = DataProcessing.Instance.IsOnlyInputEscape();
                if (isInputEscape) //esc 눌렀을때 뒤로가기
                    Console.CursorVisible = true;
            }
        }

        private void BorrowBookStatus(AdministratorScreen administratorScreen)
        {
            isInputEscape = false;
            string memberName = "";
            List<string> AllTablesName = DataBase.Instance.GetAllTablesName();

            administratorScreen.PrintAdministratorCheckBorrowedBookLabel();
            foreach (string tableName in AllTablesName)
            {
                memberName = DataBase.Instance.GetSelectedElement(Constant.MEMBER_FILED_NAME, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, tableName));
                if (tableName != Constant.TABLE_NAME_ADMINISTRATOR && tableName != Constant.TABLE_NAME_MEMBER && tableName != Constant.TABLE_NAME_BOOK)
                {
                    administratorScreen.PrintSelectedValues(DataBase.Instance.Select(Constant.FILED_ALL, tableName), tableName, memberName, Constant.IS_ADMINISTRATOR_MODE);
                }
            }
            Console.CursorVisible = false;
            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.CURSOR_POS_TOP);
            while (!isInputEscape)
            {
                isInputEscape = DataProcessing.Instance.IsOnlyInputEscape();
                if (isInputEscape) //esc 눌렀을때 뒤로가기
                    Console.CursorVisible = true;
            }
        }

        private string GetStringByUpdate(string setStringForm, string filed, string inputValue)
        {
            string resultString = "";
            if (inputValue != "" && inputValue != Constant.INPUT_ESCAPE.ToString())
                resultString = string.Format(setStringForm, filed, inputValue);
            return resultString;
        }

        private bool IsModificateBookInformationCompleted(AdministratorScreen administratorScreen, string setString, int bookId)
        {
            int getYesOrNoByModificate;
            administratorScreen.PrintMessage(Constant.TEXT_IS_MODIFICATE, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            getYesOrNoByModificate = DataProcessing.Instance.GetEnterOrEscape();
            if (getYesOrNoByModificate == Constant.INPUT_ENTER) // 변경하시겠습니까? 에서 enter입력
            {
                DataBase.Instance.Update(Constant.TABLE_NAME_BOOK, setString, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_INT, Constant.BOOK_FILED_ID, bookId));
                return true;
            }
            if (getYesOrNoByModificate == Constant.INPUT_ESCAPE) // 변경하시겠습니까? 에서 esc입력
            {
                return false;
            }
            return true;
        }

        private bool IsReModificateByBook(AdministratorScreen administratorScreen)
        {
            int getYesOrNoByReModificate;
            administratorScreen.PrintMessage(Constant.TEXT_SUCCESS_MODFICATE, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            getYesOrNoByReModificate = DataProcessing.Instance.GetEnterOrEscape();
            if (getYesOrNoByReModificate == Constant.INPUT_ESCAPE) // 계속해서 변경 x
                return false;
            return true;
        }

        private void ModificateBook(AdministratorScreen administratorScreen) //  BookId 는 primaryKey 이므로 수정불가능하게 설정
        {
            string setStringByUpdate = "";
            string bookName = "", bookPublisher = "", bookAuthor = "", bookPrice = "", bookQuantity = "";
            int currentConsoleCursorPosY;
            bool isModificateCompleted = false, isReModificateByBook = false;
            isBookDeleteCompleted = false;
            isInputEscape = false;

            administratorScreen.PrintModificateBookLabel();
            administratorScreen.PrintSelectedValues(DataBase.Instance.Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK, String.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_INT, Constant.BOOK_FILED_ID, modificateBookId)), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
            administratorScreen.PrintModificateBookScreen();

            Console.SetCursorPosition(Constant.MODIFICATE_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookModificatePosY.NAME); //좌표조정

            while (!isInputEscape && !isModificateCompleted && !isBookDeleteCompleted)
            {
                currentConsoleCursorPosY = DataProcessing.Instance.CursorMove(Constant.MODIFICATE_BOOK_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.BookModificatePosY.NAME, (int)Constant.BookModificatePosY.DELETE);
                isInputEscape = DataProcessing.Instance.IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.BookModificatePosY.NAME:
                        bookName = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.MODIFICATE_BOOK_INPUT_POS_X, (int)Constant.BookModificatePosY.NAME, Constant.MAX_LENGTH_BOOK_NAME, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_NAME);
                        setStringByUpdate = GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.BOOK_FILED_NAME, bookName);
                        break;
                    case (int)Constant.BookModificatePosY.PUBLISHER:
                        bookPublisher = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.MODIFICATE_BOOK_INPUT_POS_X, (int)Constant.BookModificatePosY.PUBLISHER, Constant.MAX_LENGTH_BOOK_PUBLISHER, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_PUBLISHER);
                        setStringByUpdate = GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.BOOK_FILED_PUBLISHER, bookPublisher);
                        break;
                    case (int)Constant.BookModificatePosY.AUTHOR:
                        bookAuthor = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.MODIFICATE_BOOK_INPUT_POS_X, (int)Constant.BookModificatePosY.AUTHOR, Constant.MAX_LENGTH_BOOK_AUTHOR, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER_KOREA, Constant.EXCEPTION_TYPE_BOOK_AUTHOR);
                        setStringByUpdate = GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.BOOK_FILED_AUTHOR, bookAuthor);
                        break;
                    case (int)Constant.BookModificatePosY.PRICE:
                        bookPrice = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.MODIFICATE_BOOK_INPUT_POS_X, (int)Constant.BookModificatePosY.PRICE, Constant.MAX_LENGTH_BOOK_PRICE, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_PRICE);
                        setStringByUpdate = GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.BOOK_FILED_PRICE, bookPrice);
                        break;
                    case (int)Constant.BookModificatePosY.QUANTITY:
                        bookQuantity = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.MODIFICATE_BOOK_INPUT_POS_X, (int)Constant.BookModificatePosY.QUANTITY, Constant.MAX_LENGTH_BOOK_QUANTITY, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_QUANTITY);
                        setStringByUpdate = GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.BOOK_FILED_QUANTITY, bookQuantity);
                        break;
                    case (int)Constant.BookModificatePosY.DELETE:
                        //isBookDeleteCompleted = IsWithdrawlCompleted(memberScreen);
                        break;
                    default:
                        break;
                }

                if (setStringByUpdate != "") // 수정사항이 있다면
                {
                    isModificateCompleted = IsModificateBookInformationCompleted(administratorScreen, setStringByUpdate, modificateBookId);
                    isReModificateByBook = IsReModificateByBook(administratorScreen);
                    if (isReModificateByBook) // 계속해서 변경
                        ModificateBook(administratorScreen);
                }
            }
        }

        private void SelectModificateBookId(AdministratorScreen administratorScreen)
        {
            string bookId = "";
            int currentConsoleCursorPosY, getYesOrNoByModificate;
            bool isSelectBookIdCompleted = false;
            isInputEscape = false;

            administratorScreen.PrintSelectModificateBookScreen();
            administratorScreen.PrintSelectedValues(DataBase.Instance.Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
            Console.SetCursorPosition(0, 0);      //입력창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.SELECT_MODIFICATE_BOOK_ID_OPTION_POS_X, (int)Constant.SelectBookIdPosY.ID); //좌표조정

            while (!isInputEscape && !isSelectBookIdCompleted)
            {
                if (bookId != "" && !IsAlreadyRegisteredBookInLibrary(bookId))// 책이름이 입력됐는데, 도서관에 없는책임
                {
                    administratorScreen.PrintMessage(Constant.TEXT_IS_NOT_EXIST_IN_LIBRARY, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    Console.SetCursorPosition(Constant.SELECT_MODIFICATE_BOOK_ID_OPTION_POS_X, (int)Constant.SelectBookIdPosY.ID); //좌표조정
                    DataProcessing.Instance.ClearConsoleLine(Constant.SELECT_MODIFICATE_BOOK_ID_POS_X, Constant.WINDOW_WIDTH, (int)Constant.SelectBookIdPosY.ID);
                    bookId = "";
                }
                currentConsoleCursorPosY = DataProcessing.Instance.CursorMove(Constant.SELECT_MODIFICATE_BOOK_ID_OPTION_POS_X, Console.CursorTop, (int)Constant.SelectBookIdPosY.ID, (int)Constant.SelectBookIdPosY.MODIFICATE_BOOK);
                isInputEscape = DataProcessing.Instance.IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.SelectBookIdPosY.ID:
                        bookId = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.SELECT_MODIFICATE_BOOK_ID_POS_X, (int)Constant.SelectBookIdPosY.ID, Constant.MAX_LENGTH_BOOK_ID, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_ID);
                        break;
                    case (int)Constant.SelectBookIdPosY.MODIFICATE_BOOK:
                        if (bookId != "" && bookId != Constant.INPUT_ESCAPE.ToString())
                        {
                            administratorScreen.PrintMessage(Constant.TEXT_IS_MODIFICATE, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
                            administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
                            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y); //좌표조정
                            getYesOrNoByModificate = DataProcessing.Instance.GetEnterOrEscape();
                            if (getYesOrNoByModificate == Constant.INPUT_ENTER)
                            {
                                modificateBookId = int.Parse(bookId);
                                isSelectBookIdCompleted = true;
                            }
                            if (getYesOrNoByModificate == Constant.INPUT_ESCAPE)
                            {
                                DataProcessing.Instance.ClearErrorMessage();
                                Console.SetCursorPosition(Constant.SELECT_MODIFICATE_BOOK_ID_OPTION_POS_X, (int)Constant.SelectBookIdPosY.ID); //좌표조정
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            if (!isInputEscape)
                ModificateBook(administratorScreen);

        }

        private void SelectMenu(AdministratorScreen administratorScreen)
        {
            bool isLogout = false;
            int menuValue;
            while (!isLogout)
            {
                menuValue = GetAddministratorMenu(administratorScreen, Constant.TEXT_ADMINISTRATOR_MODE);
                switch (menuValue)
                {
                    case (int)Constant.AdministratorMenu.BOOK_SEARCH:
                        InputBookSearchOption(administratorScreen);
                        break;
                    case (int)Constant.AdministratorMenu.BOOK_ADD:
                        AddBook(administratorScreen);
                        break;
                    case (int)Constant.AdministratorMenu.BOOK_MODIFICATE:
                        SelectModificateBookId(administratorScreen);
                        break;
                    case (int)Constant.AdministratorMenu.MEMBER_MANAGEMENT:
                        ManagementMember(administratorScreen);
                        break;
                    case (int)Constant.AdministratorMenu.BORROW_BOOK_STATUS:
                        BorrowBookStatus(administratorScreen);
                        break;
                    case Constant.INPUT_ESCAPE_IN_ARROW_KEY:
                        isLogout = DataProcessing.Instance.IsLogout(administratorScreen);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
