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
            lectureTimeBasket.AddList(fullLectureTimeDataList, 7);
            lectureTimeBasket.AddList(fullLectureTimeDataList, 7);
            lectureTimeBasket.AddList(fullLectureTimeDataList, 6);
            lectureTimeBasket.AddList(fullLectureTimeDataList, 5);
            lectureTimeBasket.AddList(fullLectureTimeDataList, 4);
            lectureTimeBasket.RemoveList(4);
            lectureTimeBasket.RemoveList(4);
            Console.WriteLine(lectureTimeBasket.basketList.Count);


        }

    }
}
