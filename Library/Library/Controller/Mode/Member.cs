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
    class Member : MenuSelection
    {
        BookSearcher bookSearcher = new BookSearcher();
        BookBorrower bookBorrower = new BookBorrower();
        BookReturn bookReturn = new BookReturn();
        MemberModifier memberModifier = new MemberModifier();
        MemberSignUp memberSignUp = new MemberSignUp();

        private string loginMemberId = "", loginMemberPassword = "", loginMemberName = "", loginMemberBirthDate = "", loginMemberAge = "", loginMemberAddress = "", loginMemberPhoneNumber = "", conditionalStringByUserInput = "";
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
            loginMemberBirthDate = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_BIRTH_DATE, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, loginMemberId)); // 로그인한 유저의 생년월일 저장
            loginMemberAddress = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_ADDRESS, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, loginMemberId)); // 로그인한 유저의 주소 저장
            loginMemberPhoneNumber = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_PHONE_NUMBER, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, loginMemberId)); // 로그인한 유저의 핸드폰번호 저장
            DataBase.GetDataBase().AddLog(string.Format(Constant.LOG_MEMBER_TEXT_FORM, loginMemberName, loginMemberId), Constant.LOG_TEXT_LOGIN);
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

        // menu && mode
        public void SelectLoginOrSignUp(MemberScreen memberScreen)
        {
            int menuValue;
            menuValue = GetMemberLoginOrSignUp(memberScreen);
            switch (menuValue)
            {
                case Constant.MODE_MEMBER_LOGIN:
                    Login(memberScreen);
                    break;
                case Constant.MODE_MEMBER_SIGN_UP:
                    memberSignUp.SignUp(memberScreen);
                    break;
                default:
                    break;
            }
        }

        private void SelectMemberMainMenu(MemberScreen memberScreen)
        {
            int menuValue;
            bool isLogout = false;
            while (!isLogout/* && !isWithdrawlCompleted*/)
            {
                menuValue = GetMemberMenu(memberScreen, string.Format(Constant.TEXT_WELCOME, loginMemberName));
                switch (menuValue)
                {
                    case (int)Constant.MemberMenu.BOOK_SEARCH:
                        bookSearcher.InputBookSearchOption(memberScreen);
                        bookSearcher.ShowSearchedBookInformation(memberScreen);
                        break;
                    case (int)Constant.MemberMenu.BOOK_BORROW:
                        SelectBorrowBookMode(memberScreen, bookSearcher);
                        break;
                    case (int)Constant.MemberMenu.BOOK_RETURN:
                        bookReturn.ReturnBook(memberScreen, loginMemberId, loginMemberName);
                        break;
                    case (int)Constant.MemberMenu.BOOK_CHECK:
                        bookBorrower.CheckBorrowedBook(memberScreen, loginMemberId, loginMemberName);
                        break;
                    case (int)Constant.MemberMenu.MODIFY_MEMBER_INFORMATION:
                        memberModifier.ModifyMemberInformation(memberScreen, loginMemberId);
                        break;
                    case Constant.INPUT_ESCAPE_IN_ARROW_KEY:
                        isLogout = DataProcessing.GetDataProcessing().IsLogout(memberScreen);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SelectBorrowBookMode(MemberScreen memberScreen, BookSearcher bookSearcher)
        {
            int menuValue;
            menuValue = GetBorrowBookMode(memberScreen);
            bookBorrower.Borrow(memberScreen, menuValue, loginMemberId, loginMemberName);
        }
    }
}
