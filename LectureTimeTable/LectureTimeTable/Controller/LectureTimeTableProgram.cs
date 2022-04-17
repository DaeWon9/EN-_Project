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
            bool isHistoryApplyScreen = false;


            while (isEntireLoop) // 모듈화
            {
                while (isLoginScreen)
                {
                    user.Login(user, ui);
                    isLoginScreen = false;
                    isSelectMenuScreen = true;
                }

                while (isSelectMenuScreen)
                {
                    string selectMenu;
                    ui.DrawMenuScreen();
                    Console.Write("메뉴를 골라주세요 : ");
                    selectMenu = user.GetInputValues(ui, -1, -1, false, Constant.EXCEPTION_TYPE_NUMBER, "숫자만 입력하세요");
                    if (selectMenu.Length < 1)
                        continue;

                    switch (int.Parse(selectMenu))
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
                            isHistoryApplyScreen=true;
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
                    bool isEscape = true;
                    string selectMenu;
                    ui.DrawBasketScreen();
                    Console.Write("메뉴를 골라주세요 : ");
                    selectMenu = user.GetInputValues(ui, -1, -1, false, Constant.EXCEPTION_TYPE_NUMBER, "숫자만 입력하세요");
                    if (selectMenu.Length < 1)
                        continue;
                    switch (int.Parse(selectMenu))
                    {
                        case Constant.MENU_NUMBER_SEARCH:
                            lectureTimeSearcher.SearchAttentionLecture(user, ui, fullLectureTimeDataList, lectureTimeBasket);
                            break;
                        case Constant.MENU_NUMBER_HISTORY:
                            lectureTimeSearcher.HistoryAttentrionLecture(ui, lectureTimeBasket);
                            Console.Write("뒤로가기 : ESC");
                            while (isEscape)
                            {
                                ConsoleKeyInfo key = Console.ReadKey();
                                if (key.Key == ConsoleKey.Escape)
                                    break;
                            }
                            break;
                        case Constant.MENU_NUMBER_TIME_TABLE:
                            lectureTimeSearcher.TimeTableAttentionLecture(ui, lectureTimeBasket);
                            Console.SetCursorPosition(0, 0);
                            Console.CursorVisible = false;
                            while (isEscape)
                            {
                                ConsoleKeyInfo key = Console.ReadKey();
                                if (key.Key == ConsoleKey.Escape)
                                    break;
                            }
                            Console.CursorVisible = true;
                            break;
                        case Constant.MENU_NUMBER_REMOVE:
                            lectureTimeSearcher.RemoveAttentionLecture(user, ui, lectureTimeBasket);
                            break;
                        case Constant.MENU_NUMBER_BACK:
                            isBasketScreen=false;
                            isSelectMenuScreen=true;
                            break;
                        default:
                            break;
                    }
                }

                while (isApplyScreen)
                {
                    bool isEscape=false;
                    string selectMenu;
                    ui.DrawApplyingScreen();
                    Console.Write("메뉴를 선택하세요 : ");
                    selectMenu = user.GetInputValues(ui, -1, -1, false, Constant.EXCEPTION_TYPE_NUMBER, "숫자만 입력하세요");
                    if (selectMenu.Length < 1)
                        continue;
                    switch (int.Parse(selectMenu))
                    {
                        case Constant.MENU_NUMBER_SEARCH: // 수강신청과목 분야별 검색
                            lectureTimeSearcher.SearchApplyingLecture(user, ui, fullLectureTimeDataList, appliedLectureTime, lectureTimeBasket);
                            break;
                        case Constant.MENU_NUMBER_HISTORY:
                            lectureTimeSearcher.HistoryApplyingLecture(ui, appliedLectureTime);
                            while (isEscape)
                            {
                                ConsoleKeyInfo key = Console.ReadKey();
                                if (key.Key == ConsoleKey.Escape)
                                    break;
                            }
                            break;
                        case Constant.MENU_NUMBER_TIME_TABLE:
                            lectureTimeSearcher.TimeTableApplyingLecture(ui, appliedLectureTime);
                            Console.SetCursorPosition(0, 0);
                            Console.CursorVisible = false;
                            while (isEscape)
                            {
                                ConsoleKeyInfo key = Console.ReadKey();
                                if (key.Key == ConsoleKey.Escape)
                                    break;
                            }
                            Console.CursorVisible = true;
                            break;
                        case Constant.MENU_NUMBER_REMOVE: // 삭제 
                            lectureTimeSearcher.RemoveApplyingLecture(user, ui, appliedLectureTime);
                            break;
                        case Constant.MENU_NUMBER_BACK:
                            isApplyScreen=false;
                            isSelectMenuScreen=true;
                            break;
                        default:
                            break;
                    }
                }

                while (isHistoryApplyScreen) //draw 시간표 & 엑셀파일저장 기능 
                {
                    ConsoleKeyInfo key;
                    lectureTimeSearcher.TimeTableApplyingLecture(ui, appliedLectureTime, true);
                    Console.SetCursorPosition(0, 0);
                    Console.CursorVisible = false;
                    key = Console.ReadKey();
                    Console.CursorVisible = true;
                    if (key.Key == ConsoleKey.Escape)
                    {
                        isHistoryApplyScreen = false;
                        isSelectMenuScreen = true;
                    }
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        Console.WriteLine("저장하시겠습니까?         취소 : ESC     저장 : ENTER");
                        key = Console.ReadKey();
                        if (key.Key == ConsoleKey.Enter)
                        {
                            lectureTimeSearcher.SaveToExcel(appliedLectureTime);
                            Console.Clear();
                            Console.WriteLine("저장되었습니다.");
                            Console.Write("뒤로가기 : ESC");
                            Console.ReadKey();
                        }
                    }
                }

            }
        }

    }
}
