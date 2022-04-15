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

                while (isBasketScreen)
                {
                    ConsoleKeyInfo key;
                    ui.DrawBasketScreen();
                    Console.Write("메뉴를 선택하세요 : ");
                    int inputNumber = int.Parse(user.GetInputData());
                    int inputNoNumber;
                    switch (inputNumber) // case 숫자 상수처리하기 & 케이스별 실행부분 함수화하기 -> 나중에
                    {
                        case 1: // 검색 후 담기
                            Console.Clear();
                            List<int> searchedLectureTimeIndex = lectureTimeSearcher.GetSearchedLectureTimeIndex(user, ui, fullLectureTimeDataList);
                            ui.DrawAttentionLecture(fullLectureTimeDataList, searchedLectureTimeIndex);
                            while (true)
                            {
                                //Console.Clear();
                                Console.Write("등록가능 학점 : {0}\t담은 학점 : {1}\t\t담을과목 NO : ", Constant.MAX_GRADES - lectureTimeBasket.GetGrades(), lectureTimeBasket.GetGrades());
                                inputNoNumber = int.Parse(user.GetInputData()); //1~184만 입력가능하게 예외처리 해야함
                                lectureTimeBasket.AddList(fullLectureTimeDataList, inputNoNumber, searchedLectureTimeIndex);
                                Console.Write("뒤로가기 : ESC | 다시담기 : ENTER");
                                key = Console.ReadKey();
                                if (key.Key == ConsoleKey.Escape)
                                {
                                    break;
                                }
                            }
                            break;
                        case 2: // 내역
                            Console.Clear();
                            Console.WriteLine("======================================================================================================================================================================================");
                            Console.WriteLine("등록가능 학점 : {0}\t담은 학점 : {1}", Constant.MAX_GRADES - lectureTimeBasket.GetGrades(), lectureTimeBasket.GetGrades());
                            ui.DrawLectureTime(lectureTimeBasket.lectureTimeList);
                            Console.Write("뒤로가기 : ESC");
                            key = Console.ReadKey();
                            if (key.Key == ConsoleKey.Escape)
                            {
                                isSearchLectureTimeScreen = false;
                                isSelectMenuScreen = true;
                            }
                            break;
                        case 3: // 시간표
                            Console.Clear();
                            List<string> basketTimeList = lectureTimeBasket.GetTimeList();
                            List<List<string>> basketTimeSplitList = lectureTimeBasket.GetTimeSplitList(basketTimeList);

                            ui.DrawTimeTableScreen();
                            for (int i = 0; i< basketTimeSplitList.Count; i++)
                            {
                                for (int j = 0; j < basketTimeSplitList[i].Count; j++)
                                {
                                    Console.Write(basketTimeSplitList[i][j]);
                                    Console.Write(" ");
                                }
                                Console.WriteLine();
                            }
                            Console.Write("뒤로가기 : ESC");
                            key = Console.ReadKey();
                            if (key.Key == ConsoleKey.Escape)
                            {
                                isSearchLectureTimeScreen = false;
                                isSelectMenuScreen = true;
                            }
                            break;
                        case 4: // 삭제 
                            while (true)
                            {
                                Console.Clear();
                                ui.DrawLectureTime(lectureTimeBasket.lectureTimeList);
                                if (lectureTimeBasket.lectureTimeList.Count > 1)
                                {
                                    Console.Write("등록가능 학점 : {0}\t담은 학점 : {1}\t\t삭제할과목 NO : ", Constant.MAX_GRADES - lectureTimeBasket.GetGrades(), lectureTimeBasket.GetGrades());
                                    inputNoNumber = int.Parse(user.GetInputData()); //1~184만 입력가능하게 예외처리 해야함
                                    lectureTimeBasket.RemoveList(inputNoNumber);
                                    Console.Write("뒤로가기 : ESC | 다른과목삭제 : ENTER");
                                    key = Console.ReadKey();
                                    if (key.Key == ConsoleKey.Escape)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("삭제가능한 과목이 없습니다.");
                                    Console.Write("뒤로가기 : ESC");
                                    key = Console.ReadKey();
                                    if (key.Key == ConsoleKey.Escape)
                                    {
                                        break;
                                    }
                                }
                            }
                            break;
                        case Constant.MENU_NUMBER_BACK:
                            isSelectMenuScreen=true;
                            isBasketScreen=false;
                            break;
                        default:
                            break;
                    }
                }
                
                while (isApplyScreen)
                {
                    ConsoleKeyInfo key;
                    Console.Clear();
                    Console.WriteLine("준비중입니다...");
                    Console.Write("뒤로가기 : ESC");
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                    {
                        isApplyScreen = false;
                        isSelectMenuScreen = true;
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
