using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Utility;
using Library.Model;
using Library.View;

namespace Library.Controller
{
    internal class AdministratorFuntions
    {
        public void Login(AdministratorScreen administratorScreen, Message message, DataProcessing dataProcessing) // id : admin1    pw: admin1
        {
            bool isLogin = false;
            string id, password;
            administratorScreen.LoginScreenDraw(true);
            while (!isLogin)
            {
                id = dataProcessing.GetInputValues(message, Constant.LOGIN_POS_X, Constant.LOGIN_ID_POS_Y, false, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, "영어 & 숫자만 입력하세요");
                password = dataProcessing.GetInputValues(message, Constant.LOGIN_POS_X, Constant.LOGIN_PASSWORD_POS_Y, true, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, "영어 & 숫자만 입력하세요");
                isLogin = LoginCheck(message, id, password);
            }
            MenuSelect(administratorScreen);
        }

        private bool LoginCheck(Message message, string id, string password)
        {
            if (id == "admin1" && password == "admin1")
                return true;
            message.Draw("ID & PW 가 틀립니다.", Constant.EXCEPTION_CURSOR_POS_X, Constant.EXCEPTION_CURSOR_POS_Y, false, true);
            return false;
        }

        private void MenuSelect(AdministratorScreen administratorScreen)
        {
            administratorScreen.MenuScreenDraw(true);
            int menuValue;
            menuValue = menuSelection.UserModeSelect(modeScreen, arrow);
        }
    }
}
