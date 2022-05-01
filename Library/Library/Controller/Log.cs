using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Library.Utility;
using Library.Model;
using Library.View;

namespace Library.Controller
{
    class Log : Message
    {
        LogScreen logScreen = new LogScreen(); 
        public void ShowLogScreen()
        {
            logScreen.PrintLog(DataBase.GetDataBase().GetLog(Constant.TEXT_NONE));
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
                PrintMessage(Constant.TEXT_SUCCESS_RESET_LOG, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);
                Console.ReadKey();
            }
        }
    }
}
