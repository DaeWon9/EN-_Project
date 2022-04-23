using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.View;
using Library.Utility;

namespace Library.Controller
{
    class MenuSelection
    {
        private int menuValue;

        public int GetUserMode(BothScreen bothScreen)
        {
            bothScreen.PrintSelectUserModeScreen();
            menuValue = DataProcessing.Instance.CursorMove(Constant.MENU_CURSOR_POS_X, Constant.MENU_CURSOR_MIN_POS_Y, Constant.MENU_CURSOR_MIN_POS_Y, Constant.FIRST_MENU_CURSOR_MAX_POS_Y);
            return menuValue;
        }

        public int GetMemberLoginOrSignUp(MemberScreen memberScreen)
        {
            memberScreen.PrintMainScreen();
            menuValue = DataProcessing.Instance.CursorMove(Constant.MENU_CURSOR_POS_X, Constant.MENU_CURSOR_MIN_POS_Y, Constant.MENU_CURSOR_MIN_POS_Y, Constant.FIRST_MENU_CURSOR_MAX_POS_Y);
            return menuValue;
        }

        public int GetMemberMenu(MemberScreen memberScreen, string welcomeString)
        {
            memberScreen.PrintMenuScreen();
            memberScreen.PrintMessage(welcomeString, Constant.WELCOME_MESSAGE_CURSOR_POS_X, Constant.WELCOME_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            menuValue = DataProcessing.Instance.CursorMove(Constant.MENU_CURSOR_POS_X, Constant.MENU_CURSOR_MIN_POS_Y, Constant.MENU_CURSOR_MIN_POS_Y,  Constant.MEMBER_MENU_CURSOR_MAX_POS_Y);
            return menuValue;
        }

        public int GetAddministratorMenu(AdministratorScreen administratorScreen, string administratorModeString)
        {
            administratorScreen.PrintMenuScreen();
            administratorScreen.PrintMessage(administratorModeString, Constant.WELCOME_MESSAGE_CURSOR_POS_X, Constant.WELCOME_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
            menuValue = DataProcessing.Instance.CursorMove(Constant.MENU_CURSOR_POS_X, Constant.MENU_CURSOR_MIN_POS_Y, Constant.MENU_CURSOR_MIN_POS_Y, Constant.ADMINISTRATOR_MENU_CURSOR_MAX_POS_Y);
            return menuValue;
        }
    }
}
