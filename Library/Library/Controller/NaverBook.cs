using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Library.View;
using Library.Utility;
using Library.Model;

namespace Library.Controller
{
    class NaverBook
    {
        private string clientId = Constant.CLIENT_ID;
        private string clientSecert = Constant.CLIENT_SECRET;

        public void SearchBookByNaver(AdministratorScreen administratorScreen)
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

        private bool IsSearchBookByNaverCompleted(AdministratorScreen administratorScreen, string bookName, string bookDisplay)
        {
            int GetYesOrNoByNaverSearch, GetYesOrNoByNaverResearch;
            JObject naverSearchResult;
            // 모든 값이 입력됐는지 체크
            if ((bookName == "" || bookName == Constant.INPUT_ESCAPE.ToString()) || (bookDisplay == "" || bookDisplay == Constant.INPUT_ESCAPE.ToString()))
            {
                administratorScreen.PrintMessage(Constant.TEXT_PLEASE_INPUT_OPTION, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                Console.SetCursorPosition(Constant.SEARCH_BY_NAVER_SELECT_OPTION_POS_X, (int)Constant.NaverBookPosY.NAME); //좌표조정
                return false;
            }

            administratorScreen.PrintMessage(Constant.TEXT_IS_SEARCH, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            administratorScreen.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y); //좌표조정
            Console.CursorVisible = false;
            GetYesOrNoByNaverSearch = DataProcessing.GetDataProcessing().GetEnterOrEscape();

            if (GetYesOrNoByNaverSearch == Constant.INPUT_ENTER) // 검색확인문구에서 enter입력
            {
                DataProcessing.GetDataProcessing().ClearErrorMessage();
                naverSearchResult = GetSearchBookInformationByNaver(bookName, int.Parse(bookDisplay));
                administratorScreen.PrintResultSerchedBookByNaver(naverSearchResult, bookName, int.Parse(bookDisplay));
                DataBase.GetDataBase().AddLog(Constant.LOG_ADMINISTRATOR_TEXT_FROM, string.Format(Constant.LOG_STRING_SEARCH_BOOK_BY_NAVER, bookName, bookDisplay, Constant.LOG_TEXT_SEARCH_BOOK_BY_NABER));
                GetYesOrNoByNaverResearch = DataProcessing.GetDataProcessing().GetEnterOrEscape();
                if (GetYesOrNoByNaverResearch == Constant.INPUT_ENTER)
                {
                    Console.CursorVisible = true;
                    SearchBookByNaver(administratorScreen);
                    Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.CURSOR_POS_TOP); // 좌표조정
                }
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

        private void SelectMenuBasedOnSearchResult()
        {

        }

        public JObject GetSearchBookInformationByNaver(string query, int display)
        {
            // title -> d_titl
            //jsonStr = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);

            string queryString = "query=" + query;
            string displayString = "&display=" + display;
            string url = "https://openapi.naver.com/v1/search/book.json?" + queryString + displayString;
            //string url = "	https://openapi.naver.com/v1/search/book_adv.xml?d_isbn = 8954763006 9788954763004";

            //request 
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("X-Naver-Client-Id", clientId);
            request.Headers.Add("X-Naver-Client-Secret", clientSecert);

            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            JObject jsonResult = JObject.Parse(reader.ReadToEnd());

            reader.Close();
            response.Close();
            responseStream.Close();
            return jsonResult;
        }
    }
}
