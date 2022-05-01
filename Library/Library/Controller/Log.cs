using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Library.Utility;
using Library.Model;
using Library.View;
using System.IO;

namespace Library.Controller
{
    class Log : Message
    {
        LogScreen logScreen = new LogScreen();
        private string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Constant.LOG_FILE_NAME); // 로그 파일 저장 경로 
        public void ShowLogScreen()
        {
            logScreen.PrintLog(DataBase.GetDataBase().GetLog(Constant.TEXT_NONE));
            Console.ReadKey();
        }

        public void ResetLog()
        {
            int getYesOrNoByResetLog;
            PrintMessage(Constant.TEXT_IS_RESET_LOG, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Red);
            PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y); //좌표조정
            getYesOrNoByResetLog = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (getYesOrNoByResetLog == Constant.INPUT_ESCAPE) // 초기화 하시겠습니까?? -> ESC
            {
                DataProcessing.GetDataProcessing().ClearErrorMessage();
            }
            if (getYesOrNoByResetLog == Constant.INPUT_ENTER) // 초기화 하시겠습니까?? -> ENTER
            {
                DataProcessing.GetDataProcessing().ClearErrorMessage();
                DataBase.GetDataBase().ResetLog();
                if (File.Exists(path)) // Log파일이 존재한다면
                    File.Delete(path); // 파일삭제
                PrintMessage(Constant.TEXT_SUCCESS_RESET_LOG, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
                Console.ReadKey();
            }
        }

        public void SaveLogToTxt() // Log.txt 파일 저장하기
        {
            MySqlDataReader reader = DataBase.GetDataBase().GetLog(Constant.TEXT_NONE);
            StreamWriter writer;
            writer = File.CreateText(path);
            writer.WriteLine("----------------------------------------------------------------------------------------------------");
            writer.WriteLine("              시간              |          회원정보          |                      활동            ");
            writer.WriteLine("----------------------------------------------------------------------------------------------------");
            while (reader.Read())
            {
                writer.Write(" {0}", reader[Constant.LOG_FILED_DATE] + "".PadRight(Constant.LOG_LENGTH_DATE - Encoding.Default.GetBytes(reader[Constant.LOG_FILED_DATE].ToString()).Length) + "|");
                writer.Write(" {0}", reader[Constant.LOG_FILED_MEMBER] + "".PadRight(Constant.LOG_LENGTH_MEMBER - Encoding.Default.GetBytes(reader[Constant.LOG_FILED_MEMBER].ToString()).Length) + "|");
                writer.WriteLine(" {0}", reader[Constant.LOG_FILED_ACTIVITY]);
                writer.WriteLine("----------------------------------------------------------------------------------------------------");
            }
            reader.Close();
            writer.Close();
        }
    }
}
