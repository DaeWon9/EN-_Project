using System;
using System.Collections.Generic;
using System.Text;


namespace LectureTimeTable.View
{
    class UI
    {
        public void DrawLoginScreen()
        {
            Console.Clear();
            Console.WriteLine("-----------------------------  Sejong University ------------------------------");
            Console.WriteLine("세종대학교                                                                     ");
            Console.WriteLine("학사정보시스템                                                                 ");
            Console.WriteLine("                                                   ID :                        ");
            Console.WriteLine("                                                   PW :                        ");
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        public void DrawMenuScreen()
        {
            Console.Clear();
            Console.WriteLine("---------------------------- 강좌조회 및 수강신청 -----------------------------");
            Console.WriteLine("1. 강의 시간표 조회                                                            ");
            Console.WriteLine("2. 관심과목 담기                                                               ");
            Console.WriteLine("3. 수강신청                                                                    ");
            Console.WriteLine("4. 수강내역조회                                                                ");
            Console.WriteLine("5. 로그아웃                                                                    ");
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        public void DrawSearchScreen()
        {
            Console.Clear();
            Console.WriteLine("-----------------------------  강의시간표 조회   ------------------------------");
            Console.WriteLine("개설학과전공 :                                                                 ");
            Console.WriteLine("이수구분     :                                                                 ");
            Console.WriteLine("교과목명     :                                                                 ");
            Console.WriteLine("교수명       :                                                                 ");
            Console.WriteLine("학년         :                                                                 ");
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        public void DrawSearchScreenInApplying()
        {
            Console.Clear();
            Console.WriteLine("----------------------------  수강신청 강의 검색  -----------------------------");
            Console.WriteLine("1. 개설학과전공                                                                ");
            Console.WriteLine("2. 학수번호/분반                                                               ");
            Console.WriteLine("3. 교과목명                                                                    ");
            Console.WriteLine("4. 교수명                                                                      ");
            Console.WriteLine("5. 학년                                                                        ");
            Console.WriteLine("6. 관심과목                                                                    ");
            Console.WriteLine("7. 뒤로가기                                                                    ");
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        public void DrawSearchScreenInAttention()
        {
            Console.Clear();
            Console.WriteLine("----------------------------  관심과목 강의 검색  -----------------------------");
            Console.WriteLine("1. 개설학과전공                                                                ");
            Console.WriteLine("2. 학수번호/분반                                                               ");
            Console.WriteLine("3. 교과목명                                                                    ");
            Console.WriteLine("4. 교수명                                                                      ");
            Console.WriteLine("5. 학년                                                                        ");
            Console.WriteLine("6. 뒤로가기                                                                    ");
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        public void DrawSearchScreenInContents(string message)
        {
            Console.Clear();
            Console.WriteLine("----------------------------  수강신청 강의 검색  -----------------------------");
            Console.WriteLine("{0} :", message + "".PadRight(GetPadLength(12, message)));
            Console.WriteLine("-------------------------------------------------------------------------------");
        }


        public void DrawSearchScreenInHagsu()
        {
            Console.Clear();
            Console.WriteLine("----------------------------  수강신청 강의 검색  -----------------------------");
            Console.WriteLine("학수번호     :                                                                 ");
            Console.WriteLine("분반         :                                                                 ");
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        public void DrawBasketScreen()
        {
            Console.Clear();
            Console.WriteLine("-----------------------------    관심과목 담기   ------------------------------");
            Console.WriteLine("1. 관심과목 검색                                                               ");
            Console.WriteLine("2. 관심과목 내역                                                               ");
            Console.WriteLine("3. 관심과목 시간표                                                             ");
            Console.WriteLine("4. 관심과목 삭제                                                               ");
            Console.WriteLine("5. 뒤로가기                                                                    ");
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        public void DrawApplyingScreen()
        {
            Console.Clear();
            Console.WriteLine("-----------------------------    수강신청 메뉴   ------------------------------");
            Console.WriteLine("1. 수강과목 검색                                                               ");
            Console.WriteLine("2. 수강신청 내역                                                               ");
            Console.WriteLine("3. 수강신청 시간표                                                             ");
            Console.WriteLine("4. 수강과목 삭제                                                               ");
            Console.WriteLine("5. 뒤로가기                                                                    ");
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        public void DrawTimeTableScreen(bool isExcel = false)
        {
            Console.Clear();
            if (isExcel)
                Console.WriteLine("                                                                                                         엑셀저장 : ENTER     뒤로가기 : ESC    ");
            else
                Console.WriteLine("                                                                                                                              뒤로가기 : ESC    ");
            Console.WriteLine("------------------------------------------------------------------   시간표   ------------------------------------------------------------------");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("\t\t\t월\t\t\t화\t\t\t수\t\t\t목\t\t\t금                                                                                                        ");
            Console.WriteLine("09:00~09:30                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("09:30~10:00                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("10:00~10:30                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("10:30~11:00                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("11:00~11:30                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("11:30~12:00                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("12:00~12:30                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("12:30~13:00                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("13:00~13:30                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("13:30~14:00                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("14:00~14:30                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("14:30~15:00                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("15:00~15:30                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("15:30~16:00                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("16:00~16:30                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("16:30~17:00                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("17:00~17:30                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("17:30~18:00                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("18:00~18:30                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("18:30~19:00                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("19:00~19:30                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("19:30~20:00                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("20:00~20:30                                                                                                                                     ");
            Console.WriteLine("                                                                                                                                                ");
            Console.WriteLine("20:30~21:00                                                                                                                                     ");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------");
        }

        public void DrawMessage(string message, bool isClear = true) // is else 줄이기
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
                case Constant.DATA_LECTURE_NAME:
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
