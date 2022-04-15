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
            LectureTimeBasket lectureTimeBasket = new LectureTimeBasket(new List<List<string>>());
            AppliedLecutreTimeData appliedLecutreTimeData = new AppliedLecutreTimeData(new List<List<string>>());
            LectureTImeSearcher lectureTimeSearcher = new LectureTImeSearcher();  

            List<List<string>> fullLectureTimeDataList = lectureTimeData.GetLectureDataList();
            lectureTimeBasket.AddList(fullLectureTimeDataList, 0);


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
                        case Constant.MENU_NUMBER_BACK:
                            isSelectMenuScreen=false;
                            isLoginScreen=true;
                            break;
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
                        default:
                            break;
                    }
                }

                while (isSearchLectureTimeScreen)
                {
                    lectureTimeSearcher.SearchLectureTime(user, ui, fullLectureTimeDataList);
                    Console.Write("뒤로가기 : ESC | 다시조회 : 아무키");
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                    {
                        isSearchLectureTimeScreen = false;
                        isSelectMenuScreen = true;
                    }
                }
                //////////////////////////////////////// 장바구니에 담기
                ///
                while (isBasketScreen)
                {
                    ui.DrawBasketScreen();
                    Console.Write("메뉴를 선택하세요 : ");
                    int inputNumber = int.Parse(user.GetInputData());
                    if (inputNumber == 1) // 담을래
                    {
                        Console.Write("담을과목의 번호를 입력하세요 :");
                        int secondInputNumber1 = int.Parse(user.GetInputData()); // 1~184까지만 가능
                        lectureTimeBasket.AddList(fullLectureTimeDataList, secondInputNumber1);
                    }
                    else if (inputNumber == 2) //삭제할래
                    {
                        Console.WriteLine("======================================================================================================================================================================================");
                        Console.WriteLine("등록가능 학점 : {0}\t담은 학점 : {1}", Constant.MAX_GRADES - lectureTimeBasket.GetGrades(), lectureTimeBasket.GetGrades());
                        ui.DrawLectureTime(lectureTimeBasket.basketList);
                        Console.Write("삭제할과목의 번호를 입력하세요 :");
                        int secondInputNumber2 = int.Parse(user.GetInputData()); // 1~184까지만 가능
                        lectureTimeBasket.RemoveList(secondInputNumber2);
                    }
                    else
                    {
                        continue;
                    }

                    Console.WriteLine("======================================================================================================================================================================================");
                    Console.WriteLine("등록가능 학점 : {0}\t신청한 학점 : {1}", Constant.MAX_GRADES - lectureTimeBasket.GetGrades(), lectureTimeBasket.GetGrades());
                    ui.DrawLectureTime(lectureTimeBasket.basketList);
                    //Thread.Sleep(1000);
                    Console.WriteLine("아무키나 입력하세요");
                    Console.ReadKey();
                }
            }
        }

    }
}
