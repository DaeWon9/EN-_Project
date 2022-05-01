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
    class Administrator : MenuSelection // 기능별로 쪼개기.. 모듈화...
    {
        Log log = new Log();
        private List<string> searchedBookIdList = new List<string>();
        private bool isInputEscape = false, isBookDeleteCompleted = false, isSearchAndModify = false;
        private int modifyBookId = 0;
        private string modifyBookName = "", modifyBookPublisher = "", modifyBookAuthor = "", modifyBookPrice = "", modifyBookQuantity = "";
        private string conditionalStringByUserInput = "", managementMemberId = "", managementMemberName = "", managementMemberAge = "", managementMemberAddress = "", managementMemberPhoneNumber = "";

        // Login
        public void Login(AdministratorScreen administratorScreen) // id : admin1    pw: admin1
        {
            bool isLogin = false;
            string id = "", password = "";
            administratorScreen.PrintLoginScreen();
            while (!isLogin)
            {
                id = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.LOGIN_POS_X, Constant.LOGIN_ID_POS_Y, Constant.MAX_LENGTH_MEMBER_ID, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_ID);
                if (id == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 뒤로가기
                    break;
                password = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.LOGIN_POS_X, Constant.LOGIN_PASSWORD_POS_Y, Constant.MAX_LENGTH_MEMBER_PASSWORD, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_PASSWORD, Constant.IS_PASSWORD);
                if (password == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 뒤로가기
                    break;
                isLogin = IsLogin(administratorScreen, id, password);
            }
            if (id == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString() || password == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 처음 회원 or 관리자모드 선택으로 돌아가야함
                return;
            SelectMenu(administratorScreen);
        }

        private bool IsLogin(AdministratorScreen administratorScreen, string id, string password)
        {
            List<string> administratorId = DataBase.GetDataBase().GetSelectedElements(Constant.ADMINISTRATOR_FILED_ID, Constant.TABLE_NAME_ADMINISTRATOR);
            List<string> administratorPassword = DataBase.GetDataBase().GetSelectedElements(Constant.ADMINISTRATOR_FILED_PASSWORD, Constant.TABLE_NAME_ADMINISTRATOR);

            for (int repeat = 0; repeat < administratorId.Count; repeat++)
            {
                if (id == administratorId[repeat] && password == administratorPassword[repeat])
                    return true;
            }

            administratorScreen.PrintMessage(Constant.TEXT_IS_NOT_CORRECT_ID_PASSWORD, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
            DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_MAX_POS_X, Constant.LOGIN_ID_POS_Y);
            DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_MAX_POS_X, Constant.LOGIN_PASSWORD_POS_Y);
            return false;
        }

        // SearchBook
        private void InputBookSearchOption(AdministratorScreen administratorScreen)
        {
            string bookId = "", bookName = "", bookPublisher = "", bookAuthor = "", bookPrice = "", bookQuantity = "";
            int currentConsoleCursorPosY;
            bool isSearchBookCompleted = false;
            isInputEscape = false;
            Console.CursorVisible = true;

            administratorScreen.PrintBookSearchScreen();
            administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
            Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.BookSearchPosY.ID); //좌표조정

            while (!isInputEscape && !isSearchBookCompleted)
            {
                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.SEARCH_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.BookSearchPosY.ID, (int)Constant.BookSearchPosY.SEARCH);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.BookSearchPosY.ID:
                        bookId = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.ID, Constant.MAX_LENGTH_BOOK_ID, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_ID);
                        break;
                    case (int)Constant.BookSearchPosY.NAME:
                        bookName = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.NAME, Constant.MAX_LENGTH_BOOK_NAME, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_NAME);
                        break;
                    case (int)Constant.BookSearchPosY.PUBLISHER:
                        bookPublisher = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.PUBLISHER, Constant.MAX_LENGTH_BOOK_PUBLISHER, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_PUBLISHER);
                        break;
                    case (int)Constant.BookSearchPosY.AUTHOR:
                        bookAuthor = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.AUTHOR, Constant.MAX_LENGTH_BOOK_AUTHOR, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER_KOREA, Constant.EXCEPTION_TYPE_BOOK_AUTHOR);
                        break;
                    case (int)Constant.BookSearchPosY.PRICE:
                        bookPrice = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.PRICE, Constant.MAX_LENGTH_BOOK_PRICE, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_PRICE);
                        break;
                    case (int)Constant.BookSearchPosY.QUANTITY:
                        bookQuantity = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.QUANTITY, Constant.MAX_LENGTH_BOOK_QUANTITY, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_QUANTITY);
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
            conditionalStringByUserInput = DataProcessing.GetDataProcessing().GetConditionalStringBySearchBook(bookId, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity);

            administratorScreen.PrintMessage(Constant.TEXT_IS_SEARCH, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y); //좌표조정
            getYesOrNoBySearching = DataProcessing.GetDataProcessing().GetEnterOrEscape();

            if (getYesOrNoBySearching == Constant.INPUT_ENTER && isSearchAndModify == Constant.IS_ONLY_SEARCH) // 검색만
            {
                administratorScreen.PrintSearchResultScreen();
                administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK, conditionalStringByUserInput), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
                Console.SetCursorPosition(0, 0); // 출력되는 자료가 많아서 화면이 내려갈 수 있어 최상단으로 커서 옮기기
                Console.CursorVisible = false;
                getYesOrNoByResearching = DataProcessing.GetDataProcessing().GetEnterOrEscape();
                if (getYesOrNoByResearching == Constant.INPUT_ENTER)
                    InputBookSearchOption(administratorScreen);
            }
            if (getYesOrNoBySearching == Constant.INPUT_ENTER && isSearchAndModify == Constant.IS_SEARCH_AND_BORROW) // 검색 후 수정까지하는 함수 -> 여기서 검색된 도서 id 리스트 만들어서 대여할때 중복체크하기
            {
                administratorScreen.PrintSelectModifyBookScreen(); // 도서 수정 UI 출력
                administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK, DataProcessing.GetDataProcessing().GetConditionalStringBySearchBook(bookId, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity)), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
                searchedBookIdList = DataBase.GetDataBase().GetSelectedElements(Constant.BOOK_FILED_ID, Constant.TABLE_NAME_BOOK, DataProcessing.GetDataProcessing().GetConditionalStringBySearchBook(bookId, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity));
                SelectModifyBookId(administratorScreen);
            }
            if (getYesOrNoBySearching == Constant.INPUT_ESCAPE)
            {
                administratorScreen.PrintBookSearchScreen();
                administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
                Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
                Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.BookSearchPosY.ID); //좌표조정
            }
            return true;
        }

        // AddBook
        private bool IsAlreadyRegisteredBookInLibrary(string bookId)
        {
            List<string> memberIdList = DataBase.GetDataBase().GetSelectedElements(Constant.BOOK_FILED_ID, Constant.TABLE_NAME_BOOK);
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
            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y); //좌표조정
            GetYesOrNoByAdd = DataProcessing.GetDataProcessing().GetEnterOrEscape();

            if (GetYesOrNoByAdd == Constant.INPUT_ESCAPE) // 추가하시겠습니까?? -> ESC
            {
                DataProcessing.GetDataProcessing().ClearErrorMessage();
                Console.SetCursorPosition(Constant.ADD_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookAddPosY.ID); //좌표조정
                return false;
            }
            if (GetYesOrNoByAdd == Constant.INPUT_ENTER) // 추가하시겠습니까?? -> ENTER
            {
                DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_FORM_CONTAIN_ID,bookName, bookId, Constant.LOG_TEXT_ADD_BOOK));
                DataBase.GetDataBase().InsertAddBook(Constant.TABLE_NAME_BOOK, int.Parse(bookId), bookName, bookPublisher, bookAuthor, int.Parse(bookPrice), int.Parse(bookQuantity));
                Console.CursorVisible = false;
                administratorScreen.PrintMessage(Constant.TEXT_SUCCESS_ADD_BOOK, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
                administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);

                GetYesOrNoByReAdd = DataProcessing.GetDataProcessing().GetEnterOrEscape();
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
            administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
            Console.SetCursorPosition(0, 0);      //입력창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.ADD_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookAddPosY.ID); //좌표조정

            while (!isInputEscape && !isAddBookCompleted)
            {
                if (IsAlreadyRegisteredBookInLibrary(bookId)) // 이미 등록되어있는 책
                {
                    administratorScreen.PrintMessage(Constant.TEXT_ALREADY_REGISTERED_BOOK, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    Console.SetCursorPosition(Constant.ADD_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookAddPosY.ID); //좌표조정
                    DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.ADD_BOOK_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.BookAddPosY.ID);
                    bookId = "";
                }

                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.ADD_BOOK_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.BookAddPosY.ID, (int)Constant.BookSearchPosY.SEARCH);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.BookAddPosY.ID:
                        bookId = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.ID, Constant.MAX_LENGTH_BOOK_ID, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_ID);
                        break;
                    case (int)Constant.BookAddPosY.NAME:
                        bookName = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.NAME, Constant.MAX_LENGTH_BOOK_NAME, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_NAME);
                        break;
                    case (int)Constant.BookAddPosY.PUBLISHER:
                        bookPublisher = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.PUBLISHER, Constant.MAX_LENGTH_BOOK_PUBLISHER, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_PUBLISHER);
                        break;
                    case (int)Constant.BookAddPosY.AUTHOR:
                        bookAuthor = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.AUTHOR, Constant.MAX_LENGTH_BOOK_AUTHOR, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER_KOREA, Constant.EXCEPTION_TYPE_BOOK_AUTHOR);
                        break;
                    case (int)Constant.BookAddPosY.PRICE:
                        bookPrice = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.PRICE, Constant.MAX_LENGTH_BOOK_PRICE, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_PRICE);
                        break;
                    case (int)Constant.BookAddPosY.QUANTITY:
                        bookQuantity = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.QUANTITY, Constant.MAX_LENGTH_BOOK_QUANTITY, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_QUANTITY);
                        break;
                    case (int)Constant.BookAddPosY.ADD:
                        isAddBookCompleted = IsAddBookCompleted(administratorScreen, bookId, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity);
                        break;
                    default:
                        break;
                }
            }
        }

        // ManagementMember
        private bool IsRegisteredMemberId(string memberId)
        {
            List<string> memberIdList = DataBase.GetDataBase().GetSelectedElements(Constant.MEMBER_FILED_ID, Constant.TABLE_NAME_MEMBER);
            for (int repeat = 0; repeat < memberIdList.Count; repeat++)
            {
                if (memberIdList[repeat] == memberId)
                    return true;
            }
            return false;
        }

        private void ManagementMember(AdministratorScreen administratorScreen)
        {
            int currentConsoleCursorPosY, getYesOrNoByModify;
            string memberId = "";
            bool isSelectMemberIdCompleted = false;
            isInputEscape = false;
            administratorScreen.PrintManagementMemberScreen();
            administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_MEMBER), Constant.TABLE_NAME_MEMBER, Constant.TEXT_NONE);
            Console.SetCursorPosition(0, 0);      //입력창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.SELECT_MANAGEMENT_MEMBER_ID_OPTION_POS_X, (int)Constant.SelectMemberIdPosY.ID); //좌표조정

            while (!isInputEscape && !isSelectMemberIdCompleted)
            {
                if (memberId != "" && !IsRegisteredMemberId(memberId))// 회원아이디가 입력됐는데, 등록되지 않은 아이디임
                {
                    administratorScreen.PrintMessage(Constant.TEXT_IS_NOT_REGISTERED_MEMBER_ID, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    Console.SetCursorPosition(Constant.SELECT_MANAGEMENT_MEMBER_ID_OPTION_POS_X, (int)Constant.SelectMemberIdPosY.ID); //좌표조정
                    DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.SELECT_MANAGEMENT_MEMBER_ID_POS_X, Constant.WINDOW_WIDTH, (int)Constant.SelectMemberIdPosY.ID);
                    memberId = "";
                }

                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.SELECT_MANAGEMENT_MEMBER_ID_OPTION_POS_X, Console.CursorTop, (int)Constant.SelectMemberIdPosY.ID, (int)Constant.SelectMemberIdPosY.MANAGEMEMT_MEMBER);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.SelectMemberIdPosY.ID:
                        memberId = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SELECT_MANAGEMENT_MEMBER_ID_POS_X, (int)Constant.SelectMemberIdPosY.ID, Constant.MAX_LENGTH_MEMBER_ID, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_ID);
                        break;
                    case (int)Constant.SelectMemberIdPosY.MANAGEMEMT_MEMBER:
                        if (memberId != "" && memberId != Constant.INPUT_ESCAPE.ToString())
                        {
                            administratorScreen.PrintMessage(Constant.TEXT_IS_MANAGEMENT, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
                            administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
                            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y); //좌표조정
                            getYesOrNoByModify = DataProcessing.GetDataProcessing().GetEnterOrEscape();
                            if (getYesOrNoByModify == Constant.INPUT_ENTER)
                                isSelectMemberIdCompleted = true;
                            if (getYesOrNoByModify == Constant.INPUT_ESCAPE)
                            {
                                DataProcessing.GetDataProcessing().ClearErrorMessage();
                                Console.SetCursorPosition(Constant.SELECT_MANAGEMENT_MEMBER_ID_OPTION_POS_X, (int)Constant.SelectMemberIdPosY.ID); //좌표조정
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            if (isSelectMemberIdCompleted)
            {
                managementMemberId = memberId;
                managementMemberName = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_NAME, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, managementMemberId));
                managementMemberAge = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_AGE, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, managementMemberId));
                managementMemberAddress = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_ADDRESS, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, managementMemberId));
                managementMemberPhoneNumber = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_PHONE_NUMBER, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, managementMemberId));
            }


            if (!isInputEscape)
                ModifyMemberInformation(administratorScreen);
        }

        private bool IsMemberNotReturnBorrowedBook()
        {

            List<string> BorrowedBookList = DataBase.GetDataBase().GetSelectedElements(Constant.BOOK_FILED_ID, managementMemberId);
            if (BorrowedBookList.Count > 0) // 반납안한 책이 있음 
                return true;
            return false;
        }

        private bool IsReModifyByMember(AdministratorScreen administratorScreen)
        {
            int getYesOrNoByReModify;
            administratorScreen.PrintMessage(Constant.TEXT_SUCCESS_MODIFY, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            getYesOrNoByReModify = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (getYesOrNoByReModify == Constant.INPUT_ESCAPE) // 계속해서 변경 x
                return false;
            return true;
        }

        private bool IsWithdrawlCompleted(AdministratorScreen administratorScreen)
        {
            int getYesOrNoByWithdrawl;
            string memberName;
            if (IsMemberNotReturnBorrowedBook()) // 반납안한 책이 있음
            {
                administratorScreen.PrintMessage(Constant.TEXT_UNABLE_WITHDRAWAL, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.MODIFY_MEMBER_SELECT_OPTION_POS_X, (int)Constant.MemberModifyModePosY.NAME); //좌표조정
                return false;
            }
            else // 클린한 상태임 -> 회원탈퇴 가능
            {
                administratorScreen.PrintMessage(Constant.TEXT_IS_WITHDRAWAL, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Red); // 정말로 탈퇴할건지 물어보기
                administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y); //좌표조정

                getYesOrNoByWithdrawl = DataProcessing.GetDataProcessing().GetEnterOrEscape();
                if (getYesOrNoByWithdrawl == Constant.INPUT_ENTER) // 탈퇴진행
                {
                    memberName = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_NAME, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, managementMemberId));
                    DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_FORM_CONTAIN_ID, memberName, managementMemberId, Constant.LOG_TEXT_DELETE_MEMBER));
                    DataBase.GetDataBase().Drop(managementMemberId); // 회원아이디로 된 테이블 drop
                    DataBase.GetDataBase().Delete(Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, managementMemberId));
                }
                if (getYesOrNoByWithdrawl == Constant.INPUT_ESCAPE) // 탈퇴취소
                {
                    DataProcessing.GetDataProcessing().ClearErrorMessage();
                    Console.SetCursorPosition(Constant.MODIFY_MEMBER_SELECT_OPTION_POS_X, (int)Constant.MemberModifyModePosY.NAME); //좌표조정
                    return false;
                }

            }
            return true;
        }

        private bool IsModifyMemberInformationCompleted(AdministratorScreen administratorScreen, string setString)
        {
            int getYesOrNoByModify;
            administratorScreen.PrintMessage(Constant.TEXT_IS_MODIFY, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y); //좌표조정
            getYesOrNoByModify = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (getYesOrNoByModify == Constant.INPUT_ENTER) // 변경하시겠습니까? 에서 enter입력
            {
                DataBase.GetDataBase().Update(Constant.TABLE_NAME_MEMBER, setString, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_NAME, managementMemberName));
                return true;
            }
            if (getYesOrNoByModify == Constant.INPUT_ESCAPE) // 변경하시겠습니까? 에서 esc입력
            {
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.MODIFY_MEMBER_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.MemberModifyModePosY.NAME);
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.MODIFY_MEMBER_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.MemberModifyModePosY.PASSWORD);
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.MODIFY_MEMBER_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.MemberModifyModePosY.AGE);
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.MODIFY_MEMBER_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.MemberModifyModePosY.ADDRESS);
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.MODIFY_MEMBER_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.MemberModifyModePosY.PHONE_NUMBER);
                DataProcessing.GetDataProcessing().ClearErrorMessage();
                Console.SetCursorPosition(Constant.MODIFY_BOOK_SELECT_OPTION_POS_X, (int)Constant.MemberModifyModePosY.NAME); //좌표조정

                return false;
            }
            return true;
        }

        private void ModifyMemberInformation(AdministratorScreen administratorScreen) // memberId 는 primaryKey 이므로 수정불가능하게 설정
        {
            string setStringByUpdate = "";
            string memberName = "", memberPassword = "", memberAge = "", memberAddress = "", memberPhoneNumber = "";
            bool isModifyCompleted = false, isWithdrawlCompleted = false;
            isInputEscape = false;
            int currentConsoleCursorPosY;
            administratorScreen.PrintModifyMemberInformationLabel();
            administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_MEMBER, String.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, managementMemberId)), Constant.TABLE_NAME_MEMBER, Constant.TEXT_NONE);
            administratorScreen.PrintModifyMemberInformationScreen();
            Console.SetCursorPosition(Constant.MODIFY_MEMBER_SELECT_OPTION_POS_X, (int)Constant.MemberModifyModePosY.NAME); //좌표조정

            while (!isInputEscape && !isModifyCompleted && !isWithdrawlCompleted)
            {
                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.MODIFY_MEMBER_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.MemberModifyModePosY.NAME, (int)Constant.MemberModifyModePosY.WITHDRAWAL);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.MemberModifyModePosY.NAME:
                        memberName = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.MODIFY_MEMBER_INPUT_POS_X, (int)Constant.MemberModifyModePosY.NAME, Constant.MAX_LENGTH_MEMBER_NAME, Constant.TEXT_PLEASE_INPUT_CORRECT_STRING, Constant.EXCEPTION_TYPE_KOREAN, Constant.EXCEPTION_TYPE_MEMBER_NAME);
                        setStringByUpdate = GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.MEMBER_FILED_NAME, memberName);
                        break;
                    case (int)Constant.MemberModifyModePosY.PASSWORD:
                        memberPassword = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.MODIFY_MEMBER_INPUT_POS_X, (int)Constant.MemberModifyModePosY.PASSWORD, Constant.MAX_LENGTH_MEMBER_PASSWORD, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_PASSWORD);
                        setStringByUpdate = GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.MEMBER_FILED_PASSWORD, memberPassword);
                        break;
                    case (int)Constant.MemberModifyModePosY.AGE:
                        memberAge = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.MODIFY_MEMBER_INPUT_POS_X, (int)Constant.MemberModifyModePosY.AGE, Constant.MAX_LENGTH_MEMBER_AGE, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_AGE);
                        setStringByUpdate = GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.MEMBER_FILED_AGE, memberAge);
                        break;
                    case (int)Constant.MemberModifyModePosY.ADDRESS:
                        memberAddress = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.MODIFY_MEMBER_INPUT_POS_X, (int)Constant.MemberModifyModePosY.ADDRESS, Constant.MAX_LENGTH_MEMBER_ADDRESS, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_MEMBER_ADDRESS);
                        setStringByUpdate = GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.MEMBER_FILED_ADDRESS, memberAddress);
                        break;
                    case (int)Constant.MemberModifyModePosY.PHONE_NUMBER:
                        memberPhoneNumber = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.MODIFY_MEMBER_INPUT_POS_X, (int)Constant.MemberModifyModePosY.PHONE_NUMBER, Constant.MAX_LENGTH_MEMBER_PHONE_NUMBER, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_PHONE_NUMBER);
                        setStringByUpdate = GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.MEMBER_FILED_PHONE_NUMBER, memberPhoneNumber);
                        break;
                    case (int)Constant.MemberModifyModePosY.WITHDRAWAL:
                        isWithdrawlCompleted = IsWithdrawlCompleted(administratorScreen);
                        break;
                    default:
                        break;
                }

                if (setStringByUpdate != "")
                {
                    isModifyCompleted = IsModifyMemberInformationCompleted(administratorScreen, setStringByUpdate);

                    switch (currentConsoleCursorPosY)
                    {
                        case (int)Constant.MemberModifyModePosY.NAME:
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_BY_ADMINISTRATOR, managementMemberId, Constant.LOG_TEXT_MODIFY_MEMBER_NAME, managementMemberName, memberName));
                            break;
                        case (int)Constant.MemberModifyModePosY.PASSWORD:
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_PASSWORD_BY_ADMINISTRATOR, managementMemberId, Constant.LOG_TEXT_MODIFY_MEMBER_PASSWORD));
                            break;
                        case (int)Constant.MemberModifyModePosY.AGE:
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_BY_ADMINISTRATOR, managementMemberId, Constant.LOG_TEXT_MODIFY_MEMBER_AGE, managementMemberAge, memberAge));
                            break;
                        case (int)Constant.MemberModifyModePosY.ADDRESS:
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_ADDRESS_BY_ADMINISTRATOR, managementMemberId, Constant.LOG_TEXT_MODIFY_MEMBER_ADDRESS, managementMemberAddress));
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFIED_MEMBER_ADDRESS_BY_ADINISTRATOR, memberAddress));
                            break;
                        case (int)Constant.MemberModifyModePosY.PHONE_NUMBER:
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_BY_ADMINISTRATOR, managementMemberId, Constant.LOG_TEXT_MODIFY_MEMBER_PHONE_NUMBER, managementMemberPhoneNumber, memberPhoneNumber));
                            break;
                        default:
                            break;
                    }

                    if (isModifyCompleted && IsReModifyByMember(administratorScreen)) // 계속해서 변경
                        ModifyMemberInformation(administratorScreen);
                }
            }

        }


        // ModifyBook
        private string GetStringByUpdate(string setStringForm, string filed, string inputValue)
        {
            string resultString = "";
            if (inputValue != "" && inputValue != Constant.INPUT_ESCAPE.ToString())
                resultString = string.Format(setStringForm, filed, inputValue);
            return resultString;
        }
        
        private bool IsExistBookIdInSearchedBookList(string bookId)
        {
            List<string> searchedBookList = DataBase.GetDataBase().GetSelectedElements(Constant.BOOK_FILED_ID, Constant.TABLE_NAME_BOOK, conditionalStringByUserInput);
            for (int repeat = 0; repeat < searchedBookList.Count; repeat++)
            {
                if (searchedBookList[repeat] == bookId)
                    return true;
            }
            return false;
        }
        
        private bool IsModifyBookInformationCompleted(AdministratorScreen administratorScreen, string setString, int bookId)
        {
            int getYesOrNoByModify;
            administratorScreen.PrintMessage(Constant.TEXT_IS_MODIFY, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y); //좌표조정
            getYesOrNoByModify = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (getYesOrNoByModify == Constant.INPUT_ENTER) // 변경하시겠습니까? 에서 enter입력
            {
                DataBase.GetDataBase().Update(Constant.TABLE_NAME_BOOK, setString, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_INT, Constant.BOOK_FILED_ID, bookId));
                return true;
            }
            if (getYesOrNoByModify == Constant.INPUT_ESCAPE) // 변경하시겠습니까? 에서 esc입력
            {
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.MODIFY_BOOK_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.BookModifyPosY.NAME);
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.MODIFY_BOOK_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.BookModifyPosY.PUBLISHER);
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.MODIFY_BOOK_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.BookModifyPosY.AUTHOR);
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.MODIFY_BOOK_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.BookModifyPosY.PRICE);
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.MODIFY_BOOK_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.BookModifyPosY.QUANTITY);
                DataProcessing.GetDataProcessing().ClearErrorMessage();
                Console.SetCursorPosition(Constant.MODIFY_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookModifyPosY.NAME); //좌표조정
                return false;
            }
            return true;
        }

        private bool IsReModifyByBook(AdministratorScreen administratorScreen)
        {
            int getYesOrNoByReModify;
            administratorScreen.PrintMessage(Constant.TEXT_SUCCESS_MODIFY, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            getYesOrNoByReModify = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (getYesOrNoByReModify == Constant.INPUT_ESCAPE) // 계속해서 변경 x
                return false;
            return true;
        }

        private void ModifyBook(AdministratorScreen administratorScreen) //  BookId 는 primaryKey 이므로 수정불가능하게 설정
        {
            string setStringByUpdate = "";
            string bookName = "", bookPublisher = "", bookAuthor = "", bookPrice = "", bookQuantity = "";
            int currentConsoleCursorPosY;
            bool isModifyCompleted = false;
            isBookDeleteCompleted = false;
            isInputEscape = false;

            administratorScreen.PrintModifyBookLabel();
            administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK, String.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_INT, Constant.BOOK_FILED_ID, modifyBookId)), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
            administratorScreen.PrintModifyBookScreen();

            Console.SetCursorPosition(Constant.MODIFY_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookModifyPosY.NAME); //좌표조정

            while (!isInputEscape && !isModifyCompleted && !isBookDeleteCompleted)
            {
                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.MODIFY_BOOK_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.BookModifyPosY.NAME, (int)Constant.BookModifyPosY.DELETE);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.BookModifyPosY.NAME:
                        bookName = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.MODIFY_BOOK_INPUT_POS_X, (int)Constant.BookModifyPosY.NAME, Constant.MAX_LENGTH_BOOK_NAME, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_NAME);
                        setStringByUpdate = GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.BOOK_FILED_NAME, bookName);
                        break;
                    case (int)Constant.BookModifyPosY.PUBLISHER:
                        bookPublisher = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.MODIFY_BOOK_INPUT_POS_X, (int)Constant.BookModifyPosY.PUBLISHER, Constant.MAX_LENGTH_BOOK_PUBLISHER, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_PUBLISHER);
                        setStringByUpdate = GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.BOOK_FILED_PUBLISHER, bookPublisher);
                        break;
                    case (int)Constant.BookModifyPosY.AUTHOR:
                        bookAuthor = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.MODIFY_BOOK_INPUT_POS_X, (int)Constant.BookModifyPosY.AUTHOR, Constant.MAX_LENGTH_BOOK_AUTHOR, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER_KOREA, Constant.EXCEPTION_TYPE_BOOK_AUTHOR);
                        setStringByUpdate = GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.BOOK_FILED_AUTHOR, bookAuthor);
                        break;
                    case (int)Constant.BookModifyPosY.PRICE:
                        bookPrice = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.MODIFY_BOOK_INPUT_POS_X, (int)Constant.BookModifyPosY.PRICE, Constant.MAX_LENGTH_BOOK_PRICE, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_PRICE);
                        setStringByUpdate = GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.BOOK_FILED_PRICE, bookPrice);
                        break;
                    case (int)Constant.BookModifyPosY.QUANTITY:
                        bookQuantity = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.MODIFY_BOOK_INPUT_POS_X, (int)Constant.BookModifyPosY.QUANTITY, Constant.MAX_LENGTH_BOOK_QUANTITY, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_QUANTITY);
                        setStringByUpdate = GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.BOOK_FILED_QUANTITY, bookQuantity);
                        break;
                    case (int)Constant.BookModifyPosY.DELETE:
                        isBookDeleteCompleted = IsDeleteBookCompleted(administratorScreen, modifyBookId);
                        break;
                    default:
                        break;
                }

                if (setStringByUpdate != "") // 수정사항이 있다면
                {
                    isModifyCompleted = IsModifyBookInformationCompleted(administratorScreen, setStringByUpdate, modifyBookId);

                    
                    switch (currentConsoleCursorPosY)
                    {
                        case (int)Constant.BookModifyPosY.NAME:
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_BOOK_NAME_BY_ADMINISTRATOR, modifyBookId, Constant.LOG_TEXT_MODIFY_BOOK_NAME, modifyBookName));
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFIED_BOOK_NAME, bookName));
                            break;
                        case (int)Constant.BookModifyPosY.PUBLISHER:
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_BY_ADMINISTRATOR, modifyBookId, Constant.LOG_TEXT_MODIFY_BOOK_PUBLISHER, modifyBookPublisher, bookPublisher));
                            break;
                        case (int)Constant.BookModifyPosY.AUTHOR:
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_BY_ADMINISTRATOR, modifyBookId, Constant.LOG_TEXT_MODIFY_BOOK_AUTHOR, modifyBookAuthor, bookAuthor));
                            break;
                        case (int)Constant.BookModifyPosY.PRICE:
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_BY_ADMINISTRATOR, modifyBookId, Constant.LOG_TEXT_MODIFY_BOOK_PRICE, modifyBookPrice, bookPrice));
                            break;
                        case (int)Constant.BookModifyPosY.QUANTITY:
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_BY_ADMINISTRATOR, modifyBookId, Constant.LOG_TEXT_MODIFY_BOOK_QUANTITY, modifyBookQuantity, bookQuantity));
                            break;
                        default:
                            break;
                    }
                    

                    if (isModifyCompleted && IsReModifyByBook(administratorScreen))
                        ModifyBook(administratorScreen);
                }
            }
        }

        private void SelectModifyBookId(AdministratorScreen administratorScreen)
        {
            string bookId = "";
            int currentConsoleCursorPosY, getYesOrNoByModify;
            bool isSelectBookIdCompleted = false;
            isInputEscape = false;

            if (isSearchAndModify == Constant.IS_SEARCH_AND_MODIFY) //겹치는부분 빼기
            {
                administratorScreen.PrintSelectModifyBookScreen();
                administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK, conditionalStringByUserInput), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
            }
            else
            {
                administratorScreen.PrintSelectModifyBookScreen();
                administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);

            }



            Console.SetCursorPosition(0, 0);      //입력창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.SELECT_MODIFY_BOOK_ID_OPTION_POS_X, (int)Constant.SelectBookIdPosY.ID); //좌표조정

            while (!isInputEscape && !isSelectBookIdCompleted)
            {
                if (bookId != "" && !IsAlreadyRegisteredBookInLibrary(bookId))// 책이름이 입력됐는데, 도서관에 없는책임
                {
                    administratorScreen.PrintMessage(Constant.TEXT_IS_NOT_EXIST_IN_LIBRARY, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    Console.SetCursorPosition(Constant.SELECT_MODIFY_BOOK_ID_OPTION_POS_X, (int)Constant.SelectBookIdPosY.ID); //좌표조정
                    DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.SELECT_MODIFY_BOOK_ID_POS_X, Constant.WINDOW_WIDTH, (int)Constant.SelectBookIdPosY.ID);
                    bookId = "";
                }

                if (isSearchAndModify && bookId != "" && !IsExistBookIdInSearchedBookList(bookId)) // 검색 후 책이름 입력됐는데, 검색된 책이 아님
                {
                    administratorScreen.PrintMessage(Constant.TEXT_THIS_BOOK_IS_NOT_SEARCHED, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    Console.SetCursorPosition(Constant.SELECT_MODIFY_BOOK_ID_OPTION_POS_X, (int)Constant.SelectBookIdPosY.ID); //좌표조정
                    DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.SELECT_MODIFY_BOOK_ID_POS_X, Constant.WINDOW_WIDTH, (int)Constant.SelectBookIdPosY.ID);
                    bookId = "";
                }
                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.SELECT_MODIFY_BOOK_ID_OPTION_POS_X, Console.CursorTop, (int)Constant.SelectBookIdPosY.ID, (int)Constant.SelectBookIdPosY.MODIFY_BOOK);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.SelectBookIdPosY.ID:
                        bookId = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SELECT_MODIFY_BOOK_ID_POS_X, (int)Constant.SelectBookIdPosY.ID, Constant.MAX_LENGTH_BOOK_ID, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_ID);
                        break;
                    case (int)Constant.SelectBookIdPosY.MODIFY_BOOK:
                        if (bookId != "" && bookId != Constant.INPUT_ESCAPE.ToString())
                        {
                            administratorScreen.PrintMessage(Constant.TEXT_IS_MODIFY, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
                            administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
                            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y); //좌표조정
                            getYesOrNoByModify = DataProcessing.GetDataProcessing().GetEnterOrEscape();
                            if (getYesOrNoByModify == Constant.INPUT_ENTER)
                            {
                                modifyBookId = int.Parse(bookId);
                                isSelectBookIdCompleted = true;
                            }
                            if (getYesOrNoByModify == Constant.INPUT_ESCAPE)
                            {
                                DataProcessing.GetDataProcessing().ClearErrorMessage();
                                Console.SetCursorPosition(Constant.SELECT_MODIFY_BOOK_ID_OPTION_POS_X, (int)Constant.SelectBookIdPosY.ID); //좌표조정
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            if (isSelectBookIdCompleted)
            {
                modifyBookName = DataBase.GetDataBase().GetSelectedElement(Constant.BOOK_FILED_NAME, Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, bookId)); // 수정할 도서의 이름 저장
                modifyBookPublisher = DataBase.GetDataBase().GetSelectedElement(Constant.BOOK_FILED_PUBLISHER, Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, bookId)); // 수정할 도서의 출판사 저장
                modifyBookAuthor = DataBase.GetDataBase().GetSelectedElement(Constant.BOOK_FILED_AUTHOR, Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, bookId)); // 수정할 도서의 저자 저장
                modifyBookPrice = DataBase.GetDataBase().GetSelectedElement(Constant.BOOK_FILED_PRICE, Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, bookId)); // 수정할 도서의 가격 저장
                modifyBookQuantity = DataBase.GetDataBase().GetSelectedElement(Constant.BOOK_FILED_QUANTITY, Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, bookId)); // 수정할 도서의 수량 저장
            }

            if (!isInputEscape)
                ModifyBook(administratorScreen);

        }

        private bool IsDeleteBookCompleted(AdministratorScreen administratorScreen, int bookId)
        {
            int getYesOrNoByDeleteBook;
            string bookName;
            if (IsBookBorrwed(bookId)) // 해당책을 대여한 회원이 있음
            {
                administratorScreen.PrintMessage(Constant.TEXT_UNABLE_DELETE, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.MODIFY_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookModifyPosY.NAME); //좌표조정
                return false;
            }
            else // 클린한 상태임 -> 도서삭제 가능
            {
                administratorScreen.PrintMessage(Constant.TEXT_IS_DELETE, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Red); // 정말로 탈퇴할건지 물어보기
                administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y); //좌표조정

                getYesOrNoByDeleteBook = DataProcessing.GetDataProcessing().GetEnterOrEscape();
                if (getYesOrNoByDeleteBook == Constant.INPUT_ENTER) // 삭제진행
                {
                    bookName = DataBase.GetDataBase().GetSelectedElement(Constant.BOOK_FILED_NAME, Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, bookId));
                    DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_FORM_CONTAIN_ID, bookName, bookId, Constant.LOG_TEXT_DELETE_BOOK));
                    DataBase.GetDataBase().Delete(Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_INT, Constant.BOOK_FILED_ID, bookId));// 해당도서 delete
                }
                if (getYesOrNoByDeleteBook == Constant.INPUT_ESCAPE) // 삭제취소
                {
                    DataProcessing.GetDataProcessing().ClearErrorMessage();
                    Console.SetCursorPosition(Constant.MODIFY_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookModifyPosY.NAME); //좌표조정
                    return false;
                }
            }
            return true;
        }

        // SearchMember
        private void InputMemberSearchOption(AdministratorScreen administratorScreen)
        {
            string memberName = "", memberId = "", memberAge = "", memberAddress = "", memberPhoneNumber = "";
            int currentConsoleCursorPosY;
            bool isSearchMemberCompleted = false;
            isInputEscape = false;
            Console.CursorVisible = true;

            administratorScreen.PrintMemberSearchScreen();
            administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_MEMBER), Constant.TABLE_NAME_MEMBER, Constant.TEXT_NONE);
            Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.MemberSearchPosY.NAME); //좌표조정

            while (!isInputEscape && !isSearchMemberCompleted)
            {
                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.SEARCH_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.MemberSearchPosY.NAME, (int)Constant.MemberSearchPosY.SEARCH);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.MemberSearchPosY.NAME:
                        memberName = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.MemberSearchPosY.NAME, Constant.MAX_LENGTH_MEMBER_NAME, Constant.TEXT_PLEASE_INPUT_CORRECT_STRING, Constant.EXCEPTION_TYPE_KOREAN, Constant.EXCEPTION_TYPE_MEMBER_NAME);
                        break;
                    case (int)Constant.MemberSearchPosY.ID:
                        memberId = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.MemberSearchPosY.ID, Constant.MAX_LENGTH_MEMBER_ID, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER);
                        break;
                    case (int)Constant.MemberSearchPosY.AGE:
                        memberAge = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.MemberSearchPosY.AGE, Constant.MAX_LENGTH_MEMBER_AGE, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_AGE);
                        break;
                    case (int)Constant.MemberSearchPosY.ADDRESS:
                        memberAddress = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.MemberSearchPosY.ADDRESS, Constant.MAX_LENGTH_MEMBER_ADDRESS, Constant.TEXT_PLEASE_INPUT_KOREAN_OR_NUMBER, Constant.EXCEPTION_TYPE_KOREAN_NUMBER_SPACE, Constant.EXCEPTION_TYPE_KOREAN_NUMBER_SPACE);
                        break;
                    case (int)Constant.MemberSearchPosY.PHONE_NUMBER:
                        memberPhoneNumber = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.MemberSearchPosY.PHONE_NUMBER, Constant.MAX_LENGTH_MEMBER_PHONE_NUMBER, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_NUMBER);
                        break;
                    case (int)Constant.MemberSearchPosY.SEARCH:
                        isSearchMemberCompleted = IsSearchMemberCompleted(administratorScreen, memberName, memberId, memberAge, memberAddress, memberPhoneNumber);
                        break;
                    default:
                        break;
                }
            }
        }

        private bool IsSearchMemberCompleted(AdministratorScreen administratorScreen, string memberName, string memberId, string memberAge, string memberAddress, string memberPhoneNumber)
        {
            int getYesOrNoBySearching, getYesOrNoByResearching;
            // ↓ 옵션입력시 모두 공백일경우 체크하는 조건문
            if ((memberName == "" || memberName == Constant.INPUT_ESCAPE.ToString()) && (memberId == "" || memberId == Constant.INPUT_ESCAPE.ToString()) && (memberAge == "" || memberAge == Constant.INPUT_ESCAPE.ToString()) && (memberAddress == "" || memberAddress == Constant.INPUT_ESCAPE.ToString()) && (memberPhoneNumber == "" || memberPhoneNumber == Constant.INPUT_ESCAPE.ToString()))
            {
                administratorScreen.PrintMessage(Constant.TEXT_PLEASE_INPUT_OPTION, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.MemberSearchPosY.NAME); //좌표조정
                return false;
            }
            conditionalStringByUserInput = DataProcessing.GetDataProcessing().GetConditionalStringBySearchMember(memberName, memberId, memberAge, memberAddress, memberPhoneNumber);

            administratorScreen.PrintMessage(Constant.TEXT_IS_SEARCH, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y); //좌표조정
            getYesOrNoBySearching = DataProcessing.GetDataProcessing().GetEnterOrEscape();

            if (getYesOrNoBySearching == Constant.INPUT_ENTER) // 검색만
            {
                administratorScreen.PrintSearchResultScreen();
                administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_MEMBER, conditionalStringByUserInput), Constant.TABLE_NAME_MEMBER, Constant.TEXT_NONE);
                Console.SetCursorPosition(0, 0); // 출력되는 자료가 많아서 화면이 내려갈 수 있어 최상단으로 커서 옮기기
                Console.CursorVisible = false;
                getYesOrNoByResearching = DataProcessing.GetDataProcessing().GetEnterOrEscape();
                if (getYesOrNoByResearching == Constant.INPUT_ENTER)
                    InputBookSearchOption(administratorScreen);
            }
            if (getYesOrNoBySearching == Constant.INPUT_ESCAPE)
            {
                administratorScreen.PrintBookSearchScreen();
                administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_MEMBER), Constant.TABLE_NAME_MEMBER, Constant.TEXT_NONE);
                Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
                Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.MemberSearchPosY.NAME); //좌표조정
            }
            return true;
        }


        // ShowBorrowBookStatus
        private void ShowBorrowBookStatus(AdministratorScreen administratorScreen)
        {
            isInputEscape = false;
            string memberName = "", memberId = "", bookId = "";
            int currentConsoleCursorPosY;
            List<string> AllTablesName = DataBase.GetDataBase().GetAllTablesName();

            administratorScreen.PrintSelectCheckBorrowedBookModeScreen();
            foreach (string tableName in AllTablesName)
            {
                memberName = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_NAME, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, tableName));
                if (tableName != Constant.TABLE_NAME_ADMINISTRATOR && tableName != Constant.TABLE_NAME_MEMBER && tableName != Constant.TABLE_NAME_BOOK && tableName != Constant.TABLE_NAME_LOG)
                {
                    administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, tableName, Constant.TEXT_NONE, Constant.FILED_BORROW_DATE), tableName, memberName, Constant.IS_ADMINISTRATOR_MODE);
                }
            }
            Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.SEARCH_POS_X, (int)Constant.CheckBorrowedBookModePosY.BOOK_ID); //좌표조정

            while (!isInputEscape)
            {
                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.SELECT_MANAGEMENT_MEMBER_ID_OPTION_POS_X, Console.CursorTop, (int)Constant.SelectMemberIdPosY.ID, (int)Constant.SelectMemberIdPosY.MANAGEMEMT_MEMBER);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.CheckBorrowedBookModePosY.BOOK_ID:
                        bookId = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.CheckBorrowedBookModePosY.BOOK_ID, Constant.MAX_LENGTH_BOOK_ID, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_ID);
                        ShowBorrowBookStatusByBookId(administratorScreen, bookId);
                        break;
                    case (int)Constant.CheckBorrowedBookModePosY.MEMBER_ID:
                        memberId = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.CheckBorrowedBookModePosY.MEMBER_ID, Constant.MAX_LENGTH_MEMBER_ID, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_ID);
                        ShowBorrowBookStatusByMemberId(administratorScreen, memberId);
                        break;
                    default:
                        break;
                }
            }
        }

        private bool IsBookBorrwed(int bookId)
        {
            string conditionalString;
            List<string> AllTablesName = DataBase.GetDataBase().GetAllTablesName();
            conditionalString = string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_INT, Constant.BOOK_FILED_ID, bookId);
            foreach (string tableName in AllTablesName)
            {
                if (tableName != Constant.TABLE_NAME_ADMINISTRATOR && tableName != Constant.TABLE_NAME_MEMBER && tableName != Constant.TABLE_NAME_BOOK && tableName != Constant.TABLE_NAME_LOG)
                {
                    MySqlDataReader reader = DataBase.GetDataBase().Select(Constant.FILED_ALL, tableName, conditionalString);
                    if (reader.HasRows)
                    {
                        reader.Close();
                        return true;
                    }
                    else
                        reader.Close();
                }
            }
            return false;
        }

        private void ShowBorrowBookStatusByBookId(AdministratorScreen administratorScreen, string bookId)
        {
            if (!IsAlreadyRegisteredBookInLibrary(bookId))// 도서관에 없는책임
            {
                administratorScreen.PrintMessage(Constant.TEXT_IS_NOT_EXIST_IN_LIBRARY, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.SEARCH_POS_X, Constant.WINDOW_WIDTH, (int)Constant.CheckBorrowedBookModePosY.BOOK_ID);
                return;
            }

            isInputEscape = false;
            string memberName = "", conditionalString = "";
            List<string> AllTablesName = DataBase.GetDataBase().GetAllTablesName();

            conditionalString = string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_INT, Constant.BOOK_FILED_ID, bookId);

            administratorScreen.PrintAdministratorCheckBorrowedBookLabel();
            foreach (string tableName in AllTablesName)
            {
                memberName = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_NAME, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, tableName));
                if (tableName != Constant.TABLE_NAME_ADMINISTRATOR && tableName != Constant.TABLE_NAME_MEMBER && tableName != Constant.TABLE_NAME_BOOK)
                {
                    MySqlDataReader reader = DataBase.GetDataBase().Select(Constant.FILED_ALL, tableName, conditionalString);
                    administratorScreen.PrintSelectedValues(reader, tableName, memberName, Constant.IS_ADMINISTRATOR_MODE);
                }
            }
            Console.CursorVisible = false;
            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.CURSOR_POS_TOP);
            while (!isInputEscape)
            {
                isInputEscape = DataProcessing.GetDataProcessing().IsOnlyInputEscape();
                if (isInputEscape) //esc 눌렀을때 뒤로가기
                {
                    Console.CursorVisible = true;
                    ShowBorrowBookStatus(administratorScreen);
                }
            }
        }

        private void ShowBorrowBookStatusByMemberId(AdministratorScreen administratorScreen, string memberId)
        {

            if (!IsRegisteredMemberId(memberId))// 회원아이디가 입력됐는데, 등록되지 않은 아이디임
            {
                administratorScreen.PrintMessage(Constant.TEXT_IS_NOT_REGISTERED_MEMBER_ID, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.SEARCH_POS_X, Constant.WINDOW_WIDTH, (int)Constant.CheckBorrowedBookModePosY.MEMBER_ID);
                return;
            }

            isInputEscape = false;
            string memberName = "";
            administratorScreen.PrintAdministratorCheckBorrowedBookLabel();
            memberName = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_NAME, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, memberId));
            administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, memberId, Constant.TEXT_NONE, Constant.FILED_BORROW_DATE), memberId, memberName, Constant.IS_ADMINISTRATOR_MODE, Constant.IS_TITLE_OPTION);
            Console.CursorVisible = false;
            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.CURSOR_POS_TOP);
            while (!isInputEscape)
            {
                isInputEscape = DataProcessing.GetDataProcessing().IsOnlyInputEscape();
                if (isInputEscape) //esc 눌렀을때 뒤로가기
                {
                    Console.CursorVisible = true;
                    ShowBorrowBookStatus(administratorScreen);
                }
            }
        }

        //Menu && Mode
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
                    case (int)Constant.AdministratorMenu.BOOK_MODIFY:
                        SelectModifyBookMode(administratorScreen);
                        break;
                    case (int)Constant.AdministratorMenu.MEMBER_MANAGEMENT:
                        SelectManagementMemberMode(administratorScreen);
                        break;
                    case (int)Constant.AdministratorMenu.BORROW_BOOK_STATUS:
                        ShowBorrowBookStatus(administratorScreen);
                        break;
                    case (int)Constant.AdministratorMenu.SEARCH_BY_NAVER:
                        break;
                    case (int)Constant.AdministratorMenu.SHOW_LOG:
                        log.SaveLogToTxt();
                        administratorScreen.PrintLogLabel();
                        log.ShowLogScreen();
                        break;
                    case (int)Constant.AdministratorMenu.RESET_LOG:
                        log.ResetLog();
                        break;
                    case Constant.INPUT_ESCAPE_IN_ARROW_KEY:
                        isLogout = DataProcessing.GetDataProcessing().IsLogout(administratorScreen);
                        break;
                    default:
                        break;
                }
            }
        }
    
        private void SelectModifyBookMode(AdministratorScreen administratorScreen)
        {
            int menuValue;
            isSearchAndModify = Constant.IS_ONLY_SEARCH;
            searchedBookIdList.Clear();
            menuValue = GetModifyBookMode(administratorScreen);
            switch (menuValue)
            {
                case (int)Constant.BookModifyModePosY.IMMEDIATE:
                    SelectModifyBookId(administratorScreen);
                    break;
                case (int)Constant.BookModifyModePosY.SEARCH:
                    isSearchAndModify = Constant.IS_SEARCH_AND_MODIFY;
                    InputBookSearchOption(administratorScreen);
                    break;
                default:
                    break;
            }
        }
    
        private void SelectManagementMemberMode(AdministratorScreen administratorScreen)
        {
            bool isBack = false;
            int menuValue;

            while (!isBack)
            {
                menuValue = GetManagementMemberMode(administratorScreen);
                switch (menuValue)
                {
                    case (int)Constant.MemberManagementModePosY.SEARCH:
                        InputMemberSearchOption(administratorScreen);
                        break;
                    case (int)Constant.MemberManagementModePosY.MODIFY:
                        ManagementMember(administratorScreen);
                        break;
                    case Constant.INPUT_ESCAPE_IN_ARROW_KEY:
                        isBack = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
