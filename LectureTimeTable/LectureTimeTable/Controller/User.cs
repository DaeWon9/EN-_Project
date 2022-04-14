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

        public bool IsUserInputData(List<string> list, int index)
        {
            if (list[index] == "")
                return false;
            else
                return true;
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


        public List<List<int>> SearchAttentionLectureIndex(List<List<string>> lectureList, List<int> selectOptionList, List<string> userInputOptionList)
        {
            int numberOfLine = lectureList.Count;
            List<List<int>> matchingIndex = new List<List<int>>();

            foreach (int option in selectOptionList)
            {
                switch (option)
                {
                    case Constant.DATA_DEPARTMENT:
                        matchingIndex.Add(new List<int>(compareData(lectureList, option, userInputOptionList[Constant.USER_INPUT_OPTOIN_INDEX_DEPARTMENT])));
                        break;
                    case Constant.DATA_DIVISION:
                        matchingIndex.Add(new List<int>(compareData(lectureList, option, userInputOptionList[Constant.USER_INPUT_OPTOIN_INDEX_DIVISION])));
                        break;
                    case Constant.DATA_LECUTRE_NAME:
                        matchingIndex.Add(new List<int>(compareData(lectureList, option, userInputOptionList[Constant.USER_INPUT_OPTOIN_INDEX_LECTURE_NAME])));
                        break;
                    case Constant.DATA_PROFESSOR_NAME:
                        matchingIndex.Add(new List<int>(compareData(lectureList, option, userInputOptionList[Constant.USER_INPUT_OPTOIN_INDEX_PROFESSOR_NAME])));
                        break;
                    case Constant.DATA_GRADE:
                        matchingIndex.Add(new List<int>(compareData(lectureList, option, userInputOptionList[Constant.USER_INPUT_OPTOIN_INDEX_GRADE])));
                        break;
                    default:
                        break;
                }
            }
            return matchingIndex;
        }


        public List<int> compareData(List<List<string>> lectureList, int option, string userInputData)
        {
            List<int> semiMactchingIndex = new List<int>();
            int numberOfLine = lectureList.Count;
            for (int row = 0; row < numberOfLine; row++)
            {
                if (lectureList[row][option].Contains(userInputData))
                {
                    semiMactchingIndex.Add(row);
                }
            }

            return semiMactchingIndex;
        }


    }
}
