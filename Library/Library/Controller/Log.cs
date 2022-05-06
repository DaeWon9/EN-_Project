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
        
        
        public void ManagementLog(AdministratorScreen administratorScreen)
        {
            bool isInputEscape = false;
            int currentConsoleCursorPosY;
            while (!isInputEscape)
            {
                administratorScreen.PrintManagementLogScreen();
                Console.SetCursorPosition(Constant.MENU_CURSOR_POS_X, (int)Constant.LogManagementPosY.SHOW); // 좌표조정

                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.MENU_CURSOR_POS_X, Console.CursorTop, (int)Constant.LogManagementPosY.SHOW, (int)Constant.LogManagementPosY.RESET);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.LogManagementPosY.SHOW:
                        ShowLogScreen(administratorScreen);
                        break;
                    case (int)Constant.LogManagementPosY.DELETE:
                        DeleteLog(logScreen);
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
        
        private void ShowLogScreen(AdministratorScreen administratorScreen)
        {
            administratorScreen.PrintLogLabel();
            logScreen.PrintLog(DataBase.GetDataBase().GetLog(Constant.TEXT_NONE));
            DataProcessing.GetDataProcessing().IsOnlyInputEscape();
        }


        private bool IsLogNumberValid(string logNumber)
        {
            List<string> logNumberList = DataBase.GetDataBase().GetSelectedElements(Constant.LOG_FILED_NUMBER, Constant.TABLE_NAME_LOG);
            for (int repeat = 0; repeat < logNumberList.Count; repeat++)
            {
                if (logNumberList[repeat] == logNumber)
                    return true;
            }
            return false;
        }

        private void DeleteLog(LogScreen logScreen)
        {
            bool isInputEscape = false, isDeleteLogCompleted = false;
            int currentConsoleCursorPosY, getYesOrNoByDelete;
            string logNumber = "";
            logScreen.PrintDeleteLogScreen();
            logScreen.PrintLog(DataBase.GetDataBase().GetLog(Constant.TEXT_NONE));


            Console.SetCursorPosition(0, 0);      //입력창 보이게 맨위로 올리고 
            Console.SetCursorPosition(Constant.SELECT_MODIFY_BOOK_ID_OPTION_POS_X, (int)Constant.LogDeletePosY.NUMBER); //좌표조정

            while (!isInputEscape && !isDeleteLogCompleted)
            {
                if (logNumber != "" && !IsLogNumberValid(logNumber))// 책이름이 입력됐는데, 도서관에 없는책임
                {
                    logScreen.PrintMessage("존재하지 않는 로그번호입니다.", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    Console.SetCursorPosition(Constant.SELECT_MODIFY_BOOK_ID_OPTION_POS_X, (int)Constant.SelectBookIdPosY.ID); //좌표조정
                    DataProcessing.GetDataProcessing().ClearConsoleLine(Constant.SELECT_MODIFY_BOOK_ID_POS_X, Constant.WINDOW_WIDTH, (int)Constant.SelectBookIdPosY.ID);
                    logNumber = "";
                }

                currentConsoleCursorPosY = DataProcessing.GetDataProcessing().CursorMove(Constant.SELECT_DELETE_LOG_OPTION_POS_X, Console.CursorTop, (int)Constant.LogDeletePosY.NUMBER, (int)Constant.LogDeletePosY.DELETE);
                isInputEscape = DataProcessing.GetDataProcessing().IsInputEscape(currentConsoleCursorPosY.ToString());
                switch (currentConsoleCursorPosY)
                {
                    case (int)Constant.SelectBookIdPosY.ID:
                        logNumber = DataProcessing.GetDataProcessing().GetInputValues(logScreen, Constant.SELECT_LOG_NUMBER_POS_X, (int)Constant.LogDeletePosY.NUMBER, Constant.MAX_LENGTH_BOOK_ID, Constant.TEXT_PLEASE_INPUT_NUMBER, Constant.EXCEPTION_TYPE_NUMBER, Constant.EXCEPTION_TYPE_ANY);
                        break;
                    case (int)Constant.SelectBookIdPosY.MODIFY_BOOK:
                        if (logNumber != "" && logNumber != Constant.INPUT_ESCAPE.ToString())
                        {
                            logScreen.PrintConfirmationMessage("해당 로그를 삭제하시겠습니까??", ConsoleColor.Yellow);
                            getYesOrNoByDelete = DataProcessing.GetDataProcessing().GetEnterOrEscape();
                            if (getYesOrNoByDelete == Constant.INPUT_ENTER)
                            {
                                DataBase.GetDataBase().Delete(Constant.TABLE_NAME_LOG, string.Format(Constant.CONDITIONAL_STRING_COMPARE_EQUAL_BY_INT, Constant.LOG_FILED_NUMBER, logNumber));// 해당로그 delete
                                DataProcessing.GetDataProcessing().ClearErrorMessage();
                                isDeleteLogCompleted = true;
                            }
                            if (getYesOrNoByDelete == Constant.INPUT_ESCAPE)
                            {
                                DataProcessing.GetDataProcessing().ClearErrorMessage();
                                Console.SetCursorPosition(Constant.SELECT_MODIFY_BOOK_ID_OPTION_POS_X, (int)Constant.LogDeletePosY.NUMBER); //좌표조정
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            if (!isInputEscape)
                DeleteLog(logScreen);


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
                Console.SetCursorPosition(Constant.MENU_CURSOR_POS_X, (int)Constant.LogManagementPosY.SHOW); // 좌표조정
            }
            if (getYesOrNoByResetLog == Constant.INPUT_ENTER) // 초기화 하시겠습니까?? -> ENTER
            {
                DataProcessing.GetDataProcessing().ClearErrorMessage();
                DataBase.GetDataBase().ResetLog();
                DataProcessing.GetDataProcessing().ClearErrorMessage();
                PrintMessage("로그초기화에 성공하였습니다!", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
                Console.ReadKey();
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
                Console.SetCursorPosition(Constant.MENU_CURSOR_POS_X, (int)Constant.LogManagementPosY.SHOW); // 좌표조정
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
                writer.Close();
                DataProcessing.GetDataProcessing().ClearErrorMessage();
                PrintMessage("로그저장에 성공했습니다!", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
                Console.ReadKey();
            }
            reader.Close();
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
                Console.SetCursorPosition(Constant.MENU_CURSOR_POS_X, (int)Constant.LogManagementPosY.SHOW); // 좌표조정
            }
            if (getYesOrNoByDeleteLogFile == Constant.INPUT_ENTER) // 삭제 하시겠습니까?? -> ENTER
            {
                if (File.Exists(path)) // Log파일이 존재한다면
                {
                    File.Delete(path); // 파일삭제
                    DataProcessing.GetDataProcessing().ClearErrorMessage();
                    PrintMessage("로그파일 삭제에 성공했습니다!", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
                    Console.ReadKey();
                }
                else
                {
                    DataProcessing.GetDataProcessing().ClearErrorMessage();
                    PrintMessage("로그파일이 존재하지 않습니다", Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
                    Console.ReadKey();
                }
            }
        }
    }
}
