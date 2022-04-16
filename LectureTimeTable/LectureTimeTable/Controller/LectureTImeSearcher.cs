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
                selectOptionList.Add(Constant.DATA_LECTURE_NAME);
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
                    case Constant.DATA_LECTURE_NAME:
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
                    case Constant.DATA_LECTURE_NAME:
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

            if (userInputData == "")
                return semiMactchingIndex;

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

        public void SearchAttentionLectureTest(User user, UI ui, List<List<string>> fullLectureTimeData, LectureTime lectureTimeBasket)
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
                Console.Write("뒤로가기 : ESC | 계속담기 : ENTER");
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }

        public void HistoryAttentrionLecture(UI ui, LectureTime lectureTimeBasket, bool isShowGrades = true)
        {
            Console.Clear();
            if (isShowGrades)
            {
                Console.WriteLine("======================================================================================================================================================================================");
                Console.WriteLine("등록가능 학점 : {0}\t담은 학점 : {1}", Constant.MAX_GRADES - lectureTimeBasket.GetGrades(), lectureTimeBasket.GetGrades());
            }
            ui.DrawLectureTime(lectureTimeBasket.lectureTimeList);
        }

        public void TimeTableAttentionLecture(UI ui, LectureTime lectureTimeBasket) //시간체크 해야함
        {
            Console.Clear();

            List<string> getTimeList = lectureTimeBasket.GetTimeList();
            List<List<string>> timeSplitList = lectureTimeBasket.GetTimeSplitList(getTimeList);
            List<string> lectureNameList = lectureTimeBasket.GetNameList();
            List<string> lectureRoomList = lectureTimeBasket.GetRoomList();

            List<List<string>> dayList = new List<List<string>>();
            List<List<int>> firstTimeList = new List<List<int>>();
            List<List<int>> diffCount = new List<List<int>>();

            List<string> semiDayList =  new List<string>();
            List<int> semiFirstTimeList = new List<int>();
            List<int> semiDiffCount = new List<int>();

            int posX = 0;
            int posY = 0;

            ui.DrawTimeTableScreen();
            for (int i = 0; i< timeSplitList.Count; i++)
            {
                semiDayList.Clear();
                semiFirstTimeList.Clear();
                semiDiffCount.Clear();
                for (int j = 0; j < timeSplitList[i].Count; j++)
                {
                    if (timeSplitList[i][j].Length > 1) // 시간임
                    {
                        List<string> getStartTimeList = timeSplitList[i][j].Split('~').ToList();
                        DateTime StartTime = Convert.ToDateTime(getStartTimeList[0]);
                        DateTime EndTime = Convert.ToDateTime(getStartTimeList[1]);
                        TimeSpan timeDiff = EndTime - StartTime;

                        int diffHour = timeDiff.Hours; //시간차이
                        int diffMinute = timeDiff.Minutes; //분차이

                        diffMinute = diffMinute + diffHour*60;
                        int diffcnt = diffMinute / 30;

                        semiDiffCount.Add(diffcnt);
                        semiFirstTimeList.Add(StartTime.Hour*60 + StartTime.Minute);
                    }
                    else // 요일임
                        semiDayList.Add(timeSplitList[i][j]);
                }

                if (semiDayList.Count > semiFirstTimeList.Count) // 요일, 시간 개수 맞춰주기 최대 2개임
                {
                    semiFirstTimeList.Add(semiFirstTimeList[0]);
                    semiDiffCount.Add(semiDiffCount[0]);
                }

                diffCount.Add(new List<int>(semiDiffCount));
                firstTimeList.Add(new List<int>(semiFirstTimeList));
                dayList.Add(new List<string>(semiDayList));
            }

            for (int i = 0; i < dayList.Count; i++)
            {
                for (int j = 0; j < dayList[i].Count; j++)
                {
                    if (dayList[i][j].Equals("월"))
                        posX = Constant.CURSOR_X_POS_TIME_TABLE_MON;
                    if (dayList[i][j].Equals("화"))
                        posX = Constant.CURSOR_X_POS_TIME_TABLE_TUE;
                    if (dayList[i][j].Equals("수"))
                        posX = Constant.CURSOR_X_POS_TIME_TABLE_WED;
                    if (dayList[i][j].Equals("목"))
                        posX = Constant.CURSOR_X_POS_TIME_TABLE_THR;
                    if (dayList[i][j].Equals("금"))
                        posX = Constant.CURSOR_X_POS_TIME_TABLE_FRI;

                    switch (firstTimeList[i][j])  // 분
                    {
                        case 0:
                            break;
                        case 9 * 60: // 9:00
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE;
                            break;
                        case 9 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (1 * 2);
                            break;
                        case 10 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (2 * 2);
                            break;
                        case 10 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (3 * 2);
                            break;
                        case 11 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (4 * 2);
                            break;
                        case 11 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (5 * 2);
                            break;
                        case 12 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (6 * 2);
                            break;
                        case 12 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (7 * 2);
                            break;
                        case 13 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (8 * 2);
                            break;
                        case 13 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (9 * 2);
                            break;
                        case 14 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (10 * 2);
                            break;
                        case 14 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (11 * 2);
                            break;
                        case 15 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (12 * 2);
                            break;
                        case 15 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (13 * 2);
                            break;
                        case 16 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (14 * 2);
                            break;
                        case 16 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (15 * 2);
                            break;
                        case 17 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (16 * 2);
                            break;
                        case 17 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (17 * 2);
                            break;
                        case 18 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (18 * 2);
                            break;
                        case 18 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (19 * 2);
                            break;
                        case 19 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (20 * 2);
                            break;
                        case 19 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (21 * 2);
                            break;
                        case 20 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (22 * 2);
                            break;
                        case 20 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (23 * 2);
                            break;
                        default:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (24 * 2);
                            break;
                    }
   
                    for (int k=0; k<diffCount[i][j]; k++)
                    {
                        Console.SetCursorPosition(posX, posY + (k * 2));
                        Console.Write(lectureNameList[i]);
                        Console.SetCursorPosition(posX, posY + ((k * 2) + 1));
                        Console.Write(lectureRoomList[i]);
                    }
                    
                }
            }
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

        public void SearchAttentionLecture(User user, UI ui, List<List<string>> fullLectureTimeData, LectureTime lectureTimeBasket)
        {
            int selectMenu;
            List<int> searchedLectureTimeIndex = new List<int>();
            List<int> showedBasketNoList = new List<int>();
            bool isPutLoop = true;

            for (int i = 1; i < lectureTimeBasket.lectureTimeList.Count; i++)
            {
                showedBasketNoList.Add(int.Parse(lectureTimeBasket.lectureTimeList[i][0]));
            }

            Console.Clear();
            ui.DrawSearchScreenInAttention();
            Console.Write("메뉴를 골라주세요 : ");
            selectMenu = int.Parse(user.GetInputData());
            switch (selectMenu)
            {
                case Constant.CONTENT_NUMBER_DEPARTMENT: //전공
                    searchedLectureTimeIndex = GetSearchedByContentsIndex(user, ui, fullLectureTimeData, "개설학과전공", Constant.DATA_DEPARTMENT);
                    ui.DrawAttentionLecture(fullLectureTimeData, searchedLectureTimeIndex);
                    break;
                case Constant.CONTENT_NUMBER_HAGSU_NUMBER: // 학수번호/분반
                    searchedLectureTimeIndex = GetSearchedByContentsIndex(user, ui, fullLectureTimeData, "학수번호/분반", Constant.DATA_HAGSU_NUMBER);
                    ui.DrawAttentionLecture(fullLectureTimeData, searchedLectureTimeIndex);
                    break;
                case Constant.CONTENT_NUMBER_LECTURE_NAME: // 교과목명
                    searchedLectureTimeIndex = GetSearchedByContentsIndex(user, ui, fullLectureTimeData, "교과목명", Constant.DATA_LECTURE_NAME);
                    ui.DrawAttentionLecture(fullLectureTimeData, searchedLectureTimeIndex);
                    break;
                case Constant.CONTENT_NUMBER_PROFESSOR_NAME: // 교수명
                    searchedLectureTimeIndex = GetSearchedByContentsIndex(user, ui, fullLectureTimeData, "교수명", Constant.DATA_PROFESSOR_NAME);
                    ui.DrawAttentionLecture(fullLectureTimeData, searchedLectureTimeIndex);
                    break;
                case Constant.CONTENT_NUMBER_GRADE: // 학년
                    searchedLectureTimeIndex = GetSearchedByContentsIndex(user, ui, fullLectureTimeData, "학년", Constant.DATA_GRADE);
                    ui.DrawAttentionLecture(fullLectureTimeData, searchedLectureTimeIndex);
                    break;
                case Constant.CONTENT_NUMBER_BACK - 1: // 뒤로가기
                    isPutLoop = false;
                    break;
                default:
                    break;
            }
            while (isPutLoop)
            {
                if (searchedLectureTimeIndex.Count > 1)
                {
                    Console.Write("등록가능 학점 : {0}\t담은 학점 : {1}\t\t담을과목 NO : ", Constant.MAX_GRADES - lectureTimeBasket.GetGrades(), lectureTimeBasket.GetGrades());
                    inputNoNumber = int.Parse(user.GetInputData()); //1~184만 입력가능하게 예외처리 해야함
                    lectureTimeBasket.AddList(fullLectureTimeData, inputNoNumber, searchedLectureTimeIndex, Constant.MAX_GRADES - lectureTimeBasket.GetGrades(), "★ 관심과목을 담았습니다. ★");
                    Console.Write("뒤로가기 : ESC | 계속담기 : ENTER");
                }
                else
                {
                    Console.WriteLine("검색된 항목이 없습니다.");
                    Console.Write("뒤로가기 : ESC");
                }

                key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    isPutLoop = false;
                }
            }
        }

        public void SearchApplyingLecture(User user, UI ui, List<List<string>> fullLectureTimeData, LectureTime appliedLectureTime, LectureTime lectureTimeBasket)
        {
            int selectMenu;
            List<int> searchedLectureTimeIndex = new List<int>();
            List<int> showedBasketNoList = new List<int>();
            bool isPutLoop = true;

            for (int i = 1; i < lectureTimeBasket.lectureTimeList.Count; i++)
            {
                showedBasketNoList.Add(int.Parse(lectureTimeBasket.lectureTimeList[i][0]));
            }

            Console.Clear();
            ui.DrawSearchScreenInApplying();
            Console.Write("메뉴를 골라주세요 : ");
            selectMenu = int.Parse(user.GetInputData());
            switch (selectMenu)
            {
                case Constant.CONTENT_NUMBER_DEPARTMENT: //전공
                    searchedLectureTimeIndex = GetSearchedByContentsIndex(user, ui, fullLectureTimeData, "개설학과전공", Constant.DATA_DEPARTMENT);
                    ui.DrawAttentionLecture(fullLectureTimeData, searchedLectureTimeIndex);
                    break;
                case Constant.CONTENT_NUMBER_HAGSU_NUMBER: // 학수번호/분반
                    searchedLectureTimeIndex = GetSearchedByContentsIndex(user, ui, fullLectureTimeData, "학수번호/분반", Constant.DATA_HAGSU_NUMBER);
                    ui.DrawAttentionLecture(fullLectureTimeData, searchedLectureTimeIndex);
                    break;
                case Constant.CONTENT_NUMBER_LECTURE_NAME: // 교과목명
                    searchedLectureTimeIndex = GetSearchedByContentsIndex(user, ui, fullLectureTimeData, "교과목명", Constant.DATA_LECTURE_NAME);
                    ui.DrawAttentionLecture(fullLectureTimeData, searchedLectureTimeIndex);
                    break;
                case Constant.CONTENT_NUMBER_PROFESSOR_NAME: // 교수명
                    searchedLectureTimeIndex = GetSearchedByContentsIndex(user, ui, fullLectureTimeData, "교수명", Constant.DATA_PROFESSOR_NAME);
                    ui.DrawAttentionLecture(fullLectureTimeData, searchedLectureTimeIndex);
                    break;
                case Constant.CONTENT_NUMBER_GRADE: // 학년
                    searchedLectureTimeIndex = GetSearchedByContentsIndex(user, ui, fullLectureTimeData, "학년", Constant.DATA_GRADE);
                    ui.DrawAttentionLecture(fullLectureTimeData, searchedLectureTimeIndex);
                    break;
                case Constant.CONTENT_NUMBER_ATTENTION: // 관심과목
                    HistoryAttentrionLecture(ui, lectureTimeBasket, false);
                    break;
                case Constant.CONTENT_NUMBER_BACK: // 뒤로가기
                    isPutLoop = false;
                    break;
                default:
                    break;
            }
            while (isPutLoop)
            {
                if (selectMenu == Constant.CONTENT_NUMBER_ATTENTION && lectureTimeBasket.lectureTimeList.Count > 1)
                {
                    Console.Write("신청가능 학점 : {0}\t수강 학점 : {1}\t\t수강신청 NO : ", Constant.MAX_APPLYING_GRADES - appliedLectureTime.GetGrades(), appliedLectureTime.GetGrades());
                    inputNoNumber = int.Parse(user.GetInputData()); //1~184만 입력가능하게 예외처리 해야함
                    appliedLectureTime.AddList(fullLectureTimeData, inputNoNumber, showedBasketNoList, Constant.MAX_APPLYING_GRADES - appliedLectureTime.GetGrades(), "★ 수강신청에 성공하였습니다. ★");
                    Console.Write("뒤로가기 : ESC | 계속신청 : ENTER");
                }
                else if (searchedLectureTimeIndex.Count > 1)
                {
                    Console.Write("신청가능 학점 : {0}\t수강 학점 : {1}\t\t수강신청 NO : ", Constant.MAX_APPLYING_GRADES - appliedLectureTime.GetGrades(), appliedLectureTime.GetGrades());
                    inputNoNumber = int.Parse(user.GetInputData()); //1~184만 입력가능하게 예외처리 해야함
                    appliedLectureTime.AddList(fullLectureTimeData, inputNoNumber, searchedLectureTimeIndex, Constant.MAX_APPLYING_GRADES - appliedLectureTime.GetGrades(), "★ 수강신청에 성공하였습니다. ★");
                    Console.Write("뒤로가기 : ESC | 계속신청 : ENTER");
                }
                else
                {
                    Console.WriteLine("검색된 항목이 없습니다.");
                    Console.Write("뒤로가기 : ESC");
                }

                key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    isPutLoop = false;
                }
            }
        }

        public void HistoryApplyingLecture(UI ui, LectureTime appliedLectureTime)
        {
            Console.Clear();
            Console.WriteLine("======================================================================================================================================================================================");
            Console.WriteLine("신청가능 학점 : {0}\t수강 학점 : {1}", Constant.MAX_APPLYING_GRADES - appliedLectureTime.GetGrades(), appliedLectureTime.GetGrades());
            ui.DrawLectureTime(appliedLectureTime.lectureTimeList);
        }

        public void TimeTableApplyingLecture(UI ui, LectureTime appliedLectureTime)
        {
            Console.Clear();
            List<string> getTimeList = appliedLectureTime.GetTimeList();
            List<List<string>> timeSplitList = appliedLectureTime.GetTimeSplitList(getTimeList);
            List<string> lectureNameList = appliedLectureTime.GetNameList();
            List<string> lectureRoomList = appliedLectureTime.GetRoomList();

            List<List<string>> dayList = new List<List<string>>();
            List<List<int>> firstTimeList = new List<List<int>>();
            List<List<int>> diffCount = new List<List<int>>();

            List<string> semiDayList = new List<string>();
            List<int> semiFirstTimeList = new List<int>();
            List<int> semiDiffCount = new List<int>();

            int posX = 0;
            int posY = 0;

            ui.DrawTimeTableScreen();
            for (int i = 0; i< timeSplitList.Count; i++)
            {
                semiDayList.Clear();
                semiFirstTimeList.Clear();
                semiDiffCount.Clear();
                for (int j = 0; j < timeSplitList[i].Count; j++)
                {
                    if (timeSplitList[i][j].Length > 1) // 시간임
                    {
                        List<string> getStartTimeList = timeSplitList[i][j].Split('~').ToList();
                        DateTime StartTime = Convert.ToDateTime(getStartTimeList[0]);
                        DateTime EndTime = Convert.ToDateTime(getStartTimeList[1]);
                        TimeSpan timeDiff = EndTime - StartTime;

                        int diffHour = timeDiff.Hours; //시간차이
                        int diffMinute = timeDiff.Minutes; //분차이

                        diffMinute = diffMinute + diffHour*60;
                        int diffcnt = diffMinute / 30;

                        semiDiffCount.Add(diffcnt);
                        semiFirstTimeList.Add(StartTime.Hour*60 + StartTime.Minute); // ->분으로 나옴.
                    }
                    else // 요일임
                        semiDayList.Add(timeSplitList[i][j]);
                }
                diffCount.Add(new List<int>(semiDiffCount));
                firstTimeList.Add(new List<int>(semiFirstTimeList));
                dayList.Add(new List<string>(semiDayList));
            }

            for (int i = 0; i < dayList.Count; i++)
            {
                for (int j = 0; j < dayList[i].Count; j++)
                {
                    if (dayList[i][j].Equals("월"))
                        posX = Constant.CURSOR_X_POS_TIME_TABLE_MON;
                    if (dayList[i][j].Equals("화"))
                        posX = Constant.CURSOR_X_POS_TIME_TABLE_TUE;
                    if (dayList[i][j].Equals("수"))
                        posX = Constant.CURSOR_X_POS_TIME_TABLE_WED;
                    if (dayList[i][j].Equals("목"))
                        posX = Constant.CURSOR_X_POS_TIME_TABLE_THR;
                    if (dayList[i][j].Equals("금"))
                        posX = Constant.CURSOR_X_POS_TIME_TABLE_FRI;

                    switch (firstTimeList[i][j])  // 분
                    {
                        case 9 * 60: // 9:00
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE;
                            break;
                        case 9 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (1 * 2);
                            break;
                        case 10 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (2 * 2);
                            break;
                        case 10 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (3 * 2);
                            break;
                        case 11 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (4 * 2);
                            break;
                        case 11 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (5 * 2);
                            break;
                        case 12 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (6 * 2);
                            break;
                        case 12 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (7 * 2);
                            break;
                        case 13 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (8 * 2);
                            break;
                        case 13 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (9 * 2);
                            break;
                        case 14 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (10 * 2);
                            break;
                        case 14 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (11 * 2);
                            break;
                        case 15 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (12 * 2);
                            break;
                        case 15 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (13 * 2);
                            break;
                        case 16 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (14 * 2);
                            break;
                        case 16 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (15 * 2);
                            break;
                        case 17 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (16 * 2);
                            break;
                        case 17 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (17 * 2);
                            break;
                        case 18 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (18 * 2);
                            break;
                        case 18 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (19 * 2);
                            break;
                        case 19 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (20 * 2);
                            break;
                        case 19 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (21 * 2);
                            break;
                        case 20 * 60:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (22 * 2);
                            break;
                        case 20 * 60 + 30:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (23 * 2);
                            break;
                        default:
                            posY = Constant.CURSOR_Y_POS_TIME_TABLE + (24 * 2);
                            break;
                    }

                    for (int k = 0; k<diffCount[i][j]; k++)
                    {
                        Console.SetCursorPosition(posX, posY + (k * 2));
                        Console.Write(lectureNameList[i]);
                        Console.SetCursorPosition(posX, posY + ((k * 2) + 1));
                        Console.Write(lectureRoomList[i]);
                    }

                }
            }
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
                    Console.Write("뒤로가기 : ESC | 계속삭제 : ENTER");
                }
                else
                {
                    Console.WriteLine("삭제가능한 과목이 없습니다.");
                    Console.Write("뒤로가기 : ESC");
                }
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }

    }
}
