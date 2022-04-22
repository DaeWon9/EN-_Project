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
    class AdministratorFuntions
    {
        private bool isInputEscape = false;
        public void Login(AdministratorScreen administratorScreen, BothScreen bothScreen, Message message, DataProcessing dataProcessing, MenuSelection menuSelection) // id : admin1    pw: admin1
        {
            bool isLogin = false;
            string id = "", password = "";
            administratorScreen.PrintLoginScreen(Constant.IS_CONSOLE_CLEAR);
            while (!isLogin)
            {
                id = dataProcessing.GetInputValues(message, Constant.LOGIN_POS_X, Constant.LOGIN_ID_POS_Y, Constant.MAX_LENGTH_ID, Constant.IS_NOT_PASSWORD, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_ID);
                if (id == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 뒤로가기
                    break;
                password = dataProcessing.GetInputValues(message, Constant.LOGIN_POS_X, Constant.LOGIN_PASSWORD_POS_Y, Constant.MAX_LENGTH_PASSWORD, Constant.IS_PASSWORD, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_PASSWORD);
                if (password == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 뒤로가기
                    break;
                isLogin = CheckLogin(message, dataProcessing, id, password);
            }
            if (id == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString() || password == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 처음 회원 or 관리자모드 선택으로 돌아가야함
                return;
            SelectMenu(menuSelection, message, dataProcessing, administratorScreen, bothScreen);
        }

        private bool CheckLogin(Message message, DataProcessing dataProcessing, string id, string password)
        {
            List<string> administratorId = DataBase.Instance.GetSelectedElements(Constant.ADMINISTRATOR_FILED_ID, Constant.TABLE_NAME_ADMINISTRATOR);
            List<string> administratorPassword = DataBase.Instance.GetSelectedElements(Constant.ADMINISTRATOR_FILED_PASSWORD, Constant.TABLE_NAME_ADMINISTRATOR);

            for (int repeat = 0; repeat < administratorId.Count; repeat++)
            {
                if (id == administratorId[repeat] && password == administratorPassword[repeat])
                    return true;
            }

            message.PrintMessage("ID & PASSWORD 가 틀립니다", Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, Constant.IS_NOT_CONSOLE_CLEAR, ConsoleColor.Red);
            dataProcessing.ClearConsoleLine(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_MAX_POS_X, Constant.LOGIN_ID_POS_Y);
            dataProcessing.ClearConsoleLine(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_MAX_POS_X, Constant.LOGIN_PASSWORD_POS_Y);
            return false;
        }


        private bool IsInputEscape(string stringValue)
        {
            if (stringValue == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString())
                return true;
            return false;
        }

        private void InputBookSearchOption(AdministratorScreen administratorScreen, BothScreen bothScreen, Message message, DataProcessing dataProcessing, MenuSelection menuSelection)
        {
            string bookId = "", bookName = "", bookPublisher = "", bookAuthor = "", bookPrice = "", bookQuantity = "";
            int currentConsoleCursorPosY;
            isInputEscape = false;
            Console.CursorVisible = true;

            administratorScreen.PrintBookSearchScreen(Constant.IS_CONSOLE_CLEAR);
            bothScreen.PrintSelectedValues(DataBase.Instance.Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK),Constant.TABLE_NAME_BOOK);
            Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.BookSearchPosY.ID); //좌표조정

            while (!isInputEscape)
            {
                currentConsoleCursorPosY = dataProcessing.CursorMove(Constant.SEARCH_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.BookSearchPosY.ID, (int)Constant.BookSearchPosY.SEARCH);
                isInputEscape = IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.BookSearchPosY.ID:
                        bookId = dataProcessing.GetInputValues(message, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.ID, Constant.MAX_LENGTH_BOOK_ID, Constant.IS_NOT_PASSWORD, Constant.EXCEPTION_TYPE_NUMBER, "숫자만 입력하세요", Constant.EXCEPTION_TYPE_BOOK_ID);
                        break;
                    case (int)Constant.BookSearchPosY.NAME:
                        bookName = dataProcessing.GetInputValues(message, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.NAME, Constant.MAX_LENGTH_BOOK_NAME, Constant.IS_NOT_PASSWORD, Constant.EXCEPTION_TYPE_ANY, "", Constant.EXCEPTION_TYPE_BOOK_NAME);
                        break;
                    case (int)Constant.BookSearchPosY.PUBLISHER:
                        bookPublisher = dataProcessing.GetInputValues(message, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.PUBLISHER, Constant.MAX_LENGTH_BOOK_PUBLISHER, Constant.IS_NOT_PASSWORD, Constant.EXCEPTION_TYPE_ANY, "", Constant.EXCEPTION_TYPE_BOOK_PUBLISHER);
                        break;
                    case (int)Constant.BookSearchPosY.AUTHOR:
                        bookAuthor = dataProcessing.GetInputValues(message, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.AUTHOR, Constant.MAX_LENGTH_BOOK_AUTHOR, Constant.IS_NOT_PASSWORD, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER_KOREA, "", Constant.EXCEPTION_TYPE_BOOK_AUTHOR);
                        break;
                    case (int)Constant.BookSearchPosY.PRICE:
                        bookPrice = dataProcessing.GetInputValues(message, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.PRICE, Constant.MAX_LENGTH_BOOK_PRICE, Constant.IS_NOT_PASSWORD, Constant.EXCEPTION_TYPE_NUMBER, "숫자만 입력하세요", Constant.EXCEPTION_TYPE_BOOK_PRICE);
                        break;
                    case (int)Constant.BookSearchPosY.QUANTITY:
                        bookQuantity = dataProcessing.GetInputValues(message, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.QUANTITY, Constant.MAX_LENGTH_BOOK_QUANTITY, Constant.IS_NOT_PASSWORD, Constant.EXCEPTION_TYPE_NUMBER, "숫자만 입력하세요", Constant.EXCEPTION_TYPE_BOOK_QUANTITY);
                        break;
                    case (int)Constant.BookSearchPosY.SEARCH:
                        SearchBook(administratorScreen, bothScreen, message, dataProcessing, menuSelection, bookId, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity);
                        break;
                    default:
                        break;

                }
            }
            SelectMenu(menuSelection, message, dataProcessing, administratorScreen, bothScreen);
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

        private void SearchBook(AdministratorScreen administratorScreen, BothScreen bothScreen, Message message, DataProcessing dataProcessing, MenuSelection menuSelection, string bookId, string bookName, string bookPublisher, string bookAuthor, string bookPrice, string bookQuantity)
        {
            int GetyesOrNoBySearching, GetyesOrNoByResearching; // 아래 문자열 기능완성 후 constant로 빼기
            message.PrintMessage("검색하시겠습니까??", Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, Constant.IS_NOT_CONSOLE_CLEAR, ConsoleColor.Green);
            message.PrintMessage("< YES : ENTER | NO : ESC >", Constant.YES_OR_NO_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, Constant.IS_NOT_CONSOLE_CLEAR, ConsoleColor.Green);
            GetyesOrNoBySearching = dataProcessing.GetEnterOrEsc();

            if (GetyesOrNoBySearching == Constant.INPUT_ENTER)
            {
                administratorScreen.PrintSearchResultScreen(Constant.IS_CONSOLE_CLEAR);
                bothScreen.PrintSelectedValues(DataBase.Instance.Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK, GetConditionalStringBySelectBook(bookId, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity)), Constant.TABLE_NAME_BOOK);
                Console.SetCursorPosition(0, 0); // 출력되는 자료가 많아서 화면이 내려갈 수 있어 최상단으로 커서 옮기기
                Console.CursorVisible = false;
                GetyesOrNoByResearching = dataProcessing.GetEnterOrEsc();
                if (GetyesOrNoByResearching == Constant.INPUT_ENTER)
                    InputBookSearchOption(administratorScreen, bothScreen, message, dataProcessing, menuSelection);
                SelectMenu(menuSelection, message, dataProcessing, administratorScreen, bothScreen);
            }
            administratorScreen.PrintBookSearchScreen(Constant.IS_CONSOLE_CLEAR);
            bothScreen.PrintSelectedValues(DataBase.Instance.Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK);
            Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.BookSearchPosY.ID); //좌표조정
        }

        private void SelectMenu(MenuSelection menuSelection, Message message, DataProcessing dataProcessing,  AdministratorScreen administratorScreen, BothScreen bothScreen)
        { 
            int menuValue;
            menuValue = menuSelection.AddministratorMenuSelect(administratorScreen, dataProcessing);

            switch (menuValue)
            {
                case (int)Constant.AdministratorMenu.BOOK_SEARCH://책이름이 ㄱㄴㄷㄹ. 이런식으로 시작할수도 있을거임 이거 나중에 처리해주기.
                    InputBookSearchOption(administratorScreen, bothScreen, message, dataProcessing, menuSelection);
                    break;
                case (int)Constant.AdministratorMenu.BOOK_ADD:
                    SelectMenu(menuSelection, message, dataProcessing, administratorScreen, bothScreen);
                    break;
                case (int)Constant.AdministratorMenu.BOOK_REMOVE:
                    SelectMenu(menuSelection, message, dataProcessing, administratorScreen, bothScreen);
                    break;
                case (int)Constant.AdministratorMenu.BOOK_REVISE:
                    break;
                case (int)Constant.AdministratorMenu.MEMBER_MANAGEMENT:
                    Console.Clear();
                    Console.ReadKey();
                    SelectMenu(menuSelection, message, dataProcessing, administratorScreen, bothScreen);
                    break;
                case (int)Constant.AdministratorMenu.RENTAL_STATUS:
                    SelectMenu(menuSelection, message, dataProcessing, administratorScreen, bothScreen);
                    break;
                case Constant.INPUT_ESCAPE_IN_ARROW_KEY:
                    Login(administratorScreen, bothScreen, message, dataProcessing, menuSelection);
                    break;
                default:
                    break;
            }
        }
    }
}
