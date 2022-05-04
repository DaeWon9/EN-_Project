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
        Log log = new Log();
        NaverBookSearcher naverBookSearcher = new NaverBookSearcher();
        BookSearcher bookSearcher = new BookSearcher();
        BookModifier bookModifier = new BookModifier();
        MemberSearcher memberSearcher = new MemberSearcher();
        MemberModifier memberModifier = new MemberModifier();
        BookAdder BookAdder = new BookAdder();
        BorrowBookStatus borrowBookStatus = new BorrowBookStatus();

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
                        bookSearcher.InputBookSearchOption(administratorScreen);
                        bookSearcher.ShowSearchedBookInformation(administratorScreen);
                        break;
                    case (int)Constant.AdministratorMenu.BOOK_ADD:
                        BookAdder.AddBook(administratorScreen);
                        break;
                    case (int)Constant.AdministratorMenu.BOOK_MODIFY:
                        SelectModifyBookMode(administratorScreen);
                        break;
                    case (int)Constant.AdministratorMenu.MEMBER_MANAGEMENT:
                        SelectManagementMemberMode(administratorScreen);
                        break;
                    case (int)Constant.AdministratorMenu.BORROW_BOOK_STATUS:
                        borrowBookStatus.ShowBorrowBookStatus(administratorScreen);
                        break;
                    case (int)Constant.AdministratorMenu.SEARCH_BY_NAVER:
                        naverBookSearcher.SearchBookByNaver(administratorScreen);
                        break;
                    case (int)Constant.AdministratorMenu.SHOW_LOG:
                        administratorScreen.PrintLogLabel();
                        log.ShowLogScreen();
                        break;
                    case (int)Constant.AdministratorMenu.SAVE_LOG:
                        log.SaveLogToTxt();
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

        public void SelectModifyBookMode(AdministratorScreen administratorScreen)
        {
            int menuValue;
            menuValue = GetModifyBookMode(administratorScreen);
            bookModifier.SelectModifyBookId(administratorScreen, menuValue);
        }

        private void SelectManagementMemberMode(AdministratorScreen administratorScreen)
        {
            bool isBack = false;
            int menuValue;
            int modifyModValue;

            while (!isBack)
            {
                menuValue = GetManagementMemberMode(administratorScreen);
                switch (menuValue)
                {
                    case (int)Constant.MemberManagementModePosY.SEARCH:
                        memberSearcher.InputMemberSearchOption(administratorScreen);
                        memberSearcher.ShowSearchedMemberInformation(administratorScreen);
                        break;
                    case (int)Constant.MemberManagementModePosY.MODIFY:
                        modifyModValue = GetModifyMemberMode(administratorScreen);
                        memberModifier.ManagementMember(administratorScreen, modifyModValue);
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
