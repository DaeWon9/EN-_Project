using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LectureTimeTable.View;

namespace LectureTimeTable.Controller
{
    internal class LectureTImeSearcher
    {
        public void SearchLectureTime(User user, UI ui, List<List<string>> lectureList)
        {
            bool isUserInputDepartmentOption = false;
            bool isUserInputDivisionOption = false;
            bool isUserInputLectureNameOption = false;
            bool isUserInputProfessorNameOption = false;
            bool isUserInputGradeOption = false;

            ui.DrawSearchScreen();

            List<string> userInputOptionList = new List<string>();

            //5개 입력받기
            for (int repeat = 0; repeat < 5; repeat++)
            {
                Console.SetCursorPosition(Constant.CURSOR_X_POS_SEARCH, Constant.CURSOR_Y_POS_SEARCH + repeat);
                userInputOptionList.Add(user.GetInputData());
            }

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
            isSlectOptionList = GetSelectOptionList(isUserInputDepartmentOption, isUserInputDivisionOption, isUserInputLectureNameOption, isUserInputProfessorNameOption, isUserInputGradeOption);

            List<int> resultAttentionIndex = SearchLectureTimeIndex(lectureList, isSlectOptionList, userInputOptionList);

            ui.DrawAttentionLecture(lectureList, resultAttentionIndex);
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

            if (selectOptionList.Count > 0)
                return selectOptionList;
            selectOptionList.Add(-1);
            return selectOptionList;
        }

        public List<int> SearchLectureTimeIndex(List<List<string>> lectureList, List<int> selectOptionList, List<string> userInputOptionList)
        {
            List<List<int>> matchingIndex = new List<List<int>>();

            foreach (int option in selectOptionList)
            {
                switch (option)
                {
                    case Constant.DATA_DEPARTMENT:
                        matchingIndex.Add(new List<int>(CompareData(lectureList, option, userInputOptionList[Constant.USER_INPUT_OPTOIN_INDEX_DEPARTMENT])));
                        break;
                    case Constant.DATA_DIVISION:
                        matchingIndex.Add(new List<int>(CompareData(lectureList, option, userInputOptionList[Constant.USER_INPUT_OPTOIN_INDEX_DIVISION])));
                        break;
                    case Constant.DATA_LECUTRE_NAME:
                        matchingIndex.Add(new List<int>(CompareData(lectureList, option, userInputOptionList[Constant.USER_INPUT_OPTOIN_INDEX_LECTURE_NAME])));
                        break;
                    case Constant.DATA_PROFESSOR_NAME:
                        matchingIndex.Add(new List<int>(CompareData(lectureList, option, userInputOptionList[Constant.USER_INPUT_OPTOIN_INDEX_PROFESSOR_NAME])));
                        break;
                    case Constant.DATA_GRADE:
                        matchingIndex.Add(new List<int>(CompareData(lectureList, option, userInputOptionList[Constant.USER_INPUT_OPTOIN_INDEX_GRADE])));
                        break;
                    default:
                        matchingIndex.Add(new List<int> { 0 });
                        break;
                }
            }

            // 중복체크
            List<int> resultAttentionIndex = matchingIndex[0]; //초기
            //Linq 구문 이용해서 리스트에서 중복인 값 조회
            for (int i = 1; i < matchingIndex.Count; i++)
            {
                var result = resultAttentionIndex.Where(x => matchingIndex[i].Any(y => y == x)).ToList();
                resultAttentionIndex = result;
            }


            return resultAttentionIndex;
        }

        public List<int> CompareData(List<List<string>> lectureList, int option, string userInputData)
        {
            List<int> semiMactchingIndex = new List<int>();
            int numberOfLine = lectureList.Count;
            semiMactchingIndex.Add(0); // 0인덱스는 항상 추가

            if (userInputData.Equals("전체")) // 모든 인덱스 추가
            {
                for (int row = 1; row < numberOfLine; row++)
                {
                    semiMactchingIndex.Add(row);
                }
            }
            else
            {
                for (int row = 1; row < numberOfLine; row++)
                {
                    if (lectureList[row][option].Contains(userInputData))
                    {
                        semiMactchingIndex.Add(row);
                    }
                }
            }
            return semiMactchingIndex;
        }
    }
}
