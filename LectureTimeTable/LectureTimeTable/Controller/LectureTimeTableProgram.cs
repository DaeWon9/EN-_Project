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
            LectureData lectureData = new LectureData();
            List<List<string>> fullLectureDataList = lectureData.GetLectureDataList();
            /*
            ////////////////// Login
            user.Login(user, ui);
            Thread.Sleep(500);
            */


            while (true)
            {
                user.SearchAttentionLecture(user, ui, fullLectureDataList);
            }


            //ui.DrawLectureTime(fullLectureDataList);



            //////////////////
        }

    }
}
