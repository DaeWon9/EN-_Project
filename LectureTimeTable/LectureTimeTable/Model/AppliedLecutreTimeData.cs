using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureTimeTable.Model
{
    class AppliedLecutreTimeData
    {
        private List<List<string>> myLectureTimeData;
        /*
        public MyLecutreTimeData(List<List<string>> lectureTimeData)
        {
            this.myLectureTimeData = lectureTimeData;
        }
        */

        public List<List<string>> ListData
        {
            get { return myLectureTimeData; } 
            set { myLectureTimeData = value; } 
        }
    }
}
