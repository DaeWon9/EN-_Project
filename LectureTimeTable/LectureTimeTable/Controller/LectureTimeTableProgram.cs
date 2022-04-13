using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LectureTimeTable.View;
using LectureTimeTable.Model;


namespace LectureTimeTable.Controller
{
    class LectureTimeTableProgram
    {
        public void Start()
        {
            string id, password;
            bool isLogin = false;



            UI ui = new UI();
            User user = new User(Constant.USER_ID, Constant.USER_PASSWORD);
            LectureData lectureData = new LectureData();

            ////////////////// Login
            while (!isLogin)
            {
                ui.DrawFirstScreen();
                id = user.GetInputData();
                password = user.GetInputData();

                isLogin = user.LoginCheck(user, id, password);
            }
            ui.DrawMessage("로그인완료");

            Console.Clear();
            lectureData.GetLectureData();
            ui.DrawLectureTime(lectureData.GetLectureData());



        }
      
    }
}
