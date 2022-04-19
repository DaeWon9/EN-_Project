using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Library.View;

namespace Library.Utility
{
    class DataProcessing
    {
        private bool isInputEnter = false;
        private bool isInputEscape = false;
        public int CursorMove(int posX, int posY, int maxPosY)
        {
            int cursorPosX = posX;
            int cursorPosY = posY;
            isInputEnter = false;
            isInputEscape = false;
            while (!isInputEnter && !isInputEscape)
            {
                Console.SetCursorPosition(cursorPosX, cursorPosY);
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        cursorPosY--;
                        if (cursorPosY < posY)
                            cursorPosY++;
                        break;
                    case ConsoleKey.DownArrow:
                        cursorPosY++;
                        if (cursorPosY > maxPosY)
                            cursorPosY--;
                        break;
                    case ConsoleKey.Enter:
                        isInputEnter = true;
                        break;
                    case ConsoleKey.Escape:
                        cursorPosY = Constant.INPUT_ESCAPE_IN_ARROW_KEY;
                        isInputEscape = true;
                        break;
                    default:
                        break;
                }
            }
            return cursorPosY; // -1 반환되면 esc / 다른값 -> enter
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
                case Constant.EXCEPTION_TYPE_ENGLISH_NUMBER:
                    regex = new Regex(@"^[0-9a-zA-Z]*$", RegexOptions.None);
                    break;
                default:
                    break;
            }

            return regex.IsMatch(stringValue);
        }

        public string GetInputValues(Message message, int posX, int posY, bool isPassword = false, int exceptionType = Constant.EXCEPTION_TYPE_ANY, string messageString = "올바른 형식의 값을 입력하세요")
        {
            string input = "";
            isInputEnter = false;
            isInputEscape = false;
            

            if (posX == -1)
                posX = Console.CursorLeft;
            if (posY == -1)
                posY = Console.CursorTop;

            while (!isInputEnter && !isInputEscape)
            {

                ClearCurrentLine(posX, input.Length);
                Console.SetCursorPosition(posX, posY);
                if (isPassword)
                {
                    for (int i = 0; i < input.Length; i++)
                        Console.Write("*");
                }
                else
                    Console.Write(input);
                ConsoleKeyInfo key = Console.ReadKey(); //키입력 받고

                if(key.Key == ConsoleKey.Escape) // Esc 눌리면
                {
                    isInputEscape = true;
                    return Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString();
                }


                if ((key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace) && (!IsExceptionCheck(key.KeyChar.ToString(), exceptionType)))  //입력받은 키가 예외처리에 통과하는지
                {
                    message.Draw(messageString, Constant.EXCEPTION_CURSOR_POS_X, Constant.EXCEPTION_CURSOR_POS_Y, false, true);
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
                    ClearCurrentLine(posX, input.Length);
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

        public void ClearCurrentLine(int startPosX = 0, int lastPosX = -1)
        {
            string str = "";
            if (startPosX == 0)
                str = "\r";
            if (lastPosX == -1)
                str += new string(' ', Console.CursorLeft);
            else
                str += new string(' ', lastPosX);
            if (startPosX == 0)
                str += "\r";
            Console.SetCursorPosition(startPosX + 1, Console.CursorTop);
            Console.Write(str);
            if (startPosX != 0)
                Console.SetCursorPosition(startPosX, Console.CursorTop);
        }
    }
}
