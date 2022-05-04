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
    class BookBorrower : BookSearcher
    {
        private bool isInputEscape;

        public void CheckBorrowedBook(MemberScreen memberScreen, string loginMemberId, string loginMemberName)
        {
            isInputEscape = false;
            memberScreen.PrintBorrowedBookListScreen();
            memberScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, loginMemberId, Constant.TEXT_NONE, Constant.FILED_BORROW_DATE), loginMemberId.ToString(), Constant.TEXT_NONE); // 대여도서확인시 테이블명은 각 유저의 id임
            Console.CursorVisible = false;
            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.CURSOR_POS_TOP);
            DataBase.GetDataBase().AddLog(string.Format(Constant.LOG_MEMBER_TEXT_FORM, loginMemberName, loginMemberId), Constant.LOG_TEXT_CHECK_BORROWED_BOOK_STATUS);
            while (!isInputEscape)
            {
                isInputEscape = DataProcessing.GetDataProcessing().IsOnlyInputEscape();
                if (isInputEscape) //esc 눌렀을때 뒤로가기
                    Console.CursorVisible = true;
            }
        }

        private bool IsReBorrow(MemberScreen memberScreen, int borrowMode)
        {
            int getYesOrNoByReborrowing;
            DataProcessing.GetDataProcessing().ClearErrorMessage();
            memberScreen.PrintMessage(Constant.TEXT_SUCCESS_BORROW, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            memberScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            getYesOrNoByReborrowing = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (getYesOrNoByReborrowing == Constant.INPUT_ENTER) // 계속해서 대여하시겠습니까? 에서 enter입력
            {
                if (borrowMode == (int)Constant.ModifyModePosY.IMMEDIATE)
                {
                    memberScreen.PrintBorrowBookScreen(); // 도서 대여 UI 출력
                    memberScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE); // 도서관에 보유중인 책 정보 표시
                }
                else
                {
                    memberScreen.PrintBorrowBookScreen(); // 도서 대여 UI 출력
                    memberScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK, GetConditionalStringByUserInput()), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
                }
                Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
                Console.SetCursorPosition(Constant.BORROW_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookBorrowPosY.ID); // 좌표조정
                return false;
            }
            if (getYesOrNoByReborrowing == Constant.INPUT_ESCAPE) // 계속해서 대여하시겠습니까? 에서 esc입력
                return true;

            return false;
        }

        private bool IsBorrowBookCompleted(MemberScreen memberScreen, int borrowMode, string bookId, string loginMemberId, string loginMemberName)
        {
            int getYesOrNoByBorrowing, checkInsertBorrowedBook = 0;
            string bookName = "";

            if ((bookId == "" || bookId == Constant.INPUT_ESCAPE.ToString()))// 입력값이 공백인지 체크
            {
                memberScreen.PrintMessage(Constant.TEXT_PLEASE_INPUT_OPTION, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.BORROW_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookBorrowPosY.ID); //좌표조정
                return false; // 다시입력받기
            }
            ////대여하기
            DataProcessing.GetDataProcessing().ClearErrorMessage();
            memberScreen.PrintConfirmationMessage("대여하시겠습니까??", ConsoleColor.Yellow);


            getYesOrNoByBorrowing = DataProcessing.GetDataProcessing().GetEnterOrEscape();

            if (getYesOrNoByBorrowing == Constant.INPUT_ENTER) // 대여하기 확인 문구에서 엔터 눌림
            {
                checkInsertBorrowedBook = DataBase.GetDataBase().GetBookBorrowResult(loginMemberId, int.Parse(bookId), GetSearchedBookIdList()); // 사용자의 개별 테이블에 대여도서 정보 등록하고 결과값 리턴
                switch (checkInsertBorrowedBook)
                {
                    case (int)Constant.CheckInsertBorrowedBook.NOT_EXIST_BOOK:
                        DataProcessing.GetDataProcessing().ClearErrorMessage();
                        memberScreen.PrintMessage(Constant.TEXT_IS_NOT_EXIST_IN_LIBRARY_OR_NOT_SEARCHED, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                        break;
                    case (int)Constant.CheckInsertBorrowedBook.DUPLICATE_BOOK_ID:
                        DataProcessing.GetDataProcessing().ClearErrorMessage();
                        memberScreen.PrintMessage(Constant.TEXT_IS_ALREADY_BORROWED, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                        break;
                    case (int)Constant.CheckInsertBorrowedBook.SHORTAGE_BOOK_QUANTITY:
                        DataProcessing.GetDataProcessing().ClearErrorMessage();
                        memberScreen.PrintMessage(Constant.TEXT_IS_NOT_ENOUGH_QUANTITY, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                        break;
                    case (int)Constant.CheckInsertBorrowedBook.SUCCESS:
                        bookName = DataBase.GetDataBase().GetSelectedElement(Constant.BOOK_FILED_NAME, Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, bookId));
                        DataBase.GetDataBase().AddLog(string.Format(Constant.LOG_MEMBER_TEXT_FORM, loginMemberName, loginMemberId), string.Format(Constant.LOG_STRING_FORM_CONTAIN_ID, bookName, bookId, Constant.LOG_TEXT_BORROW_BOOK));
                        return IsReBorrow(memberScreen, borrowMode);
                    default:
                        break;
                }
            }
            if (getYesOrNoByBorrowing == Constant.INPUT_ESCAPE)
            {
                memberScreen.PrintBorrowBookScreen(); // 도서 대여 UI 출력
                memberScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE); // 도서관에 보유중인 책 정보 표시
                Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
                Console.SetCursorPosition(Constant.BORROW_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookBorrowPosY.ID); //좌표조정
                return false;
            }

            if (checkInsertBorrowedBook == (int)Constant.CheckInsertBorrowedBook.NOT_EXIST_BOOK || checkInsertBorrowedBook == (int)Constant.CheckInsertBorrowedBook.DUPLICATE_BOOK_ID || checkInsertBorrowedBook == (int)Constant.CheckInsertBorrowedBook.SHORTAGE_BOOK_QUANTITY)
            {
                Console.SetCursorPosition(Constant.BORROW_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookBorrowPosY.ID); // 좌표조정
                return false;
            }
            return true;
        }

        public void Borrow(MemberScreen memberScreen, int borrowMode, string loginMemberId, string loginMemberName)
        {
            string bookId = "";
            int currentConsoleCursorPosY;
            bool isBorrowBookCompleted = false;
            isInputEscape = false;
            Console.CursorVisible = true;

            if (borrowMode == (int)Constant.ModifyModePosY.IMMEDIATE)
            {
                memberScreen.PrintBorrowBookScreen(); // 도서 대여 UI 출력
                memberScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE); // 도서관에 보유중인 책 정보 표시
            }
            else
            {
                InputBookSearchOption(memberScreen);
                memberScreen.PrintBorrowBookScreen(); // 도서 대여 UI 출력
                memberScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK, GetConditionalStringByUserInput()), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
            }

            Console.SetCursorPosition(0, 0);      //대여창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.BORROW_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookBorrowPosY.ID); //좌표조정

            while (!isInputEscape && !isBorrowBookCompleted)
            {
                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.SEARCH_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.BookBorrowPosY.ID, (int)Constant.BookBorrowPosY.BORROW);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.BookBorrowPosY.ID:
                        bookId = DataProcessing.GetDataProcessing().GetInputValues(memberScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.ID, Constant.MAX_LENGTH_BOOK_ID, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_ID);
                        break;
                    case (int)Constant.BookBorrowPosY.BORROW:
                        isBorrowBookCompleted = IsBorrowBookCompleted(memberScreen, borrowMode, bookId, loginMemberId, loginMemberName);
                        break;
                    default:
                        break;
                }
            }
            /*
            if (borrowMode == (int)Constant.ModifyModePosY.SEARCH)
                InputBookSearchOption(memberScreen);
            */
        }

    }
}
