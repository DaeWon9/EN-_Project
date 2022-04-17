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

        public bool IsExceptionCheck(string stringValue, int exceptionType = Constant.EXCEPTION_TYPE_ANY) //문자열처리는 Utility쪽으로 빼보기
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
        public string GetInputData() // 예외처리 필요
        {
            string userInput;
            userInput = Console.ReadLine();
            return userInput;
        }
        public string GetInputValues(UI ui, int posX, int posY, bool isPassword = false,  int exceptionType = Constant.EXCEPTION_TYPE_ANY, string message = "올바른 형식의 값을 입력하세요")
        {
            string input = "";
            bool isInputEnter = false;

            if (posX == -1)
                posX = Console.CursorLeft;
            if (posY == -1)
                posY = Console.CursorTop;

            while (!isInputEnter)
            {
                
                ui.ClearCurrentLine(posX, input.Length);
                Console.SetCursorPosition(posX, posY);
                if (isPassword)
                {
                    for (int i = 0; i < input.Length; i++)
                        Console.Write("*");
                }
                else
                    Console.Write(input); 
                ConsoleKeyInfo key = Console.ReadKey(); //키입력 받고


                if ((key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace) && (!IsExceptionCheck(key.KeyChar.ToString(), exceptionType)))  //입력받은 키가 예외처리에 통과하는지
                {
                    ui.DrawMessage(message, Constant.EXCEPTION_CURSOR_POS_X, -1, false,  true);
                    continue;
                } 

                if (key.Key != ConsoleKey.Enter)
                {
                    if (key.Key != ConsoleKey.Backspace)
                    {
                        input += key.KeyChar.ToString();
                    }
                    else
                    {
                        if (input.Length > 0) 
                        {
                            input = input.Substring(0, input.Length-1);
                        }

                        Console.Write("\b \b"); // 지우고 
                        Console.Write("  ");

                    }
                }
                else
                {
                    ui.ClearCurrentLine(posX, input.Length);
                    Console.SetCursorPosition(posX, posY);
                    if (isPassword)
                    {
                        for (int i = 0; i < input.Length; i++)
                            Console.Write("*");
                    }
                    else
                        Console.Write(input);
                    isInputEnter = true;
                }

            }
            return input;
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
                id = user.GetInputValues(ui, Constant.CURSOR_X_POS_ID, Constant.CURSOR_Y_POS_ID, false, Constant.EXCEPTION_TYPE_NUMBER, "숫자만 입력하세요");
                password = user.GetInputValues(ui, Constant.CURSOR_X_POS_PW, Constant.CURSOR_Y_POS_PW, true, Constant.EXCEPTION_TYPE_ANY, "영어 & 숫자만 입력하세요");

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
