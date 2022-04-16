using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;


namespace LectureTimeTable.Model
{
    class LectureTimeData
    {
        List<List<string>> dataList = new List<List<string>>();
        List<string> subList = new List<string>();
        public List<List<string>> GetLectureDataList()
        {
            try
            {
                // Excel Application 객체 생성
                Application application = new Application();

                string paths = AppDomain.CurrentDomain.BaseDirectory;
                //Console.WriteLine(paths);

                // Workbook 객체 생성 및 파일 오픈
                Workbook workbook = application.Workbooks.Open(paths + "\\LectureTable.xlsx");
                
                // sheets에 읽어온 엑셀값을 넣기 (한 workbook 내의 모든 sheet 가져옴)
                Sheets sheets = workbook.Sheets;

                // 특정 sheet의 값 가져오기
                Worksheet worksheet = sheets["LectureTable"] as Worksheet;

                // 범위 설정 (좌측 상단, 우측 하단)
                Range cellRange = worksheet.get_Range("A1", "L185") as Range;

                // 설정한 범위만큼 데이터 담기 (Value2 -셀의 기본 값 제공)
                Array dataArray = cellRange.Cells.Value2;

                // 리스트형식에 옮겨담기
                int numberOfLine = dataArray.Length / dataArray.GetLength(1);
                for (int row = 1; row <= numberOfLine; row++)
                {
                    subList.Clear();
                    for (int column = 1; column <= dataArray.GetLength(1); column++)
                    {
                        if (dataArray.GetValue(row, column) == null)
                            subList.Add(null);
                        else
                            subList.Add(dataArray.GetValue(row, column).ToString());    
                    }
                    dataList.Add(new List<string>(subList));
                }

                // 모든 워크북 닫기
                application.Workbooks.Close();

                // application 종료
                application.Quit();

                return dataList;

            }
            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
            }
            return dataList;
        }
    }
}
