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

        public void AddLogByModifyBook(int currentConsoleCursorPosY, string modifyBookId, string modifiedBookName, string modifiedBookPublisher, string modifiedBookAuthor, string modifiedBookPrice, string modifiedBookQuantity)
        {
            string modifyBookName = DataBase.GetDataBase().GetSelectedElement(Constant.BOOK_FILED_NAME, Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, modifyBookId));
            string modifyBookPublisher = DataBase.GetDataBase().GetSelectedElement(Constant.BOOK_FILED_PUBLISHER, Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, modifyBookId));
            string modifyBookAuthor = DataBase.GetDataBase().GetSelectedElement(Constant.BOOK_FILED_AUTHOR, Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, modifyBookId));
            string modifyBookPrice = DataBase.GetDataBase().GetSelectedElement(Constant.BOOK_FILED_PRICE, Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, modifyBookId));
            string modifyBookQuantity = DataBase.GetDataBase().GetSelectedElement(Constant.BOOK_FILED_QUANTITY, Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, modifyBookId));

            switch (currentConsoleCursorPosY) 
            {
                case (int)Constant.BookModifyPosY.NAME:
                    DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_BOOK, modifyBookName, modifyBookId, Constant.LOG_TEXT_MODIFY_BOOK_NAME, modifyBookName, modifiedBookName));
                    break;
                case (int)Constant.BookModifyPosY.PUBLISHER:
                    DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_BOOK, modifyBookName, modifyBookId, Constant.LOG_TEXT_MODIFY_BOOK_PUBLISHER, modifyBookPublisher, modifiedBookPublisher));
                    break;
                case (int)Constant.BookModifyPosY.AUTHOR:
                    DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_BOOK, modifyBookName, modifyBookId, Constant.LOG_TEXT_MODIFY_BOOK_AUTHOR, modifyBookAuthor, modifiedBookAuthor));
                    break;
                case (int)Constant.BookModifyPosY.PRICE:
                    DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_BOOK, modifyBookName, modifyBookId, Constant.LOG_TEXT_MODIFY_BOOK_PRICE, modifyBookPrice, modifiedBookPrice));
                    break;
                case (int)Constant.BookModifyPosY.QUANTITY:
                    DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_BOOK, modifyBookName, modifyBookId, Constant.LOG_TEXT_MODIFY_BOOK_QUANTITY, modifyBookQuantity, modifiedBookQuantity));
                    break;
                default:
                    break;
            }
        }
    }
}
