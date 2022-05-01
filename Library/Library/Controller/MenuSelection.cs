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
    class MenuSelection 
    {
        private int menuValue;

        public int GetAddministratorMenu(AdministratorScreen administratorScreen, string administratorModeString)
        {
            administratorScreen.PrintMenuScreen();
            administratorScreen.PrintMessage(administratorModeString, Constant.WINDOW_WIDTH_CENTER, Constant.WELCOME_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
            menuValue = DataProcessing.GetDataProcessing().CursorMove(Constant.MENU_CURSOR_POS_X, (int)Constant.AdministratorMenu.BOOK_SEARCH, (int)Constant.AdministratorMenu.BOOK_SEARCH, (int)Constant.AdministratorMenu.SHOW_LOG);
            return menuValue;
        }

        public int GetUserMode(BothScreen bothScreen)
        {
            bothScreen.PrintSelectUserModeScreen();
            menuValue = DataProcessing.GetDataProcessing().CursorMove(Constant.MENU_CURSOR_POS_X, Constant.MENU_CURSOR_MIN_POS_Y, Constant.MENU_CURSOR_MIN_POS_Y, Constant.FIRST_MENU_CURSOR_MAX_POS_Y);
            return menuValue;
        }

        public int GetMemberLoginOrSignUp(MemberScreen memberScreen)
        {
            memberScreen.PrintMainScreen();
            menuValue = DataProcessing.GetDataProcessing().CursorMove(Constant.MENU_CURSOR_POS_X, Constant.MENU_CURSOR_MIN_POS_Y, Constant.MENU_CURSOR_MIN_POS_Y, Constant.FIRST_MENU_CURSOR_MAX_POS_Y);
            return menuValue;
        }

        public int GetMemberMenu(MemberScreen memberScreen, string welcomeString)
        {
            memberScreen.PrintMenuScreen();
            memberScreen.PrintMessage(welcomeString, Constant.WINDOW_WIDTH_CENTER, Constant.WELCOME_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            menuValue = DataProcessing.GetDataProcessing().CursorMove(Constant.MENU_CURSOR_POS_X, (int)Constant.MemberMenu.BOOK_SEARCH, (int)Constant.MemberMenu.BOOK_SEARCH, (int)Constant.MemberMenu.MODIFY_MEMBER_INFORMATION);
            return menuValue;
        }

        public int GetBorrowBookMode(MemberScreen memberScreen)
        {
            memberScreen.PrintSelectBorrowBookModeScreen();
            menuValue = DataProcessing.GetDataProcessing().CursorMove(Constant.MENU_CURSOR_POS_X, (int)Constant.BookBorrowModePosY.IMMEDIATE, (int)Constant.BookBorrowModePosY.IMMEDIATE, (int)Constant.BookBorrowModePosY.SEARCH);
            return menuValue;
        }

        public int GetModifyBookMode(AdministratorScreen administratorScreen)
        {
            administratorScreen.PrintSelectModifyBookModeScreen();
            menuValue = DataProcessing.GetDataProcessing().CursorMove(Constant.MENU_CURSOR_POS_X, (int)Constant.BookModifyModePosY.IMMEDIATE, (int)Constant.BookModifyModePosY.IMMEDIATE, (int)Constant.BookModifyModePosY.SEARCH);
            return menuValue;
        }

        public int GetManagementMemberMode(AdministratorScreen administratorScreen)
        {
            administratorScreen.PrintSelectManagementMemberModeScreen();
            administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_MEMBER), Constant.TABLE_NAME_MEMBER, Constant.TEXT_NONE);
            menuValue = DataProcessing.GetDataProcessing().CursorMove(Constant.MENU_CURSOR_POS_X, (int)Constant.MemberManagementModePosY.SEARCH, (int)Constant.MemberManagementModePosY.SEARCH, (int)Constant.MemberManagementModePosY.MODIFY);
            return menuValue;
        }
    }
}
