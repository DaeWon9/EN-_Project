using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Library.Utility;
using Library.Model;
using Library.View;

namespace Library.Controller
{
    class BorrowedBookStatus
    {
        private bool isInputEscape;

        private bool IsAlreadyRegisteredBookInLibrary(string bookId)
        {
            List<string> memberIdList = DataBase.GetDataBase().GetSelectedElements(Constant.BOOK_FILED_ID, Constant.TABLE_NAME_BOOK);
            for (int repeat = 0; repeat < memberIdList.Count; repeat++)
            {
                if (memberIdList[repeat] == bookId)
                    return true;
            }
            return false;
        }

        public void Show(AdministratorScreen administratorScreen)
        {
            isInputEscape = false;
            string memberName = "", memberId = "", bookId = "";
            int currentConsoleCursorPosY;
            List<string> AllTablesName = DataBase.GetDataBase().GetAllTablesName();

            administratorScreen.PrintSelectCheckBorrowedBookModeScreen();
            foreach (string tableName in AllTablesName)
            {
                memberName = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_NAME, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, tableName));
                if (tableName != Constant.TABLE_NAME_ADMINISTRATOR && tableName != Constant.TABLE_NAME_MEMBER && tableName != Constant.TABLE_NAME_BOOK && tableName != Constant.TABLE_NAME_LOG)
                {
                    administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, tableName, Constant.TEXT_NONE, Constant.FILED_BORROW_DATE), tableName, memberName, Constant.IS_ADMINISTRATOR_MODE);
                }
            }
            Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.SEARCH_POS_X, (int)Constant.CheckBorrowedBookModePosY.BOOK_ID); //좌표조정

            while (!isInputEscape)
            {
                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.SELECT_MANAGEMENT_MEMBER_ID_OPTION_POS_X, Console.CursorTop, (int)Constant.SelectMemberIdPosY.ID, (int)Constant.SelectMemberIdPosY.MANAGEMEMT_MEMBER);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.CheckBorrowedBookModePosY.BOOK_ID:
                        bookId = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.CheckBorrowedBookModePosY.BOOK_ID, Constant.MAX_LENGTH_BOOK_ID, "숫자만 입력하세요", Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_ID);
                        ShowBorrowBookStatusByBookId(administratorScreen, bookId);
                        break;
                    case (int)Constant.CheckBorrowedBookModePosY.MEMBER_ID:
                        memberId = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.CheckBorrowedBookModePosY.MEMBER_ID, Constant.MAX_LENGTH_MEMBER_ID, "영어 & 숫자만 입력하세요", Constant.EXCEPTION_TYPE_ENGLISH_NUMBER, Constant.EXCEPTION_TYPE_MEMBER_ID);
                        ShowBorrowBookStatusByMemberId(administratorScreen, memberId);
                        break;
                    default:
                        break;
                }
            }
        }

        private void ShowBorrowBookStatusByBookId(AdministratorScreen administratorScreen, string bookId)
        {
            if (!IsAlreadyRegisteredBookInLibrary(bookId))// 도서관에 없는책임
            {
                administratorScreen.PrintMessage("도서관에 없는 도서입니다.", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.SEARCH_POS_X, Constant.WINDOW_WIDTH, (int)Constant.CheckBorrowedBookModePosY.BOOK_ID);
                return;
            }
            isInputEscape = false;
            string memberName = "", conditionalString = "";
            List<string> AllTablesName = DataBase.GetDataBase().GetAllTablesName();

            conditionalString = string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_INT, Constant.BOOK_FILED_ID, bookId);

            administratorScreen.PrintAdministratorCheckBorrowedBookLabel();
            foreach (string tableName in AllTablesName)
            {
                memberName = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_NAME, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, tableName));
                if (tableName != Constant.TABLE_NAME_ADMINISTRATOR && tableName != Constant.TABLE_NAME_MEMBER && tableName != Constant.TABLE_NAME_BOOK && tableName != Constant.TABLE_NAME_LOG)
                {
                    MySqlDataReader reader = DataBase.GetDataBase().Select(Constant.FILED_ALL, tableName, conditionalString);
                    administratorScreen.PrintSelectedValues(reader, tableName, memberName, Constant.IS_ADMINISTRATOR_MODE);
                }
            }
            Console.CursorVisible = false;
            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.CURSOR_POS_TOP);
            while (!isInputEscape)
            {
                isInputEscape = DataProcessing.GetDataProcessing().IsOnlyInputEscape();
                if (isInputEscape) //esc 눌렀을때 뒤로가기
                {
                    Console.CursorVisible = true;
                    Show(administratorScreen);
                }
            }
        }

        private void ShowBorrowBookStatusByMemberId(AdministratorScreen administratorScreen, string memberId)
        {

            if (!DataBase.GetDataBase().IsRegisteredMemberId(memberId))// 회원아이디가 입력됐는데, 등록되지 않은 아이디임
            {
                administratorScreen.PrintMessage("등록되지않은 회원ID입니다.", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.SEARCH_POS_X, Constant.WINDOW_WIDTH, (int)Constant.CheckBorrowedBookModePosY.MEMBER_ID);
                return;
            }


            isInputEscape = false;
            string memberName = "";
            administratorScreen.PrintAdministratorCheckBorrowedBookLabel();
            memberName = DataBase.GetDataBase().GetSelectedElement(Constant.MEMBER_FILED_NAME, Constant.TABLE_NAME_MEMBER, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, memberId));
            administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, memberId, Constant.TEXT_NONE, Constant.FILED_BORROW_DATE), memberId, memberName, Constant.IS_ADMINISTRATOR_MODE, Constant.IS_TITLE_OPTION);
            Console.CursorVisible = false;
            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.CURSOR_POS_TOP);
            while (!isInputEscape)
            {
                isInputEscape = DataProcessing.GetDataProcessing().IsOnlyInputEscape();
                if (isInputEscape) //esc 눌렀을때 뒤로가기
                {
                    Console.CursorVisible = true;
                    Show(administratorScreen);
                }
            }
        }

    }
}
