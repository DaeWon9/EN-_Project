﻿using System;
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

        public void RemoveList(int targetIndex)
        {
            int removeListIndex = GetTargetIndex(targetIndex);
            if (removeListIndex != Constant.ERROR_NUMBER)
                basketList.RemoveAt(GetTargetIndex(targetIndex));
        }
        public int GetTargetIndex(int targetIndex)
        {
            for (int row = 0; row < basketList.Count; row++)
            {
                if (basketList[row][0].Equals(targetIndex.ToString()))
                {
                    //Console.WriteLine("해당 번호의 과목을 지웁니다.{0} index : {1}", targetIndex, row);
                    return row;
                }
            }
            Console.WriteLine("{0}번 과목이 존재하지 않습니다.", targetIndex);
            return Constant.ERROR_NUMBER;

        }
    }
}
