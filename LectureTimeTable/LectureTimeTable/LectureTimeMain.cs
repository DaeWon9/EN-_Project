using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LectureTimeTable.View;
using LectureTimeTable.Controller;
using LectureTimeTable.Model;


namespace LectureTimeTable
{
    class LectureTimeMain
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(Constant.WINDOW_WIDTH, Constant.WINDOW_HEIGHT);

            LectureTimeTableProgram lectureTimeTableProgram = new LectureTimeTableProgram();
            lectureTimeTableProgram.Start();
        }
    }
}
