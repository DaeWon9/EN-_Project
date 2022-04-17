using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public bool IsExceptionCheck(string stringValue, int exceptionType = Constant.EXCEPTION_TYPE_ANY)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9가-힣]*$", RegexOptions.None);
            if (stringValue == null || stringValue.Length < 1)
                return false;

            switch (exceptionType)
            {
                case Constant.EXCEPTION_TYPE_ANY:
                    regex = new Regex(@"^[a-zA-Z0-9가-힣]*$", RegexOptions.None);
                    break;
                case Constant.EXCEPTION_TYPE_NUMBER:
                    regex = new Regex(@"^[0-9]*$", RegexOptions.None);
                    break;
                case Constant.EXCEPTION_TYPE_KOREA:
                    regex = new Regex(@"^[가-힣]*$", RegexOptions.None);
                    break;
                case Constant.EXCEPTION_TYPE_ENGLISH:
                    regex = new Regex(@"^[a-zA-Z]*$", RegexOptions.None);
                    break;
                default:
                    break;
            }

            return regex.IsMatch(stringValue);
        }

        public string GetTest(UI ui, bool isPassword = false,  int exceptionType = Constant.EXCEPTION_TYPE_ANY, string message = "올바른 형식의 값을 입력하세요")
        {
            string input = "";
            bool isInputEnter = false;
            while (!isInputEnter)
            {
                ConsoleKeyInfo key = Console.ReadKey(); //키입력 받고
                Console.Write("\b"); // 입력받은건 안보이게 지우고
                if ((key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Escape&& key.Key != ConsoleKey.Backspace) &&  (!IsExceptionCheck(key.KeyChar.ToString(), exceptionType)))  //입려받은 키가 예외처리에 통과하는지
                {
                    ui.DrawMessage(message, -1, -1, false,  true);

                    continue;
                }

                if (key.Key != ConsoleKey.Enter)
                {
                    if (key.Key != ConsoleKey.Backspace)
                    {
                        input += key.KeyChar.ToString(); // 예외처리는 되는데 예외처리할때 누적된 입력값이 아닌 하나하나를 체크한 후에 인풋값에 플러스 해줘야함
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
                        Console.Write("\b\b \b\b");
                    }
                }
                else
                {
                    //Console.WriteLine();
                    isInputEnter = true;
                }

            }
            return input;
        }

        public string GetInputData() // 예외처리 필요
        {
            string userInput;
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
                id = user.GetTest(ui,false,Constant.EXCEPTION_TYPE_NUMBER);
                Console.SetCursorPosition(Constant.CURSOR_X_POS_PW, Constant.CURSOR_Y_POS_PW);
                password = user.GetTest(ui,true, Constant.EXCEPTION_TYPE_NUMBER);

                isLogin = user.LoginCheck(user, id, password);
            }
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
