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
            bool isUserInputDepartmentOption = false;
            bool isUserInputDivisionOption = false;
            bool isUserInputLectureNameOption = false;
            bool isUserInputProfessorNameOption = false;
            bool isUserInputGradeOption = false;


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

            Console.ReadKey();

            while (true)
            {
                ////////////////////////// Search
                ui.DrawSearchScreen();

                List<string> userInputOptionList = new List<string>();

                userInputOptionList.Add(user.GetInputData());
                userInputOptionList.Add(user.GetInputData());
                userInputOptionList.Add(user.GetInputData());
                userInputOptionList.Add(user.GetInputData());
                userInputOptionList.Add(user.GetInputData());

                for (int index = 0; index < userInputOptionList.Count; index++)
                {
                    switch (index)
                    {
                        case Constant.USER_INPUT_OPTOIN_INDEX_DEPARTMENT:
                            isUserInputDepartmentOption = user.IsUserInputData(userInputOptionList, Constant.USER_INPUT_OPTOIN_INDEX_DEPARTMENT);
                            break;

                        case Constant.USER_INPUT_OPTOIN_INDEX_DIVISION:
                            isUserInputDivisionOption = user.IsUserInputData(userInputOptionList, Constant.USER_INPUT_OPTOIN_INDEX_DIVISION);
                            break;

                        case Constant.USER_INPUT_OPTOIN_INDEX_LECTURE_NAME:
                            isUserInputLectureNameOption = user.IsUserInputData(userInputOptionList, Constant.USER_INPUT_OPTOIN_INDEX_LECTURE_NAME);
                            break;

                        case Constant.USER_INPUT_OPTOIN_INDEX_PROFESSOR_NAME:
                            isUserInputProfessorNameOption = user.IsUserInputData(userInputOptionList, Constant.USER_INPUT_OPTOIN_INDEX_PROFESSOR_NAME);
                            break;

                        case Constant.USER_INPUT_OPTOIN_INDEX_GRADE:
                            isUserInputGradeOption = user.IsUserInputData(userInputOptionList, Constant.USER_INPUT_OPTOIN_INDEX_GRADE);
                            break;

                        default:
                            break;
                    }
                }


                List<int> isSlectOptionList = new List<int>();
                isSlectOptionList = user.GetSelectOptionList(isUserInputDepartmentOption, isUserInputDivisionOption, isUserInputLectureNameOption, isUserInputProfessorNameOption, isUserInputGradeOption);
                //Console.WriteLine(selectOptionList[1]);


                //user.SearchAttentionLectureIndex(fullLectureDataList, isSlectOptionList, userInputOptionList);
                List<List<int>> attentionIndex = user.SearchAttentionLectureIndex(fullLectureDataList, isSlectOptionList, userInputOptionList);


                //Console.WriteLine("크기 : {0}", attentionIndex.Count);



                List<int> resultAttentionIndex = attentionIndex[0]; //초기
                                                                    //Linq 구문 이용해서 리스트에서 중복인 값 조회
                for (int i = 1; i < attentionIndex.Count; i++)
                {
                    var result = resultAttentionIndex.Where(x => attentionIndex[i].Any(y => y == x)).ToList();
                    resultAttentionIndex = result;
                }

                /*//중복된 값
                for (int i = 0; i < resultAttentionIndex.Count; i++)
                {
                    Console.WriteLine(resultAttentionIndex[i]);
                }
                */


                ui.DrawAttentionLecture(fullLectureDataList, resultAttentionIndex);
                Console.ReadKey();
            }
        }
      
    }
}
