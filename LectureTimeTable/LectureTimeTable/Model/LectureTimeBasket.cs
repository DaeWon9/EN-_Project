using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureTimeTable.Model
{
    class LectureTimeBasket
    {
        public List<List<string>> basketList;
        public List<string> subList = new List<string>();

        public LectureTimeBasket(List<List<string>> lectureTimeData)
        {
            this.basketList = lectureTimeData;
        }


        public void AddList(List<List<string>> lectureData, int targetIndex)
        {
            for (int row = 1; row < basketList.Count; row++) // NO Check
            {
                if (basketList[row][Constant.DATA_NO].Equals(targetIndex.ToString()))
                {
                    Console.WriteLine("{0}번 과목이 이미 담겨있습니다.", targetIndex);
                    return;
                }
            }

            subList.Clear();
            for (int column = 0; column < lectureData[targetIndex].Count; column++)
            {
                subList.Add(lectureData[targetIndex][column]);
            }
            basketList.Add(new List<string>(subList));

            if (targetIndex != 0)
                Console.WriteLine("관심과목 담기에 성공했습니다.");
        }

        public void RemoveList(int targetIndex)
        {
            int removeListIndex = GetTargetIndex(targetIndex);
            //Console.WriteLine("{0}번 인덱스의 과목을 지웁니다", removeListIndex);
            if (removeListIndex != Constant.ERROR_NUMBER)
            {
                basketList.RemoveAt(removeListIndex);
                Console.WriteLine("관심과목 삭제에 성공했습니다.");
            }
        }
        public int GetTargetIndex(int targetIndex)
        {
            for (int row = 1; row < basketList.Count; row++)
            {
                if (basketList[row][0].Equals(targetIndex.ToString()))
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
            for (int row = 1; row < basketList.Count; row++) //0은 "학점" 데이터가 담겨있음
            {
                grades = grades + int.Parse(basketList[row][Constant.DATA_GRADES]);
            }
            return grades;  
        }
    }
}
