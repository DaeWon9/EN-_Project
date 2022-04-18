using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.View;
using Library.Utility;

namespace Library.Controller
{
    internal class MenuSelection
    {
        private int menuValue;
        public int UserModeSelect(ModeScreen modeScreen, Arrow arrow)
        {
            modeScreen.SelectUserModeScreenDraw(true);
            menuValue = arrow.CursorMove(Constant.MENU_CURSOR_POS_X, Constant.MENU_CURSOR_MIN_POS_Y, Constant.FIRST_MENU_CURSOR_MAX_POS_Y);
            return menuValue;
        }

        public int MemberModeSelect(MemberScreen memberScreen, Arrow arrow)
        {
            memberScreen.MeberMainScreenDraw(true);
            menuValue = arrow.CursorMove(Constant.MENU_CURSOR_POS_X, Constant.MENU_CURSOR_MIN_POS_Y, Constant.FIRST_MENU_CURSOR_MAX_POS_Y);
            return menuValue;
        }

        public int AddministratorMenuSelect(AdministratorScreen administratorScreen, Arrow arrow)
        {
            administratorScreen.MenuScreenDraw(true);
            menuValue = arrow.CursorMove(Constant.MENU_CURSOR_POS_X, Constant.MENU_CURSOR_MIN_POS_Y, Constant.ADMINISTRATOR_MENU_CURSOR_MAX_POS_Y);
            return menuValue;
        }
    }
}
