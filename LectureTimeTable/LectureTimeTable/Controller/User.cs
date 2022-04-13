using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureTimeTable.Controller
{
    class User // id : 20003321 , pw:11111111
    {
        private string id;
        private string password;
        public User(string id, string password)
        {
            this.id = id;
            this.password = password;
        }

        public string GetInputData() // id, pw입력받을때 예외처리 필요
        {
            string userInput;
            /*
            ConsoleKeyInfo keyInfo;

            while (true)
            {
                keyInfo = Console.ReadKey();
                userInput = keyInfo.KeyChar.ToString();
                if (keyInfo.Key == ConsoleKey.Enter)
                    break;
            }
            */
            userInput = Console.ReadLine();
            return userInput;
        }

        public bool LoginCheck(User userData, string id, string password)
        {
            if (userData.id == id && userData.password == password)
                return true;
            else
                return false;
        }

        List<int> selectOptionList = new List<int>();
        public List<int> GetSelectOptionList(bool isOptionDepartment, bool isOptionDivison, bool isOptionLectureName, bool isOptionProfessorName, bool isOptionGrade)
        {
            selectOptionList.Clear();
            if (isOptionDepartment)
                selectOptionList.Add(Constant.DATA_DEPARTMENT);
            if (isOptionDivison)
                selectOptionList.Add(Constant.DATA_DIVISION);
            if (isOptionLectureName)
                selectOptionList.Add(Constant.DATA_LECUTRE_NAME);
            if (isOptionProfessorName)
                selectOptionList.Add(Constant.DATA_PROFESSOR_NAME);
            if (isOptionGrade)
                selectOptionList.Add(Constant.DATA_GRADE);

            return selectOptionList;  
        }


        List<int> matchingIndex = new List<int>();
        public List<int> SearchAttentionLectureIndex(Array fullLectureArrary, List<int> selectOptionList)
        {
            //Array attentionLectrue = fullLectureArrary;
            int numberOfLine = fullLectureArrary.Length / fullLectureArrary.GetLength(1);
            matchingIndex.Clear();
            for (int row = 1; row < numberOfLine; row++)
            {
                foreach (var option in selectOptionList)
                {
                    switch (option)
                    {
                        case Constant.DATA_DEPARTMENT:
                            compareData(fullLectureArrary, numberOfLine, option);
                            break;
                        case Constant.DATA_DIVISION:
                            break;
                        case Constant.DATA_LECUTRE_NAME:
                            break;
                        case Constant.DATA_PROFESSOR_NAME:
                            break;
                        case Constant.DATA_GRADE:
                            break;
                        default:
                            break;
                    }
                }
            }

            // matchinIndex가 같은놈들만 빼서 리턴 해야함
            return matchingIndex;
        }


        public void compareData(Array fullLectureArray, int numberOfLine, int option)
        {
            for (int row = 1; row <= numberOfLine; row++)
            {
                if (fullLectureArray.GetValue(row, option).Equals("컴퓨터")) ; //일단은 컴공만 -> 나중에 사용자 입력값으로 처리
                matchingIndex.Add(row);
            }
        }


    }
}
