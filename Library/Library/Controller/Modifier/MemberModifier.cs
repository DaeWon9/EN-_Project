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
        private string managementMemberId = "", managementMemberName = "", managementMemberBirthDate = "", managementMemberAddress = "", managementMemberPhoneNumber = "";

        public void ManagementMember(AdministratorScreen administratorScreen, int modifyMode)
        {
            int currentConsoleCursorPosY, getYesOrNoByModify;
            string memberId = "";
            bool isSelectMemberIdCompleted = false, isInputEscape = false;

            if (modifyMode == (int)Constant.ModifyModePosY.IMMEDIATE)
            {
                administratorScreen.PrintManagementMemberScreen();
                administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_MEMBER), Constant.TABLE_NAME_MEMBER, Constant.TEXT_NONE);
            }
            else 
            {
                InputMemberSearchOption(administratorScreen);
                administratorScreen.PrintManagementMemberScreen();
                administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_MEMBER, GetConditionalStringByUserInput()), Constant.TABLE_NAME_MEMBER, Constant.TEXT_NONE);

            }

            Console.SetCursorPosition(0, 0);      //입력창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.SELECT_MANAGEMENT_MEMBER_ID_OPTION_POS_X, (int)Constant.SelectMemberIdPosY.ID); //좌표조정

            while (!isInputEscape && !isSelectMemberIdCompleted)
            {
                if (memberId != "" && !DataBase.GetDataBase().IsRegisteredMemberId(memberId))// 회원아이디가 입력됐는데, 등록되지 않은 아이디임
                {
                    administratorScreen.PrintMessage(Constant.TEXT_IS_NOT_REGISTERED_MEMBER_ID, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    Console.SetCursorPosition(Constant.SELECT_MANAGEMENT_MEMBER_ID_OPTION_POS_X, (int)Constant.SelectMemberIdPosY.ID); //좌표조정
                    DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.SELECT_MANAGEMENT_MEMBER_ID_POS_X, Constant.WINDOW_WIDTH, (int)Constant.SelectMemberIdPosY.ID);
                    memberId = "";
                }

                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.SELECT_MANAGEMENT_MEMBER_ID_OPTION_POS_X, Console.CursorTop, (int)Constant.SelectMemberIdPosY.ID, (int)Constant.SelectMemberIdPosY.MANAGEMEMT_MEMBER);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.SelectMemberIdPosY.ID:
                        memberId = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SELECT_MANAGEMENT_MEMBER_ID_POS_X, (int)Constant.SelectMemberIdPosY.ID, Constant.MAX_LENGTH_MEMBER_ID, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_ID);
                        break;
                    case (int)Constant.SelectMemberIdPosY.MANAGEMEMT_MEMBER:
                        if (memberId != "" && memberId != Constant.INPUT_ESCAPE.ToString())
                        {
                            administratorScreen.PrintMessage(Constant.TEXT_IS_MANAGEMENT, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
                            administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
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
            {
                managementMemberId = memberId;
                managementMemberName = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_NAME, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, managementMemberId));
                managementMemberBirthDate = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_BIRTH_DATE, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, managementMemberId));
                managementMemberAddress = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_ADDRESS, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, managementMemberId));
                managementMemberPhoneNumber = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_PHONE_NUMBER, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, managementMemberId));
            }

            if (!isInputEscape)
                ModifyMemberInformation(administratorScreen);
        }

        private bool IsMemberNotReturnBorrowedBook()
        {

            List<string> BorrowedBookList = DataBase.GetDataBase().GetSelectedElements(Constant.BOOK_FILED_ID, managementMemberId);
            if (BorrowedBookList.Count > 0) // 반납안한 책이 있음 
                return true;
            return false;
        }

        private bool IsReModifyByMember(AdministratorScreen administratorScreen)
        {
            int getYesOrNoByReModify;
            administratorScreen.PrintConfirmationMessage("정보 변경에 성공하였습니다! 계속해서 변경하시겠습니까??", ConsoleColor.Yellow);
            getYesOrNoByReModify = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (getYesOrNoByReModify == Constant.INPUT_ESCAPE) // 계속해서 변경 x
                return false;
            return true;
        }

        private bool IsWithdrawlCompleted(AdministratorScreen administratorScreen)
        {
            int getYesOrNoByWithdrawl;
            string memberName;
            if (IsMemberNotReturnBorrowedBook()) // 반납안한 책이 있음
            {
                administratorScreen.PrintMessage(Constant.TEXT_UNABLE_WITHDRAWAL, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.MODIFY_SELECT_OPTION_POS_X, (int)Constant.MemberModifyModePosY.NAME); //좌표조정
                return false;
            }
            else // 클린한 상태임 -> 회원탈퇴 가능
            {
                administratorScreen.PrintConfirmationMessage("< 주의 > 모든정보가 삭제됩니다. 정말로 삭제하시겠습니까??", ConsoleColor.Yellow);

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

        private bool IsModifyMemberInformationCompleted(AdministratorScreen administratorScreen, string setString)
        {
            int getYesOrNoByModify;
            administratorScreen.PrintConfirmationMessage("수정하시겠습니까??", ConsoleColor.Yellow);
        
            getYesOrNoByModify = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (getYesOrNoByModify == Constant.INPUT_ENTER) // 변경하시겠습니까? 에서 enter입력
            {
                DataBase.GetDataBase().Update(Constant.TABLE_NAME_MEMBER, setString, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_NAME, managementMemberName));
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

        private void ModifyMemberInformation(AdministratorScreen administratorScreen) // memberId 는 primaryKey 이므로 수정불가능하게 설정
        {
            string setStringByUpdate = "";
            string memberName = "", memberPassword = "", memberBirthDate = "", memberAddress = "", memberPhoneNumber = "";
            bool isModifyCompleted = false, isWithdrawlCompleted = false, isInputEscape = false;
            int currentConsoleCursorPosY;
            administratorScreen.PrintModifyMemberInformationLabel();
            administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_MEMBER, String.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, managementMemberId)), Constant.TABLE_NAME_MEMBER, Constant.TEXT_NONE);
            administratorScreen.PrintModifyMemberInformationScreen();
            Console.SetCursorPosition(Constant.MODIFY_SELECT_OPTION_POS_X, (int)Constant.MemberModifyModePosY.NAME); //좌표조정

            while (!isInputEscape && !isModifyCompleted && !isWithdrawlCompleted)
            {
                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.MODIFY_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.MemberModifyModePosY.NAME, (int)Constant.MemberModifyModePosY.WITHDRAWAL);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.MemberModifyModePosY.NAME:
                        memberName = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.MODIFY_MEMBER_INPUT_POS_X, (int)Constant.MemberModifyModePosY.NAME, Constant.MAX_LENGTH_MEMBER_NAME, Constant.TEXT_PLEASE_INPUT_CORRECT_STRING, Constant.EXCEPTION_TYPE_KOREAN_ENGLISH, Constant.EXCEPTION_TYPE_MEMBER_NAME);
                        setStringByUpdate = DataProcessing.GetDataProcessing().GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.MEMBER_FILED_NAME, memberName);
                        break;
                    case (int)Constant.MemberModifyModePosY.PASSWORD:
                        memberPassword = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.MODIFY_MEMBER_INPUT_POS_X, (int)Constant.MemberModifyModePosY.PASSWORD, Constant.MAX_LENGTH_MEMBER_PASSWORD, Constant.TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_PASSWORD);
                        setStringByUpdate = DataProcessing.GetDataProcessing().GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.MEMBER_FILED_PASSWORD, memberPassword);
                        break;
                    case (int)Constant.MemberModifyModePosY.BIRTH_DATE:
                        memberBirthDate = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.MODIFY_MEMBER_INPUT_POS_X, (int)Constant.MemberModifyModePosY.BIRTH_DATE, Constant.MAX_LENGTH_DATE, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_DATE);
                        setStringByUpdate = DataProcessing.GetDataProcessing().GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.MEMBER_FILED_BIRTH_DATE, memberBirthDate);
                        break;
                    case (int)Constant.MemberModifyModePosY.ADDRESS:
                        memberAddress = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.MODIFY_MEMBER_INPUT_POS_X, (int)Constant.MemberModifyModePosY.ADDRESS, Constant.MAX_LENGTH_MEMBER_ADDRESS, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_MEMBER_ADDRESS);
                        setStringByUpdate = DataProcessing.GetDataProcessing().GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.MEMBER_FILED_ADDRESS, memberAddress);
                        break;
                    case (int)Constant.MemberModifyModePosY.PHONE_NUMBER:
                        memberPhoneNumber = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.MODIFY_MEMBER_INPUT_POS_X, (int)Constant.MemberModifyModePosY.PHONE_NUMBER, Constant.MAX_LENGTH_MEMBER_PHONE_NUMBER, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_PHONE_NUMBER);
                        setStringByUpdate = DataProcessing.GetDataProcessing().GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.MEMBER_FILED_PHONE_NUMBER, memberPhoneNumber);
                        break;
                    case (int)Constant.MemberModifyModePosY.WITHDRAWAL:
                        isWithdrawlCompleted = IsWithdrawlCompleted(administratorScreen);
                        break;
                    default:
                        break;
                }

                if (setStringByUpdate != "")
                {
                    isModifyCompleted = IsModifyMemberInformationCompleted(administratorScreen, setStringByUpdate);

                    switch (currentConsoleCursorPosY)
                    {
                        case (int)Constant.MemberModifyModePosY.NAME:
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_BY_ADMINISTRATOR, managementMemberId, Constant.LOG_TEXT_MODIFY_MEMBER_NAME, managementMemberName, memberName));
                            break;
                        case (int)Constant.MemberModifyModePosY.PASSWORD:
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_PASSWORD_BY_ADMINISTRATOR, managementMemberId, Constant.LOG_TEXT_MODIFY_MEMBER_PASSWORD));
                            break;
                        case (int)Constant.MemberModifyModePosY.BIRTH_DATE:
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_BY_ADMINISTRATOR, managementMemberId, Constant.LOG_TEXT_MODIFY_MEMBER_BIRTH_DATE, managementMemberBirthDate, memberBirthDate));
                            break;
                        case (int)Constant.MemberModifyModePosY.ADDRESS:
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_ADDRESS_BY_ADMINISTRATOR, managementMemberId, Constant.LOG_TEXT_MODIFY_MEMBER_ADDRESS, managementMemberAddress, memberAddress));
                            break;
                        case (int)Constant.MemberModifyModePosY.PHONE_NUMBER:
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_BY_ADMINISTRATOR, managementMemberId, Constant.LOG_TEXT_MODIFY_MEMBER_PHONE_NUMBER, managementMemberPhoneNumber, memberPhoneNumber));
                            break;
                        default:
                            break;
                    }

                    if (isModifyCompleted && IsReModifyByMember(administratorScreen)) // 계속해서 변경
                        ModifyMemberInformation(administratorScreen);
                }
            }

        }

    }
}
