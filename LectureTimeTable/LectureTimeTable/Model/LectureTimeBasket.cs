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
            for (int row = 0; row < basketList.Count; row++)
            {
                if (basketList[row][0].Equals(targetIndex.ToString()))
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
        }

        public void RemoveList(int targetIndex) // 오류있음 ->>>>>> list[1] list[2] list[3] 이 있을때 list[2]를 지우면 list[3]이 list[2]위치로 옮겨가나?? 봐야함 자고일어나서 볼것.
        {
            int removeListIndex = GetTargetIndex(targetIndex);
            Console.WriteLine("{0}번 인덱스의 과목을 지웁니다", removeListIndex);
            if (removeListIndex != Constant.ERROR_NUMBER)
                basketList.RemoveAt(GetTargetIndex(targetIndex));
        }
        public int GetTargetIndex(int targetIndex)
        {
            for (int row = 1; row < basketList.Count; row++)
            {
                if (basketList[row][0].Equals(targetIndex.ToString()))
                {
                    Console.WriteLine("해당 번호의 과목을 지웁니다.{0} index : {1}", targetIndex, row);
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
