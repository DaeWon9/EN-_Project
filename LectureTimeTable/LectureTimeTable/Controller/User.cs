using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LectureTimeTable.View;

namespace LectureTimeTable.Controller
{
    class User // id : 20003321 , pw:11111111
    {
        private string id;
        private string password;
        public User(string id, string password)
        {
            this.id = id;
            this.password = password;
        }

        public string GetInputData() // 예외처리 필요
        {
            string userInput;
            /*
            ConsoleKeyInfo keyInfo;

            while (true)
            {
                keyInfo = Console.ReadKey();
                userInput = keyInfo.KeyChar.ToString();
                if (keyInfo.Key == ConsoleKey.Enter)
                    break;
            }
            */
            userInput = Console.ReadLine();
            return userInput;
        }

        public bool LoginCheck(User userData, string id, string password)
        {
            if (userData.id == id && userData.password == password)
                return true;
            return false;
        }

        public void Login(User user, UI ui)
        {
            bool isLogin = false;
            while (!isLogin)
            {
                ui.DrawLoginScreen();
                Console.SetCursorPosition(Constant.CURSOR_X_POS_ID, Constant.CURSOR_Y_POS_ID);
                id = user.GetInputData();
                Console.SetCursorPosition(Constant.CURSOR_X_POS_PW, Constant.CURSOR_Y_POS_PW);
                password = user.GetInputData();

                isLogin = user.LoginCheck(user, id, password);
            }
            ui.DrawMessage("로그인완료");
        }

        public bool IsUserInputData(List<string> list, int index)
        {
            if (list[index] == "")
                return false;
            return true;
        }

    }
}
