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
    class BookSearcher
    {
        private string conditionalStringByUserInput = "";
        private List<string> searchedBookIdList = new List<string>();
        private bool isBack = false;

        public List<string> GetSearchedBookIdList()
        {
            return searchedBookIdList;
        }

        public string GetConditionalStringByUserInput()
        {
            return conditionalStringByUserInput;
        }

        public bool IsExistBookIdInSearchedBookList(string bookId)
        {
            for (int repeat = 0; repeat < searchedBookIdList.Count; repeat++)
            {
                if (searchedBookIdList[repeat] == bookId)
                    return true;
            }
            return false;
        }

        public void Search(BothScreen bothScreen)
        {
            if (IsInputBookSearchOption(bothScreen))
                ShowSearchedBookInformation(bothScreen);
        }

        public bool IsInputBookSearchOption(BothScreen bothScreen)
        {
            string bookId = "", bookName = "", bookPublisher = "", bookAuthor = "", bookISBN = "", bookPrice = "", bookQuantity = "";
            int currentConsoleCursorPosY;
            bool isGetConditionalStringCompleted = false, isInputEscape = false;
            Console.CursorVisible = true;

            bothScreen.PrintBookSearchScreen();
            bothScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
            Console.SetCursorPosition(0, 0);      //검색창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.BookSearchPosY.ID); //좌표조정

            while (!isInputEscape && !isGetConditionalStringCompleted)
            {
                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.SEARCH_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.BookSearchPosY.ID, (int)Constant.BookSearchPosY.SEARCH);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.BookSearchPosY.ID:
                        bookId = DataProcessing.GetDataProcessing().GetInputValues(bothScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.ID, Constant.MAX_LENGTH_BOOK_ID, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_ID);
                        break;
                    case (int)Constant.BookSearchPosY.NAME:
                        bookName = DataProcessing.GetDataProcessing().GetInputValues(bothScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.NAME, Constant.MAX_LENGTH_BOOK_NAME, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_NAME);
                        break;
                    case (int)Constant.BookSearchPosY.PUBLISHER:
                        bookPublisher = DataProcessing.GetDataProcessing().GetInputValues(bothScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.PUBLISHER, Constant.MAX_LENGTH_BOOK_PUBLISHER, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_PUBLISHER);
                        break;
                    case (int)Constant.BookSearchPosY.AUTHOR:
                        bookAuthor = DataProcessing.GetDataProcessing().GetInputValues(bothScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.AUTHOR, Constant.MAX_LENGTH_BOOK_AUTHOR, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ENGLISH_NUMBER_KOREA, Constant.EXCEPTION_TYPE_BOOK_AUTHOR);
                        break;
                    case (int)Constant.BookSearchPosY.ISBN:
                        bookISBN = DataProcessing.GetDataProcessing().GetInputValues(bothScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.ISBN, Constant.MAX_LENGTH_BOOK_ISBN, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER_SPACE_PYPHEN, Constant.EXCEPTION_TYPE_ANY);
                        break;
                    case (int)Constant.BookSearchPosY.PRICE:
                        bookPrice = DataProcessing.GetDataProcessing().GetInputValues(bothScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.PRICE, Constant.MAX_LENGTH_BOOK_PRICE, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_PRICE);
                        break;
                    case (int)Constant.BookSearchPosY.QUANTITY:
                        bookQuantity = DataProcessing.GetDataProcessing().GetInputValues(bothScreen, Constant.SEARCH_POS_X, (int)Constant.BookSearchPosY.QUANTITY, Constant.MAX_LENGTH_BOOK_QUANTITY, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_QUANTITY);
                        break;
                    case (int)Constant.BookSearchPosY.SEARCH:
                        // ↓ 옵션입력시 모두 공백일경우 체크하는 조건문
                        if ((bookId == "" || bookId == Constant.INPUT_ESCAPE.ToString()) && (bookName == "" || bookName == Constant.INPUT_ESCAPE.ToString()) && (bookPublisher == "" || bookPublisher == Constant.INPUT_ESCAPE.ToString()) && (bookAuthor == "" || bookAuthor == Constant.INPUT_ESCAPE.ToString()) && (bookISBN == "" || bookISBN == Constant.INPUT_ESCAPE.ToString()) && (bookPrice == "" || bookPrice == Constant.INPUT_ESCAPE.ToString()) && (bookQuantity == "" || bookQuantity == Constant.INPUT_ESCAPE.ToString()))
                        {
                            bothScreen.PrintMessage(Constant.TEXT_PLEASE_INPUT_OPTION, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                            Console.SetCursorPosition(Constant.SEARCH_SELECT_OPTION_POS_X, (int)Constant.BookSearchPosY.ID); //좌표조정
                        }
                        else
                        {
                            conditionalStringByUserInput = DataProcessing.GetDataProcessing().GetConditionalStringBySearchBook(bookId, bookName, bookPublisher, bookAuthor, bookISBN, bookPrice, bookQuantity); // 검색시 입력값에 대한 교집합을 골라내는 조건문
                            searchedBookIdList = DataBase.GetDataBase().GetSelectedElements(Constant.BOOK_FILED_ID, Constant.TABLE_NAME_BOOK, conditionalStringByUserInput);
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

        public void ShowSearchedBookInformation(BothScreen bothScreen)
        {
            int getYesOrNoBySearching, getYesOrNoByResearching;
            bothScreen.PrintConfirmationMessage("검색하시겠습니까?", ConsoleColor.Yellow);

            getYesOrNoBySearching = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (getYesOrNoBySearching == Constant.INPUT_ENTER)
            {
                bothScreen.PrintSearchResultScreen();
                bothScreen.PrintSelectedValues(DataBase.GetDataBase().Select(Constant.FILED_ALL, Constant.TABLE_NAME_BOOK, conditionalStringByUserInput), Constant.TABLE_NAME_BOOK, Constant.TEXT_NONE);
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
