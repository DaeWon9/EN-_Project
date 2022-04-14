using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureTimeTable.Model
{
    internal class LectureTimeBasket
    {
        private List<List<string>> lectureTimeBasket;
        /*
        public LectureTimeBasket(List<List<string>> lectureTimeData)
        {
            this.lectureTimeBasket = lectureTimeData;
        }
        */

        public List<List<string>> ListData
        {
            get { return lectureTimeBasket; }
            set { lectureTimeBasket = value; }
        }
    }
}
