using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LectureTimeTable.View;
using LectureTimeTable.Model;

namespace LectureTimeTable.Controller
{
    class LectureTImeSearcher
    {
        int inputNoNumber;
        ConsoleKeyInfo key;

        public List<int> GetSearchedByContentsIndex(User user, UI ui, List<List<string>> lectureList, string content, int contentNumber)
        {
            List<string> userInputOptionList = new List<string>();
            List<int> selectOptionList = new List<int>();
            int repeatTime = 1;
            Console.Clear();

            if (contentNumber == Constant.DATA_HAGSU_NUMBER)
            {
                repeatTime = 2;
                ui.DrawSearchScreenInHagsu();
                selectOptionList.Add(Constant.DATA_HAGSU_NUMBER);
                selectOptionList.Add(Constant.DATA_CLASS_NUMBER);
            }
            else
            {
                ui.DrawSearchScreenInContents(content);
                selectOptionList.Add(contentNumber);
            }

            for (int repeat = 0; repeat < repeatTime; repeat++)
            {
                Console.SetCursorPosition(Constant.CURSOR_X_POS_SEARCH, Constant.CURSOR_Y_POS_SEARCH + repeat);
                userInputOptionList.Add(user.GetInputData());
            }

            List<int> resultAttentionIndex = SearchLectureTimeByContentsIndex(lectureList, selectOptionList, userInputOptionList);

            return resultAttentionIndex;
        }

        public List<int> GetSearchedLectureTimeIndex(User user, UI ui, List<List<string>> lectureList)
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

            return resultAttentionIndex;
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
            selectOptionList.Add(Constant.ERROR_NUMBER);
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

        public List<int> SearchLectureTimeByContentsIndex(List<List<string>> lectureList, List<int> selectOptionList, List<string> userInputOptionList)
        {
            List<List<int>> matchingIndex = new List<List<int>>();

            foreach (int option in selectOptionList)
            {
                switch (option)
                {
                    case Constant.DATA_DEPARTMENT:
                    case Constant.DATA_LECUTRE_NAME:
                    case Constant.DATA_PROFESSOR_NAME:
                    case Constant.DATA_GRADE:
                        matchingIndex.Add(new List<int>(CompareData(lectureList, option, userInputOptionList[0])));
                        break;
                    case Constant.DATA_HAGSU_NUMBER:
                        matchingIndex.Add(new List<int>(CompareData(lectureList, option, userInputOptionList[0])));
                        break;
                    case Constant.DATA_CLASS_NUMBER:
                        matchingIndex.Add(new List<int>(CompareData(lectureList, option, userInputOptionList[1])));
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
            semiMactchingIndex.Add(0); // 0인덱스는 항상 추가 -> cloumn 타이틀임

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

        public void SearchAttentionLecture(User user, UI ui, List<List<string>> fullLectureTimeData, LectureTime lectureTimeBasket)
        {
            Console.Clear();
            List<int> searchedLectureTimeIndex = GetSearchedLectureTimeIndex(user, ui, fullLectureTimeData);
            ui.DrawAttentionLecture(fullLectureTimeData, searchedLectureTimeIndex);
            while (true)
            {
                //Console.Clear();
                Console.Write("등록가능 학점 : {0}\t담은 학점 : {1}\t\t담을과목 NO : ", Constant.MAX_GRADES - lectureTimeBasket.GetGrades(), lectureTimeBasket.GetGrades());
                inputNoNumber = int.Parse(user.GetInputData()); //1~184만 입력가능하게 예외처리 해야함
                lectureTimeBasket.AddList(fullLectureTimeData, inputNoNumber, searchedLectureTimeIndex, Constant.MAX_GRADES - lectureTimeBasket.GetGrades(), "★ 관심과목을 담았습니다. ★");
                Console.Write("뒤로가기 : ESC | 다시담기 : ENTER");
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
    
        public void HistoryAttentrionLecture(UI ui, LectureTime lectureTimeBasket)
        {
            Console.Clear();
            Console.WriteLine("======================================================================================================================================================================================");
            Console.WriteLine("등록가능 학점 : {0}\t담은 학점 : {1}", Constant.MAX_GRADES - lectureTimeBasket.GetGrades(), lectureTimeBasket.GetGrades());
            ui.DrawLectureTime(lectureTimeBasket.lectureTimeList);
            Console.Write("뒤로가기 : ESC");
            key = Console.ReadKey();
        }

        public void TimeTableAttentionLecture(UI ui, LectureTime lectureTimeBasket)
        {
            Console.Clear();
            List<string> timeList = lectureTimeBasket.GetTimeList();
            List<List<string>> timeSplitList = lectureTimeBasket.GetTimeSplitList(timeList);

            ui.DrawTimeTableScreen();
            for (int i = 0; i< timeSplitList.Count; i++)
            {
                for (int j = 0; j < timeSplitList[i].Count; j++)
                {
                    Console.Write(timeSplitList[i][j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.Write("뒤로가기 : ESC");
            key = Console.ReadKey();
        }

        public void RemoveAttentionLecture(User user, UI ui, LectureTime lectureTimeBasket)
        {
            while (true)
            {
                Console.Clear();
                ui.DrawLectureTime(lectureTimeBasket.lectureTimeList);
                if (lectureTimeBasket.lectureTimeList.Count > 1)
                {
                    Console.Write("등록가능 학점 : {0}\t담은 학점 : {1}\t\t삭제할과목 NO : ", Constant.MAX_GRADES - lectureTimeBasket.GetGrades(), lectureTimeBasket.GetGrades());
                    inputNoNumber = int.Parse(user.GetInputData()); //1~184만 입력가능하게 예외처리 해야함
                    lectureTimeBasket.RemoveList(inputNoNumber, "★ 관심과목을 삭제했습니다. ★");
                    Console.Write("뒤로가기 : ESC | 다른과목삭제 : ENTER");
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("삭제가능한 과목이 없습니다.");
                    Console.Write("뒤로가기 : ESC");
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
            }
        }

        public void SearchApplyingLecture(User user, UI ui, List<List<string>> fullLectureTimeData, LectureTime appliedLectureTime)
        {
            Console.Clear();
            List<int> searchedLectureTimeIndex = GetSearchedLectureTimeIndex(user, ui, fullLectureTimeData);
            ui.DrawAttentionLecture(fullLectureTimeData, searchedLectureTimeIndex);
            while (true)
            {
                //Console.Clear();
                Console.Write("신청가능 학점 : {0}\t수강 학점 : {1}\t\t수강신청 NO : ", Constant.MAX_APPLYING_GRADES - appliedLectureTime.GetGrades(), appliedLectureTime.GetGrades());
                inputNoNumber = int.Parse(user.GetInputData()); //1~184만 입력가능하게 예외처리 해야함
                appliedLectureTime.AddList(fullLectureTimeData, inputNoNumber, searchedLectureTimeIndex, Constant.MAX_APPLYING_GRADES - appliedLectureTime.GetGrades(), "★ 수강신청에 성공하였습니다. ★");
                Console.Write("뒤로가기 : ESC | 다시신청 : ENTER");
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }

        public void HistoryApplyingLecture(UI ui, LectureTime appliedLectureTime)
        {
            Console.Clear();
            Console.WriteLine("======================================================================================================================================================================================");
            Console.WriteLine("신청가능 학점 : {0}\t수강 학점 : {1}", Constant.MAX_APPLYING_GRADES - appliedLectureTime.GetGrades(), appliedLectureTime.GetGrades());
            ui.DrawLectureTime(appliedLectureTime.lectureTimeList);
            Console.Write("뒤로가기 : ESC");
            key = Console.ReadKey();
        }

        public void TimeTableApplyingLecture(UI ui, LectureTime appliedLectureTime)
        {
            Console.Clear();
            List<string> timeList = appliedLectureTime.GetTimeList();
            List<List<string>> TimeSplitList = appliedLectureTime.GetTimeSplitList(timeList);

            ui.DrawTimeTableScreen();
            for (int i = 0; i< TimeSplitList.Count; i++)
            {
                for (int j = 0; j < TimeSplitList[i].Count; j++)
                {
                    Console.Write(TimeSplitList[i][j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.Write("뒤로가기 : ESC");
            key = Console.ReadKey();
        }

        public void RemoveApplyingLecture(User user, UI ui, LectureTime appliedLectureTime)
        {
            while (true)
            {
                Console.Clear();
                ui.DrawLectureTime(appliedLectureTime.lectureTimeList);
                if (appliedLectureTime.lectureTimeList.Count > 1)
                {
                    Console.Write("신청가능 학점 : {0}\t수강 학점 : {1}\t\t삭제할과목 NO : ", Constant.MAX_GRADES - appliedLectureTime.GetGrades(), appliedLectureTime.GetGrades());
                    inputNoNumber = int.Parse(user.GetInputData()); //1~184만 입력가능하게 예외처리 해야함
                    appliedLectureTime.RemoveList(inputNoNumber, "★ 수강과목을 삭제했습니다. ★");
                    Console.Write("뒤로가기 : ESC | 다른과목삭제 : ENTER");
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("삭제가능한 과목이 없습니다.");
                    Console.Write("뒤로가기 : ESC");
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
            }
        }


    }
}
