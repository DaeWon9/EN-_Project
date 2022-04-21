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
        DataBase tableFuntions = new DataBase("Server=localhost;Port=3307;Database=library;Uid=root;Pwd=0000;"); // Contant처리
        public void Login(AdministratorScreen administratorScreen, Message message, DataProcessing dataProcessing, MenuSelection menuSelection) // id : admin1    pw: admin1
        {
            bool isLogin = false;
            string id = "", password = "";
            administratorScreen.LoginScreenDraw(true);
            while (!isLogin)
            {
                id = dataProcessing.GetInputValues(message, Constant.LOGIN_POS_X, Constant.LOGIN_ID_POS_Y, Constant.MAX_LENGTH_ID, false, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_ID);
                if (id == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 뒤로가기
                    break;
                password = dataProcessing.GetInputValues(message, Constant.LOGIN_POS_X, Constant.LOGIN_PASSWORD_POS_Y, Constant.MAX_LENGTH_PASSWORD, true, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_PASSWORD);
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
            if (id == "admin1" && password == "admin1")
                return true;
            message.PrintMessage("ID & PASSWORD 가 틀립니다", Constant.EXCEPTION_CURSOR_POS_X, Constant.EXCEPTION_CURSOR_POS_Y, false, ConsoleColor.Red);
            dataProcessing.ClearCurrentLine(Constant.LOGIN_POS_X, 90, Constant.LOGIN_ID_POS_Y);
            dataProcessing.ClearCurrentLine(Constant.LOGIN_POS_X, 90, Constant.LOGIN_PASSWORD_POS_Y);
            return false;
        }

        private void MenuSelect(MenuSelection menuSelection, Message message, DataProcessing dataProcessing,  AdministratorScreen administratorScreen)
        { 
            int menuValue;
            menuValue = menuSelection.AddministratorMenuSelect(administratorScreen, dataProcessing);

            switch (menuValue)
            {
                case Constant.MENU_BOOK_SEARCH: //04.19 14:00 여기까지 진행함 
                    Console.Clear();
                    tableFuntions.BookSelect("*", "book");
                    Console.ReadKey();
                    MenuSelect(menuSelection, message, dataProcessing, administratorScreen);
                    break;
                case Constant.MENU_BOOK_ADD:
                    tableFuntions.BookInsert("book", 100, "테스트", "테스트출판", "홍길동", 50000, 5);
                    MenuSelect(menuSelection, message, dataProcessing, administratorScreen);
                    break;
                case Constant.MENU_BOOK_REMOVE:
                    tableFuntions.BookDelete("book", 100);
                    MenuSelect(menuSelection, message, dataProcessing, administratorScreen);
                    break;
                case Constant.MENU_BOOK_REVISE:
                    MenuSelect(menuSelection, message, dataProcessing, administratorScreen);
                    break;
                case Constant.MENU_MEMBER_MANAGEMENT:
                    Console.Clear();
                    tableFuntions.MemberSelect("*", "member");
                    Console.ReadKey();
                    MenuSelect(menuSelection, message, dataProcessing, administratorScreen);
                    break;
                case Constant.MENU_RENTAL_STATUS:
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
