using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureTimeTable.Model
{
    class AppliedLecutreTimeData
    {
        public List<List<string>> appliedLectureTimeData;
        public List<string> subData = new List<string>();

        public AppliedLecutreTimeData(List<List<string>> lectureTimeData)
        {
            this.appliedLectureTimeData = lectureTimeData;
        }


        public void AddList(List<List<string>> lectureData, int targetIndex)
        {
            for (int row = 0; row < appliedLectureTimeData.Count; row++)
            {
                if(appliedLectureTimeData[row][0].Equals(targetIndex.ToString()))
                {
                    Console.WriteLine("중복값입니다.");
                    return;
                }
            }
            subData.Clear();
            for (int column = 0; column < lectureData[targetIndex].Count; column++)
            {
                subData.Add(lectureData[targetIndex][column]);
            }
            appliedLectureTimeData.Add(new List<string>(subData));
        }
    }
}
