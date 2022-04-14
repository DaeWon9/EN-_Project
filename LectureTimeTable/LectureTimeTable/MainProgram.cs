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
    class MainProgram
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(200, 30);

            LectureTimeTableProgram lectureTimeTableProgram = new LectureTimeTableProgram();
            lectureTimeTableProgram.Start();
        }
    }
}
