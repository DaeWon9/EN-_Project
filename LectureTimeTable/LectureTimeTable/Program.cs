using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LectureTimeTable.View;

namespace LectureTimeTable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UI ui = new UI();
            ui.DrawFirstScreen();
        }
    }
}
