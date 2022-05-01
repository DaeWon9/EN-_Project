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
    class Log
    {
        LogScreen logScreen = new LogScreen(); 
        public void ShowLogScreen()
        {
            logScreen.PrintLog(DataBase.Instance.GetLog(Constant.TEXT_NONE));
        }
    }
}
