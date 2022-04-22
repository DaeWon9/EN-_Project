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
                isLogin = LoginCheck(message, dataProcessing, id, password);
            }
            if (id == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString() || password == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 처음 회원 or 관리자모드 선택으로 돌아가야함
                return;
            MenuSelect(menuSelection, message, dataProcessing, administratorScreen, bothScreen);
        }

        private bool LoginCheck(Message message, DataProcessing dataProcessing, string id, string password)
        {
            List<string> administratorId = DataBaseTestSingleton.Instance.GetSelectedElements(Constant.ADMINISTRATOR_FILED_ID, Constant.TABLE_NAME_ADMINISTRATOR);
            List<string> administratorPassword = DataBaseTestSingleton.Instance.GetSelectedElements(Constant.ADMINISTRATOR_FILED_PASSWORD, Constant.TABLE_NAME_ADMINISTRATOR);

            for (int repeat = 0; repeat < administratorId.Count; repeat++)
            {
                if (id == administratorId[repeat] && password == administratorPassword[repeat])
                    return true;
            }

            message.PrintMessage("ID & PASSWORD 가 틀립니다", Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, Constant.IS_NOT_CONSOLE_CLEAR, ConsoleColor.Red);
            dataProcessing.ConsoleLineClear(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_MAX_POS_X, Constant.LOGIN_ID_POS_Y);
            dataProcessing.ConsoleLineClear(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_MAX_POS_X, Constant.LOGIN_PASSWORD_POS_Y);
            return false;
        }

        private void InputBookSearchOption(AdministratorScreen administratorScreen, BothScreen bothScreen, Message message, DataProcessing dataProcessing)
        {
            string bookId = "", bookName = "", bookPublisher = "", bookAuthor = "", bookPrice = "", bookQuantity = "";
            int currentCursorPosY;
            Console.CursorVisible = true;
            administratorScreen.PrintBookSearchScreen(Constant.IS_CONSOLE_CLEAR);
            bothScreen.PrintSelectedValues(DataBaseTestSingleton.Instance.Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK),Constant.TABLE_NAME_BOOK);
            Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.BookSearchPosY.ID);
            while (true) // bool 형식 변수로 처리하기
            {
                currentCursorPosY = dataProcessing.CursorMove(Constant.SEARCH_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.BookSearchPosY.ID, (int)Constant.BookSearchPosY.SEARCH);
                switch (currentCursorPosY)
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
                        BookSearch(administratorScreen, bothScreen, message, dataProcessing, bookId, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity);
                        break;
                    default:
                        break;

                }
            }
        }
        

        private string GetConditionalString(string bookId, string bookName, string bookPublisher, string bookAuthor, string bookPrice, string bookQuantity)
        {
            string conditionalString = "";

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

        private void BookSearch(AdministratorScreen administratorScreen, BothScreen bothscreen, Message message, DataProcessing dataProcessing, string bookId, string bookName, string bookPublisher, string bookAuthor, string bookPrice, string bookQuantity)
        {
            int yesOrNo;
            message.PrintMessage("검색하시겠습니까??", Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, Constant.IS_NOT_CONSOLE_CLEAR, ConsoleColor.Blue);
            message.PrintMessage("YES : ENTER\tNO : ESC", Constant.YES_OR_NO_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, Constant.IS_NOT_CONSOLE_CLEAR, ConsoleColor.Blue);
            yesOrNo = dataProcessing.GetEnterOrEsc();

            if (yesOrNo == Constant.INPUT_ENTER)
            {
                administratorScreen.PrintSearchResultScreen(Constant.IS_CONSOLE_CLEAR);
                bothscreen.PrintSelectedValues(DataBaseTestSingleton.Instance.Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK, GetConditionalString(bookId, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity)), Constant.TABLE_NAME_BOOK);
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;
                Console.ReadKey();
                InputBookSearchOption(administratorScreen, bothscreen, message, dataProcessing);
            }
            else // Esc
                InputBookSearchOption(administratorScreen, bothscreen, message, dataProcessing);
        }

        private void MenuSelect(MenuSelection menuSelection, Message message, DataProcessing dataProcessing,  AdministratorScreen administratorScreen, BothScreen bothScreen)
        { 
            int menuValue;
            menuValue = menuSelection.AddministratorMenuSelect(administratorScreen, dataProcessing);

            switch (menuValue)
            {
                case (int)Constant.AdministratorMenu.BOOK_SEARCH://책이름이 ㄱㄴㄷㄹ. 이런식으로 시작할수도 있을거임 이거 나중에 처리해주기.
                    InputBookSearchOption(administratorScreen, bothScreen, message, dataProcessing);
                    Console.ReadKey();
                    break;
                case (int)Constant.AdministratorMenu.BOOK_ADD:
                    MenuSelect(menuSelection, message, dataProcessing, administratorScreen, bothScreen);
                    break;
                case (int)Constant.AdministratorMenu.BOOK_REMOVE:
                    MenuSelect(menuSelection, message, dataProcessing, administratorScreen, bothScreen);
                    break;
                case (int)Constant.AdministratorMenu.BOOK_REVISE:
                    break;
                case (int)Constant.AdministratorMenu.MEMBER_MANAGEMENT:
                    Console.Clear();
                    Console.ReadKey();
                    MenuSelect(menuSelection, message, dataProcessing, administratorScreen, bothScreen);
                    break;
                case (int)Constant.AdministratorMenu.RENTAL_STATUS:
                    MenuSelect(menuSelection, message, dataProcessing, administratorScreen, bothScreen);
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
