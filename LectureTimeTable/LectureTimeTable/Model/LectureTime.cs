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
            if (showedLectureNoList.Contains(targetIndex))
            {
                if (int.Parse(lectureData[targetIndex][Constant.DATA_GRADES]) > possibleGrades) // 학점 초과
                {
                    Console.WriteLine("학점이 초과되어 불가능합니다.");
                    return;
                }

                for (int row = 1; row < lectureTimeList.Count; row++) // 중복check
                {
                    if (lectureTimeList[row][Constant.DATA_NO].Equals(targetIndex.ToString()))
                    {
                        Console.WriteLine("{0}번 과목은 중복과목입니다..", targetIndex);
                        return;
                    }
                }

                subList.Clear();
                for (int column = 0; column < lectureData[targetIndex].Count; column++)
                {
                    subList.Add(lectureData[targetIndex][column]);
                }
                lectureTimeList.Add(new List<string>(subList));

                if (targetIndex != 0)
                    Console.WriteLine("{0}", successMessage);
            }
            else
            {
                Console.WriteLine("{0}번 과목은 조회되지 않은 과목입니다.", targetIndex);
                return;
            }
        }

        public void RemoveList(int targetIndex, string successMessage)
        {
            int removeListIndex = GetTargetIndex(targetIndex);
            //Console.WriteLine("{0}번 인덱스의 과목을 지웁니다", removeListIndex);
            if (removeListIndex != Constant.ERROR_NUMBER)
            {
                lectureTimeList.RemoveAt(removeListIndex);
                Console.WriteLine("{0}", successMessage);
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
            Console.WriteLine("{0}번 과목이 존재하지 않습니다.", targetIndex);
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
                splitList.Add(new List<string>(TimeList[row].Split().ToList()));
            }
            return splitList;
        }
    }
}
