using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Library.Utility;
using Library.Model;
using Library.View;

namespace Library.Controller
{
    class BookModifier : BookSearcher
    {
        private int modifyBookId = 0;
        private string modifyBookName = "", modifyBookPublisher = "", modifyBookAuthor = "", modifyBookPrice = "", modifyBookQuantity = "";

        private bool IsModifyBookInformationCompleted(AdministratorScreen administratorScreen, string setString, int bookId)
        {
            int getYesOrNoByModify;
            administratorScreen.PrintMessage(Constant.TEXT_IS_MODIFY, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y); //좌표조정
            getYesOrNoByModify = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (getYesOrNoByModify == Constant.INPUT_ENTER) // 변경하시겠습니까? 에서 enter입력
            {
                DataBase.GetDataBase().Update(Constant.TABLE_NAME_BOOK, setString, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_INT, Constant.BOOK_FILED_ID, bookId));
                return true;
            }
            if (getYesOrNoByModify == Constant.INPUT_ESCAPE) // 변경하시겠습니까? 에서 esc입력
            {
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.MODIFY_BOOK_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.BookModifyPosY.NAME);
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.MODIFY_BOOK_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.BookModifyPosY.PUBLISHER);
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.MODIFY_BOOK_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.BookModifyPosY.AUTHOR);
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.MODIFY_BOOK_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.BookModifyPosY.PRICE);
                DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.MODIFY_BOOK_INPUT_POS_X, Constant.WINDOW_WIDTH, (int)Constant.BookModifyPosY.QUANTITY);
                DataProcessing.GetDataProcessing().ClearErrorMessage();
                Console.SetCursorPosition(Constant.MODIFY_SELECT_OPTION_POS_X, (int)Constant.BookModifyPosY.NAME); //좌표조정
                return false;
            }
            return true;
        }

        private bool IsReModifyByBook(AdministratorScreen administratorScreen)
        {
            int getYesOrNoByReModify;
            administratorScreen.PrintMessage(Constant.TEXT_SUCCESS_MODIFY, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            getYesOrNoByReModify = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (getYesOrNoByReModify == Constant.INPUT_ESCAPE) // 계속해서 변경 x
                return false;
            return true;
        }

        private void Modify(AdministratorScreen administratorScreen) //  BookId 는 primaryKey 이므로 수정불가능하게 설정
        {
            string setStringByUpdate = "";
            string bookName = "", bookPublisher = "", bookAuthor = "", bookPrice = "", bookQuantity = "";
            int currentConsoleCursorPosY;
            bool isModifyCompleted = false, isBookDeleteCompleted = false, isInputEscape = false;

            administratorScreen.PrintModifyBookLabel();
            administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK, String.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_INT, Constant.BOOK_FILED_ID, modifyBookId)), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
            administratorScreen.PrintModifyBookScreen();

            Console.SetCursorPosition(Constant.MODIFY_SELECT_OPTION_POS_X, (int)Constant.BookModifyPosY.NAME); //좌표조정

            while (!isInputEscape && !isModifyCompleted && !isBookDeleteCompleted)
            {
                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.MODIFY_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.BookModifyPosY.NAME, (int)Constant.BookModifyPosY.DELETE);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.BookModifyPosY.NAME:
                        bookName = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.MODIFY_BOOK_INPUT_POS_X, (int)Constant.BookModifyPosY.NAME, Constant.MAX_LENGTH_BOOK_NAME, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_NAME);
                        setStringByUpdate = DataProcessing.GetDataProcessing().GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.BOOK_FILED_NAME, bookName);
                        break;
                    case (int)Constant.BookModifyPosY.PUBLISHER:
                        bookPublisher = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.MODIFY_BOOK_INPUT_POS_X, (int)Constant.BookModifyPosY.PUBLISHER, Constant.MAX_LENGTH_BOOK_PUBLISHER, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_PUBLISHER);
                        setStringByUpdate = DataProcessing.GetDataProcessing().GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.BOOK_FILED_PUBLISHER, bookPublisher);
                        break;
                    case (int)Constant.BookModifyPosY.AUTHOR:
                        bookAuthor = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.MODIFY_BOOK_INPUT_POS_X, (int)Constant.BookModifyPosY.AUTHOR, Constant.MAX_LENGTH_BOOK_AUTHOR, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER_KOREA, Constant.EXCEPTION_TYPE_BOOK_AUTHOR);
                        setStringByUpdate = DataProcessing.GetDataProcessing().GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.BOOK_FILED_AUTHOR, bookAuthor);
                        break;
                    case (int)Constant.BookModifyPosY.PRICE:
                        bookPrice = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.MODIFY_BOOK_INPUT_POS_X, (int)Constant.BookModifyPosY.PRICE, Constant.MAX_LENGTH_BOOK_PRICE, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_PRICE);
                        setStringByUpdate = DataProcessing.GetDataProcessing().GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.BOOK_FILED_PRICE, bookPrice);
                        break;
                    case (int)Constant.BookModifyPosY.QUANTITY:
                        bookQuantity = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.MODIFY_BOOK_INPUT_POS_X, (int)Constant.BookModifyPosY.QUANTITY, Constant.MAX_LENGTH_BOOK_QUANTITY, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_QUANTITY);
                        setStringByUpdate = DataProcessing.GetDataProcessing().GetStringByUpdate(Constant.SET_STRING_EQUAL_BY_STRING, Constant.BOOK_FILED_QUANTITY, bookQuantity);
                        break;
                    case (int)Constant.BookModifyPosY.DELETE:
                        //isBookDeleteCompleted = IsDeleteBookCompleted(administratorScreen, modifyBookId);
                        break;
                    default:
                        break;
                }

                if (setStringByUpdate != "") // 수정사항이 있다면
                {
                    isModifyCompleted = IsModifyBookInformationCompleted(administratorScreen, setStringByUpdate, modifyBookId);


                    switch (currentConsoleCursorPosY) // 로그추가
                    {
                        case (int)Constant.BookModifyPosY.NAME:
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_BOOK_NAME_BY_ADMINISTRATOR, modifyBookId, Constant.LOG_TEXT_MODIFY_BOOK_NAME, modifyBookName, bookName));
                            break;
                        case (int)Constant.BookModifyPosY.PUBLISHER:
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_BY_ADMINISTRATOR, modifyBookId, Constant.LOG_TEXT_MODIFY_BOOK_PUBLISHER, modifyBookPublisher, bookPublisher));
                            break;
                        case (int)Constant.BookModifyPosY.AUTHOR:
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_BY_ADMINISTRATOR, modifyBookId, Constant.LOG_TEXT_MODIFY_BOOK_AUTHOR, modifyBookAuthor, bookAuthor));
                            break;
                        case (int)Constant.BookModifyPosY.PRICE:
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_BY_ADMINISTRATOR, modifyBookId, Constant.LOG_TEXT_MODIFY_BOOK_PRICE, modifyBookPrice, bookPrice));
                            break;
                        case (int)Constant.BookModifyPosY.QUANTITY:
                            DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_MODIFY_MEMBER_BY_ADMINISTRATOR, modifyBookId, Constant.LOG_TEXT_MODIFY_BOOK_QUANTITY, modifyBookQuantity, bookQuantity));
                            break;
                        default:
                            break;
                    }


                    if (isModifyCompleted && IsReModifyByBook(administratorScreen))
                        ModifyBook(administratorScreen);
                }
            }
        }

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

        public void SelectModifyBookId(AdministratorScreen administratorScreen, int modifyMode)
        {
            string bookId = "";
            int currentConsoleCursorPosY, getYesOrNoByModify;
            bool isSelectBookIdCompleted = false, isInputEscape = false;

            if (modifyMode == (int)Constant.ModifyModePosY.IMMEDIATE)
            {
                administratorScreen.PrintSelectModifyBookScreen();
                administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
            }
            else
            {
                InputBookSearchOption(administratorScreen);
                administratorScreen.PrintSelectModifyBookScreen();
                administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK, GetConditionalStringByUserInput()), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
            }

            Console.SetCursorPosition(0, 0);      //입력창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.SELECT_MODIFY_BOOK_ID_OPTION_POS_X, (int)Constant.SelectBookIdPosY.ID); //좌표조정

            while (!isInputEscape && !isSelectBookIdCompleted)
            {
                if (bookId != "" && !IsAlreadyRegisteredBookInLibrary(bookId))// 책이름이 입력됐는데, 도서관에 없는책임
                {
                    administratorScreen.PrintMessage(Constant.TEXT_IS_NOT_EXIST_IN_LIBRARY, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    Console.SetCursorPosition(Constant.SELECT_MODIFY_BOOK_ID_OPTION_POS_X, (int)Constant.SelectBookIdPosY.ID); //좌표조정
                    DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.SELECT_MODIFY_BOOK_ID_POS_X, Constant.WINDOW_WIDTH, (int)Constant.SelectBookIdPosY.ID);
                    bookId = "";
                }

                if (modifyMode == (int)Constant.ModifyModePosY.SEARCH && bookId != "" && !IsExistBookIdInSearchedBookList(bookId)) // 검색 후 책이름 입력됐는데, 검색된 책이 아님
                {
                    administratorScreen.PrintMessage(Constant.TEXT_THIS_BOOK_IS_NOT_SEARCHED, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    Console.SetCursorPosition(Constant.SELECT_MODIFY_BOOK_ID_OPTION_POS_X, (int)Constant.SelectBookIdPosY.ID); //좌표조정
                    DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.SELECT_MODIFY_BOOK_ID_POS_X, Constant.WINDOW_WIDTH, (int)Constant.SelectBookIdPosY.ID);
                    bookId = "";
                }

                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.SELECT_MODIFY_BOOK_ID_OPTION_POS_X, Console.CursorTop, (int)Constant.SelectBookIdPosY.ID, (int)Constant.SelectBookIdPosY.MODIFY_BOOK);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.SelectBookIdPosY.ID:
                        bookId = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SELECT_MODIFY_BOOK_ID_POS_X, (int)Constant.SelectBookIdPosY.ID, Constant.MAX_LENGTH_BOOK_ID, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_ID);
                        break;
                    case (int)Constant.SelectBookIdPosY.MODIFY_BOOK:
                        if (bookId != "" && bookId != Constant.INPUT_ESCAPE.ToString())
                        {
                            administratorScreen.PrintMessage(Constant.TEXT_IS_MODIFY, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
                            administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
                            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y); //좌표조정
                            getYesOrNoByModify = DataProcessing.GetDataProcessing().GetEnterOrEscape();
                            if (getYesOrNoByModify == Constant.INPUT_ENTER)
                            {
                                modifyBookId = int.Parse(bookId);
                                isSelectBookIdCompleted = true;
                            }
                            if (getYesOrNoByModify == Constant.INPUT_ESCAPE)
                            {
                                DataProcessing.GetDataProcessing().ClearErrorMessage();
                                Console.SetCursorPosition(Constant.SELECT_MODIFY_BOOK_ID_OPTION_POS_X, (int)Constant.SelectBookIdPosY.ID); //좌표조정
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            if (isSelectBookIdCompleted)
            {
                modifyBookName = DataBase.GetDataBase().GetSelectedElement(Constant.BOOK_FILED_NAME, Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, bookId)); // 수정할 도서의 이름 저장
                modifyBookPublisher = DataBase.GetDataBase().GetSelectedElement(Constant.BOOK_FILED_PUBLISHER, Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, bookId)); // 수정할 도서의 출판사 저장
                modifyBookAuthor = DataBase.GetDataBase().GetSelectedElement(Constant.BOOK_FILED_AUTHOR, Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, bookId)); // 수정할 도서의 저자 저장
                modifyBookPrice = DataBase.GetDataBase().GetSelectedElement(Constant.BOOK_FILED_PRICE, Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, bookId)); // 수정할 도서의 가격 저장
                modifyBookQuantity = DataBase.GetDataBase().GetSelectedElement(Constant.BOOK_FILED_QUANTITY, Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, bookId)); // 수정할 도서의 수량 저장
            }

            if (!isInputEscape)
                ModifyBook(administratorScreen);

        }

        private bool IsBookBorrwed(int bookId)
        {
            string conditionalString;
            List<string> AllTablesName = DataBase.GetDataBase().GetAllTablesName();
            conditionalString = string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_INT, Constant.BOOK_FILED_ID, bookId);
            foreach (string tableName in AllTablesName)
            {
                if (tableName != Constant.TABLE_NAME_ADMINISTRATOR && tableName != Constant.TABLE_NAME_MEMBER && tableName != Constant.TABLE_NAME_BOOK && tableName != Constant.TABLE_NAME_LOG)
                {
                    MySqlDataReader reader = DataBase.GetDataBase().Select(Constant.FILED_ALL, tableName, conditionalString);
                    if (reader.HasRows)
                    {
                        reader.Close();
                        return true;
                    }
                    else
                        reader.Close();
                }
            }
            return false;
        }

        private bool IsDeleteBookCompleted(AdministratorScreen administratorScreen, int bookId)
        {
            int getYesOrNoByDeleteBook;
            string bookName;
            if (IsBookBorrwed(bookId)) // 해당책을 대여한 회원이 있음
            {
                administratorScreen.PrintMessage(Constant.TEXT_UNABLE_DELETE, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.MODIFY_SELECT_OPTION_POS_X, (int)Constant.BookModifyPosY.NAME); //좌표조정
                return false;
            }
            else // 클린한 상태임 -> 도서삭제 가능
            {
                administratorScreen.PrintMessage(Constant.TEXT_IS_DELETE, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Red); // 정말로 탈퇴할건지 물어보기
                administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y); //좌표조정

                getYesOrNoByDeleteBook = DataProcessing.GetDataProcessing().GetEnterOrEscape();
                if (getYesOrNoByDeleteBook == Constant.INPUT_ENTER) // 삭제진행
                {
                    bookName = DataBase.GetDataBase().GetSelectedElement(Constant.BOOK_FILED_NAME, Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_ID, bookId));
                    DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_FORM_CONTAIN_ID, bookName, bookId, Constant.LOG_TEXT_DELETE_BOOK));
                    DataBase.GetDataBase().Delete(Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_INT, Constant.BOOK_FILED_ID, bookId));// 해당도서 delete
                }
                if (getYesOrNoByDeleteBook == Constant.INPUT_ESCAPE) // 삭제취소
                {
                    DataProcessing.GetDataProcessing().ClearErrorMessage();
                    Console.SetCursorPosition(Constant.MODIFY_SELECT_OPTION_POS_X, (int)Constant.BookModifyPosY.NAME); //좌표조정
                    return false;
                }
            }
            return true;
        }

    }
}
