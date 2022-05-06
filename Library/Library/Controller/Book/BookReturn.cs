using System;
using System.Collections.Generic;
using Library.Utility;
using Library.Model;
using Library.View;

namespace Library.Controller
{
    class BookReturn
    {
        public void Return(MemberScreen memberScreen, string loginMemberId, string loginMemberName)
        {
            bool isRetunrBookCompleted = false, isInputEscape = false;
            int currentConsoleCursorPosY;
            string bookId = "";

            memberScreen.PrintReturnBookScreen();
            memberScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, loginMemberId, Constant.TEXT_NONE, Constant.FILED_BORROW_DATE), loginMemberId.ToString(), Constant.TEXT_NONE); // 대여도서확인시 테이블명은 각 유저의 id임
            Console.SetCursorPosition(0, 0);      //대여창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.BORROW_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookBorrowPosY.ID); //좌표조정

            while (!isInputEscape && !isRetunrBookCompleted)
            {
                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.SEARCH_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.BookBorrowPosY.ID, (int)Constant.BookBorrowPosY.BORROW);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.BookReturnPosY.ID:
                        bookId = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.ID, Constant.MAX_LENGTH_BOOK_ID, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_ID);
                        break;
                    case (int)Constant.BookReturnPosY.RETURN:
                        isRetunrBookCompleted = IsReturnBookCompleted(memberScreen, bookId, loginMemberId, loginMemberName);
                        break;
                    default:
                        break;
                }
            }
        }

        private bool IsBorrowedBookIdListContainReturnBookId(string returnBookId, string loginMemberId)
        {
            List<string> memberBorrowedBookIdList = DataBase.GetDataBase().GetSelectedElements(Constant.BOOK_FILED_ID, loginMemberId);
            for (int repeat = 0; repeat < memberBorrowedBookIdList.Count; repeat++)
            {
                if (memberBorrowedBookIdList[repeat] == returnBookId)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsReturnBookCompleted(MemberScreen memberScreen, string returnBookId, string loginMemberId, string loginMemberName) // 반납시 해당유저의 대여도서 목록에서는 제거 & 도서관 책정보에서는 수량 + 1
        {
            string returnBookName = "";
            int getYesOrNoByReturn, getYesOrNoByReturnAgain;

            if ((returnBookId == "" || returnBookId == Constant.INPUT_ESCAPE.ToString()))// 입력값이 공백인지 체크
            {
                memberScreen.PrintMessage(Constant.TEXT_PLEASE_INPUT_OPTION, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.BORROW_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookBorrowPosY.ID); //좌표조정
                return false; // 다시입력받기
            }

            DataProcessing.GetDataProcessing().ClearErrorMessage();
            memberScreen.PrintMessage(Constant.TEXT_IS_RETURN, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            memberScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);

            getYesOrNoByReturn = DataProcessing.GetDataProcessing().GetEnterOrEscape();

            if (getYesOrNoByReturn == Constant.INPUT_ENTER) // 반납하기 확인 문구에서 엔터 눌림
            {
                if (IsBorrowedBookIdListContainReturnBookId(returnBookId, loginMemberId)) // 반납하고자하는 도서아이디가 대여중인 도서목록에 있다면
                { //반납하는 쿼리문실행 -> 유저의 대여도서목록에서는  delete, 도서관 보유 책수량은 1플러스

                    returnBookName = DataBase.GetDataBase().GetSelectedElement(Constant.BOOK_FILED_NAME, Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, returnBookId));
                    DataBase.GetDataBase().AddLog(string.Format(Constant.LOG_MEMBER_TEXT_FORM, loginMemberName, loginMemberId), string.Format(Constant.LOG_STRING_FORM_CONTAIN_ID, returnBookName, returnBookId, Constant.LOG_TEXT_RETURN_BOOK));

                    DataBase.GetDataBase().Delete(loginMemberId, String.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, returnBookId));
                    DataBase.GetDataBase().PlusBookQuantity(int.Parse(returnBookId));
                    DataProcessing.GetDataProcessing().ClearErrorMessage();
                    memberScreen.PrintMessage(Constant.TEXT_SUCCESS_RETURN_BOOK, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
                    memberScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow); // 계속해서 반납의사 물어보는 문구
                    getYesOrNoByReturnAgain = DataProcessing.GetDataProcessing().GetEnterOrEscape(); // enter or esc받을때까지 입력받음
                    if (getYesOrNoByReturnAgain == Constant.INPUT_ENTER) // 계속해서 반납하기 -> 즉 반납이 끝나지 않음
                    {
                        memberScreen.PrintReturnBookScreen();
                        memberScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, loginMemberId.ToString(), Constant.TEXT_NONE, Constant.FILED_BORROW_DATE), loginMemberId.ToString(), Constant.TEXT_NONE); // 대여도서확인시 테이블명은 각 유저의 id임
                        Console.SetCursorPosition(0, 0);      //대여창 보이게 맨위로 올리고 
                        Console.SetCursorPosition(Constant.BORROW_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookBorrowPosY.ID); //좌표조정
                        return false;
                    }
                    if (getYesOrNoByReturnAgain == Constant.INPUT_ESCAPE) // 그만 반납하기 -> 즉 반납이 끝남
                        return true;
                }
                else // 대여중인 도서목록에 반납할도서가 없다면. 반납 실패.
                {
                    DataProcessing.GetDataProcessing().ClearErrorMessage();
                    memberScreen.PrintMessage(Constant.TEXT_IS_NOT_BORROWED_BOOK, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    Console.SetCursorPosition(Constant.BORROW_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookBorrowPosY.ID); //좌표조정
                    return false;
                }
            }
            return true;
        }

    }
}
