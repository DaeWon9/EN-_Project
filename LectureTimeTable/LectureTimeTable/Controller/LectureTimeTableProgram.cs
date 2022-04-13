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

            Array fullLecture = lectureData.GetLectureDataArray();

            List<List<string>> fullLectureDataList = lectureData.GetLectureDataList();


            
            for (int i=0; i<fullLectureDataList.Count; i++)
            {
                for (int j=0; j<fullLectureDataList[0].Count; j++)
                {
                    Console.Write(fullLectureDataList[i][j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            

            
      
            //lectureData.GetLectureDataList();
            //Console.WriteLine(fullLectureDataList.Count);


            //Console.WriteLine(fullLectureData.Count);


            //ui.DrawLectureTime(fullLecture);

            /*
            Array selectColumnIndex = Array.CreateInstance(typeof(int),12);
            selectColumnIndex.SetValue(Constant.DATA_NO, 0);
            selectColumnIndex.SetValue(Constant.DATA_DEPARTMENT, 1);
            selectColumnIndex.SetValue(Constant.DATA_HAGSU_NUMBER, 2);
            Console.WriteLine(selectColumnIndex.GetValue(11));
            Console.WriteLine(selectColumnIndex.Length);
            */
            List<int> selectOptionList = new List<int>();

            selectOptionList = user.GetSelectOptionList(true, false, false, false, false); // 매개변수값 유저한테 입력받은 값으로 처리하는걸로 바꿔야함 일단은 전공만
            

            //ui.DrawAttentionLecture(fullLectureData, user.SearchAttentionLectureIndex(fullLectureData, selectOptionList));


            //user.SearchAttentionLecture(lectureData.GetLectureData(), lectureData.GetLectureData());



        }
      
    }
}
