using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureTimeTable.Model
{
    class LectureTime
    {
        public List<List<string>> lectureTimeList;
        public List<string> subList = new List<string>();

        public LectureTime(List<List<string>> lectureTimeData)
        {
            this.lectureTimeList = lectureTimeData;
        }

        public void AddTitleList(List<List<string>> lectureData, int targetIndex)
        {
            subList.Clear();
            for (int column = 0; column < lectureData[targetIndex].Count; column++)
            {
                subList.Add(lectureData[targetIndex][column]);
            }
            lectureTimeList.Add(new List<string>(subList));
        }

        public void AddList(List<List<string>> lectureData, int targetIndex, List<int> showedLectureNoList, int possibleGrades, string successMessage)
        {
            List<List<int>> startTimeToMinList = GetstartTimeToMinList();
            List<List<int>> endTimeToMinList = GetEntTimeToMinList();
            List<List<string>> dayList = GetDayList();

            string targetTime = lectureData[targetIndex][Constant.DATA_TIME];
            List<string> targetTimeList = new List<string>();

            List<int> targetStartTime = new List<int>();
            List<int> targetEndTime = new List<int>();
            List<string> targetDay = new List<string>();

            if (targetTime == null)
                targetTimeList = ("공 23:00~23:30".Split().ToList());
            else
                targetTimeList = targetTime.Split().ToList();
            
            for (int i = 0; i<targetTimeList.Count; i++)
            {
                if (targetTimeList[i].Length > 1) // 시간임
                {
                    List<string> getTargetStartTimeList = targetTimeList[i].Split('~').ToList();
                    DateTime StartTime = Convert.ToDateTime(getTargetStartTimeList[0]);
                    DateTime EndTime = Convert.ToDateTime(getTargetStartTimeList[1]);

                    targetStartTime.Add(StartTime.Hour*60 + StartTime.Minute);
                    targetEndTime.Add(EndTime.Hour*60 + EndTime.Minute);
                }
                else
                {
                    targetDay.Add(targetTimeList[i]);
                }
            } 

            if (showedLectureNoList.Contains(targetIndex))
            {
                if (int.Parse(lectureData[targetIndex][Constant.DATA_GRADES]) > possibleGrades) // 학점 초과
                {
                    Console.SetCursorPosition(Constant.EXCEPTION_CURSOR_POS_X, Console.CursorTop);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("학점이 초과되어 불가능합니다.");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }

                for (int row = 1; row < lectureTimeList.Count; row++) // 중복과목 체크
                {
                    if (lectureTimeList[row][Constant.DATA_LECTURE_NAME].Equals(lectureData[targetIndex][Constant.DATA_LECTURE_NAME]))
                    {
                        Console.SetCursorPosition(Constant.EXCEPTION_CURSOR_POS_X, Console.CursorTop);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{0}번 과목은 중복과목입니다.", targetIndex);
                        Console.ForegroundColor = ConsoleColor.White;
                        return;
                    }
                }

                for (int i = 0; i < startTimeToMinList.Count; i++) // 중복시간 체크
                {
                    for(int j=0; j < startTimeToMinList[i].Count; j++)
                    {
                        if (dayList[i][j] == targetDay[j]) // 요일이 같은지 판단 후에 시간겹치는거 체크해야함.!!
                        {
                            if (startTimeToMinList[i][j] < targetEndTime[j] && targetStartTime[j] < endTimeToMinList[i][j]) // 시간이 겹치는거.!
                            {
                                Console.SetCursorPosition(Constant.EXCEPTION_CURSOR_POS_X, Console.CursorTop);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("{0}번 과목은 시간이 겹칩니다.", targetIndex);
                                Console.ForegroundColor = ConsoleColor.White;
                                return;
                            }
                        }
                    }
                }

                subList.Clear();
                for (int column = 0; column < lectureData[targetIndex].Count; column++)
                {
                    subList.Add(lectureData[targetIndex][column]);
                }
                lectureTimeList.Add(new List<string>(subList));

                if (targetIndex != 0)
                {
                    Console.SetCursorPosition(Constant.EXCEPTION_CURSOR_POS_X, Console.CursorTop);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("{0}", successMessage);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(Constant.EXCEPTION_CURSOR_POS_X, Console.CursorTop);
                Console.WriteLine("{0}번 과목은 조회되지 않은 과목입니다.", targetIndex);
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
        }

        public void RemoveList(int targetIndex, string successMessage)
        {
            int removeListIndex = GetTargetIndex(targetIndex);
            if (removeListIndex != Constant.ERROR_NUMBER)
            {
                lectureTimeList.RemoveAt(removeListIndex);
                Console.SetCursorPosition(Constant.EXCEPTION_CURSOR_POS_X, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("{0}", successMessage);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public int GetTargetIndex(int targetIndex)
        {
            for (int row = 1; row < lectureTimeList.Count; row++)
            {
                if (lectureTimeList[row][0].Equals(targetIndex.ToString()))
                {
                    //Console.WriteLine("{0}번 과목의 index : {1}", targetIndex, row);
                    return row;
                }
            }
            Console.SetCursorPosition(Constant.EXCEPTION_CURSOR_POS_X, Console.CursorTop - 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0}번 과목이 존재하지 않습니다.                 ", targetIndex);
            Console.ForegroundColor = ConsoleColor.White;
            return Constant.ERROR_NUMBER;

        }

        public int GetGrades()
        {
            int grades = 0;
            for (int row = 1; row < lectureTimeList.Count; row++) //0은 "학점" 데이터가 담겨있음
            {
                grades = grades + int.Parse(lectureTimeList[row][Constant.DATA_GRADES]);
            }
            return grades;
        }

        public List<string> GetRoomList()
        {
            List<string> roomList = new List<string>();
            for (int row = 1; row < lectureTimeList.Count; row++)
            {
                roomList.Add(lectureTimeList[row][Constant.DATA_LECTURE_ROOM]);
            }
            return roomList;
        }
        
        public List<string> GetNameList()
        {
            List<string> nameList = new List<string>();
            for (int row = 1; row < lectureTimeList.Count; row++)
            {
                nameList.Add(lectureTimeList[row][Constant.DATA_LECTURE_NAME]);
            }
            return nameList;
        }

        public List<string> GetTimeList()
        {
            List<string> timeList = new List<string>();
            for (int row = 1; row < lectureTimeList.Count; row++)
            {
                timeList.Add(lectureTimeList[row][Constant.DATA_TIME]);
            }
            return timeList;
        }
        public List<List<string>> GetTimeSplitList(List<string> TimeList)
        {
            List<List<string>> splitList = new List<List<string>>();

            for (int row = 0; row< TimeList.Count; row++)
            {
                if (TimeList[row] == null) 
                {
                    splitList.Add(new List<string>("공 23:00~23:30".Split().ToList()));
                }
                else
                {
                    splitList.Add(new List<string>(TimeList[row].Split().ToList()));
                }
            }
            return splitList;
        }

        public List<List<string>> GetDayList()
        {
            List<string> getTimeList = GetTimeList();
            List<List<string>> timeSplitList = GetTimeSplitList(getTimeList);

            List<List<string>> dayList = new List<List<string>>();
            List<string> semiDayList = new List<string>();

            for (int i = 0; i< timeSplitList.Count; i++)
            {
                semiDayList.Clear();
                for (int j = 0; j < timeSplitList[i].Count; j++)
                {
                    if (timeSplitList[i][j].Length == 1) // 요일임
                    {
                        semiDayList.Add(timeSplitList[i][j]);
                    }
                }
                dayList.Add(new List<string>(semiDayList));
            }

            return dayList;
        }

        public List<List<int>> GetstartTimeToMinList()
        {
            List<string> getTimeList = GetTimeList();
            List<List<string>> timeSplitList = GetTimeSplitList(getTimeList);
            List<List<int>> timeList = new List<List<int>>();
            List<int> semiTimeList = new List<int>();

            for (int i = 0; i< timeSplitList.Count; i++)
            {
                semiTimeList.Clear();
                for (int j = 0; j < timeSplitList[i].Count; j++)
                {
                    if (timeSplitList[i][j].Length > 1) // 시간임
                    {
                        List<string> getStartTimeList = timeSplitList[i][j].Split('~').ToList();
                        DateTime StartTime = Convert.ToDateTime(getStartTimeList[0]);
                        semiTimeList.Add(StartTime.Hour*60 + StartTime.Minute);
                    }
                }
                timeList.Add(new List<int>(semiTimeList));
            }

            return timeList;
        }

        public List<List<int>> GetEntTimeToMinList()
        {
            List<string> getTimeList = GetTimeList();
            List<List<string>> timeSplitList = GetTimeSplitList(getTimeList);
            List<List<int>> timeList = new List<List<int>>();
            List<int> semiTimeList = new List<int>();

            for (int i = 0; i< timeSplitList.Count; i++)
            {
                semiTimeList.Clear();
                for (int j = 0; j < timeSplitList[i].Count; j++)
                {
                    if (timeSplitList[i][j].Length > 1) // 시간임
                    {
                        List<string> getStartTimeList = timeSplitList[i][j].Split('~').ToList();
                        DateTime EndTime = Convert.ToDateTime(getStartTimeList[1]);
                        semiTimeList.Add(EndTime.Hour*60 + EndTime.Minute);
                    }
                }
                timeList.Add(new List<int>(semiTimeList));
            }

            return timeList;
        }
    }
}
