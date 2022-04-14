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
            /////////////////////////////////////////Login
            /*
            ////////////////// Login
            user.Login(user, ui);
            Thread.Sleep(500);
            */


            ///////////////////////////////////////// 강의시간표 조회
            /*
             * 
            while (true)
            {
                lectureTimeSearcher.SearchLectureTime(user, ui, fullLectureTimeDataList);
            }
            */

            //////////////////////////////////////// 장바구니에 담기

            while (true)
            {
                ui.DrawBasketScreen();

                int inputNumber = int.Parse(user.GetInputData());
                if (inputNumber == 1) // 담을래
                {
                    Console.Write("담을과목의 번호를 입력하세요 :");
                    int secondInputNumber = int.Parse(user.GetInputData()); // 1~184까지만 가능
                    lectureTimeBasket.AddList(fullLectureTimeDataList, secondInputNumber);
                }
                else if (inputNumber == 2) //삭제할래
                {
                    ui.DrawLectureTime(lectureTimeBasket.basketList);
                    Console.Write("삭제할과목의 번호를 입력하세요 :");
                    int secondInputNumber = int.Parse(user.GetInputData()); // 1~184까지만 가능
                    lectureTimeBasket.RemoveList(secondInputNumber);
                }
                else
                {
                    continue;
                }

                ui.DrawLectureTime(lectureTimeBasket.basketList);
                Console.WriteLine("등록가능 학점 : {0}\t신청한 학점 : {1}", Constant.MAX_GRADES - lectureTimeBasket.GetGrades(), lectureTimeBasket.GetGrades());
                //Thread.Sleep(1000);
                Console.ReadKey();
            }

        }

    }
}
