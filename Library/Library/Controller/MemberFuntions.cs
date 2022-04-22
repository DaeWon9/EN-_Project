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
    class MemberFuntions : MenuSelection
    {
        private int menuValue;
        private void Login(MemberScreen memberScreen) // id : admin1    pw: admin1
        {
            bool isLoginCheck = false;
            string id = "", password = "";
            memberScreen.PrintLoginScreen(Constant.IS_CONSOLE_CLEAR);
            while (!isLoginCheck)
            {
                id = DataProcessing.Instance.GetInputValues(memberScreen, Constant.LOGIN_POS_X, Constant.LOGIN_ID_POS_Y, Constant.MAX_LENGTH_ID, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_ID);
                if (id == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 뒤로가기
                    break;
                password = DataProcessing.Instance.GetInputValues(memberScreen, Constant.LOGIN_POS_X, Constant.LOGIN_PASSWORD_POS_Y, Constant.MAX_LENGTH_PASSWORD, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_PASSWORD, Constant.IS_PASSWORD);
                if (password == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 뒤로가기
                    break;
                isLoginCheck = IsLogin(memberScreen, id, password);
            }
            if (id == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString() || password == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) // 처음 회원 or 관리자모드 선택으로 돌아가야함
                return;
            MemberMainMenuSelect(memberScreen);
        }

        private bool IsLogin(MemberScreen memberScreen, string id, string password)
        {
            List<string> memberId = DataBase.Instance.GetSelectedElements(Constant.MEMBER_FILED_ID, Constant.TABLE_NAME_MEMBER);
            List<string> memberPassword = DataBase.Instance.GetSelectedElements(Constant.MEMBER_FILED_PASSWORD, Constant.TABLE_NAME_MEMBER);

            for (int repeat = 0; repeat < memberId.Count; repeat++)
            {
                if (id == memberId[repeat] && password == memberPassword[repeat])
                    return true;
            }

            memberScreen.PrintMessage("ID & PASSWORD 가 틀립니다", Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, Constant.IS_NOT_CONSOLE_CLEAR, ConsoleColor.Red);
            DataProcessing.Instance.ClearConsoleLine(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_MAX_POS_X, Constant.LOGIN_ID_POS_Y);
            DataProcessing.Instance.ClearConsoleLine(Constant.LOGIN_POS_X, Constant.EXCEPTION_MESSAGE_MAX_POS_X, Constant.LOGIN_PASSWORD_POS_Y);
            return false;
        }


        private bool IsIdDuplicate(string id)
        {
            List<string> memberIdList = DataBase.Instance.GetSelectedElements(Constant.MEMBER_FILED_ID, Constant.TABLE_NAME_MEMBER);
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

        private void SignUp(MemberScreen memberScreen)
        {
            bool isSignUp = false, isIdDuplicate, isPasswordCorrect;
            string name = "", id = "", password = "", passwordCheck = "", age = "", address = "", phoneNumber = "";
            memberScreen.PrintSignUpScreen(Constant.IS_CONSOLE_CLEAR);
            while (!isSignUp)
            {
                name = DataProcessing.Instance.GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.NAME, Constant.MAX_LENGTH_NAME, Constant.EXCEPTION_TYPE_KOREA, "올바른 글자만 입력하세요", Constant.EXCEPTION_TYPE_NAME);
                if (name == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) 
                    break;
                isIdDuplicate = true;
                while (isIdDuplicate)
                {
                    id = DataProcessing.Instance.GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.ID, Constant.MAX_LENGTH_ID, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_ID);
                    if (id == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString())
                        break;
                    isIdDuplicate = IsIdDuplicate(id);
                    if (isIdDuplicate)
                        memberScreen.PrintMessage("이미 등록되어있는 ID입니다.", Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, Constant.IS_NOT_CONSOLE_CLEAR, ConsoleColor.Red);
                }
                if (id == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString())
                    break;

                password = DataProcessing.Instance.GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.PASSWORD, Constant.MAX_LENGTH_PASSWORD, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_PASSWORD, Constant.IS_PASSWORD);
                if (password == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString()) 
                    break;

                isPasswordCorrect = false;
                while (!isPasswordCorrect)
                {
                    passwordCheck = DataProcessing.Instance.GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.PASSWORD_CHECK, Constant.MAX_LENGTH_PASSWORD, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_PASSWORD, Constant.IS_PASSWORD);
                    if (passwordCheck == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString())
                        break;
                    isPasswordCorrect = IsPasswordCorrect(password, passwordCheck);
                    if (!isPasswordCorrect)
                        memberScreen.PrintMessage("비밀번호가 일치하지 않습니다.", Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, Constant.IS_NOT_CONSOLE_CLEAR, ConsoleColor.Red);
                }
                if (passwordCheck == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString())
                    break;
                age = DataProcessing.Instance.GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.AGE, Constant.MAX_LENGTH_AGE, Constant.EXCEPTION_TYPE_NUMBER, "숫자만 입력하세요", Constant.EXCEPTION_TYPE_AGE);
                if (age == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString())
                    break;
                address = DataProcessing.Instance.GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.ADDRESS, Constant.MAX_LENGTH_ADDRESS, Constant.EXCEPTION_TYPE_KOREA_NUMBER_SPACE, "한글 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_ADDRESS);
                if (address == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString())
                    break;
                phoneNumber = DataProcessing.Instance.GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.PHONE_NUMBER, Constant.MAX_LENGTH_PHONE_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, "숫자만 입력하세요", Constant.EXCEPTION_TYPE_PHONE_NUMBER);
                if (phoneNumber == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString())
                    break;


                if (phoneNumber.Length > 0) // 폰번호까지 입력된거임
                    isSignUp = true;
            }

            DataBase.Instance.InsertMember(Constant.TABLE_NAME_MEMBER, name, id, password, int.Parse(age), address, phoneNumber);
            memberScreen.PrintMessage("회원가입에 성공하였습니다!", Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, Constant.IS_NOT_CONSOLE_CLEAR, ConsoleColor.Blue);
            Console.ReadKey(); // 회원가입 성공 후 키입력하는부분 추가하기

        }


        public void LoginOrSignUpSelect(MemberScreen memberScreen)
        {
            menuValue = MemberLoginOrSignUpSelect(memberScreen);
            switch (menuValue)
            {
                case Constant.MODE_MEMBER_LOGIN:
                    Login(memberScreen);
                    break;
                case Constant.MODE_MEMBER_SIGN_UP:
                    SignUp(memberScreen);
                    break;
                default:
                    break;
            }
        }

        private void MemberMainMenuSelect(MemberScreen memberScreen)
        {
            menuValue = MemberMenuSelect(memberScreen);

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
