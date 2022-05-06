using System;
using System.Collections.Generic;
using Library.Utility;
using Library.Model;
using Library.View;

namespace Library.Controller
{
    class BookAdder
    {
        public bool IsAlreadyRegisteredBookISBNInLibrary(string bookISBN)
        {
            List<string> bookISBNList = DataBase.GetDataBase().GetSelectedElements(Constant.BOOK_FILED_ISBN, Constant.TABLE_NAME_BOOK);
            for (int repeat = 0; repeat < bookISBNList.Count; repeat++)
            {
                if (bookISBNList[repeat] == bookISBN)
                    return true;
            }
            return false;
        }

        private bool IsAddBookCompleted(AdministratorScreen administratorScreen, string bookName, string bookPublisher, string bookAuthor, string bookPrice, string bookQuantity, string bookPublicationDate, string bookISBN)
        {
            int GetYesOrNoByAdd, GetYesOrNoByReAdd;
            string bookId = "";
            // 모든 값이 입력됐는지 체크
            if ((bookName == "" || bookName == Constant.INPUT_ESCAPE.ToString()) || (bookPublisher == "" || bookPublisher == Constant.INPUT_ESCAPE.ToString()) || (bookAuthor == "" || bookAuthor == Constant.INPUT_ESCAPE.ToString()) || (bookPrice == "" || bookPrice == Constant.INPUT_ESCAPE.ToString()) || (bookQuantity == "" || bookQuantity == Constant.INPUT_ESCAPE.ToString()) || (bookPublicationDate == "" || bookPublicationDate == Constant.INPUT_ESCAPE.ToString()) || (bookISBN == "" || bookISBN == Constant.INPUT_ESCAPE.ToString()))
            {
                administratorScreen.PrintMessage("옵션을 입력해주세요", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.ADD_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookAddPosY.NAME); //좌표조정
                return false;
            }
            if (IsAlreadyRegisteredBookISBNInLibrary(bookISBN))
            {
                administratorScreen.PrintMessage("이미등록되어있는 도서입니다.", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.ADD_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookAddPosY.NAME); //좌표조정
                return false;
            }

            administratorScreen.PrintConfirmationMessage("추가하시겠습니까??", ConsoleColor.Yellow);

            GetYesOrNoByAdd = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (GetYesOrNoByAdd == Constant.INPUT_ESCAPE) // 추가하시겠습니까?? -> ESC
            {
                DataProcessing.GetDataProcessing().ClearErrorMessage();
                Console.SetCursorPosition(Constant.ADD_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookAddPosY.NAME); //좌표조정
                return false;
            }
            if (GetYesOrNoByAdd == Constant.INPUT_ENTER) // 추가하시겠습니까?? -> ENTER
            {
                DataBase.GetDataBase().InsertAddBook(Constant.TABLE_NAME_BOOK, bookName, bookPublisher, bookAuthor, int.Parse(bookPrice), int.Parse(bookQuantity), bookPublicationDate, bookISBN);
                bookId = DataBase.GetDataBase().GetSelectedElement(Constant.BOOK_FILED_ID, Constant.TABLE_NAME_BOOK, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING, Constant.BOOK_FILED_NAME, bookName));
                DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_FORM_CONTAIN_ID, bookName, bookId, Constant.LOG_TEXT_ADD_BOOK));
                Console.CursorVisible = false;
                administratorScreen.PrintMessage("도서추가에 성공하였습니다! 계속해서 추가하시겠습니까??", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
                administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);

                GetYesOrNoByReAdd = DataProcessing.GetDataProcessing().GetEnterOrEscape();
                if (GetYesOrNoByReAdd == Constant.INPUT_ENTER) // 계속해서 추가할것임.
                    Add(administratorScreen);
            }
            return true;
        }

        public void Add(AdministratorScreen administratorScreen)
        {
            string bookName = "", bookPublisher = "", bookAuthor = "", bookPrice = "", bookQuantity = "", bookPublicationDate = "", bookISBN = "";
            int currentConsoleCursorPosY;
            bool isAddBookCompleted = false, isInputEscape = false;
            Console.CursorVisible = true;

            administratorScreen.PrintAddBookScreen();
            administratorScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
            Console.SetCursorPosition(0, 0);      //입력창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.ADD_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookAddPosY.NAME); //좌표조정

            while (!isInputEscape && !isAddBookCompleted)
            {
                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.ADD_BOOK_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.BookAddPosY.NAME, (int)Constant.BookAddPosY.ADD);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.BookAddPosY.NAME:
                        bookName = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.NAME, Constant.MAX_LENGTH_BOOK_NAME, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_NAME);
                        break;
                    case (int)Constant.BookAddPosY.PUBLISHER:
                        bookPublisher = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.PUBLISHER, Constant.MAX_LENGTH_BOOK_PUBLISHER, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_PUBLISHER);
                        break;
                    case (int)Constant.BookAddPosY.AUTHOR:
                        bookAuthor = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.AUTHOR, Constant.MAX_LENGTH_BOOK_AUTHOR, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER_KOREA, Constant.EXCEPTION_TYPE_BOOK_AUTHOR);
                        break;
                    case (int)Constant.BookAddPosY.PRICE:
                        bookPrice = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.PRICE, Constant.MAX_LENGTH_BOOK_PRICE, "숫자만 입력하세요", Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_PRICE);
                        break;
                    case (int)Constant.BookAddPosY.QUANTITY:
                        bookQuantity = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.QUANTITY, Constant.MAX_LENGTH_BOOK_QUANTITY, "숫자만 입력하세요", Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_QUANTITY);
                        break;
                    case (int)Constant.BookAddPosY.PUBLICATION_DATE:
                        bookPublicationDate = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.PUBLICATION_DATE, Constant.MAX_LENGTH_DATE, "숫자만 입력하세요", Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_DATE);
                        break;
                    case (int)Constant.BookAddPosY.ISBN:
                        bookISBN = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.ISBN, Constant.MAX_LENGTH_BOOK_ISBN, "숫자만 입력하세요", Constant.EXCEPTION_TYPE_NUMBER_SPACE_PYPHEN, Constant.EXCEPTION_TYPE_ANY);
                        break;
                    case (int)Constant.BookAddPosY.ADD:
                        isAddBookCompleted = IsAddBookCompleted(administratorScreen, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity, bookPublicationDate, bookISBN);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
