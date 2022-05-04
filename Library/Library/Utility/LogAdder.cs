using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.View;
using Library.Model;

namespace Library.Utility
{
    class LogAdder
    {
        private static LogAdder logAdder;

        public static LogAdder GetLogAdder()
        {
            if (logAdder == null)
                logAdder = new LogAdder();
            return logAdder;
        }

        public void AddLogByModifyMember(BothScreen bothScreen, int currentConsoleCursorPosY, string modifyMemberId, string modifiedMemberName, string modifiedMemberPassword, string modifiedMemberBirthDate, string modifiedMemberAddress, string modifiedMemberPhoneNumber)
        {
            string modifyMemberName = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_NAME, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, modifyMemberId));
            string modifyMemberPassword = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_PASSWORD, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, modifyMemberId));
            string modifyMemberBirthDate = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_BIRTH_DATE, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, modifyMemberId));
            string modifyMemberAddress = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_ADDRESS, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, modifyMemberId));
            string modifyMemberPhoneNumber = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_PHONE_NUMBER, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.MEMBER_FILED_ID, modifyMemberId));

            if (bothScreen.GetType().ToString() == "Library.View.AdministratorScreen") // 관리자형식으로 로그찍기
            {
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.MemberModifyModePosY.NAME:
                        DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_BY_ADMINISTRATOR, modifyMemberId, Constant.LOG_TEXT_MODIFY_MEMBER_NAME, modifyMemberName, modifiedMemberName));
                        break;
                    case (int)Constant.MemberModifyModePosY.PASSWORD:
                        DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_BY_ADMINISTRATOR, modifyMemberId, Constant.LOG_TEXT_MODIFY_MEMBER_PASSWORD, modifyMemberPassword, modifiedMemberPassword));
                        break;
                    case (int)Constant.MemberModifyModePosY.BIRTH_DATE:
                        DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_BY_ADMINISTRATOR, modifyMemberId, Constant.LOG_TEXT_MODIFY_MEMBER_BIRTH_DATE, modifyMemberBirthDate, modifiedMemberBirthDate));
                        break;
                    case (int)Constant.MemberModifyModePosY.ADDRESS:
                        DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_BY_ADMINISTRATOR, modifyMemberId, Constant.LOG_TEXT_MODIFY_MEMBER_ADDRESS, modifyMemberAddress, modifiedMemberAddress));
                        break;
                    case (int)Constant.MemberModifyModePosY.PHONE_NUMBER:
                        DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_BY_ADMINISTRATOR, modifyMemberId, Constant.LOG_TEXT_MODIFY_MEMBER_PHONE_NUMBER, modifyMemberPhoneNumber, modifiedMemberPhoneNumber));
                        break;
                    default:
                        break;
                }
            }
            else // 회원형식으로 로그찍기
            {
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.MemberModifyModePosY.NAME:
                        DataBase.GetDataBase().AddLog(string.Format(Constant.LOG_MEMBER_TEXT_FORM, modifyMemberName, modifyMemberId), string.Format(Constant.LOG_STRING_MODIFY_MEMBER, Constant.LOG_TEXT_MODIFY_MEMBER_NAME, modifyMemberName, modifiedMemberName));
                        break;
                    case (int)Constant.MemberModifyModePosY.PASSWORD:
                        DataBase.GetDataBase().AddLog(string.Format(Constant.LOG_MEMBER_TEXT_FORM, modifyMemberName, modifyMemberId), string.Format(Constant.LOG_STRING_MODIFY_MEMBER, Constant.LOG_TEXT_MODIFY_MEMBER_PASSWORD, modifyMemberPassword, modifiedMemberPassword));
                        break;
                    case (int)Constant.MemberModifyModePosY.BIRTH_DATE:
                        DataBase.GetDataBase().AddLog(string.Format(Constant.LOG_MEMBER_TEXT_FORM, modifyMemberName, modifyMemberId), string.Format(Constant.LOG_STRING_MODIFY_MEMBER, Constant.LOG_TEXT_MODIFY_MEMBER_BIRTH_DATE, modifyMemberBirthDate, modifiedMemberBirthDate));
                        break;
                    case (int)Constant.MemberModifyModePosY.ADDRESS:
                        DataBase.GetDataBase().AddLog(string.Format(Constant.LOG_MEMBER_TEXT_FORM, modifyMemberName, modifyMemberId), string.Format(Constant.LOG_STRING_MODIFY_MEMBER, Constant.LOG_TEXT_MODIFY_MEMBER_ADDRESS, modifyMemberAddress, modifiedMemberAddress));
                        break;
                    case (int)Constant.MemberModifyModePosY.PHONE_NUMBER:
                        DataBase.GetDataBase().AddLog(string.Format(Constant.LOG_MEMBER_TEXT_FORM, modifyMemberName, modifyMemberId), string.Format(Constant.LOG_STRING_MODIFY_MEMBER, Constant.LOG_TEXT_MODIFY_MEMBER_PHONE_NUMBER, modifyMemberPhoneNumber, modifiedMemberPhoneNumber));
                        break;
                    default:
                        break;
                }

            }
        }

    }
}
