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
            int numberOfLine = lectureArrary.Length / lectureArrary.GetLength(1);
            Console.WriteLine("==================================================================================================================================================");
            for (int row = 1; row <= numberOfLine; row++)
            {
                //Console.WriteLine("{0,-4}{1,-8}{2,-4}", lectureArrary.GetValue(i, 1), lectureArrary.GetValue(i, 2), lectureArrary.GetValue(i, 3));
                
                for (int column = 1; column <= lectureArrary.GetLength(1); column++)
                {
                    Console.Write(lectureArrary.GetValue(row, column));
                    if (column == Constant.DATA_DEPARTMENT)
                        Console.Write("\t\t");
                    else
                        Console.Write("\t");
                }
                Console.Write("\n");

            }
            Console.WriteLine("==================================================================================================================================================");

        }

        public void DrawAttentionLecture(Array fullLectureArrary, List<int> matchingIndex)
        {
            int numberOfLine = fullLectureArrary.Length / fullLectureArrary.GetLength(1);
            Console.WriteLine("==================================================================================================================================================");
            foreach (var row in matchingIndex)
            {
                //Console.WriteLine("{0,-4}{1,-8}{2,-4}", lectureArrary.GetValue(i, 1), lectureArrary.GetValue(i, 2), lectureArrary.GetValue(i, 3));

                for (int column = 1; column <= fullLectureArrary.GetLength(1); column++)
                {
                    Console.Write(fullLectureArrary.GetValue(row, column));
                    if (column == Constant.DATA_DEPARTMENT)
                        Console.Write("\t\t");
                    else
                        Console.Write("\t");
                }
                Console.Write("\n");

            }
            Console.WriteLine("==================================================================================================================================================");

        }
    }
}
