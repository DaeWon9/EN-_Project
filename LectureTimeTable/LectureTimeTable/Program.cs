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
    class Program
    {
        static void Main(string[] args)
        {
            LectureTimeTableProgram lectureTimeTableProgram = new LectureTimeTableProgram();
            lectureTimeTableProgram.Start();
        }
    }
}
