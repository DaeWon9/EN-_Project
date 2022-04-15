using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using LectureTimeTable.View;
using LectureTimeTable.Model;


namespace LectureTimeTable.Controller
{
    class LectureTimeTableProgram
    {
        public void Start()
        {
            UI ui = new UI();
            User user = new User(Constant.USER_ID, Constant.USER_PASSWORD);
            LectureTimeData lectureTimeData = new LectureTimeData();
            LectureTImeSearcher lectureTimeSearcher = new LectureTImeSearcher();

            LectureTime lectureTimeBasket = new LectureTime(new List<List<string>>());
            LectureTime appliedLectureTime = new LectureTime(new List<List<string>>());

            List<List<string>> fullLectureTimeDataList = lectureTimeData.GetLectureDataList();
            lectureTimeBasket.AddTitleList(fullLectureTimeDataList, 0);
            appliedLectureTime.AddTitleList(fullLectureTimeDataList, 0);

            bool isEntireLoop = true;
            bool isLoginScreen = true;
            bool isSelectMenuScreen = false;
            bool isSearchLectureTimeScreen = false;
            bool isBasketScreen = false;
            bool isApplyScreen = false;
            bool isSearchApplyScreen = false;


            while (isEntireLoop)
            {
                while (isLoginScreen)
                {
                    user.Login(user, ui);
                    Thread.Sleep(500);
                    isLoginScreen = false;
                    isSelectMenuScreen = true;
                }

                while (isSelectMenuScreen)
                {
                    int selectMenu;
                    ui.DrawMenuScreen();
                    Console.Write("메뉴를 골라주세요 : ");
                    selectMenu = int.Parse(user.GetInputData()); // 한글입력시 예외처리 해야함
                    switch (selectMenu)
                    {
                        case Constant.MENU_NUMBER_SEARCH_LECTURE_TIME:
                            isSelectMenuScreen=false;
                            isSearchLectureTimeScreen=true;
                            break;
                        case Constant.MENU_NUMBER_BASKET:
                            isSelectMenuScreen=false;
                            isBasketScreen=true;
                            break;
                        case Constant.MENU_NUMBER_APPLY:
                            isSelectMenuScreen=false;
                            isApplyScreen=true; 
                            break;
                        case Constant.MENU_NUMBER_SEARCH_APPLY:
                            isSelectMenuScreen=false;
                            isSearchApplyScreen=true;
                            break;
                        case Constant.MENU_NUMBER_BACK:
                            isSelectMenuScreen=false;
                            isLoginScreen=true;
                            break;
                        default:
                            break;
                    }
                }

                while (isSearchLectureTimeScreen)
                {
                    List<int> searchedLectureTimeIndex = lectureTimeSearcher.GetSearchedLectureTimeIndex(user, ui, fullLectureTimeDataList);
                    ui.DrawAttentionLecture(fullLectureTimeDataList, searchedLectureTimeIndex);
                    Console.Write("뒤로가기 : ESC | 다시조회 : ENTER");
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                    {
                        isSearchLectureTimeScreen = false;
                        isSelectMenuScreen = true;
                    }
                }

                while (isBasketScreen) //관심과목 담기
                {
                    ConsoleKeyInfo key;
                    ui.DrawBasketScreen();
                    Console.Write("메뉴를 선택하세요 : ");
                    int inputNumber = int.Parse(user.GetInputData());
                    switch (inputNumber)
                    {
                        case Constant.MENU_NUMBER_SEARCH:
                            lectureTimeSearcher.SearchAttentionLecture(user, ui, fullLectureTimeDataList, lectureTimeBasket);
                            break;
                        case Constant.MENU_NUMBER_HISTORY:
                            lectureTimeSearcher.HistoryAttentrionLecture(ui, lectureTimeBasket);
                            Console.Write("뒤로가기 : ESC");
                            key = Console.ReadKey();
                            if (key.Key == ConsoleKey.Escape)
                            {
                                isBasketScreen = false;
                                isSelectMenuScreen = true;
                            }
                            break;
                        case Constant.MENU_NUMBER_TIME_TABLE:
                            lectureTimeSearcher.TimeTableAttentionLecture(ui, lectureTimeBasket);
                            Console.Write("뒤로가기 : ESC");
                            key = Console.ReadKey();
                            if (key.Key == ConsoleKey.Escape)
                            {
                                isBasketScreen = false;
                                isSelectMenuScreen = true;
                            }
                            break;
                        case Constant.MENU_NUMBER_REMOVE: // 삭제 
                            lectureTimeSearcher.RemoveAttentionLecture(user, ui, lectureTimeBasket);
                            break;
                        case Constant.MENU_NUMBER_BACK:
                            isBasketScreen=true;
                            isBasketScreen=false;
                            break;
                        default:
                            break;
                    }
                }
                
                while (isApplyScreen)
                {
                    ConsoleKeyInfo key;
                    ui.DrawApplyingScreen();
                    Console.Write("메뉴를 선택하세요 : ");
                    int inputNumber = int.Parse(user.GetInputData());
                    switch (inputNumber)
                    {
                        case Constant.MENU_NUMBER_SEARCH:
                            lectureTimeSearcher.SearchApplyingLecture(user, ui, fullLectureTimeDataList, lectureTimeBasket);
                            break;
                        case Constant.MENU_NUMBER_HISTORY:
                            lectureTimeSearcher.HistoryAttentrionLecture(ui, lectureTimeBasket);
                            Console.Write("뒤로가기 : ESC");
                            key = Console.ReadKey();
                            if (key.Key == ConsoleKey.Escape)
                            {
                                isApplyScreen = false;
                                isSelectMenuScreen = true;
                            }
                            break;
                        case Constant.MENU_NUMBER_TIME_TABLE:
                            lectureTimeSearcher.TimeTableApplyingLecture(ui, lectureTimeBasket);
                            Console.Write("뒤로가기 : ESC");
                            key = Console.ReadKey();
                            if (key.Key == ConsoleKey.Escape)
                            {
                                isApplyScreen = false;
                                isSelectMenuScreen = true;
                            }
                            break;
                        case Constant.MENU_NUMBER_REMOVE: // 삭제 
                            lectureTimeSearcher.RemoveApplyingLecture(user, ui, lectureTimeBasket);
                            break;
                        case Constant.MENU_NUMBER_BACK:
                            isApplyScreen=true;
                            isBasketScreen=false;
                            break;
                        default:
                            break;
                    }
                }

                while (isSearchApplyScreen)
                {
                    ConsoleKeyInfo key;
                    Console.Clear();
                    Console.WriteLine("준비중입니다...");
                    Console.Write("뒤로가기 : ESC");
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                    {
                        isSearchApplyScreen = false;
                        isSelectMenuScreen = true;
                    }
                }

            }
        }

    }
}
