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
            bool isLoginCheck = false;
            string id = "", password = "";
            memberScreen.LoginScreenPrint(Constant.IS_CONSOLE_CLEAR);
            while (!isLoginCheck)
            {
                id = dataProcessing.GetInputValues(message, Constant.LOGIN_POS_X, Constant.LOGIN_ID_POS_Y, Constant.MAX_LENGTH_ID, Constant.IS_NOT_PASSWORD, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_ID);
                if (id == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 뒤로가기
                    break;
                password = dataProcessing.GetInputValues(message, Constant.LOGIN_POS_X, Constant.LOGIN_PASSWORD_POS_Y, Constant.MAX_LENGTH_PASSWORD, Constant.IS_PASSWORD, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_PASSWORD);
                if (password == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 뒤로가기
                    break;
                isLoginCheck = IsLogin(message, dataProcessing, id, password);
            }
            if (id == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString() || password == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 처음 회원 or 관리자모드 선택으로 돌아가야함
                return;
            MemberMenuSelect(menuSelection, message, memberScreen, dataProcessing);
        }

        private bool IsLogin(Message message, DataProcessing dataProcessing, string id, string password)
        {
            List<string> memberId = DataBaseTestSingleton.Instance.GetSelectedElements(Constant.MEMBER_FILED_ID, Constant.TABLE_NAME_MEMBER);
            List<string> memberPassword = DataBaseTestSingleton.Instance.GetSelectedElements(Constant.MEMBER_FILED_PASSWORD, Constant.TABLE_NAME_MEMBER);

            for (int repeat = 0; repeat < memberId.Count; repeat++)
            {
                if (id == memberId[repeat] && password == memberPassword[repeat])
                    return true;
            }

            message.PrintMessage("ID & PASSWORD 가 틀립니다", Constant.EXCEPTION_CURSOR_POS_X, Constant.EXCEPTION_CURSOR_POS_Y, Constant.IS_NOT_CONSOLE_CLEAR, ConsoleColor.Red);
            dataProcessing.ConsoleLineClear(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_MAX_POS_X, Constant.LOGIN_ID_POS_Y);
            dataProcessing.ConsoleLineClear(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_MAX_POS_X, Constant.LOGIN_PASSWORD_POS_Y);
            return false;
        }


        private bool IsIdDuplicate(string id)
        {
            List<string> memberIdList = DataBaseTestSingleton.Instance.GetSelectedElements(Constant.MEMBER_FILED_ID, Constant.TABLE_NAME_MEMBER);
            for (int repeat = 0; repeat < memberIdList.Count; repeat++)
            {
                if (memberIdList[repeat] == id)
                    return true;
            }
            return false;
        }
        
        private bool IsPasswordCorrect(string password, string passwordCheck)
        {
            if (password == passwordCheck)
                return true;
            return false;
        }

        private void SignUp(MemberScreen memberScreen, Message message, DataProcessing dataProcessing)
        {
            bool isSignUp = false, isIdDuplicate, isPasswordCorrect;
            string name = "", id = "", password = "", passwordCheck = "", age = "", address = "", phoneNumber = "";
            memberScreen.SignUpScreenPrint(Constant.IS_CONSOLE_CLEAR);
            while (!isSignUp)
            {
                name = dataProcessing.GetInputValues(message, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.NAME, Constant.MAX_LENGTH_NAME, Constant.IS_NOT_PASSWORD, Constant.EXCEPTION_TYPE_KOREA, "올바른 글자만 입력하세요", Constant.EXCEPTION_TYPE_NAME);
                if (name == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) 
                    break;
                isIdDuplicate = true;
                while (isIdDuplicate)
                {
                    id = dataProcessing.GetInputValues(message, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.ID, Constant.MAX_LENGTH_ID, Constant.IS_NOT_PASSWORD, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_ID);
                    isIdDuplicate = IsIdDuplicate(id);
                    if (isIdDuplicate)
                        message.PrintMessage("이미 등록되어있는 ID입니다.", Constant.EXCEPTION_CURSOR_POS_X, Constant.EXCEPTION_CURSOR_POS_Y, Constant.IS_NOT_CONSOLE_CLEAR, ConsoleColor.Red);
                }
                if (id == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString())
                    break;

                password = dataProcessing.GetInputValues(message, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.PASSWORD, Constant.MAX_LENGTH_PASSWORD, Constant.IS_PASSWORD, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_PASSWORD);
                if (password == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) 
                    break;

                isPasswordCorrect = false;
                while (!isPasswordCorrect)
                {
                    passwordCheck = dataProcessing.GetInputValues(message, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.PASSWORD_CHECK, Constant.MAX_LENGTH_PASSWORD, Constant.IS_PASSWORD, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_PASSWORD);
                    isPasswordCorrect = IsPasswordCorrect(password, passwordCheck);
                    if (!isPasswordCorrect)
                        message.PrintMessage("비밀번호가 일치하지 않습니다.", Constant.EXCEPTION_CURSOR_POS_X, Constant.EXCEPTION_CURSOR_POS_Y, Constant.IS_NOT_CONSOLE_CLEAR, ConsoleColor.Red);
                    if (passwordCheck == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString())
                        break;
                }
                if (passwordCheck == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString())
                    break;
                age = dataProcessing.GetInputValues(message, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.AGE, Constant.MAX_LENGTH_AGE, Constant.IS_NOT_PASSWORD, Constant.EXCEPTION_TYPE_NUMBER, "숫자만 입력하세요", Constant.EXCEPTION_TYPE_AGE);
                if (age == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString())
                    break;
                address = dataProcessing.GetInputValues(message, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.ADDRESS, Constant.MAX_LENGTH_ADDRESS, Constant.IS_NOT_PASSWORD, Constant.EXCEPTION_TYPE_KOREA_NUMBER_SPACE, "한글 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_ADDRESS);
                if (address == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString())
                    break;
                phoneNumber = dataProcessing.GetInputValues(message, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.PHONE_NUMBER, Constant.MAX_LENGTH_PHONE_NUMBER, Constant.IS_NOT_PASSWORD, Constant.EXCEPTION_TYPE_NUMBER, "숫자만 입력하세요", Constant.EXCEPTION_TYPE_PHONE_NUMBER);
                if (phoneNumber == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString())
                    break;


                if (phoneNumber.Length > 0) // 폰번호까지 입력된거임
                    isSignUp = true;
            }

            DataBaseTestSingleton.Instance.MemberInsert(Constant.TABLE_NAME_MEMBER, name, id, password, int.Parse(age), address, phoneNumber);
            message.PrintMessage("회원가입에 성공하였습니다!", Constant.EXCEPTION_CURSOR_POS_X, Constant.EXCEPTION_CURSOR_POS_Y, Constant.IS_NOT_CONSOLE_CLEAR, ConsoleColor.Blue);
            Console.ReadKey();

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
                    SignUp(memberScreen, message, dataProcessing);
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
                case (int)Constant.MemberMenu.BOOK_SEARCH: 
                    break;
                case (int)Constant.MemberMenu.BOOK_RENTAL:
                    break;
                case (int)Constant.MemberMenu.BOOK_CHECK:
                    break;
                case (int)Constant.MemberMenu.MODIFICATION_MEMBER_INFORMATION:
                    break;
                default:
                    break;
            }
        }
    }
}
