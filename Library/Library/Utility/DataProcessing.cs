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
                case Constant.EXCEPTION_TYPE_ID:
                    regex = new Regex(@"^[0-9a-zA-Z]{6,10}$", RegexOptions.None);
                    break;
                case Constant.EXCEPTION_TYPE_PASSWORD:
                    regex = new Regex(@"^[0-9a-zA-Z]{6,10}$", RegexOptions.None);
                    break;
                default:
                    break;
            }

            return regex.IsMatch(stringValue);
        }

        public string GetInputValues(Message message, int posX, int posY, int maxIputLength, bool isPassword = false, int exceptionType = Constant.EXCEPTION_TYPE_ANY,  string messageString = "올바른 형식의 값을 입력하세요", int finalExceptionType = Constant.EXCEPTION_TYPE_ANY)
        {
            string input = "";
            isInputEnter = false;
            isInputEscape = false;
            

            if (posX == Constant.CURSOR_POS_NONE)
                posX = Console.CursorLeft;
            if (posY == Constant.CURSOR_POS_NONE)
                posY = Console.CursorTop;

            while (!isInputEnter && !isInputEscape) // enter or esc가 눌릴때까지
            {

                ClearCurrentLine(posX, Constant.CURSOR_POS_RIGHT, posY); // 받았던 값 지우고
                Console.SetCursorPosition(posX, posY); // 입력받는 좌표로 이동
                if (isPassword)
                {
                    for (int i = 0; i < input.Length; i++)
                        Console.Write("*");
                }
                else
                    Console.Write(input);

                if (input.Length > maxIputLength) // 키입력받기전에 최대길이 넘어가는경우 체크
                {
                    input = input.Substring(0, input.Length-1);
                    message.Draw("지정된 범위로 입력하세요", Constant.EXCEPTION_CURSOR_POS_X, Constant.EXCEPTION_CURSOR_POS_Y, false, ConsoleColor.Red);
                    continue;
                }

                ConsoleKeyInfo key = Console.ReadKey(); //키입력 받고
                ClearCurrentLine(Constant.EXCEPTION_CURSOR_POS_X, Encoding.Default.GetBytes(messageString).Length + 2, Constant.EXCEPTION_CURSOR_POS_Y); //에러 메세지 지우고
                Console.SetCursorPosition(posX, posY); // 입력받는 좌표로 이동


                if (key.Key == ConsoleKey.Escape) // Esc 눌리면
                {
                    isInputEscape = true;
                    return Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString();
                }


                if ((key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace) && (!IsExceptionCheck(key.KeyChar.ToString(), exceptionType)))  //입력받은 키가 예외처리에 통과하는지
                {
                    message.Draw(messageString, Constant.EXCEPTION_CURSOR_POS_X, Constant.EXCEPTION_CURSOR_POS_Y, false, ConsoleColor.Red);
                    continue;
                }

                if (key.Key != ConsoleKey.Enter) // 엔터키가 아닐때
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
                    }
                }
                else
                {
                    if (IsExceptionCheck(input, finalExceptionType))
                    {
                        message.Draw("[OK]", Constant.CHECK_MESSAGE_CURSOR_POS_X, Console.CursorTop, false, ConsoleColor.Green);
                        isInputEnter = true;
                    }
                    else
                    {
                        message.Draw("지정된 범위로 입력하세요", Constant.EXCEPTION_CURSOR_POS_X, Constant.EXCEPTION_CURSOR_POS_Y, false, ConsoleColor.Red);
                        ClearCurrentLine(posX, Encoding.Default.GetBytes(input).Length * 2, posY); // 받았던 값 지우고
                        input = ""; // 입력받은값 초기화
                    }
                }
            }
            return input;
        }

        public void ClearCurrentLine(int startPosX = Constant.CURSOR_POS_LEFT, int lastPosX = Constant.CURSOR_POS_NONE, int posY = Constant.CURSOR_POS_NONE)
        {
            string str = "";
            int setPosY;
            if (startPosX == Constant.CURSOR_POS_LEFT)
                str = "\r";
            if (lastPosX == Constant.CURSOR_POS_NONE)
                str += new string(' ', Console.CursorLeft);
            else
                str += new string(' ', lastPosX);
            if (startPosX == Constant.CURSOR_POS_LEFT)
                str += "\r";

            if (posY == Constant.CURSOR_POS_NONE)
                setPosY = Console.CursorTop;
            else
                setPosY = posY;

            Console.SetCursorPosition(startPosX, setPosY);
            Console.Write(str);

            if (startPosX != Constant.CURSOR_POS_LEFT)
                Console.SetCursorPosition(startPosX, setPosY);
        }
    }
}
