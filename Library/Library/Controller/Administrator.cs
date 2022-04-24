using System;
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
        public void Login(AdministratorScreen administratorScreen) // id : admin1    pw: admin1
        {
            bool isLogin = false;
            string id = "", password = "";
            administratorScreen.PrintLoginScreen();
            while (!isLogin)
            {
                id = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.LOGIN_POS_X, Constant.LOGIN_ID_POS_Y, Constant.MAX_LENGTH_ID, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_ID);
                if (id == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 뒤로가기
                    break;
                password = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.LOGIN_POS_X, Constant.LOGIN_PASSWORD_POS_Y, Constant.MAX_LENGTH_PASSWORD, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_PASSWORD, Constant.IS_PASSWORD);
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

            administratorScreen.PrintMessage("ID & PASSWORD 가 틀립니다" , Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
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
            administratorScreen.PrintSelectedValues(DataBase.Instance.Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK);
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
            if (!isSearchBookCompleted)
                SelectMenu(administratorScreen);
        }

        private bool IsSearchBookCompleted(AdministratorScreen administratorScreen, string bookId, string bookName, string bookPublisher, string bookAuthor, string bookPrice, string bookQuantity)
        {
            int getYesOrNoBySearching, getYesOrNoByResearching;
            // ↓ 옵션입력시 모두 공백일경우 체크하는 조건문
            if ((bookId == "" || bookId == Constant.INPUT_ESCAPE.ToString()) && (bookName == "" || bookName == Constant.INPUT_ESCAPE.ToString()) && (bookPublisher == "" || bookPublisher == Constant.INPUT_ESCAPE.ToString()) && (bookAuthor == "" || bookAuthor == Constant.INPUT_ESCAPE.ToString()) && (bookPrice == "" || bookPrice == Constant.INPUT_ESCAPE.ToString()) && (bookQuantity == "" || bookQuantity == Constant.INPUT_ESCAPE.ToString()))
            {
                administratorScreen.PrintMessage(Constant.TEXT_PLEASE_INPUT_OPTION , Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.BookSearchPosY.ID); //좌표조정
                return false;
            }
            
            administratorScreen.PrintMessage(Constant.TEXT_IS_SEARCH , Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO , Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            getYesOrNoBySearching = DataProcessing.Instance.GetEnterOrEscape();

            if (getYesOrNoBySearching == Constant.INPUT_ENTER)
            {
                administratorScreen.PrintSearchResultScreen();
                administratorScreen.PrintSelectedValues(DataBase.Instance.Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK, DataProcessing.Instance.GetConditionalStringBySearchBook(bookId, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity)), Constant.TABLE_NAME_BOOK);
                Console.SetCursorPosition(0, 0); // 출력되는 자료가 많아서 화면이 내려갈 수 있어 최상단으로 커서 옮기기
                Console.CursorVisible = false;
                getYesOrNoByResearching = DataProcessing.Instance.GetEnterOrEscape();
                if (getYesOrNoByResearching == Constant.INPUT_ENTER)
                    InputBookSearchOption(administratorScreen);
                if (getYesOrNoByResearching == Constant.INPUT_ESCAPE)
                    SelectMenu(administratorScreen);
            }
            if (getYesOrNoBySearching == Constant.INPUT_ESCAPE)
            {
                administratorScreen.PrintBookSearchScreen();
                administratorScreen.PrintSelectedValues(DataBase.Instance.Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK);
                Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
                Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.BookSearchPosY.ID); //좌표조정
            }
            return true;
        }

        private void SelectMenu(AdministratorScreen administratorScreen)
        {
            int menuValue;
            menuValue = GetAddministratorMenu(administratorScreen, Constant.TEXT_ADMINISTRATOR_MODE);

            switch (menuValue)
            {
                case (int)Constant.AdministratorMenu.BOOK_SEARCH:
                    InputBookSearchOption(administratorScreen);
                    break;
                case (int)Constant.AdministratorMenu.BOOK_ADD:
                    SelectMenu(administratorScreen);
                    break;
                case (int)Constant.AdministratorMenu.BOOK_REMOVE:
                    SelectMenu(administratorScreen);
                    break;
                case (int)Constant.AdministratorMenu.BOOK_REVISE:
                    break;
                case (int)Constant.AdministratorMenu.MEMBER_MANAGEMENT:
                    SelectMenu(administratorScreen);
                    break;
                case (int)Constant.AdministratorMenu.BORROW_STATUS:
                    SelectMenu(administratorScreen);
                    break;
                case Constant.INPUT_ESCAPE_IN_ARROW_KEY:
                    Login(administratorScreen);
                    break;
                default:
                    break;
            }
        }
    }
}
