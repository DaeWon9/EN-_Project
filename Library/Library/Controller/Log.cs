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

namespace Library
{
    class Log : Message //부분삭제 + 파일삭제
    {
        LogScreen logScreen = new LogScreen();
        private string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Constant.LOG_FILE_NAME); // 로그 파일 저장 경로 
        
        
        public void ManagemenLog(AdministratorScreen administratorScreen)
        {
            bool isInputEscape = false;
            int currentConsoleCursorPosY;
            administratorScreen.PrintManagementLogScreen();
            Console.SetCursorPosition(Constant.MENU_CURSOR_POS_X, (int)Constant.LogManagementPosY.SHOW); // 좌표조정

            while (!isInputEscape)
            {
                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.MENU_CURSOR_POS_X, Console.CursorTop, (int)Constant.LogManagementPosY.SHOW, (int)Constant.LogManagementPosY.RESET);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.LogManagementPosY.SHOW:
                        ShowLogScreen();
                        break;
                    case (int)Constant.LogManagementPosY.DELETE:
                        break;
                    case (int)Constant.LogManagementPosY.SAVE_FILE:
                        SaveToTxtFile();
                        break;
                    case (int)Constant.LogManagementPosY.DELETE_FILE:
                        DeleteTxtFile();
                        break;
                    case (int)Constant.LogManagementPosY.RESET:
                        Reset();
                        break;
                    default:
                        break;
                }
            }
        }
        
        private void ShowLogScreen()
        {
            logScreen.PrintLog(DataBase.GetDataBase().GetLog(Constant.TEXT_NONE));
        }

        private void Reset()
        {
            int getYesOrNoByResetLog;

            DataProcessing.GetDataProcessing().ClearErrorMessage();
            PrintConfirmationMessage("< 주의 > 모든로그가 삭제됩니다. 정말로 삭제하시겠습니까??", ConsoleColor.Red);
            getYesOrNoByResetLog = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (getYesOrNoByResetLog == Constant.INPUT_ESCAPE) // 초기화 하시겠습니까?? -> ESC
            {
                DataProcessing.GetDataProcessing().ClearErrorMessage();
            }
            if (getYesOrNoByResetLog == Constant.INPUT_ENTER) // 초기화 하시겠습니까?? -> ENTER
            {
                DataProcessing.GetDataProcessing().ClearErrorMessage();
                DataBase.GetDataBase().ResetLog();
                DataProcessing.GetDataProcessing().ClearErrorMessage();
                PrintMessage("로그초기화에 성공하였습니다!", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            }
        }

        private void SaveToTxtFile() // Log.txt 파일 저장하기
        {
            MySqlDataReader reader = DataBase.GetDataBase().GetLog(Constant.TEXT_NONE);
            StreamWriter writer;
            int getYesOrNoBySaveLogFile;

            DataProcessing.GetDataProcessing().ClearErrorMessage();
            PrintConfirmationMessage("저장하시겠습니까??", ConsoleColor.Yellow);
            getYesOrNoBySaveLogFile = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (getYesOrNoBySaveLogFile == Constant.INPUT_ESCAPE) // 저장 하시겠습니까?? -> ESC
            {
                DataProcessing.GetDataProcessing().ClearErrorMessage();
            }
            if (getYesOrNoBySaveLogFile == Constant.INPUT_ENTER) // 저장 하시겠습니까?? -> ENTER
            {
                writer = File.CreateText(path);
                writer.WriteLine("----------------------------------------------------------------------------------------------------");
                writer.WriteLine("                                              < 로 그 현 황 >                                       ");
                writer.WriteLine("----------------------------------------------------------------------------------------------------");
                while (reader.Read())
                {
                    writer.WriteLine(" < {0}번 >", reader[Constant.LOG_FILED_NUMBER]);
                    writer.WriteLine("----------------------------------------------------------------------------------------------------");
                    writer.WriteLine(" 활동시간 : {0}", reader[Constant.LOG_FILED_DATE]);
                    writer.WriteLine(" 회원정보 : {0}", reader[Constant.LOG_FILED_MEMBER]);
                    writer.WriteLine(" 활동내역 : {0}", reader[Constant.LOG_FILED_ACTIVITY]);
                    writer.WriteLine("----------------------------------------------------------------------------------------------------");
                }
                reader.Close();
                writer.Close();
                DataProcessing.GetDataProcessing().ClearErrorMessage();
                PrintMessage("로그저장에 성공했습니다!", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            }
        }

        private void DeleteTxtFile()
        {
            int getYesOrNoByDeleteLogFile;
            DataProcessing.GetDataProcessing().ClearErrorMessage();
            PrintConfirmationMessage("정말로 삭제하시겠습니까??", ConsoleColor.Red);
            getYesOrNoByDeleteLogFile = DataProcessing.GetDataProcessing().GetEnterOrEscape();
            if (getYesOrNoByDeleteLogFile == Constant.INPUT_ESCAPE) // 삭제 하시겠습니까?? -> ESC
            {
                DataProcessing.GetDataProcessing().ClearErrorMessage();
            }
            if (getYesOrNoByDeleteLogFile == Constant.INPUT_ENTER) // 삭제 하시겠습니까?? -> ENTER
            {
                if (File.Exists(path)) // Log파일이 존재한다면
                    File.Delete(path); // 파일삭제
                DataProcessing.GetDataProcessing().ClearErrorMessage();
                PrintMessage("로그파일 삭제에 성공했습니다!", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
            }
        }
    }
}
