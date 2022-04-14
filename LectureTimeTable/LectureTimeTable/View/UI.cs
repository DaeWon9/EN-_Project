using System;
using System.Collections.Generic;
using System.Text;


namespace LectureTimeTable.View
{
    class UI
    {
        public void DrawFirstScreen()
        {
            Console.Clear();
            Console.WriteLine("-----------------------------  Sejong University ------------------------------");
            Console.WriteLine("세종대학교                                                                     ");
            Console.WriteLine("학사정보시스템                                                                 ");
            Console.WriteLine("                                                   ID :                        ");
            Console.WriteLine("                                                   PW :                        ");
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        public void DrawSearchScreen()
        {
            Console.Clear();
            Console.WriteLine("-----------------------------  강의시간표 조회   ------------------------------");
            Console.WriteLine("개설학과전공                                                                   ");
            Console.WriteLine("이수구분                                                                       ");
            Console.WriteLine("교과목명                                                                       ");
            Console.WriteLine("교수명                                                                         ");
            Console.WriteLine("학년                                                                           ");
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        public void DrawBasketScreen()
        {
            Console.Clear();
            Console.WriteLine("-----------------------------    관심과목 담기   ------------------------------");
            Console.WriteLine("1. 과목담기                                                                    ");
            Console.WriteLine("2. 과목삭제                                                                    ");
            Console.WriteLine("                                                                               ");
            Console.WriteLine("                                                                               ");
            Console.WriteLine("                                                                               ");
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

        public void DrawLectureTime(List<List<string>> lectureList)
        {
            int numberOfLine = lectureList.Count;
            Console.WriteLine("======================================================================================================================================================================================");
            for (int row = 0; row < numberOfLine; row++)
            {
                for (int column = 0; column < lectureList[row].Count; column++)
                {
                    Console.Write("{0}", lectureList[row][column] + "".PadRight(GetPadLength(GetSortLength(column), lectureList[row][column])));
                }
                Console.Write("\n");
            }
            Console.WriteLine("======================================================================================================================================================================================");

        }

        //Console.Write("{0}", strToPrint1 + "".PadRight(padLen));

        public void DrawAttentionLecture(List<List<string>> lectureList, List<int> matchingIndex)
        {
            Console.WriteLine("======================================================================================================================================================================================");
            foreach (var row in matchingIndex)
            {
                for (int column = 0; column < lectureList[row].Count; column++)
                {
                    Console.Write("{0}", lectureList[row][column] + "".PadRight(GetPadLength(GetSortLength(column), lectureList[row][column])));
                }
                Console.Write("\n");
            }
            Console.WriteLine("======================================================================================================================================================================================");
        }

        public int GetPadLength(int sortLength, string str)
        {
            int bytesString;
            if (str == null)
                bytesString = 0;
            else
            {
                bytesString = Encoding.Default.GetBytes(str).Length;
            }
            return sortLength - bytesString;
        }
        public int GetSortLength(int column)
        {
            int sortLength;
            switch (column)
            {
                case Constant.DATA_NO:
                    sortLength = 4;
                    break;
                case Constant.DATA_DEPARTMENT:
                    sortLength = 19;
                    break;
                case Constant.DATA_HAGSU_NUMBER:
                    sortLength = 9;
                    break;
                case Constant.DATA_CLASS_NUMBER:
                    sortLength = 5; 
                    break;
                case Constant.DATA_LECUTRE_NAME:
                    sortLength = 33; 
                    break;
                case Constant.DATA_DIVISION:
                    sortLength = 13; 
                    break;
                case Constant.DATA_GRADE:
                case Constant.DATA_GRADES:
                    sortLength = 6; 
                    break;
                case Constant.DATA_TIME:
                    sortLength = 31; 
                    break;
                case Constant.DATA_LECTURE_ROOM:
                    sortLength = 14; 
                    break;
                case Constant.DATA_PROFESSOR_NAME:
                    sortLength = 27; 
                    break;
                case Constant.DATA_LANGUAGE:
                    sortLength = 12; 
                    break;
                default:
                    sortLength = 100;
                    break;
            }
            return sortLength;
        }
    }
}
