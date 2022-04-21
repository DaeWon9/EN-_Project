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
        public int UserModeSelect(ModeScreen modeScreen, DataProcessing dataProcessing)
        {
            modeScreen.SelectUserModeScreenDraw(Constant.IS_CONSOLE_CLEAR);
            menuValue = dataProcessing.CursorMove(Constant.MENU_CURSOR_POS_X, Constant.MENU_CURSOR_MIN_POS_Y, Constant.FIRST_MENU_CURSOR_MAX_POS_Y);
            return menuValue;
        }

        public int MemberModeSelect(MemberScreen memberScreen, DataProcessing dataProcessing)
        {
            memberScreen.MeberMainScreenDraw(Constant.IS_CONSOLE_CLEAR);
            menuValue = dataProcessing.CursorMove(Constant.MENU_CURSOR_POS_X, Constant.MENU_CURSOR_MIN_POS_Y, Constant.FIRST_MENU_CURSOR_MAX_POS_Y);
            return menuValue;
        }

        public int AddministratorMenuSelect(AdministratorScreen administratorScreen, DataProcessing dataProcessing)
        {
            administratorScreen.MenuScreenDraw(Constant.IS_CONSOLE_CLEAR);
            menuValue = dataProcessing.CursorMove(Constant.MENU_CURSOR_POS_X, Constant.MENU_CURSOR_MIN_POS_Y, Constant.ADMINISTRATOR_MENU_CURSOR_MAX_POS_Y);
            return menuValue;
        }
    }
}
