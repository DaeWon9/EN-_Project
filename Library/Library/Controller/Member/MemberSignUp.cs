using System;
using System.Collections.Generic;
using Library.Utility;
using Library.Model;
using Library.View;

namespace Library.Controller
{
    class MemberSignUp
    {
        private bool IsIdDuplicate(string id)
        {
            List<string> memberIdList = DataBase.GetDataBase().GetSelectedElements(Constant.MEMBER_FILED_ID, Constant.TABLE_NAME_MEMBER);
            for (int repeat = 0; repeat < memberIdList.Count; repeat++)
            {
                if (memberIdList[repeat] == id)
                    return true;
            }
            return false;
        }

        public void SignUp(MemberScreen memberScreen)
        {

            int currentConsoleCursorPosY;
            bool isInputEscape = false, isSignUpCompleted = false;
            string name = "", id = "", password = "", passwordCheck = "", birthDate = "", address = "", phoneNumber = "";
            isInputEscape = false;
            Console.CursorVisible = true;
            memberScreen.PrintSignUpScreen();
            Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.SignUpPosY.NAME); //좌표조정

            while (!isInputEscape && !isSignUpCompleted)
            {
                if (IsIdDuplicate(id))
                {
                    memberScreen.PrintMessage("이미 등록되어있는 ID입니다.", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    Console.SetCursorPosition(Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.ID); //좌표조정
                    DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.SIGNUP_POS_X, Constant.WINDOW_WIDTH, (int)Constant.SignUpPosY.ID);
                    id = "";
                }

                if ((password.Length > 1 && passwordCheck.Length > 1) && (password != passwordCheck))
                {
                    memberScreen.PrintMessage("비밀번호가 일치하지 않습니다.", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    Console.SetCursorPosition(Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.ID); //좌표조정
                    DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.SIGNUP_POS_X, Constant.WINDOW_WIDTH, (int)Constant.SignUpPosY.PASSWORD_CHECK);
                    passwordCheck = "";
                }

                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.SEARCH_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.SignUpPosY.NAME, (int)Constant.SignUpPosY.SIGN_UP);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString()); // 화살표 움직이면서 esc입력했는지 확인
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.SignUpPosY.NAME:
                        name = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.NAME, Constant.MAX_LENGTH_MEMBER_NAME, "올바른 글자만 입력하세요", Constant.EXCEPTION_TYPE_KOREAN_ENGLISH, Constant.EXCEPTION_TYPE_MEMBER_NAME);
                        break;
                    case (int)Constant.SignUpPosY.ID:
                        id = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.ID, Constant.MAX_LENGTH_MEMBER_ID, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_ID);
                        break;
                    case (int)Constant.SignUpPosY.PASSWORD:
                        password = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.PASSWORD, Constant.MAX_LENGTH_MEMBER_PASSWORD, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_PASSWORD, Constant.IS_PASSWORD);
                        break;
                    case (int)Constant.SignUpPosY.PASSWORD_CHECK:
                        passwordCheck = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.PASSWORD_CHECK, Constant.MAX_LENGTH_MEMBER_PASSWORD, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_PASSWORD, Constant.IS_PASSWORD);
                        break;
                    case (int)Constant.SignUpPosY.BIRTH_DATE:
                        birthDate = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.BIRTH_DATE, Constant.MAX_LENGTH_DATE, "숫자만 입력하세요", Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_DATE);
                        break;
                    case (int)Constant.SignUpPosY.ADDRESS:
                        address = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.ADDRESS, Constant.MAX_LENGTH_MEMBER_ADDRESS, "한글 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_KOREAN_NUMBER_SPACE, Constant.EXCEPTION_TYPE_MEMBER_ADDRESS);
                        break;
                    case (int)Constant.SignUpPosY.PHONE_NUMBER:
                        phoneNumber = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SIGNUP_POS_X, (int)Constant.SignUpPosY.PHONE_NUMBER, Constant.MAX_LENGTH_MEMBER_PHONE_NUMBER, "숫자만 입력하세요", Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_PHONE_NUMBER);
                        break;
                    case (int)Constant.SignUpPosY.SIGN_UP:
                        isSignUpCompleted = IsSignUpCompleted(memberScreen, name, id, password, birthDate, address, phoneNumber);
                        break;
                    default:
                        break;
                }
            }

        }

        private bool IsSignUpCompleted(MemberScreen memberScreen, string name, string id, string password, string birthDate, string address, string phoneNumber)
        {
            int GetYesOrNoBySignUp;
            // 모든 값이 입력됐는지 체크
            if ((name == "" || name == Constant.INPUT_ESCAPE.ToString()) || (id == "" || id == Constant.INPUT_ESCAPE.ToString()) || (password == "" || password == Constant.INPUT_ESCAPE.ToString()) || (birthDate == "" || birthDate == Constant.INPUT_ESCAPE.ToString()) || (address == "" || address == Constant.INPUT_ESCAPE.ToString()) || (phoneNumber == "" || phoneNumber == Constant.INPUT_ESCAPE.ToString()))
            {
                memberScreen.PrintMessage("옵션을 입력해주세요", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.SignUpPosY.NAME); //좌표조정
                return false;
            }

            memberScreen.PrintMessage("가입하시겠습니까??", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            memberScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y); //좌표조정
            Console.CursorVisible = false;
            GetYesOrNoBySignUp = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (GetYesOrNoBySignUp == Constant.INPUT_ENTER) // 가입확인문구에서 enter입력
            {
                DataProcessing.GetDataProcessing().ClearErrorMessage();
                DataBase.GetDataBase().InsertMember(Constant.TABLE_NAME_MEMBER, name, id, password, birthDate, address, phoneNumber); // member 테이블에 입력한 멤버정보 추가
                DataBase.GetDataBase().CreateTable(id); // 해당 멤버의 id로 대여도서 테이블 생성 
                DataBase.GetDataBase().AddLog(string.Format(Constant.LOG_MEMBER_TEXT_FORM, name, id), Constant.LOG_TEXT_SIGN_UP);
                memberScreen.PrintMessage("회원가입에 성공하였습니다!", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
                if (DataProcessing.GetDataProcessing().GetEnterOrEscape() != Constant.INPUT_ESCAPE) //esc 눌렀을때 뒤로가기
                {
                    Console.CursorVisible = true;
                    return true;
                }
            }
            if (GetYesOrNoBySignUp == Constant.INPUT_ESCAPE) // 가입확인문구에서 esc입력
            {
                DataProcessing.GetDataProcessing().ClearErrorMessage();
                Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.SignUpPosY.NAME); //좌표조정
                Console.CursorVisible = true;
                return false;
            }
            return true;
        }

    }
}
