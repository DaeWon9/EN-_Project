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
    class MemberModifier : MemberSearcher
    {
        private string managementMemberId = "";
        private int MemberModifyMode;
        public void ManagementMember(BothScreen bothScreen, int modifyMode)
        {
            int currentConsoleCursorPosY, getYesOrNoByModify;
            string memberId = "";
            bool isSelectMemberIdCompleted = false, isInputEscape = false;
            MemberModifyMode = modifyMode;

            if (MemberModifyMode == (int)Constant.ModifyModePosY.IMMEDIATE) // 즉시 수정
            {
                bothScreen.PrintManagementMemberScreen();
                bothScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_MEMBER), Constant.TABLE_NAME_MEMBER, Constant.TEXT_NONE);
            }
            else if (MemberModifyMode == (int)Constant.ModifyModePosY.SEARCH) // 검색 후 수정
            {
                if (IsInputMemberSearchOption(bothScreen))
                {
                    bothScreen.PrintManagementMemberScreen();
                    bothScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_MEMBER, GetConditionalStringByUserInput()), Constant.TABLE_NAME_MEMBER, Constant.TEXT_NONE);
                }
                else
                    return;
            }
            else
                return;

            Console.SetCursorPosition(0, 0);      //입력창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.SELECT_MANAGEMENT_MEMBER_ID_OPTION_POS_X, (int)Constant.SelectMemberIdPosY.ID); //좌표조정

            while (!isInputEscape && !isSelectMemberIdCompleted)
            {
                if (memberId != "" && !DataBase.GetDataBase().IsRegisteredMemberId(memberId))// 회원아이디가 입력됐는데, 등록되지 않은 아이디임
                {
                    bothScreen.PrintMessage("등록되지 않은 회원ID입니다", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    Console.SetCursorPosition(Constant.SELECT_MANAGEMENT_MEMBER_ID_OPTION_POS_X, (int)Constant.SelectMemberIdPosY.ID); //좌표조정
                    DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.SELECT_MANAGEMENT_MEMBER_ID_POS_X, Constant.WINDOW_WIDTH, (int)Constant.SelectMemberIdPosY.ID);
                    memberId = "";
                }
                if (modifyMode == (int)Constant.ModifyModePosY.SEARCH && memberId != "" && !IsExistMemberIdInSearchedMemberList(memberId)) // 검색 후 멤버 이름 입력됐는데, 검색된 멤버가 아님
                {
                    bothScreen.PrintMessage("검색되지 않은 회원ID입니다", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    Console.SetCursorPosition(Constant.SELECT_MODIFY_BOOK_ID_OPTION_POS_X, (int)Constant.SelectBookIdPosY.ID); //좌표조정
                    DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.SELECT_MODIFY_BOOK_ID_POS_X, Constant.WINDOW_WIDTH, (int)Constant.SelectBookIdPosY.ID);
                    memberId = "";
                }

                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.SELECT_MANAGEMENT_MEMBER_ID_OPTION_POS_X, Console.CursorTop, (int)Constant.SelectMemberIdPosY.ID, (int)Constant.SelectMemberIdPosY.MANAGEMEMT_MEMBER);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.SelectMemberIdPosY.ID:
                        memberId = DataProcessing.GetDataProcessing().GetInputValues(bothScreen, Constant.SELECT_MANAGEMENT_MEMBER_ID_POS_X, (int)Constant.SelectMemberIdPosY.ID, Constant.MAX_LENGTH_MEMBER_ID, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_ID);
                        break;
                    case (int)Constant.SelectMemberIdPosY.MANAGEMEMT_MEMBER:
                        if (memberId != "" && memberId != Constant.INPUT_ESCAPE.ToString())
                        {
                            bothScreen.PrintMessage(Constant.TEXT_IS_MANAGEMENT, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
                            bothScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
                            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y); //좌표조정
                            getYesOrNoByModify = DataProcessing.GetDataProcessing().GetEnterOrEscape();
                            if (getYesOrNoByModify == Constant.INPUT_ENTER)
                                isSelectMemberIdCompleted = true;
                            if (getYesOrNoByModify == Constant.INPUT_ESCAPE)
                            {
                                DataProcessing.GetDataProcessing().ClearErrorMessage();
                                Console.SetCursorPosition(Constant.SELECT_MANAGEMENT_MEMBER_ID_OPTION_POS_X, (int)Constant.SelectMemberIdPosY.ID); //좌표조정
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            if (isSelectMemberIdCompleted)
                managementMemberId = memberId;
            if (!isInputEscape)
                ModifyMemberInformation(bothScreen, managementMemberId);
        }

        private bool IsMemberNotReturnBorrowedBook()
        {

            List<string> BorrowedBookList = DataBase.GetDataBase().GetSelectedElements(Constant.BOOK_FILED_ID, managementMemberId);
            if (BorrowedBookList.Count > 0) // 반납안한 책이 있음 
                return true;
            return false;
        }

        private bool IsReModifyByMember(BothScreen bothScreen)
        {
            int getYesOrNoByReModify;
            bothScreen.PrintConfirmationMessage("정보 변경에 성공하였습니다! 계속해서 변경하시겠습니까??", ConsoleColor.Yellow);
            getYesOrNoByReModify = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (getYesOrNoByReModify == Constant.INPUT_ESCAPE) // 계속해서 변경 x
                return false;
            return true;
        }

        private bool IsWithdrawlCompleted(BothScreen bothScreen)
        {
            int getYesOrNoByWithdrawl;
            string memberName;
            if (IsMemberNotReturnBorrowedBook()) // 반납안한 책이 있음
            {
                bothScreen.PrintMessage(Constant.TEXT_UNABLE_WITHDRAWAL, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.MODIFY_SELECT_OPTION_POS_X, (int)Constant.MemberModifyModePosY.NAME); //좌표조정
                return false;
            }
            else // 클린한 상태임 -> 회원탈퇴 가능
            {
                bothScreen.PrintConfirmationMessage("< 주의 > 모든정보가 삭제됩니다. 정말로 삭제하시겠습니까??", ConsoleColor.Yellow);

                getYesOrNoByWithdrawl = DataProcessing.GetDataProcessing().GetEnterOrEscape();
                if (getYesOrNoByWithdrawl == Constant.INPUT_ENTER) // 탈퇴진행
                {
                    memberName = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_NAME, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, managementMemberId));
                    DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_FORM_CONTAIN_ID, memberName, managementMemberId, Constant.LOG_TEXT_DELETE_MEMBER));
                    DataBase.GetDataBase().Drop(managementMemberId); // 회원아이디로 된 테이블 drop
                    DataBase.GetDataBase().Delete(Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, managementMemberId));
                }
                if (getYesOrNoByWithdrawl == Constant.INPUT_ESCAPE) // 탈퇴취소
                {
                    DataProcessing.GetDataProcessing().ClearErrorMessage();
                    Console.SetCursorPosition(Constant.MODIFY_SELECT_OPTION_POS_X, (int)Constant.MemberModifyModePosY.NAME); //좌표조정
                    return false;
                }
            }
            return true;
        }

        private bool IsModifyMemberInformationCompleted(BothScreen bothScreen, string setString, string modifyMemberId)
        {
            int getYesOrNoByModify;
            bothScreen.PrintConfirmationMessage("수정하시겠습니까??", ConsoleColor.Yellow);
        
            getYesOrNoByModify = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (getYesOrNoByModify == Constant.INPUT_ENTER) // 변경하시겠습니까? 에서 enter입력
            {
                DataBase.GetDataBase().Update(Constant.TABLE_NAME_MEMBER, setString, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, modifyMemberId));
                return true;
            }
            if (getYesOrNoByModify == Constant.INPUT_ESCAPE) // 변경하시겠습니까? 에서 esc입력
            {
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.MODIFY_MEMBER_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.MemberModifyModePosY.NAME);
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.MODIFY_MEMBER_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.MemberModifyModePosY.PASSWORD);
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.MODIFY_MEMBER_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.MemberModifyModePosY.BIRTH_DATE);
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.MODIFY_MEMBER_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.MemberModifyModePosY.ADDRESS);
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.MODIFY_MEMBER_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.MemberModifyModePosY.PHONE_NUMBER);
                DataProcessing.GetDataProcessing().ClearErrorMessage();
                Console.SetCursorPosition(Constant.MODIFY_SELECT_OPTION_POS_X, (int)Constant.MemberModifyModePosY.NAME); //좌표조정
                return false;
            }
            return true;
        }

        public void ModifyMemberInformation(BothScreen bothScreen, string modifyMemberId) // memberId 는 primaryKey 이므로 수정불가능하게 설정
        {
            string setStringByUpdate = "";
            string memberName = "", memberPassword = "", memberBirthDate = "", memberAddress = "", memberPhoneNumber = "";
            bool isModifyCompleted = false, isWithdrawlCompleted = false, isInputEscape = false;
            int currentConsoleCursorPosY;

            bothScreen.PrintModifyMemberInformationLabel();
            bothScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_MEMBER, String.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, modifyMemberId)), Constant.TABLE_NAME_MEMBER, Constant.TEXT_NONE);
            bothScreen.PrintModifyMemberInformationScreen();
            Console.SetCursorPosition(Constant.MODIFY_SELECT_OPTION_POS_X, (int)Constant.MemberModifyModePosY.NAME); //좌표조정         

            while (!isInputEscape && !isModifyCompleted && !isWithdrawlCompleted)
            {
                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.MODIFY_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.MemberModifyModePosY.NAME, (int)Constant.MemberModifyModePosY.WITHDRAWAL);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.MemberModifyModePosY.NAME:
                        memberName = DataProcessing.GetDataProcessing().GetInputValues(bothScreen, Constant.MODIFY_MEMBER_INPUT_POS_X, (int)Constant.MemberModifyModePosY.NAME, Constant.MAX_LENGTH_MEMBER_NAME, Constant.TEXT_PLEASE_INPUT_CORRECT_STRING, Constant.EXCEPTION_TYPE_KOREAN_ENGLISH, Constant.EXCEPTION_TYPE_MEMBER_NAME);
                        setStringByUpdate = DataProcessing.GetDataProcessing().GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.MEMBER_FILED_NAME, memberName);
                        break;
                    case (int)Constant.MemberModifyModePosY.PASSWORD:
                        memberPassword = DataProcessing.GetDataProcessing().GetInputValues(bothScreen, Constant.MODIFY_MEMBER_INPUT_POS_X, (int)Constant.MemberModifyModePosY.PASSWORD, Constant.MAX_LENGTH_MEMBER_PASSWORD, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_PASSWORD);
                        setStringByUpdate = DataProcessing.GetDataProcessing().GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.MEMBER_FILED_PASSWORD, memberPassword);
                        break;
                    case (int)Constant.MemberModifyModePosY.BIRTH_DATE:
                        memberBirthDate = DataProcessing.GetDataProcessing().GetInputValues(bothScreen, Constant.MODIFY_MEMBER_INPUT_POS_X, (int)Constant.MemberModifyModePosY.BIRTH_DATE, Constant.MAX_LENGTH_DATE, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_DATE);
                        setStringByUpdate = DataProcessing.GetDataProcessing().GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.MEMBER_FILED_BIRTH_DATE, memberBirthDate);
                        break;
                    case (int)Constant.MemberModifyModePosY.ADDRESS:
                        memberAddress = DataProcessing.GetDataProcessing().GetInputValues(bothScreen, Constant.MODIFY_MEMBER_INPUT_POS_X, (int)Constant.MemberModifyModePosY.ADDRESS, Constant.MAX_LENGTH_MEMBER_ADDRESS, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_MEMBER_ADDRESS);
                        setStringByUpdate = DataProcessing.GetDataProcessing().GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.MEMBER_FILED_ADDRESS, memberAddress);
                        break;
                    case (int)Constant.MemberModifyModePosY.PHONE_NUMBER:
                        memberPhoneNumber = DataProcessing.GetDataProcessing().GetInputValues(bothScreen, Constant.MODIFY_MEMBER_INPUT_POS_X, (int)Constant.MemberModifyModePosY.PHONE_NUMBER, Constant.MAX_LENGTH_MEMBER_PHONE_NUMBER, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_PHONE_NUMBER);
                        setStringByUpdate = DataProcessing.GetDataProcessing().GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.MEMBER_FILED_PHONE_NUMBER, memberPhoneNumber);
                        break;
                    case (int)Constant.MemberModifyModePosY.WITHDRAWAL:
                        isWithdrawlCompleted = IsWithdrawlCompleted(bothScreen);
                        break;
                    default:
                        break;
                }

                if (setStringByUpdate != "") // 수정사항이 있을때
                {
                    LogAdder.GetLogAdder().AddLogByModifyMember(bothScreen, currentConsoleCursorPosY, modifyMemberId, memberName, memberPassword, memberBirthDate, memberAddress, memberPhoneNumber);
                    isModifyCompleted = IsModifyMemberInformationCompleted(bothScreen, setStringByUpdate, modifyMemberId);
                    if (isModifyCompleted && IsReModifyByMember(bothScreen)) // 계속해서 변경
                        ModifyMemberInformation(bothScreen, modifyMemberId);
                }
            }

            if (isInputEscape)
                ManagementMember(bothScreen, MemberModifyMode);

        }

    }
}
