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
                    Console.WriteLine("중복값입니다.");
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
    }
}
