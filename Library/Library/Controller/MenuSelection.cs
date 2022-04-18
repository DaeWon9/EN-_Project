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
        public int ModeSelect(ModeScreen drawer, Arrow arrow)
        {
            int menuValue;
            drawer.SelectModeScreenDraw();
            menuValue = arrow.CursorMove(Constant.FIRST_MENU_CURSOR_POS_X, Constant.FIRST_MENU_CURSOR_MIN_POS_Y);
            return menuValue;
        }
    }
}
