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
    class AdministratorFuntions : MenuSelection
    {
        private bool isInputEscape = false;
        public void Login(AdministratorScreen administratorScreen) // id : admin1    pw: admin1
        {
            bool isLogin = false;
            string id = "", password = "";
            administratorScreen.PrintLoginScreen(Constant.IS_CONSOLE_CLEAR);
            while (!isLogin)
            {
                id = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.LOGIN_POS_X, Constant.LOGIN_ID_POS_Y, Constant.MAX_LENGTH_ID, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_ID);
                if (id == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 뒤로가기
                    break;
                password = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.LOGIN_POS_X, Constant.LOGIN_PASSWORD_POS_Y, Constant.MAX_LENGTH_PASSWORD, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_PASSWORD, Constant.IS_PASSWORD);
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

            administratorScreen.PrintMessage("ID & PASSWORD 가 틀립니다", Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, Constant.IS_NOT_CONSOLE_CLEAR, ConsoleColor.Red);
            DataProcessing.Instance.ClearConsoleLine(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_MAX_POS_X, Constant.LOGIN_ID_POS_Y);
            DataProcessing.Instance.ClearConsoleLine(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_MAX_POS_X, Constant.LOGIN_PASSWORD_POS_Y);
            return false;
        }


        private bool IsInputEscape(string stringValue)
        {
            if (stringValue == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString())
                return true;
            return false;
        }

        private void InputBookSearchOption(AdministratorScreen administratorScreen)
        {
            string bookId = "", bookName = "", bookPublisher = "", bookAuthor = "", bookPrice = "", bookQuantity = "";
            int currentConsoleCursorPosY;
            bool isSearchCompleted = false;
            isInputEscape = false;
            Console.CursorVisible = true;

            administratorScreen.PrintBookSearchScreen(Constant.IS_CONSOLE_CLEAR);
            administratorScreen.PrintSelectedValues(DataBase.Instance.Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK);
            Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.BookSearchPosY.ID); //좌표조정

            while (!isInputEscape && !isSearchCompleted)
            {
                currentConsoleCursorPosY = DataProcessing.Instance.CursorMove(Constant.SEARCH_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.BookSearchPosY.ID, (int)Constant.BookSearchPosY.SEARCH);
                isInputEscape = IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.BookSearchPosY.ID:
                        bookId = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.ID, Constant.MAX_LENGTH_BOOK_ID, Constant.EXCEPTION_TYPE_NUMBER, "숫자만 입력하세요", Constant.EXCEPTION_TYPE_BOOK_ID);
                        break;
                    case (int)Constant.BookSearchPosY.NAME:
                        bookName = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.NAME, Constant.MAX_LENGTH_BOOK_NAME, Constant.EXCEPTION_TYPE_ANY, "", Constant.EXCEPTION_TYPE_BOOK_NAME);
                        break;
                    case (int)Constant.BookSearchPosY.PUBLISHER:
                        bookPublisher = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.PUBLISHER, Constant.MAX_LENGTH_BOOK_PUBLISHER, Constant.EXCEPTION_TYPE_ANY, "", Constant.EXCEPTION_TYPE_BOOK_PUBLISHER);
                        break;
                    case (int)Constant.BookSearchPosY.AUTHOR:
                        bookAuthor = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.AUTHOR, Constant.MAX_LENGTH_BOOK_AUTHOR, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER_KOREA, "", Constant.EXCEPTION_TYPE_BOOK_AUTHOR);
                        break;
                    case (int)Constant.BookSearchPosY.PRICE:
                        bookPrice = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.PRICE, Constant.MAX_LENGTH_BOOK_PRICE, Constant.EXCEPTION_TYPE_NUMBER, "숫자만 입력하세요", Constant.EXCEPTION_TYPE_BOOK_PRICE);
                        break;
                    case (int)Constant.BookSearchPosY.QUANTITY:
                        bookQuantity = DataProcessing.Instance.GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.QUANTITY, Constant.MAX_LENGTH_BOOK_QUANTITY, Constant.EXCEPTION_TYPE_NUMBER, "숫자만 입력하세요", Constant.EXCEPTION_TYPE_BOOK_QUANTITY);
                        break;
                    case (int)Constant.BookSearchPosY.SEARCH:
                        SearchBook(administratorScreen, bookId, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity);
                        isSearchCompleted = true;
                        break;
                    default:
                        break;

                }
            }
            if (!isSearchCompleted)
                SelectMenu(administratorScreen);
        }

        private string GetConditionalStringBySelectBook(string bookId, string bookName, string bookPublisher, string bookAuthor, string bookPrice, string bookQuantity)
        {
            string conditionalString = ""; // 아래쪽 부분 기능완성 후 Constant로 빼기

            if (bookId != "" && bookId != Constant.INPUT_ESCAPE.ToString())
                if (conditionalString == "")
                    conditionalString += Constant.BOOK_FILED_ID + "=" + bookId;
                else
                {
                    conditionalString += " AND ";
                    conditionalString += Constant.BOOK_FILED_ID + "=" + bookId;
                }
            if (bookName != "" && bookName != Constant.INPUT_ESCAPE.ToString())
                if (conditionalString == "")
                {
                    conditionalString += "(" + Constant.BOOK_FILED_NAME + " LIKE " + "'" + bookName + "%' OR ";
                    conditionalString += Constant.BOOK_FILED_NAME + " LIKE " + "'%" + bookName + "' OR ";
                    conditionalString += Constant.BOOK_FILED_NAME + " LIKE " + "'%" + bookName + "%')";
                }
                else
                {
                    conditionalString += " AND ";
                    conditionalString += "(" + Constant.BOOK_FILED_NAME + " LIKE " + "'" + bookName + "%' OR ";
                    conditionalString += Constant.BOOK_FILED_NAME + " LIKE " + "'%" + bookName + "' OR ";
                    conditionalString += Constant.BOOK_FILED_NAME + " LIKE " + "'%" + bookName + "%')";
                }
            if (bookPublisher != "" && bookPublisher != Constant.INPUT_ESCAPE.ToString())
                if (conditionalString == "")
                {
                    conditionalString += "(" + Constant.BOOK_FILED_PUBLISHER + " LIKE " + "'" + bookPublisher + "%' OR ";
                    conditionalString += Constant.BOOK_FILED_PUBLISHER + " LIKE " + "'%" + bookPublisher + "' OR ";
                    conditionalString += Constant.BOOK_FILED_PUBLISHER + " LIKE " + "'%" + bookPublisher + "%')";
                }
                else
                {
                    conditionalString += " AND ";
                    conditionalString += "(" + Constant.BOOK_FILED_PUBLISHER + " LIKE " + "'" + bookPublisher + "%' OR ";
                    conditionalString += Constant.BOOK_FILED_PUBLISHER + " LIKE " + "'%" + bookPublisher + "' OR ";
                    conditionalString += Constant.BOOK_FILED_PUBLISHER + " LIKE " + "'%" + bookPublisher + "%')";
                }
            if (bookAuthor != "" && bookAuthor != Constant.INPUT_ESCAPE.ToString())
                if (conditionalString == "")
                {
                    conditionalString += "(" + Constant.BOOK_FILED_AUTHOR + " LIKE " + "'" + bookAuthor + "%' OR ";
                    conditionalString += Constant.BOOK_FILED_AUTHOR + " LIKE " + "'%" + bookAuthor + "' OR ";
                    conditionalString += Constant.BOOK_FILED_AUTHOR + " LIKE " + "'%" + bookAuthor + "%')";
                }
                else
                {
                    conditionalString += " AND ";
                    conditionalString += "(" + Constant.BOOK_FILED_AUTHOR + " LIKE " + "'" + bookAuthor + "%' OR ";
                    conditionalString += Constant.BOOK_FILED_AUTHOR + " LIKE " + "'%" + bookAuthor + "' OR ";
                    conditionalString += Constant.BOOK_FILED_AUTHOR + " LIKE " + "'%" + bookAuthor + "%')";
                }
            if (bookPrice != "" && bookPrice != Constant.INPUT_ESCAPE.ToString())
                if (conditionalString == "")
                    conditionalString += Constant.BOOK_FILED_PRICE + "=" + bookPrice;
                else
                {
                    conditionalString += " AND ";
                    conditionalString += Constant.BOOK_FILED_PRICE + "=" + bookPrice;
                }
            if (bookQuantity != "" && bookQuantity != Constant.INPUT_ESCAPE.ToString())
                if (conditionalString == "")
                    conditionalString += Constant.BOOK_FILED_QUANTITY + "=" + bookQuantity;
                else
                {
                    conditionalString += " AND ";
                    conditionalString += Constant.BOOK_FILED_QUANTITY + "=" + bookQuantity;
                }

            return conditionalString;
        }

        private void SearchBook(AdministratorScreen administratorScreen, string bookId, string bookName, string bookPublisher, string bookAuthor, string bookPrice, string bookQuantity)
        {
            int GetYesOrNoBySearching, GetYesOrNoByResearching; // 아래 문자열 기능완성 후 constant로 빼기
            administratorScreen.PrintMessage("검색하시겠습니까??", Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, Constant.IS_NOT_CONSOLE_CLEAR, ConsoleColor.Green);
            administratorScreen.PrintMessage("< YES : ENTER | NO : ESC >", Constant.YES_OR_NO_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, Constant.IS_NOT_CONSOLE_CLEAR, ConsoleColor.Green);
            GetYesOrNoBySearching = DataProcessing.Instance.GetEnterOrEscape();

            if (GetYesOrNoBySearching == Constant.INPUT_ENTER)
            {
                administratorScreen.PrintSearchResultScreen(Constant.IS_CONSOLE_CLEAR);
                administratorScreen.PrintSelectedValues(DataBase.Instance.Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK, GetConditionalStringBySelectBook(bookId, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity)), Constant.TABLE_NAME_BOOK);
                Console.SetCursorPosition(0, 0); // 출력되는 자료가 많아서 화면이 내려갈 수 있어 최상단으로 커서 옮기기
                Console.CursorVisible = false;
                GetYesOrNoByResearching = DataProcessing.Instance.GetEnterOrEscape();
                if (GetYesOrNoByResearching == Constant.INPUT_ENTER)
                    InputBookSearchOption(administratorScreen);
                if (GetYesOrNoByResearching == Constant.INPUT_ESCAPE)
                    SelectMenu(administratorScreen);
            }
            if (GetYesOrNoBySearching == Constant.INPUT_ESCAPE)
            {
                administratorScreen.PrintBookSearchScreen(Constant.IS_CONSOLE_CLEAR);
                administratorScreen.PrintSelectedValues(DataBase.Instance.Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK);
                Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
                Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.BookSearchPosY.ID); //좌표조정
            }
        }

        private void SelectMenu(AdministratorScreen administratorScreen)
        {
            int menuValue;
            menuValue = AddministratorMenuSelect(administratorScreen);

            switch (menuValue)
            {
                case (int)Constant.AdministratorMenu.BOOK_SEARCH://책이름이 ㄱㄴㄷㄹ. 이런식으로 시작할수도 있을거임 이거 나중에 처리해주기.
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
                case (int)Constant.AdministratorMenu.RENTAL_STATUS:
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
