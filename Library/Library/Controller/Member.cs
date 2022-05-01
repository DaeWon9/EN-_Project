using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.View;
using Library.Utility;
using Library.Model;

namespace Library.Controller
{
    class Member : MenuSelection // 기능별로 쪼개기... 모듈화...
    {
        private List<string> searchedBookIdList = new List<string>();
        private bool isInputEscape = false, isSearchAndBorrow = false, isWithdrawlCompleted = false;
        private int menuValue;
        private string loginMemberId = "", loginMemberPassword = "", loginMemberName = "", conditionalStringByUserInput = "";
        //Login
        private void Login(MemberScreen memberScreen) // id : admin1    pw: admin1 
        {
            bool isLoginCheck = false;
            loginMemberId = "";
            loginMemberPassword = "";
            memberScreen.PrintLoginScreen();
            while (!isLoginCheck)
            {
                loginMemberId = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.LOGIN_POS_X, Constant.LOGIN_ID_POS_Y, Constant.MAX_LENGTH_MEMBER_ID, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_ID);
                if (loginMemberId == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 뒤로가기
                    break;
                loginMemberPassword = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.LOGIN_POS_X, Constant.LOGIN_PASSWORD_POS_Y, Constant.MAX_LENGTH_MEMBER_PASSWORD, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_PASSWORD, Constant.IS_PASSWORD);
                if (loginMemberPassword == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 뒤로가기
                    break;
                isLoginCheck = IsLogin(memberScreen, loginMemberId, loginMemberPassword);
            }
            if (loginMemberId == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString() || loginMemberPassword == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 처음 회원 or 관리자모드 선택으로 돌아가야함
                return;
            loginMemberName = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_NAME, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, loginMemberId)); // 로그인한 유저의 이름 저장
            DataBase.GetDataBase().AddLog(string.Format(Constant.LOG_MEMBER_TEXT_FORM, loginMemberName, loginMemberId), "로그인");
            SelectMemberMainMenu(memberScreen); // 넘어가는부분
        }

        private bool IsLogin(MemberScreen memberScreen, string id, string password)
        {
            List<string> memberId = DataBase.GetDataBase().GetSelectedElements(Constant.MEMBER_FILED_ID, Constant.TABLE_NAME_MEMBER);
            List<string> memberPassword = DataBase.GetDataBase().GetSelectedElements(Constant.MEMBER_FILED_PASSWORD, Constant.TABLE_NAME_MEMBER);

            for (int repeat = 0; repeat < memberId.Count; repeat++)
            {
                if (id == memberId[repeat] && password == memberPassword[repeat])
                    return true;
            }

            memberScreen.PrintMessage(Constant.TEXT_IS_NOT_CORRECT_ID_PASSWORD, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
            DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_MAX_POS_X, Constant.LOGIN_ID_POS_Y);
            DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_MAX_POS_X, Constant.LOGIN_PASSWORD_POS_Y);
            return false;
        }

        //SignUp
        private bool IsIdDuplicate(string id)
        {
            List<string> memberIdList = DataBase.GetDataBase().GetSelectedElements(Constant.MEMBER_FILED_ID, Constant.TABLE_NAME_MEMBER);
            for (int repeat = 0; repeat < memberIdList.Count; repeat++)
            {
                if (memberIdList[repeat] == id)
                    return true;
            }
            return false;
        }

        private void SignUp(MemberScreen memberScreen)
        {

            int currentConsoleCursorPosY;
            bool isSignUpCompleted = false;
            string name = "", id = "", password = "", passwordCheck = "", age = "", address = "", phoneNumber = "";
            isInputEscape = false;
            Console.CursorVisible = true;
            memberScreen.PrintSignUpScreen();
            Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.SignUpPosY.NAME); //좌표조정

            while (!isInputEscape && !isSignUpCompleted)
            {
                if (IsIdDuplicate(id))
                {
                    memberScreen.PrintMessage(Constant.TEXT_ALREADY_REGISTERED_ID, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    Console.SetCursorPosition(Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.ID); //좌표조정
                    DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.SIGNUP_POS_X, Constant.WINDOW_WIDTH, (int)Constant.SignUpPosY.ID);
                    id = "";
                }

                if ((password.Length > 1 && passwordCheck.Length > 1) && (password != passwordCheck))
                {
                    memberScreen.PrintMessage(Constant.TEXT_IS_NOT_CORRECT_PASSWORD, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    Console.SetCursorPosition(Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.ID); //좌표조정
                    DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.SIGNUP_POS_X, Constant.WINDOW_WIDTH, (int)Constant.SignUpPosY.PASSWORD_CHECK);
                    passwordCheck = "";
                }

                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.SEARCH_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.SignUpPosY.NAME, (int)Constant.SignUpPosY.SIGN_UP);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString()); // 화살표 움직이면서 esc입력했는지 확인
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.SignUpPosY.NAME:
                        name = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.NAME, Constant.MAX_LENGTH_MEMBER_NAME, Constant.TEXT_PLEASE_INPUT_CORRECT_STRING, Constant.EXCEPTION_TYPE_KOREAN, Constant.EXCEPTION_TYPE_MEMBER_NAME);
                        break;
                    case (int)Constant.SignUpPosY.ID:
                        id = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.ID, Constant.MAX_LENGTH_MEMBER_ID, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_ID);
                        break;
                    case (int)Constant.SignUpPosY.PASSWORD:
                        password = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.PASSWORD, Constant.MAX_LENGTH_MEMBER_PASSWORD, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_PASSWORD, Constant.IS_PASSWORD);
                        break;
                    case (int)Constant.SignUpPosY.PASSWORD_CHECK:
                        passwordCheck = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.PASSWORD_CHECK, Constant.MAX_LENGTH_MEMBER_PASSWORD, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_PASSWORD, Constant.IS_PASSWORD);
                        break;
                    case (int)Constant.SignUpPosY.AGE:
                        age = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.AGE, Constant.MAX_LENGTH_MEMBER_AGE, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_AGE);
                        break;
                    case (int)Constant.SignUpPosY.ADDRESS:
                        address = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.ADDRESS, Constant.MAX_LENGTH_MEMBER_ADDRESS, Constant.TEXT_PLEASE_INPUT_KOREAN_OR_NUMBER, Constant.EXCEPTION_TYPE_KOREAN_NUMBER_SPACE, Constant.EXCEPTION_TYPE_MEMBER_ADDRESS);
                        break;
                    case (int)Constant.SignUpPosY.PHONE_NUMBER:
                        phoneNumber = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.PHONE_NUMBER, Constant.MAX_LENGTH_MEMBER_PHONE_NUMBER, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_PHONE_NUMBER);
                        break;
                    case (int)Constant.SignUpPosY.SIGN_UP:
                        isSignUpCompleted = IsSignUpCompleted(memberScreen, name, id, password, age, address, phoneNumber);
                        break;
                    default:
                        break;
                }
            }

        }

        private bool IsSignUpCompleted(MemberScreen memberScreen, string name, string id, string password, string age, string address, string phoneNumber)
        {
            int GetYesOrNoBySignUp;
            // 모든 값이 입력됐는지 체크
            if ((name == "" || name == Constant.INPUT_ESCAPE.ToString()) || (id == "" || id == Constant.INPUT_ESCAPE.ToString()) || (password == "" || password == Constant.INPUT_ESCAPE.ToString()) || (age == "" || age == Constant.INPUT_ESCAPE.ToString()) || (address == "" || address == Constant.INPUT_ESCAPE.ToString()) || (phoneNumber == "" || phoneNumber == Constant.INPUT_ESCAPE.ToString()))
            {
                memberScreen.PrintMessage(Constant.TEXT_PLEASE_INPUT_OPTION, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.SignUpPosY.NAME); //좌표조정
                return false;
            }

            memberScreen.PrintMessage(Constant.TEXT_IS_SIGN_UP, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            memberScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y); //좌표조정
            Console.CursorVisible = false;
            GetYesOrNoBySignUp = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (GetYesOrNoBySignUp == Constant.INPUT_ENTER) // 가입확인문구에서 enter입력
            {
                DataProcessing.GetDataProcessing().ClearErrorMessage();
                DataBase.GetDataBase().InsertMember(Constant.TABLE_NAME_MEMBER, name, id, password, int.Parse(age), address, phoneNumber); // member 테이블에 입력한 멤버정보 추가
                DataBase.GetDataBase().CreateTable(id); // 해당 멤버의 id로 대여도서 테이블 생성 
                memberScreen.PrintMessage(Constant.TEXT_SUCCESS_SIGN_UP, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
                if (DataProcessing.GetDataProcessing().GetEnterOrEscape() != Constant.INPUT_ESCAPE) //esc 눌렀을때 뒤로가기
                {
                    Console.CursorVisible = true;
                    return true;
                }
            }
            if (GetYesOrNoBySignUp == Constant.INPUT_ESCAPE) // 가입확인문구에서 esc입력
            {
                DataProcessing.GetDataProcessing().ClearErrorMessage();
                Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.SignUpPosY.NAME); //좌표조정
                Console.CursorVisible = true;
                return false;
            }
            return true;
        }

        //SearchBook
        private void InputBookSearchOption(MemberScreen memberScreen)
        {
            string bookId = "", bookName = "", bookPublisher = "", bookAuthor = "", bookPrice = "", bookQuantity = "";
            int currentConsoleCursorPosY;
            bool isSearchBookCompleted = false;
            isInputEscape = false;
            Console.CursorVisible = true;

            memberScreen.PrintBookSearchScreen();
            memberScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
            Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.BookSearchPosY.ID); //좌표조정

            while (!isInputEscape && !isSearchBookCompleted)
            {
                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.SEARCH_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.BookSearchPosY.ID, (int)Constant.BookSearchPosY.SEARCH);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.BookSearchPosY.ID:
                        bookId = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.ID, Constant.MAX_LENGTH_BOOK_ID, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_ID);
                        break;
                    case (int)Constant.BookSearchPosY.NAME:
                        bookName = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.NAME, Constant.MAX_LENGTH_BOOK_NAME, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_NAME);
                        break;
                    case (int)Constant.BookSearchPosY.PUBLISHER:
                        bookPublisher = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.PUBLISHER, Constant.MAX_LENGTH_BOOK_PUBLISHER, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_PUBLISHER);
                        break;
                    case (int)Constant.BookSearchPosY.AUTHOR:
                        bookAuthor = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.AUTHOR, Constant.MAX_LENGTH_BOOK_AUTHOR, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER_KOREA, Constant.EXCEPTION_TYPE_BOOK_AUTHOR);
                        break;
                    case (int)Constant.BookSearchPosY.PRICE:
                        bookPrice = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.PRICE, Constant.MAX_LENGTH_BOOK_PRICE, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_PRICE);
                        break;
                    case (int)Constant.BookSearchPosY.QUANTITY:
                        bookQuantity = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.QUANTITY, Constant.MAX_LENGTH_BOOK_QUANTITY, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_QUANTITY);
                        break;
                    case (int)Constant.BookSearchPosY.SEARCH:
                        isSearchBookCompleted = IsSearchBookCompleted(memberScreen, bookId, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity);
                        break;
                    default:
                        break;
                }
            }
            if(isSearchBookCompleted)
                DataBase.GetDataBase().AddLog(string.Format(Constant.LOG_MEMBER_TEXT_FORM, loginMemberName, loginMemberId), "도서검색");
        }

        private bool IsSearchBookCompleted(MemberScreen memberScreen, string bookId, string bookName, string bookPublisher, string bookAuthor, string bookPrice, string bookQuantity)
        {
            int getYesOrNoBySearching, getYesOrNoByResearching;

            // 입력값이 모두 공백인지 체크
            if ((bookId == "" || bookId == Constant.INPUT_ESCAPE.ToString()) && (bookName == "" || bookName == Constant.INPUT_ESCAPE.ToString()) && (bookPublisher == "" || bookPublisher == Constant.INPUT_ESCAPE.ToString()) && (bookAuthor == "" || bookAuthor == Constant.INPUT_ESCAPE.ToString()) && (bookPrice == "" || bookPrice == Constant.INPUT_ESCAPE.ToString()) && (bookQuantity == "" || bookQuantity == Constant.INPUT_ESCAPE.ToString()))
            {
                memberScreen.PrintMessage(Constant.TEXT_PLEASE_INPUT_OPTION, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.BookSearchPosY.ID); //좌표조정
                return false;
            }
            conditionalStringByUserInput = DataProcessing.GetDataProcessing().GetConditionalStringBySearchBook(bookId, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity);

            memberScreen.PrintMessage(Constant.TEXT_IS_SEARCH, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            memberScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            getYesOrNoBySearching = DataProcessing.GetDataProcessing().GetEnterOrEscape();

            if (getYesOrNoBySearching == Constant.INPUT_ENTER && isSearchAndBorrow == Constant.IS_ONLY_SEARCH) // 검색만
            {
                memberScreen.PrintSearchResultScreen();
                memberScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK, conditionalStringByUserInput), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
                Console.SetCursorPosition(0, 0); // 출력되는 자료가 많아서 화면이 내려갈 수 있어 최상단으로 커서 옮기기
                Console.CursorVisible = false;
                getYesOrNoByResearching = DataProcessing.GetDataProcessing().GetEnterOrEscape();
                if (getYesOrNoByResearching == Constant.INPUT_ENTER)
                    InputBookSearchOption(memberScreen);
            }

            if (getYesOrNoBySearching == Constant.INPUT_ENTER && isSearchAndBorrow == Constant.IS_SEARCH_AND_BORROW) // 검색 후 대여까지하는 함수 -> 여기서 검색된 도서 id 리스트 만들어서 대여할때 중복체크하기
            {
                memberScreen.PrintBorrowBookScreen(); // 도서 대여 UI 출력
                memberScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK, DataProcessing.GetDataProcessing().GetConditionalStringBySearchBook(bookId, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity)), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
                searchedBookIdList = DataBase.GetDataBase().GetSelectedElements(Constant.BOOK_FILED_ID, Constant.TABLE_NAME_BOOK, DataProcessing.GetDataProcessing().GetConditionalStringBySearchBook(bookId, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity));
                BorrowBookImmediately(memberScreen);
            }

            if (getYesOrNoBySearching == Constant.INPUT_ESCAPE)
            {
                memberScreen.PrintBookSearchScreen();
                memberScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
                Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
                Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.BookSearchPosY.ID); //좌표조정
                return false;
            }
            return true;
        }

        // BorrowBook
        private void CheckBorrowedBook(MemberScreen memberScreen)
        {
            isInputEscape = false;
            memberScreen.PrintBorrowedBookListScreen();
            memberScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, loginMemberId.ToString(), Constant.TEXT_NONE, Constant.FILED_BORROW_DATE), loginMemberId.ToString(), Constant.TEXT_NONE); // 대여도서확인시 테이블명은 각 유저의 id임
            Console.CursorVisible = false;
            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.CURSOR_POS_TOP);
            while (!isInputEscape)
            {
                isInputEscape = DataProcessing.GetDataProcessing().IsOnlyInputEscape();
                if (isInputEscape) //esc 눌렀을때 뒤로가기
                    Console.CursorVisible = true;
            }
        }

        private bool IsReBorrow(MemberScreen memberScreen)
        {
            int getYesOrNoByReborrowing;
            DataProcessing.GetDataProcessing().ClearErrorMessage();
            memberScreen.PrintMessage(Constant.TEXT_SUCCESS_BORROW, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            memberScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            getYesOrNoByReborrowing = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (getYesOrNoByReborrowing == Constant.INPUT_ENTER) // 계속해서 대여하시겠습니까? 에서 enter입력
            {
                if (isSearchAndBorrow == Constant.IS_SEARCH_AND_BORROW)
                {
                    memberScreen.PrintBorrowBookScreen(); // 도서 대여 UI 출력
                    memberScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK, conditionalStringByUserInput), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
                }
                else
                {
                    memberScreen.PrintBorrowBookScreen(); // 도서 대여 UI 출력
                    memberScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE); // 도서관에 보유중인 책 정보 표시
                }
                Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
                Console.SetCursorPosition(Constant.BORROW_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookBorrowPosY.ID); // 좌표조정
                return false;
            }
            if (getYesOrNoByReborrowing == Constant.INPUT_ESCAPE) // 계속해서 대여하시겠습니까? 에서 esc입력
                return true;

            return false;
        }

        private bool IsBorrowBookCompleted(MemberScreen memberScreen, string bookId)
        {
            int getYesOrNoByBorrowing, checkInsertBorrowedBook = 0;

            if ((bookId == "" || bookId == Constant.INPUT_ESCAPE.ToString()))// 입력값이 공백인지 체크
            {
                memberScreen.PrintMessage(Constant.TEXT_PLEASE_INPUT_OPTION, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.BORROW_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookBorrowPosY.ID); //좌표조정
                return false; // 다시입력받기
            }
            ////대여하기
            DataProcessing.GetDataProcessing().ClearErrorMessage();
            memberScreen.PrintMessage(Constant.TEXT_IS_BORROW, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            memberScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);

            getYesOrNoByBorrowing = DataProcessing.GetDataProcessing().GetEnterOrEscape();

            if (getYesOrNoByBorrowing == Constant.INPUT_ENTER) // 대여하기 확인 문구에서 엔터 눌림
            {
                checkInsertBorrowedBook = DataBase.GetDataBase().GetBookBorrowResult(loginMemberId, int.Parse(bookId), searchedBookIdList); // 사용자의 개별 테이블에 대여도서 정보 등록하고 결과값 리턴
                switch (checkInsertBorrowedBook)
                {
                    case (int)Constant.CheckInsertBorrowedBook.NOT_EXIST_BOOK:
                        DataProcessing.GetDataProcessing().ClearErrorMessage();
                        memberScreen.PrintMessage(Constant.TEXT_IS_NOT_EXIST_IN_LIBRARY_OR_NOT_SEARCHED, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                        break;
                    case (int)Constant.CheckInsertBorrowedBook.DUPLICATE_BOOK_ID:
                        DataProcessing.GetDataProcessing().ClearErrorMessage();
                        memberScreen.PrintMessage(Constant.TEXT_IS_ALREADY_BORROWED, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                        break;
                    case (int)Constant.CheckInsertBorrowedBook.SHORTAGE_BOOK_QUANTITY:
                        DataProcessing.GetDataProcessing().ClearErrorMessage();
                        memberScreen.PrintMessage(Constant.TEXT_IS_NOT_ENOUGH_QUANTITY, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                        break;
                    case (int)Constant.CheckInsertBorrowedBook.SUCCESS:
                        return IsReBorrow(memberScreen);
                    default:
                        break;
                }
            }
            if (getYesOrNoByBorrowing == Constant.INPUT_ESCAPE)
            {
                memberScreen.PrintBorrowBookScreen(); // 도서 대여 UI 출력
                memberScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE); // 도서관에 보유중인 책 정보 표시
                Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
                Console.SetCursorPosition(Constant.BORROW_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookBorrowPosY.ID); //좌표조정
                return false;
            }

            if (checkInsertBorrowedBook == (int)Constant.CheckInsertBorrowedBook.NOT_EXIST_BOOK || checkInsertBorrowedBook == (int)Constant.CheckInsertBorrowedBook.DUPLICATE_BOOK_ID || checkInsertBorrowedBook == (int)Constant.CheckInsertBorrowedBook.SHORTAGE_BOOK_QUANTITY)
            {
                Console.SetCursorPosition(Constant.BORROW_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookBorrowPosY.ID); // 좌표조정
                return false;
            }
            return true;
        }

        private void BorrowBookImmediately(MemberScreen memberScreen)
        {
            string bookId = "";
            int currentConsoleCursorPosY;
            bool isBorrowBookCompleted = false;
            isInputEscape = false;
            Console.CursorVisible = true;

            if (isSearchAndBorrow == Constant.IS_SEARCH_AND_BORROW)
            {
                memberScreen.PrintBorrowBookScreen(); // 도서 대여 UI 출력
                memberScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK, conditionalStringByUserInput), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
            }
            else
            {
                memberScreen.PrintBorrowBookScreen(); // 도서 대여 UI 출력
                memberScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE); // 도서관에 보유중인 책 정보 표시
            }
            Console.SetCursorPosition(0, 0);      //대여창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.BORROW_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookBorrowPosY.ID); //좌표조정

            while (!isInputEscape && !isBorrowBookCompleted)
            {
                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.SEARCH_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.BookBorrowPosY.ID, (int)Constant.BookBorrowPosY.BORROW);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.BookBorrowPosY.ID:
                        bookId = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.ID, Constant.MAX_LENGTH_BOOK_ID, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_ID);
                        break;
                    case (int)Constant.BookBorrowPosY.BORROW:
                        isBorrowBookCompleted = IsBorrowBookCompleted(memberScreen, bookId);
                        break;
                    default:
                        break;
                }
            }
            if (isSearchAndBorrow)
                InputBookSearchOption(memberScreen);
        }

        //ReturnBook
        private void ReturnBook(MemberScreen memberScreen)
        {
            isInputEscape = false;
            bool isRetunrBookCompleted = false;
            int currentConsoleCursorPosY;
            string bookId = "";

            memberScreen.PrintReturnBookScreen();
            memberScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, loginMemberId.ToString(), Constant.TEXT_NONE, Constant.FILED_BORROW_DATE), loginMemberId.ToString(), Constant.TEXT_NONE); // 대여도서확인시 테이블명은 각 유저의 id임
            Console.SetCursorPosition(0, 0);      //대여창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.BORROW_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookBorrowPosY.ID); //좌표조정

            while (!isInputEscape && !isRetunrBookCompleted)
            {
                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.SEARCH_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.BookBorrowPosY.ID, (int)Constant.BookBorrowPosY.BORROW);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.BookReturnPosY.ID:
                        bookId = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.ID, Constant.MAX_LENGTH_BOOK_ID, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_ID);
                        break;
                    case (int)Constant.BookReturnPosY.RETURN:
                        isRetunrBookCompleted = IsReturnBookCompleted(memberScreen, bookId);
                        break;
                    default:
                        break;
                }
            }
        }

        private bool IsBorrowedBookIdListContainReturnBookId(string returnBookId)
        {
            List<string> memberBorrowedBookIdList = DataBase.GetDataBase().GetSelectedElements(Constant.BOOK_FILED_ID, loginMemberId);
            for (int repeat = 0; repeat < memberBorrowedBookIdList.Count; repeat++)
            {
                if (memberBorrowedBookIdList[repeat] == returnBookId)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsReturnBookCompleted(MemberScreen memberScreen, string returnBookId) // 반납시 해당유저의 대여도서 목록에서는 제거 & 도서관 책정보에서는 수량 + 1
        {
            int getYesOrNoByReturn, getYesOrNoByReturnAgain;

            if ((returnBookId == "" || returnBookId == Constant.INPUT_ESCAPE.ToString()))// 입력값이 공백인지 체크
            {
                memberScreen.PrintMessage(Constant.TEXT_PLEASE_INPUT_OPTION, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.BORROW_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookBorrowPosY.ID); //좌표조정
                return false; // 다시입력받기
            }

            DataProcessing.GetDataProcessing().ClearErrorMessage();
            memberScreen.PrintMessage(Constant.TEXT_IS_RETURN, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            memberScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);

            getYesOrNoByReturn = DataProcessing.GetDataProcessing().GetEnterOrEscape();

            if (getYesOrNoByReturn == Constant.INPUT_ENTER) // 반납하기 확인 문구에서 엔터 눌림
            {
                if (IsBorrowedBookIdListContainReturnBookId(returnBookId)) // 반납하고자하는 도서아이디가 대여중인 도서목록에 있다면
                { //반납하는 쿼리문실행 -> 유저의 대여도서목록에서는  delete, 도서관 보유 책수량은 1플러스
                    DataBase.GetDataBase().Delete(loginMemberId, String.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, returnBookId));
                    DataBase.GetDataBase().PlusBookQuantity(int.Parse(returnBookId));
                    DataProcessing.GetDataProcessing().ClearErrorMessage();
                    memberScreen.PrintMessage(Constant.TEXT_SUCCESS_RETURN_BOOK, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
                    memberScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow); // 계속해서 반납의사 물어보는 문구
                    getYesOrNoByReturnAgain = DataProcessing.GetDataProcessing().GetEnterOrEscape(); // enter or esc받을때까지 입력받음
                    if (getYesOrNoByReturnAgain == Constant.INPUT_ENTER) // 계속해서 반납하기 -> 즉 반납이 끝나지 않음
                    {
                        memberScreen.PrintReturnBookScreen();
                        memberScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, loginMemberId.ToString(), Constant.TEXT_NONE, Constant.FILED_BORROW_DATE), loginMemberId.ToString(), Constant.TEXT_NONE); // 대여도서확인시 테이블명은 각 유저의 id임
                        Console.SetCursorPosition(0, 0);      //대여창 보이게 맨위로 올리고 
                        Console.SetCursorPosition(Constant.BORROW_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookBorrowPosY.ID); //좌표조정
                        return false;
                    }
                    if (getYesOrNoByReturnAgain == Constant.INPUT_ESCAPE) // 그만 반납하기 -> 즉 반납이 끝남
                        return true;
                }
                else // 대여중인 도서목록에 반납할도서가 없다면. 반납 실패.
                {
                    DataProcessing.GetDataProcessing().ClearErrorMessage();
                    memberScreen.PrintMessage(Constant.TEXT_IS_NOT_BORROWED_BOOK, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    Console.SetCursorPosition(Constant.BORROW_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookBorrowPosY.ID); //좌표조정
                    return false;
                }
            }
            return true;
        }

        // ModifyInformation
        private string GetStringByUpdate(string setStringForm, string filed, string inputValue)
        {
            string resultString = "";
            if (inputValue != "" && inputValue != Constant.INPUT_ESCAPE.ToString())
                resultString = string.Format(setStringForm, filed, inputValue);
            return resultString;
        }
        
        private bool IsModifyMemberInformationCompleted(MemberScreen memberScreen, string setString)
        {
            int getYesOrNoByModify;
            memberScreen.PrintMessage(Constant.TEXT_IS_MODIFY, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            memberScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            getYesOrNoByModify = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (getYesOrNoByModify == Constant.INPUT_ENTER) // 변경하시겠습니까? 에서 enter입력
            {
                DataBase.GetDataBase().Update(Constant.TABLE_NAME_MEMBER, setString, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_NAME, loginMemberName));
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

        private bool IsReModify(MemberScreen memberScreen)
        {
            int getYesOrNoByReModify;
            memberScreen.PrintMessage(Constant.TEXT_SUCCESS_MODIFY, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            memberScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            getYesOrNoByReModify = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (getYesOrNoByReModify == Constant.INPUT_ESCAPE) // 계속해서 변경 x
                return false;
            return true;
        }

        private void ModifyMemberInformation(MemberScreen memberScreen) // memberId 는 primaryKey 이므로 수정불가능하게 설정
        {
            string setStringByUpdate = "";
            string memberName = "", memberPassword = "", memberAge = "", memberAddress = "", memberPhoneNumber = "";
            bool isModifyCompleted = false;
            isWithdrawlCompleted = false;
            isInputEscape = false;
            int currentConsoleCursorPosY;
            memberScreen.PrintModifyMemberInformationLabel();
            memberScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_MEMBER, String.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, loginMemberId)), Constant.TABLE_NAME_MEMBER, Constant.TEXT_NONE);
            memberScreen.PrintModifyMemberInformationScreen();
            Console.SetCursorPosition(Constant.MODIFY_MEMBER_SELECT_OPTION_POS_X, (int)Constant.MemberModifyModePosY.NAME); //좌표조정

            while (!isInputEscape && !isModifyCompleted && !isWithdrawlCompleted)
            {
                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.MODIFY_MEMBER_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.MemberModifyModePosY.NAME, (int)Constant.MemberModifyModePosY.WITHDRAWAL);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.MemberModifyModePosY.NAME:
                        memberName = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.MODIFY_MEMBER_INPUT_POS_X, (int)Constant.MemberModifyModePosY.NAME, Constant.MAX_LENGTH_MEMBER_NAME, Constant.TEXT_PLEASE_INPUT_CORRECT_STRING, Constant.EXCEPTION_TYPE_KOREAN, Constant.EXCEPTION_TYPE_MEMBER_NAME);
                        setStringByUpdate = GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.MEMBER_FILED_NAME, memberName);
                        break;
                    case (int)Constant.MemberModifyModePosY.PASSWORD:
                        memberPassword = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.MODIFY_MEMBER_INPUT_POS_X, (int)Constant.MemberModifyModePosY.PASSWORD, Constant.MAX_LENGTH_MEMBER_PASSWORD, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_PASSWORD);
                        setStringByUpdate = GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.MEMBER_FILED_PASSWORD, memberPassword);
                        break;
                    case (int)Constant.MemberModifyModePosY.AGE:
                        memberAge = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.MODIFY_MEMBER_INPUT_POS_X, (int)Constant.MemberModifyModePosY.AGE, Constant.MAX_LENGTH_MEMBER_AGE, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_AGE);
                        setStringByUpdate = GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.MEMBER_FILED_AGE, memberAge);
                        break;
                    case (int)Constant.MemberModifyModePosY.ADDRESS:
                        memberAddress = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.MODIFY_MEMBER_INPUT_POS_X, (int)Constant.MemberModifyModePosY.ADDRESS, Constant.MAX_LENGTH_MEMBER_ADDRESS, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_MEMBER_ADDRESS);
                        setStringByUpdate = GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.MEMBER_FILED_ADDRESS, memberAddress);
                        break;
                    case (int)Constant.MemberModifyModePosY.PHONE_NUMBER:
                        memberPhoneNumber = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.MODIFY_MEMBER_INPUT_POS_X, (int)Constant.MemberModifyModePosY.PHONE_NUMBER, Constant.MAX_LENGTH_MEMBER_PHONE_NUMBER, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_PHONE_NUMBER);
                        setStringByUpdate = GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.MEMBER_FILED_PHONE_NUMBER, memberPhoneNumber);
                        break;
                    case (int)Constant.MemberModifyModePosY.WITHDRAWAL:
                        isWithdrawlCompleted = IsWithdrawlCompleted(memberScreen);
                        break;
                    default:
                        break;
                }

                if (setStringByUpdate != "")
                {
                    isModifyCompleted = IsModifyMemberInformationCompleted(memberScreen, setStringByUpdate);
                    if (memberName != "" && memberName != Constant.INPUT_ESCAPE.ToString()) // 이름 변경이 입력되었을때 로그인되어있는 유저의 이름 변수 수정
                        loginMemberName = memberName;
                    if (memberPassword != "" && memberPassword != Constant.INPUT_ESCAPE.ToString()) // 비밀번호 변경이 입력되었을때 로그인되어있는 유저의 비밀번호 변수 수정
                        loginMemberPassword = memberPassword;

                    if (isModifyCompleted && IsReModify(memberScreen)) // 계속해서 변경
                        ModifyMemberInformation(memberScreen);
                }
            }

        }
        
        // Withdrawl
        private bool IsWithdrawlCompleted(MemberScreen memberScreen)
        {
            int getYesOrNoByWithdrawl;
            if (IsMemberNotReturnBorrowedBook()) // 반납안한 책이 있음
            {
                memberScreen.PrintMessage(Constant.TEXT_UNABLE_WITHDRAWAL, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.MODIFY_MEMBER_SELECT_OPTION_POS_X, (int)Constant.MemberModifyModePosY.NAME); //좌표조정
                return false;
            }
            else // 클린한 상태임 -> 회원탈퇴 가능
            {
                memberScreen.PrintMessage(Constant.TEXT_IS_WITHDRAWAL, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Red); // 정말로 탈퇴할건지 물어보기
                memberScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);

                getYesOrNoByWithdrawl = DataProcessing.GetDataProcessing().GetEnterOrEscape();
                if (getYesOrNoByWithdrawl == Constant.INPUT_ENTER) // 탈퇴진행
                {
                    DataBase.GetDataBase().Drop(loginMemberId); // 회원아이디로 된 테이블 drop
                    DataBase.GetDataBase().Delete(Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, loginMemberId));
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

        private bool IsMemberNotReturnBorrowedBook()
        {

            List<string> BorrowedBookList = DataBase.GetDataBase().GetSelectedElements(Constant.BOOK_FILED_ID, loginMemberId);
            if (BorrowedBookList.Count > 0) // 반납안한 책이 있음 
                return true;
            return false;
        }

        // menu && mode
        public void SelectLoginOrSignUp(MemberScreen memberScreen)
        {
            menuValue = GetMemberLoginOrSignUp(memberScreen);
            switch (menuValue)
            {
                case Constant.MODE_MEMBER_LOGIN:
                    Login(memberScreen);
                    break;
                case Constant.MODE_MEMBER_SIGN_UP:
                    SignUp(memberScreen);
                    break;
                default:
                    break;
            }
        }

        private void SelectMemberMainMenu(MemberScreen memberScreen)
        {
            bool isLogout = false;
            while (!isLogout && !isWithdrawlCompleted)
            {
                menuValue = GetMemberMenu(memberScreen, string.Format(Constant.TEXT_WELCOME, loginMemberName));
                switch (menuValue)
                {
                    case (int)Constant.MemberMenu.BOOK_SEARCH:
                        isSearchAndBorrow = false;
                        InputBookSearchOption(memberScreen);
                        break;
                    case (int)Constant.MemberMenu.BOOK_BORROW:
                        SelectBorrowBookMode(memberScreen);
                        break;
                    case (int)Constant.MemberMenu.BOOK_RETURN:
                        ReturnBook(memberScreen);
                        break;
                    case (int)Constant.MemberMenu.BOOK_CHECK:
                        CheckBorrowedBook(memberScreen);
                        break;
                    case (int)Constant.MemberMenu.MODIFY_MEMBER_INFORMATION:
                        ModifyMemberInformation(memberScreen);
                        break;
                    case Constant.INPUT_ESCAPE_IN_ARROW_KEY:
                        isLogout = DataProcessing.GetDataProcessing().IsLogout(memberScreen);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SelectBorrowBookMode(MemberScreen memberScreen)
        {
            isSearchAndBorrow = Constant.IS_ONLY_SEARCH;
            searchedBookIdList.Clear();
            menuValue = GetBorrowBookMode(memberScreen);
            switch (menuValue)
            {
                case (int)Constant.BookBorrowModePosY.IMMEDIATE:
                    BorrowBookImmediately(memberScreen);
                    break;
                case (int)Constant.BookBorrowModePosY.SEARCH:
                    isSearchAndBorrow = Constant.IS_SEARCH_AND_BORROW;
                    InputBookSearchOption(memberScreen);
                    break;
                default:
                    break;
            }
        }
    }
}
