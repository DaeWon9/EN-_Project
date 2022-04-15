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
            string id, password;
            while (!isLogin)
            {
                ui.DrawLoginScreen();
                Console.SetCursorPosition(Constant.CURSOR_X_POS_ID, Constant.CURSOR_Y_POS_ID);
                id = user.GetValueKey();
                Console.SetCursorPosition(Constant.CURSOR_X_POS_PW, Constant.CURSOR_Y_POS_PW);
                password = user.GetValueKey(true);

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

        public string GetValueKey(bool isPassword = false)
        {
            string input = "";
            bool isInputEnter = false;
            while (!isInputEnter)
            {               
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Enter)
                {
                    if (key.Key != ConsoleKey.Backspace)
                    {
                        input += key.KeyChar.ToString();
                        if (isPassword)
                            Console.Write("*");
                        else
                            Console.Write(key.KeyChar.ToString());
                    }
                    else
                    {
                        if (input.Length > 0)
                        {
                            input = input.Substring(0, input.Length-1);
                        }
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    Console.WriteLine();
                    isInputEnter = true;
                }

            }
            return input;
        }
    }
}
