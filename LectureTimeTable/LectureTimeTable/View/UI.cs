using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LectureTimeTable.Model;

namespace LectureTimeTable.View
{
    class UI
    {
        public void DrawFirstScreen()
        {
            Console.WriteLine("-----------------------------  Sejong University ------------------------------");
            Console.WriteLine("세종대학교");
            Console.WriteLine("학사정보시스템");
            Console.WriteLine("                                                   ID :                        ");
            Console.WriteLine("                                                   PW :                        ");
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        public void DrawMessage(string message, bool isClear = true)
        {
            if (isClear)
            {
                Console.Clear();
                Console.WriteLine(message);
            }

            else
                Console.WriteLine(message);
        }

        public void DrawLectureTime(Array lectureArrary)
        {
            int line = lectureArrary.Length / lectureArrary.GetLength(1);
            Console.WriteLine("==================================================================================================================================================");
            for (int i = 1; i <= line; i++)
            {
                //Console.WriteLine("{0,-4}{1,-8}{2,-4}", lectureArrary.GetValue(i, 1), lectureArrary.GetValue(i, 2), lectureArrary.GetValue(i, 3));
                
                for (int j = 1; j <= lectureArrary.GetLength(1); j++)
                {
                    Console.Write(lectureArrary.GetValue(i, j));
                    if (j == 5 || j == 6 || j == 8 || j == 11)
                        Console.Write("\t");
                    else
                        Console.Write(" ");
                }
                Console.Write("\n");
                
            }
            Console.WriteLine("==================================================================================================================================================");

        }
    }
}
