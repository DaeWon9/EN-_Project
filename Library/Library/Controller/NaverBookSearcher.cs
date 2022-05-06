using System;
using Newtonsoft.Json.Linq;
using Library.View;
using Library.Utility;
using Library.Model;

namespace Library.Controller
{
    class NaverBookSearcher : BookAdder
    {
        NaverBook naverbook = new NaverBook();

        public void SearchBookByNaver(AdministratorScreen administratorScreen) // 네이버로 도서검색 하기 여기서 도서명 및 권수를 입력받음
        {
            bool isSearchBookByNaverCompleted = false, isInputEscape = false;
            string bookName = "", bookDisplay = "";
            int currentConsoleCursorPosY;

            administratorScreen.PrintGetSearchNaverBookOptionScreen();
            Console.SetCursorPosition(Constant.SEARCH_BY_NAVER_SELECT_OPTION_POS_X, (int)Constant.NaverBookPosY.NAME); //좌표조정

            while (!isInputEscape && !isSearchBookByNaverCompleted)
            {
                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.SEARCH_BY_NAVER_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.NaverBookPosY.NAME, (int)Constant.NaverBookPosY.SEARCH);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.NaverBookPosY.NAME:
                        bookName = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.NaverBookPosY.NAME, Constant.MAX_LENGTH_BOOK_NAME, Constant.TEXT_NONE, Constant.EXCEPTION_TYPE_ANY, Constant.EXCEPTION_TYPE_BOOK_NAME);
                        break;
                    case (int)Constant.NaverBookPosY.DISPLAY:
                        bookDisplay = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.NaverBookPosY.DISPLAY, Constant.MAX_LENGTH_BOOK_QUANTITY, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_QUANTITY);
                        break;
                    case (int)Constant.NaverBookPosY.SEARCH:
                        isSearchBookByNaverCompleted = IsSearchBookByNaverCompleted(administratorScreen, bookName, bookDisplay);
                        break;
                    default:
                        break;
                }
            }
        }

        private bool IsSearchBookByNaverCompleted(AdministratorScreen administratorScreen, string bookName, string bookDisplay) // 네이버로 검색이 완료됐는지 반환해주는 함수
        {
            int GetYesOrNoByNaverSearch;
            JObject naverSearchResult;
            // 모든 값이 입력됐는지 체크
            if ((bookName == "" || bookName == Constant.INPUT_ESCAPE.ToString()) || (bookDisplay == "" || bookDisplay == Constant.INPUT_ESCAPE.ToString()))
            {
                administratorScreen.PrintMessage(Constant.TEXT_PLEASE_INPUT_OPTION, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.SEARCH_BY_NAVER_SELECT_OPTION_POS_X, (int)Constant.NaverBookPosY.NAME); //좌표조정
                return false;
            }
            administratorScreen.PrintConfirmationMessage("검색하시겠습니까??", ConsoleColor.Yellow);

            Console.CursorVisible = false;
            GetYesOrNoByNaverSearch = DataProcessing.GetDataProcessing().GetEnterOrEscape();

            if (GetYesOrNoByNaverSearch == Constant.INPUT_ENTER) // 검색확인문구에서 enter입력
            {
                naverSearchResult = naverbook.GetSearchBookInformationByNaver(bookName, int.Parse(bookDisplay));
                DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_SEARCH_BOOK_BY_NAVER, bookName, bookDisplay, Constant.LOG_TEXT_SEARCH_BOOK_BY_NABER));
                SelectMenuBasedOnSearchResult(administratorScreen, naverSearchResult);
            }
            if (GetYesOrNoByNaverSearch == Constant.INPUT_ESCAPE) // 검색확인문구에서 esc입력
            {
                DataProcessing.GetDataProcessing().ClearErrorMessage();
                Console.SetCursorPosition(Constant.SEARCH_BY_NAVER_SELECT_OPTION_POS_X, (int)Constant.NaverBookPosY.NAME); //좌표조정
                Console.CursorVisible = true;
                return false;
            }
            return true;
        }

        private void SelectMenuBasedOnSearchResult(AdministratorScreen administratorScreen, JObject naverSearchResult) // 검색된 결과를 바탕으로 도서를 추가하는 함수
        {
            bool isAddBookByNaverCompleted = false, isInputEscape = false;
            string searhResultBookNumber = "";
            int currentConsoleCursorPosY;

            DataProcessing.GetDataProcessing().ClearErrorMessage();
            administratorScreen.PrintResultSerchedBookByNaver(naverSearchResult);
            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.CURSOR_POS_TOP); // 좌표조정
            Console.SetCursorPosition(Constant.ADD_BOOK_SELECT_OPTION_POS_X, (int)Constant.AddBookByNaverPosY.NUMBER); // 좌표조정
            while (!isInputEscape && !isAddBookByNaverCompleted)
            {
                if (searhResultBookNumber != "" && (int.Parse(searhResultBookNumber) < 1 || int.Parse(searhResultBookNumber) > int.Parse(naverSearchResult["display"].ToString()))) // 검색된 넘버를 사용자가 입력했을때 범위내에 있는지 체크
                {
                    administratorScreen.PrintMessage("검색되지 않은 번호입니다", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    Console.SetCursorPosition(Constant.SELECT_MODIFY_BOOK_ID_OPTION_POS_X, (int)Constant.AddBookByNaverPosY.NUMBER); //좌표조정
                    DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.SELECT_MODIFY_BOOK_ID_POS_X, Constant.WINDOW_WIDTH, (int)Constant.AddBookByNaverPosY.NUMBER);
                    searhResultBookNumber = "";
                }
                if (searhResultBookNumber != "" && IsAlreadyRegisteredBookISBNInLibrary(naverSearchResult["items"][int.Parse(searhResultBookNumber) - 1]["isbn"].ToString().Replace("<b>", "").Replace("</b>", ""))) // isbn으로 이미 등록되어있는 책인지 체크
                {
                    administratorScreen.PrintMessage("이미 등록되어있는 도서입니다.", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    Console.SetCursorPosition(Constant.ADD_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookAddPosY.NAME); //좌표조정
                    DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.SELECT_MODIFY_BOOK_ID_POS_X, Constant.WINDOW_WIDTH, (int)Constant.AddBookByNaverPosY.NUMBER);
                    searhResultBookNumber = "";
                }

                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.SEARCH_BY_NAVER_SELECT_OPTION_POS_X, Console.CursorTop, (int)Constant.AddBookByNaverPosY.NUMBER, (int)Constant.AddBookByNaverPosY.ADD);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.AddBookByNaverPosY.NUMBER:
                        searhResultBookNumber = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.SEARCH_POS_X, (int)Constant.AddBookByNaverPosY.NUMBER, Constant.MAX_LENGTH_BOOK_QUANTITY, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_QUANTITY);
                        break;
                    case (int)Constant.AddBookByNaverPosY.ADD:
                        isAddBookByNaverCompleted = IsAddBookByNaverCompleted(administratorScreen, naverSearchResult, searhResultBookNumber);
                        if (!isAddBookByNaverCompleted)
                        {
                            administratorScreen.PrintResultSerchedBookByNaver(naverSearchResult);
                            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.CURSOR_POS_TOP); // 좌표조정
                            Console.SetCursorPosition(Constant.ADD_BOOK_SELECT_OPTION_POS_X, (int)Constant.AddBookByNaverPosY.NUMBER); // 좌표조정
                        }
                        break;
                    default:
                        break;
                }
            }
            if (isInputEscape)
                SearchBookByNaver(administratorScreen);
        }
        
        private bool IsAddBookByNaverCompleted(AdministratorScreen administratorScreen, JObject naverSearchResult, string searhResultBookNumber) // 검색된 도서를 바탕으로 도서추가에 성공했는지를 반환하는 함수
        {
            bool isAddBookCompleted = false, isInputEscape = false;
            int currentConsoleCursorPosY;
            string bookName = "", bookPublisher = "", bookAuthor = "", bookPrice = "", bookQuantity = "", bookPublicationDate = "", bookISBN = "";
            bookName = naverSearchResult["items"][int.Parse(searhResultBookNumber) - 1]["title"].ToString().Replace("<b>", "").Replace("</b>", ""); // 제거하는거 모듈화로 묶기
            bookPublisher = naverSearchResult["items"][int.Parse(searhResultBookNumber) - 1]["publisher"].ToString().Replace("<b>", "").Replace("</b>", "");
            bookAuthor = naverSearchResult["items"][int.Parse(searhResultBookNumber) - 1]["author"].ToString().Replace("<b>", "").Replace("</b>", "");
            bookPrice = naverSearchResult["items"][int.Parse(searhResultBookNumber) - 1]["price"].ToString().Replace("<b>", "").Replace("</b>", "");
            bookPublicationDate = naverSearchResult["items"][int.Parse(searhResultBookNumber) - 1]["pubdate"].ToString().Replace("<b>", "").Replace("</b>", "");
            bookISBN = naverSearchResult["items"][int.Parse(searhResultBookNumber) - 1]["isbn"].ToString().Replace("<b>", "").Replace("</b>", "");

            administratorScreen.PrintAddBookScreen();
            administratorScreen.PrintBookOptionByNaver(bookName, bookPublisher, bookAuthor, bookPrice, bookPublicationDate, bookISBN);
            Console.SetCursorPosition(Constant.ADD_BOOK_SELECT_OPTION_POS_X, (int)Constant.BookAddPosY.QUANTITY); // 커서위치조정

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
                        bookPrice = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.PRICE, Constant.MAX_LENGTH_BOOK_PRICE, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_PRICE);
                        break;
                    case (int)Constant.BookAddPosY.QUANTITY:
                        bookQuantity = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.QUANTITY, Constant.MAX_LENGTH_BOOK_QUANTITY, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_BOOK_QUANTITY);
                        break;
                    case (int)Constant.BookAddPosY.PUBLICATION_DATE:
                        bookPublicationDate = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.PUBLICATION_DATE, Constant.MAX_LENGTH_DATE, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_DATE);
                        break;
                    case (int)Constant.BookAddPosY.ISBN:
                        bookISBN = DataProcessing.GetDataProcessing().GetInputValues(administratorScreen, Constant.ADD_BOOK_INPUT_POS_X, (int)Constant.BookAddPosY.ISBN, Constant.MAX_LENGTH_BOOK_ISBN, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER_SPACE_PYPHEN, Constant.EXCEPTION_TYPE_ANY);
                        break;
                    case (int)Constant.BookAddPosY.ADD:
                        isAddBookCompleted = IsAddBookCompleted(administratorScreen, naverSearchResult, bookName, bookPublisher, bookAuthor, bookPrice, bookQuantity, bookPublicationDate, bookISBN);
                        break;
                    default:
                        break;
                }
            }
            if (isAddBookCompleted)
                return true;
            return false;
        }

        private bool IsAddBookCompleted(AdministratorScreen administratorScreen, JObject naverSearchResult, string bookName, string bookPublisher, string bookAuthor, string bookPrice, string bookQuantity, string bookPublicationDate, string bookISBN)
        {
            int GetYesOrNoByAdd, GetYesOrNoByReAdd;
            string bookId = "";
            // 모든 값이 입력됐는지 체크
            if ((bookName == "" || bookName == Constant.INPUT_ESCAPE.ToString()) || (bookPublisher == "" || bookPublisher == Constant.INPUT_ESCAPE.ToString()) || (bookAuthor == "" || bookAuthor == Constant.INPUT_ESCAPE.ToString()) || (bookPrice == "" || bookPrice == Constant.INPUT_ESCAPE.ToString()) || (bookQuantity == "" || bookQuantity == Constant.INPUT_ESCAPE.ToString()) || (bookPublicationDate == "" || bookPublicationDate == Constant.INPUT_ESCAPE.ToString()) || (bookISBN == "" || bookISBN == Constant.INPUT_ESCAPE.ToString()))
            {
                administratorScreen.PrintMessage(Constant.TEXT_PLEASE_INPUT_OPTION, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
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
                administratorScreen.PrintMessage(Constant.TEXT_SUCCESS_ADD_BOOK, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
                administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);

                GetYesOrNoByReAdd = DataProcessing.GetDataProcessing().GetEnterOrEscape();
                if (GetYesOrNoByReAdd == Constant.INPUT_ENTER) // 계속해서 추가할것임.
                    SelectMenuBasedOnSearchResult(administratorScreen, naverSearchResult);
            }
            return true;
        }

    }
}
