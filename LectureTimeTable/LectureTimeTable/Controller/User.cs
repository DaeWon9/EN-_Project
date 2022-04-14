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

        public List<int> GetSelectOptionList(bool isOptionDepartment, bool isOptionDivison, bool isOptionLectureName, bool isOptionProfessorName, bool isOptionGrade)
        {
            List<int> selectOptionList = new List<int>();
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


        public List<List<int>> SearchAttentionLectureIndex(List<List<string>> lectureList, List<int> selectOptionList)
        {
            int numberOfLine = lectureList.Count;
            List<List<int>> matchingIndex = new List<List<int>>();


            foreach (int option in selectOptionList)
            {
                switch (option)
                {
                    case Constant.DATA_DEPARTMENT:
                        compareData(lectureList, option); //-> 터짐
                        matchingIndex.Add(new List<int>(compareData(lectureList, option)));
                        break;
                    case Constant.DATA_DIVISION:
                        compareData(lectureList, option); //-> 터짐
                        matchingIndex.Add(new List<int>(compareData(lectureList, option)));
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


            // matchinIndex가 같은놈들만 빼서 리턴 해야함
            return matchingIndex;
        }


        public List<int> compareData(List<List<string>> lectureList, int option)
        {
            List<int> semiMactchingIndex = new List<int>();
            int numberOfLine = lectureList.Count;
            for (int row = 0; row < numberOfLine; row++)
            {
                //Console.WriteLine(lectureList[row][option]);
                if (lectureList[row][option].Contains("컴퓨터"))//일단은 컴공만 -> 나중에 사용자 입력값으로 처리
                {
                    //Console.WriteLine(row);
                    semiMactchingIndex.Add(row);
                }
            }

            return semiMactchingIndex;
        }


    }
}
