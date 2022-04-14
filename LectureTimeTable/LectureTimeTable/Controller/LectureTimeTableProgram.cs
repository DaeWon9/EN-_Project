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
            List<List<string>> fullLectureDataList = lectureData.GetLectureDataList();
            


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


            //ui.DrawLectureTime(fullLectureDataList);

   
            List<int> selectOptionList = new List<int>();

            selectOptionList = user.GetSelectOptionList(true, true, false, false, false); // 매개변수값 유저한테 입력받은 값으로 처리하는걸로 바꿔야함 일단은 전공만
            //Console.WriteLine(selectOptionList[1]);


            user.SearchAttentionLectureIndex(fullLectureDataList, selectOptionList);
            List<List<int>> attentionIndex = user.SearchAttentionLectureIndex(fullLectureDataList, selectOptionList);
            Console.WriteLine("크기 : {0}",attentionIndex[0].Count);

            for (int i = 0; i < attentionIndex.Count; i++)
            {
                for (int j = 0; j < attentionIndex[i].Count; j++)
                {
                    Console.WriteLine(attentionIndex[i][j]);
                }
            }
            


            //ui.DrawAttentionLecture(fullLectureDataList, user.SearchAttentionLectureIndex(fullLectureDataList, selectOptionList));





        }
      
    }
}
