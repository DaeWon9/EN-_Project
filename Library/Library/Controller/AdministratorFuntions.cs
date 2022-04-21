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
    class AdministratorFuntions
    {
        public void Login(AdministratorScreen administratorScreen, Message message, DataProcessing dataProcessing, MenuSelection menuSelection) // id : admin1    pw: admin1
        {
            bool isLogin = false;
            string id = "", password = "";
            administratorScreen.LoginScreenPrint(Constant.IS_CONSOLE_CLEAR);
            while (!isLogin)
            {
                id = dataProcessing.GetInputValues(message, Constant.LOGIN_POS_X, Constant.LOGIN_ID_POS_Y, Constant.MAX_LENGTH_ID, Constant.IS_NOT_PASSWORD, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_ID);
                if (id == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 뒤로가기
                    break;
                password = dataProcessing.GetInputValues(message, Constant.LOGIN_POS_X, Constant.LOGIN_PASSWORD_POS_Y, Constant.MAX_LENGTH_PASSWORD, Constant.IS_PASSWORD, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_PASSWORD);
                if (password == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 뒤로가기
                    break;
                isLogin = LoginCheck(message, dataProcessing, id, password);
            }
            if (id == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString() || password == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 처음 회원 or 관리자모드 선택으로 돌아가야함
                return;
            MenuSelect(menuSelection, message, dataProcessing, administratorScreen);
        }

        private bool LoginCheck(Message message, DataProcessing dataProcessing, string id, string password)
        {
            List<string> administratorId = DataBaseTestSingleton.Instance.GetSelectedElements(Constant.ADMINISTRATOR_FILED_ID, Constant.TABLE_NAME_ADMINISTRATOR);
            List<string> administratorPassword = DataBaseTestSingleton.Instance.GetSelectedElements(Constant.ADMINISTRATOR_FILED_PASSWORD, Constant.TABLE_NAME_ADMINISTRATOR);

            for (int repeat = 0; repeat < administratorId.Count; repeat++)
            {
                if (id == administratorId[repeat] && password == administratorPassword[repeat])
                    return true;
            }

            message.PrintMessage("ID & PASSWORD 가 틀립니다", Constant.EXCEPTION_CURSOR_POS_X, Constant.EXCEPTION_CURSOR_POS_Y, Constant.IS_NOT_CONSOLE_CLEAR, ConsoleColor.Red);
            dataProcessing.ConsoleLineClear(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_MAX_POS_Y, Constant.LOGIN_ID_POS_Y);
            dataProcessing.ConsoleLineClear(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_MAX_POS_Y, Constant.LOGIN_PASSWORD_POS_Y);
            return false;
        }

        private void MenuSelect(MenuSelection menuSelection, Message message, DataProcessing dataProcessing,  AdministratorScreen administratorScreen)
        { 
            int menuValue;
            menuValue = menuSelection.AddministratorMenuSelect(administratorScreen, dataProcessing);

            switch (menuValue)
            {
                case Constant.ADMINISTRATOR_MENU_BOOK_SEARCH: // DATABASE 기능들 싱글톤으로 구성할거라 개체 생성후에 사용하는  방식 갈아엎기
                    Console.Clear();
                    Console.ReadKey();
                    MenuSelect(menuSelection, message, dataProcessing, administratorScreen);
                    break;
                case Constant.ADMINISTRATOR_MENU_BOOK_ADD:
                    MenuSelect(menuSelection, message, dataProcessing, administratorScreen);
                    break;
                case Constant.ADMINISTRATOR_MENU_BOOK_REMOVE:
                    MenuSelect(menuSelection, message, dataProcessing, administratorScreen);
                    break;
                case Constant.ADMINISTRATOR_MENU_BOOK_REVISE:
                    DataBaseTestSingleton.Instance.Select("publisher", "book");
                    Console.ReadKey();
                    break;
                case Constant.ADMINISTRATOR_MENU_MEMBER_MANAGEMENT:
                    Console.Clear();
                    Console.ReadKey();
                    MenuSelect(menuSelection, message, dataProcessing, administratorScreen);
                    break;
                case Constant.ADMINISTRATOR_MENU_RENTAL_STATUS:
                    MenuSelect(menuSelection, message, dataProcessing, administratorScreen);
                    break;
                case Constant.INPUT_ESCAPE_IN_ARROW_KEY:
                    Login(administratorScreen, message, dataProcessing, menuSelection);
                    break;
                default:
                    break;
            }
        }
    }
}
