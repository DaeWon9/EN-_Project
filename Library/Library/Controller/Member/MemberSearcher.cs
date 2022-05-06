using System;
using System.Collections.Generic;
using Library.Utility;
using Library.Model;
using Library.View;

namespace Library.Controller
{
    class MemberSearcher
    {
        private bool isBack = false;
        private string conditionalStringByUserInput = "";
        private List<string> searchedMemberIdList = new List<string>();

        public string GetConditionalStringByUserInput()
        {
            return conditionalStringByUserInput;
        }

        public bool IsExistMemberIdInSearchedMemberList(string memberId)
        {
            for (int repeat = 0; repeat < searchedMemberIdList.Count; repeat++)
            {
                if (searchedMemberIdList[repeat] == memberId)
                    return true;
            }
            return false;
        }
        public void Search(BothScreen bothScreen)
        {
            isBack = false;
            while (!isBack)
            {
                if (IsInputMemberSearchOption(bothScreen))
                    ShowSearchedMemberInformation(bothScreen);
            }
        }

        public bool IsInputMemberSearchOption(BothScreen bothScreen)
        {
            string memberName = "", memberId = "", memberBirthDate = "", memberAddress = "", memberPhoneNumber = "";
            int currentConsoleCursorPosY;
            bool isGetConditionalStringCompleted = false, isInputEscape = false;
            Console.CursorVisible = true;

            bothScreen.PrintMemberSearchScreen();
            bothScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_MEMBER), Constant.TABLE_NAME_MEMBER, Constant.TEXT_NONE);
            Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.MemberSearchPosY.NAME); //좌표조정

            while (!isInputEscape && !isGetConditionalStringCompleted)
            {
                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.SEARCH_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.MemberSearchPosY.NAME, (int)Constant.MemberSearchPosY.SEARCH);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.MemberSearchPosY.NAME:
                        memberName = DataProcessing.GetDataProcessing().GetInputValues(bothScreen, Constant.SEARCH_POS_X, (int)Constant.MemberSearchPosY.NAME, Constant.MAX_LENGTH_MEMBER_NAME, "올바른 글자만 입력하세요", Constant.EXCEPTION_TYPE_KOREAN_ENGLISH, Constant.EXCEPTION_TYPE_MEMBER_NAME);
                        break;
                    case (int)Constant.MemberSearchPosY.ID:
                        memberId = DataProcessing.GetDataProcessing().GetInputValues(bothScreen, Constant.SEARCH_POS_X, (int)Constant.MemberSearchPosY.ID, Constant.MAX_LENGTH_MEMBER_ID, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER);
                        break;
                    case (int)Constant.MemberSearchPosY.BIRTHDATE:
                        memberBirthDate = DataProcessing.GetDataProcessing().GetInputValues(bothScreen, Constant.SEARCH_POS_X, (int)Constant.MemberSearchPosY.BIRTHDATE, Constant.MAX_LENGTH_DATE, "숫자만 입력하세요", Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_DATE);
                        break;
                    case (int)Constant.MemberSearchPosY.ADDRESS:
                        memberAddress = DataProcessing.GetDataProcessing().GetInputValues(bothScreen, Constant.SEARCH_POS_X, (int)Constant.MemberSearchPosY.ADDRESS, Constant.MAX_LENGTH_MEMBER_ADDRESS, "한글 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_KOREAN_NUMBER_SPACE, Constant.EXCEPTION_TYPE_KOREAN_NUMBER_SPACE);
                        break;
                    case (int)Constant.MemberSearchPosY.PHONE_NUMBER:
                        memberPhoneNumber = DataProcessing.GetDataProcessing().GetInputValues(bothScreen, Constant.SEARCH_POS_X, (int)Constant.MemberSearchPosY.PHONE_NUMBER, Constant.MAX_LENGTH_MEMBER_PHONE_NUMBER, "숫자만 입력하세요", Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_NUMBER);
                        break;
                    case (int)Constant.MemberSearchPosY.SEARCH:
                        if ((memberName == "" || memberName == Constant.INPUT_ESCAPE.ToString()) && (memberId == "" || memberId == Constant.INPUT_ESCAPE.ToString()) && (memberBirthDate == "" || memberBirthDate == Constant.INPUT_ESCAPE.ToString()) && (memberAddress == "" || memberAddress == Constant.INPUT_ESCAPE.ToString()) && (memberPhoneNumber == "" || memberPhoneNumber == Constant.INPUT_ESCAPE.ToString()))
                        {
                            bothScreen.PrintMessage("옵션을 입력해주세요", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                            Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.MemberSearchPosY.NAME); //좌표조정
                        }
                        else 
                        {
                            conditionalStringByUserInput = DataProcessing.GetDataProcessing().GetConditionalStringBySearchMember(memberName, memberId, memberBirthDate, memberAddress, memberPhoneNumber);
                            searchedMemberIdList = DataBase.GetDataBase().GetSelectedElements(Constant.MEMBER_FILED_ID, Constant.TABLE_NAME_MEMBER, conditionalStringByUserInput);
                            isGetConditionalStringCompleted = true;
                        }
                        break;
                    default:
                        break;
                }
            }
            if (isGetConditionalStringCompleted)
                return true;
            if (isInputEscape)
                isBack = true;

            return false;
        }

        public void ShowSearchedMemberInformation(BothScreen bothScreen)
        {
            int getYesOrNoBySearching, getYesOrNoByResearching;
            bothScreen.PrintConfirmationMessage("검색하시겠습니까?", ConsoleColor.Yellow);

            getYesOrNoBySearching = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (getYesOrNoBySearching == Constant.INPUT_ENTER) // 검색만
            {
                bothScreen.PrintSearchResultScreen();
                bothScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_MEMBER, conditionalStringByUserInput), Constant.TABLE_NAME_MEMBER, Constant.TEXT_NONE);
                Console.SetCursorPosition(0, 0); // 출력되는 자료가 많아서 화면이 내려갈 수 있어 최상단으로 커서 옮기기
                Console.CursorVisible = false;
                getYesOrNoByResearching = DataProcessing.GetDataProcessing().GetEnterOrEscape();
                if (getYesOrNoByResearching == Constant.INPUT_ENTER)
                    Search(bothScreen);
            }
            if (getYesOrNoBySearching == Constant.INPUT_ESCAPE)
                Search(bothScreen);
        }

    }
}
