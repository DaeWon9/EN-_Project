﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.View;
using Library.Utility;
using Library.Model;

namespace Library.Controller
{
    class Member : MenuSelection
    {
        private bool isInputEscape = false;
        private int menuValue;
        private string id = "", password = "";
        private string loginedMemberName = "";
        private void Login(MemberScreen memberScreen) // id : admin1    pw: admin1 
        {
            bool isLoginCheck = false;
            id = "";
            password = "";
            memberScreen.PrintLoginScreen();
            while (!isLoginCheck)
            {
                id = DataProcessing.Instance.GetInputValues(memberScreen, Constant.LOGIN_POS_X, Constant.LOGIN_ID_POS_Y, Constant.MAX_LENGTH_ID, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_ID);
                if (id == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 뒤로가기
                    break;
                password = DataProcessing.Instance.GetInputValues(memberScreen, Constant.LOGIN_POS_X, Constant.LOGIN_PASSWORD_POS_Y, Constant.MAX_LENGTH_PASSWORD, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_PASSWORD, Constant.IS_PASSWORD);
                if (password == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 뒤로가기
                    break;
                isLoginCheck = IsLogin(memberScreen, id, password);
            }
            if (id == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString() || password == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 처음 회원 or 관리자모드 선택으로 돌아가야함
                return;
            loginedMemberName = DataBase.Instance.GetSelectedElement(Constant.MEMBER_FILED_NAME, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_BY_STRING, Constant.MEMBER_FILED_ID, id)); // 로그인한 유저의 이름 저장
            SelectMemberMainMenu(memberScreen); // 넘어가는부분
        }

        private bool IsLogin(MemberScreen memberScreen, string id, string password)
        {
            List<string> memberId = DataBase.Instance.GetSelectedElements(Constant.MEMBER_FILED_ID, Constant.TABLE_NAME_MEMBER);
            List<string> memberPassword = DataBase.Instance.GetSelectedElements(Constant.MEMBER_FILED_PASSWORD, Constant.TABLE_NAME_MEMBER);

            for (int repeat = 0; repeat < memberId.Count; repeat++)
            {
                if (id == memberId[repeat] && password == memberPassword[repeat])
                    return true;
            }

            memberScreen.PrintMessage(Constant.TEXT_IS_NOT_CORRECT_ID_PASSWORD, Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
            DataProcessing.Instance.ClearConsoleLine(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_MAX_POS_X, Constant.LOGIN_ID_POS_Y);
            DataProcessing.Instance.ClearConsoleLine(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_MAX_POS_X, Constant.LOGIN_PASSWORD_POS_Y);
            return false;
        }

        private bool IsIdDuplicate(string id)
        {
            List<string> memberIdList = DataBase.Instance.GetSelectedElements(Constant.MEMBER_FILED_ID, Constant.TABLE_NAME_MEMBER);
            for (int repeat = 0; repeat < memberIdList.Count; repeat++)
            {
                if (memberIdList[repeat] == id)
                    return true;
            }
            return false;
        }
        
        private bool IsPasswordCorrect(string password, string passwordCheck)
        {
            if (password == passwordCheck)
                return true;
            return false;
        }

        private void SignUp(MemberScreen memberScreen)
        {
            int GetYesOrNoBySignUp;
            bool isSignUpCheck = false, isIdDuplicate, isPasswordCorrect;
            string name = "", id = "", password = "", passwordCheck = "", age = "", address = "", phoneNumber = "";
            memberScreen.PrintSignUpScreen();
            while (!isSignUpCheck)
            {
                // 이름 입력부
                name = DataProcessing.Instance.GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.NAME, Constant.MAX_LENGTH_NAME, Constant.TEXT_PLEASE_INPUT_CORRECT_STRING, Constant.EXCEPTION_TYPE_KOREA, Constant.EXCEPTION_TYPE_NAME);
                if (isInputEscape = DataProcessing.Instance.IsInputEscape(name))
                    break;

                // 중복검사하면서 아이디 입력부
                isIdDuplicate = true;
                while (isIdDuplicate && !isInputEscape)
                {
                    id = DataProcessing.Instance.GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.ID, Constant.MAX_LENGTH_ID, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_ID);
                    isInputEscape = DataProcessing.Instance.IsInputEscape(id);
                    if (isIdDuplicate = IsIdDuplicate(id))
                        memberScreen.PrintMessage(Constant.TEXT_ALREADY_REGISTERED_ID, Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                }
                if (isInputEscape)
                    break;

                password = DataProcessing.Instance.GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.PASSWORD, Constant.MAX_LENGTH_PASSWORD, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_PASSWORD, Constant.IS_PASSWORD);
                if (isInputEscape = DataProcessing.Instance.IsInputEscape(password))
                    break;

                isPasswordCorrect = false;
                while (!isPasswordCorrect && !isInputEscape)
                {
                    passwordCheck = DataProcessing.Instance.GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.PASSWORD_CHECK, Constant.MAX_LENGTH_PASSWORD, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_PASSWORD, Constant.IS_PASSWORD);
                    isInputEscape = DataProcessing.Instance.IsInputEscape(passwordCheck);
                    isPasswordCorrect = IsPasswordCorrect(password, passwordCheck);
                    if (!isPasswordCorrect)
                        memberScreen.PrintMessage(Constant.TEXT_IS_NOT_CORRECT_PASSWORD, Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                }
                if (isInputEscape)
                    break;

                age = DataProcessing.Instance.GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.AGE, Constant.MAX_LENGTH_AGE, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_AGE);
                if (isInputEscape = DataProcessing.Instance.IsInputEscape(age))
                    break;

                address = DataProcessing.Instance.GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.ADDRESS, Constant.MAX_LENGTH_ADDRESS, Constant.TEXT_PLEASE_INPUT_KOREAN_OR_NUMBER, Constant.EXCEPTION_TYPE_KOREAN_NUMBER_SPACE, Constant.EXCEPTION_TYPE_ADDRESS);
                if (isInputEscape = DataProcessing.Instance.IsInputEscape(address))
                    break;

                phoneNumber = DataProcessing.Instance.GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.PHONE_NUMBER, Constant.MAX_LENGTH_PHONE_NUMBER, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_PHONE_NUMBER);
                if (isInputEscape = DataProcessing.Instance.IsInputEscape(phoneNumber))
                    break;

                memberScreen.PrintMessage(Constant.TEXT_IS_SIGN_UP, Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
                memberScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.YES_OR_NO_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
                GetYesOrNoBySignUp = DataProcessing.Instance.GetEnterOrEscape();
                if (GetYesOrNoBySignUp == Constant.INPUT_ENTER) // 가입확인문구에서 enter입력
                    isSignUpCheck = true;
                if (GetYesOrNoBySignUp == Constant.INPUT_ESCAPE) // 가입확인문구에서 enter입력
                {
                    isSignUpCheck = false;
                    SignUp(memberScreen);
                }
            }
            if (isInputEscape)
                SelectLoginOrSignUp(memberScreen);

            if (isSignUpCheck)
            {
                DataBase.Instance.InsertMember(Constant.TABLE_NAME_MEMBER, name, id, password, int.Parse(age), address, phoneNumber); // member 테이블에 입력한 멤버정보 추가
                DataBase.Instance.CreateTable(id); // 해당 멤버의 id로 대여도서 확인 테이블 생성 2022.04.23
                DataProcessing.Instance.ClearConsoleLine(Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_MAX_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y -1);
                DataProcessing.Instance.ClearConsoleLine(Constant.YES_OR_NO_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_MAX_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y);
                memberScreen.PrintMessage(Constant.TEXT_SUCCESS_SIGN_UP, Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
                Console.CursorVisible = false;
                if (DataProcessing.Instance.GetEnterOrEscape() != Constant.INPUT_ESCAPE) //esc 눌렀을때 뒤로가기
                {
                    Console.CursorVisible = true;
                    SelectLoginOrSignUp(memberScreen);
                }
            }
        }

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

        private void InputBookSearchOption(MemberScreen memberScreen)
        {
            string bookId = "", bookName = "", bookPublisher = "", bookAuthor = "", bookPrice = "", bookQuantity = "";
            int currentConsoleCursorPosY;
            bool isSearchBookCompleted = false;
            isInputEscape = false;
            Console.CursorVisible = true;

            memberScreen.PrintBookSearchScreen();
            memberScreen.PrintSelectedValues(DataBase.Instance.Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK);
            Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.BookSearchPosY.ID); //좌표조정

            while (!isInputEscape && !isSearchBookCompleted)
            {
                currentConsoleCursorPosY = DataProcessing.Instance.CursorMove(Constant.SEARCH_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.BookSearchPosY.ID, (int)Constant.BookSearchPosY.SEARCH);
                isInputEscape = DataProcessing.Instance.IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.BookSearchPosY.ID:
                        bookId = DataProcessing.Instance.GetInputValues(memberScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.ID, Constant.MAX_LENGTH_BOOK_ID, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_ID);
                        break;
                    case (int)Constant.BookSearchPosY.NAME:
                        bookName = DataProcessing.Instance.GetInputValues(memberScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.NAME, Constant.MAX_LENGTH_BOOK_NAME, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_NAME);
                        break;
                    case (int)Constant.BookSearchPosY.PUBLISHER:
                        bookPublisher = DataProcessing.Instance.GetInputValues(memberScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.PUBLISHER, Constant.MAX_LENGTH_BOOK_PUBLISHER, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_PUBLISHER);
                        break;
                    case (int)Constant.BookSearchPosY.AUTHOR:
                        bookAuthor = DataProcessing.Instance.GetInputValues(memberScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.AUTHOR, Constant.MAX_LENGTH_BOOK_AUTHOR, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER_KOREA, Constant.EXCEPTION_TYPE_BOOK_AUTHOR);
                        break;
                    case (int)Constant.BookSearchPosY.PRICE:
                        bookPrice = DataProcessing.Instance.GetInputValues(memberScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.PRICE, Constant.MAX_LENGTH_BOOK_PRICE, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_PRICE);
                        break;
                    case (int)Constant.BookSearchPosY.QUANTITY:
                        bookQuantity = DataProcessing.Instance.GetInputValues(memberScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.QUANTITY, Constant.MAX_LENGTH_BOOK_QUANTITY, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_QUANTITY);
                        break;
                    case (int)Constant.BookSearchPosY.SEARCH:
                        isSearchBookCompleted = IsSearchBookCompleted(memberScreen, bookId, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity);
                        break;
                    default:
                        break;
                }
            }
            if (!isSearchBookCompleted)
                SelectMemberMainMenu(memberScreen);
        }
        
        private bool IsSearchBookCompleted(MemberScreen memberScreen, string bookId, string bookName, string bookPublisher, string bookAuthor, string bookPrice, string bookQuantity)
        {
            int getYesOrNoBySearching, getYesOrNoByResearching; // 아래 문자열 기능완성 후 constant로 빼기

            if ((bookId == "" || bookId == Constant.INPUT_ESCAPE.ToString()) && (bookName == "" || bookName == Constant.INPUT_ESCAPE.ToString()) && (bookPublisher == "" || bookPublisher == Constant.INPUT_ESCAPE.ToString()) && (bookAuthor == "" || bookAuthor == Constant.INPUT_ESCAPE.ToString()) && (bookPrice == "" || bookPrice == Constant.INPUT_ESCAPE.ToString()) && (bookQuantity == "" || bookQuantity == Constant.INPUT_ESCAPE.ToString()))
            {
                memberScreen.PrintMessage(Constant.TEXT_PLEASE_INPUT_OPTION, Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.BookSearchPosY.ID); //좌표조정
                return false;
            }
            memberScreen.PrintMessage(Constant.TEXT_IS_SEARCH, Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            memberScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.YES_OR_NO_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            getYesOrNoBySearching = DataProcessing.Instance.GetEnterOrEscape();

            if (getYesOrNoBySearching == Constant.INPUT_ENTER)
            {
                memberScreen.PrintSearchResultScreen();
                memberScreen.PrintSelectedValues(DataBase.Instance.Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK, DataProcessing.Instance.GetConditionalStringBySearchBook(bookId, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity)), Constant.TABLE_NAME_BOOK);
                Console.SetCursorPosition(0, 0); // 출력되는 자료가 많아서 화면이 내려갈 수 있어 최상단으로 커서 옮기기
                Console.CursorVisible = false;
                getYesOrNoByResearching = DataProcessing.Instance.GetEnterOrEscape();
                if (getYesOrNoByResearching == Constant.INPUT_ENTER)
                    InputBookSearchOption(memberScreen);
                if (getYesOrNoByResearching == Constant.INPUT_ESCAPE)
                    SelectMemberMainMenu(memberScreen);
            }
            if (getYesOrNoBySearching == Constant.INPUT_ESCAPE)
            {
                memberScreen.PrintBookSearchScreen();
                memberScreen.PrintSelectedValues(DataBase.Instance.Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK);
                Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
                Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.BookSearchPosY.ID); //좌표조정
                return false;
            }
            return true;
        }

        private void CheckBorrowedBook(MemberScreen memberScreen)
        {
            memberScreen.PrintBorrowedBookListScreen();
            memberScreen.PrintSelectedValues(DataBase.Instance.Select(Constant.FILED_ALL, id.ToString()), id.ToString()); // 대여도서확인시 테이블명은 각 유저의 id임
            Console.CursorVisible = false;
            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.CURSOR_POS_TOP);
            if (DataProcessing.Instance.GetEnterOrEscape() == Constant.INPUT_ESCAPE) //esc 눌렀을때 뒤로가기
            {
                Console.CursorVisible = true;
                //뒤로갔을때 넘어가지는 부분 설정하기
            }
        }

        private void SelectMemberMainMenu(MemberScreen memberScreen)
        {
            while (true) // 안에 변수로 바꾸기 멤버모드 기능 완성 후
            {
                menuValue = GetMemberMenu(memberScreen, string.Format(Constant.TEXT_WELCOME, loginedMemberName));
                switch (menuValue)
                {
                    case (int)Constant.MemberMenu.BOOK_SEARCH:
                        InputBookSearchOption(memberScreen);
                        break;
                    case (int)Constant.MemberMenu.BOOK_RENTAL:
                        break;
                    case (int)Constant.MemberMenu.BOOK_CHECK:
                        CheckBorrowedBook(memberScreen);
                        break;
                    case (int)Constant.MemberMenu.MODIFICATION_MEMBER_INFORMATION:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
