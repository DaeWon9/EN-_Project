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
    class MemberFuntions
    {
        private int menuValue;
        private void Login(MemberScreen memberScreen, Message message, DataProcessing dataProcessing, MenuSelection menuSelection) // id : admin1    pw: admin1
        {
            bool isLogin = false;
            string id = "", password = "";
            memberScreen.LoginScreenPrint(Constant.IS_CONSOLE_CLEAR);
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
            MemberMenuSelect(menuSelection, message, memberScreen, dataProcessing);
        }

        private bool LoginCheck(Message message, DataProcessing dataProcessing, string id, string password)
        {
            List<string> memberId = DataBaseTestSingleton.Instance.GetSelectedElements(Constant.MEMBER_FILED_ID, Constant.TABLE_NAME_MEMBER);
            List<string> memberPassword = DataBaseTestSingleton.Instance.GetSelectedElements(Constant.MEMBER_FILED_PASSWORD, Constant.TABLE_NAME_MEMBER);

            for (int repeat = 0; repeat < memberId.Count; repeat++)
            {
                if (id == memberId[repeat] && password == memberPassword[repeat])
                    return true;
            }

            message.PrintMessage("ID & PASSWORD 가 틀립니다", Constant.EXCEPTION_CURSOR_POS_X, Constant.EXCEPTION_CURSOR_POS_Y, Constant.IS_NOT_CONSOLE_CLEAR, ConsoleColor.Red);
            dataProcessing.ConsoleLineClear(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_MAX_POS_Y, Constant.LOGIN_ID_POS_Y);
            dataProcessing.ConsoleLineClear(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_MAX_POS_Y, Constant.LOGIN_PASSWORD_POS_Y);
            return false;
        }


        public void LoginOrSignUpSelect(MenuSelection menuSelection, Message message, MemberScreen memberScreen, DataProcessing dataProcessing)
        {
            menuValue = menuSelection.MemberLoginOrSignUpSelect(memberScreen, dataProcessing);
            switch (menuValue)
            {
                case Constant.MODE_MEMBER_LOGIN:
                    Login(memberScreen, message, dataProcessing, menuSelection);
                    break;
                case Constant.MODE_MEMBER_SIGN_UP:
                    break;
                default:
                    break;
            }
        }

        private void MemberMenuSelect(MenuSelection menuSelection, Message message, MemberScreen memberScreen, DataProcessing dataProcessing)
        {
            menuValue = menuSelection.MemberMenuSelect(memberScreen, dataProcessing);

            switch (menuValue)
            {
                case Constant.MEMBER_MENU_BOOK_SEARCH: // DATABASE 기능들 싱글톤으로 구성할거라 개체 생성후에 사용하는  방식 갈아엎기
                    break;
                case Constant.MEMBER_MENU_BOOK_RENTAL:
                    break;
                case Constant.MEMBER_MENU_RENTAL_BOOK_CHECK:
                    break;
                case Constant.MEMBER_MENU_MODIFICATION_MEMBER_INFORMATION:
                    break;
                default:
                    break;
            }
        }
    }
}
