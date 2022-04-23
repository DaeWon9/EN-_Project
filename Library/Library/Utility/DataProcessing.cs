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
        private static DataProcessing instance;
        private bool isInputEnter = false;
        private bool isInputEscape = false;

        public static DataProcessing Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataProcessing();
                }
                return instance;
            }
        }

        public int CursorMove(int posX, int posY, int minPosY, int maxPosY)
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
                        if (cursorPosY < minPosY)
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

        public bool IsExceptionCheck(string stringValue, string exceptionType)
        {
            Regex regex = new Regex(exceptionType, RegexOptions.None);
            if (stringValue == "" || stringValue.Length < 1)
                return false;

            return regex.IsMatch(stringValue);
        }

        private void RefreshInputValues(string input, bool isPassword = false)
        {
            if (isPassword)
            {
                for (int i = 0; i < input.Length; i++)
                    Console.Write("*");
            }
            else
                Console.Write(input);
        }

        public string GetInputValues(Message message, int posX, int posY, int maxInputLength, string messageString, string exceptionType, string finalExceptionType, bool isPassword = false) // 매개변수가 너무많다.. 기능완성 후에 줄여보기.....
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
                ClearConsoleLine(posX, Constant.CURSOR_POS_RIGHT, posY); // 받았던 값 지우고
                Console.SetCursorPosition(posX, posY); // 입력받는 좌표로 이동

                RefreshInputValues(input, isPassword); // 입력받은값 화면에 print

                if (input.Length > maxInputLength) // 키입력받기전에 최대길이 넘어가는경우 체크
                {
                    input = input.Substring(0, input.Length-1);
                    message.PrintMessage("지정된 범위로 입력하세요", Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    continue;
                }

                ConsoleKeyInfo key = Console.ReadKey(); //키입력 받고
                ClearConsoleLine(Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_MAX_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y); //에러 메세지 지우고
                Console.SetCursorPosition(posX, posY); // 입력받는 좌표로 이동


                if (key.Key == ConsoleKey.Escape) // Esc 눌리면
                {
                    isInputEscape = true;
                    ClearConsoleLine(posX, Constant.CURSOR_POS_RIGHT, posY); // 받았던 값 지우고
                    return Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString();
                }


                if ((key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace) && (!IsExceptionCheck(key.KeyChar.ToString(), exceptionType)))  //입력받은 키가 예외처리에 통과하는지
                {
                    message.PrintMessage(messageString, Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                    continue;
                }

                if (key.Key != ConsoleKey.Enter) // 엔터키가 아닐때
                {
                    if (key.Key != ConsoleKey.Backspace)
                        input += key.KeyChar.ToString();
                    else
                    {
                        if (input.Length > 0)
                            input = input.Substring(0, input.Length-1);
                    }
                }
                else // 엔터키 눌리면
                {
                    if (IsExceptionCheck(input, finalExceptionType))
                    {
                        int ErrormessagePosX = posX + Encoding.Default.GetBytes(input).Length + 1;
                        message.PrintMessage("[OK]", ErrormessagePosX, Console.CursorTop, ConsoleColor.Green);
                        isInputEnter = true;
                    }
                    else
                    {
                        message.PrintMessage("지정된 형식으로 입력하세요", Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Red);
                        ClearConsoleLine(posX, Encoding.Default.GetBytes(input).Length * 2, posY); // 받았던 값 지우고
                        input = ""; // 입력받은값 초기화
                    }
                }
            }
            return input;
        }

        public void ClearConsoleLine(int startPosX, int lastPosX, int posY)
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
    
        public int GetEnterOrEscape()
        {
            int enterOrEscape = 0;
            isInputEnter = false;
            isInputEscape = false;
            Console.CursorVisible = false;
            while (!isInputEnter && !isInputEscape) // enter or esc가 눌릴때까지
            {
                ConsoleKeyInfo key = Console.ReadKey(); //키입력 받고
                if (key.Key == ConsoleKey.Escape) // Esc
                {
                    isInputEscape = true;
                    enterOrEscape =  Constant.INPUT_ESCAPE;
                }
                else if (key.Key == ConsoleKey.Enter) // Enter
                {
                    isInputEnter = true;
                    enterOrEscape =  Constant.INPUT_ENTER;
                }
                else  // 그 외의 키
                {
                    int keyLength = Encoding.Default.GetBytes(key.KeyChar.ToString()).Length;
                    for (int removeCnt = 0; removeCnt < keyLength; removeCnt++)
                        Console.Write("\b \b");
                }
            }
            Console.CursorVisible = true;
            return enterOrEscape;
        }

        public bool IsInputEscape(string stringValue)
        {
            if (stringValue == Constant.INPUT_ESCAPE_IN_ARROW_KEY.ToString())
                return true;
            return false;
        }

        public string GetConditionalStringBySearchBook(string bookId, string bookName, string bookPublisher, string bookAuthor, string bookPrice, string bookQuantity)// 아래쪽 부분 기능완성 후 Constant로 빼기
        {
            string conditionalString = "";

            if (bookId != "" && bookId != Constant.INPUT_ESCAPE.ToString())
                if (conditionalString == "")
                    conditionalString += string.Format(Constant.CONDITIONAL_STRING_COMPARE_BY_INT, Constant.BOOK_FILED_ID, bookId);
                else
                {
                    conditionalString += Constant.CONDITIONAL_STRING_AND;
                    conditionalString += string.Format(Constant.CONDITIONAL_STRING_COMPARE_BY_INT, Constant.BOOK_FILED_ID, bookId);
                }
            if (bookName != "" && bookName != Constant.INPUT_ESCAPE.ToString())
                if (conditionalString == "")
                {
                    conditionalString += string.Format(Constant.CONDITIONAL_STRING_LIKE, Constant.BOOK_FILED_NAME, bookName);
                }
                else
                {
                    conditionalString += Constant.CONDITIONAL_STRING_AND;
                    conditionalString += string.Format(Constant.CONDITIONAL_STRING_LIKE, Constant.BOOK_FILED_NAME, bookName);
                }
            if (bookPublisher != "" && bookPublisher != Constant.INPUT_ESCAPE.ToString())
                if (conditionalString == "")
                {
                    conditionalString += string.Format(Constant.CONDITIONAL_STRING_LIKE, Constant.BOOK_FILED_PUBLISHER, bookPublisher);
                }
                else
                {
                    conditionalString += Constant.CONDITIONAL_STRING_AND;
                    conditionalString += string.Format(Constant.CONDITIONAL_STRING_LIKE, Constant.BOOK_FILED_PUBLISHER, bookPublisher);
                }
            if (bookAuthor != "" && bookAuthor != Constant.INPUT_ESCAPE.ToString())
                if (conditionalString == "")
                {
                    conditionalString += string.Format(Constant.CONDITIONAL_STRING_LIKE, Constant.BOOK_FILED_AUTHOR, bookAuthor);
                }
                else
                {
                    conditionalString += Constant.CONDITIONAL_STRING_AND;
                    conditionalString += string.Format(Constant.CONDITIONAL_STRING_LIKE, Constant.BOOK_FILED_AUTHOR, bookAuthor);
                }
            if (bookPrice != "" && bookPrice != Constant.INPUT_ESCAPE.ToString())
                if (conditionalString == "")
                    conditionalString += string.Format(Constant.CONDITIONAL_STRING_COMPARE_BY_INT, Constant.BOOK_FILED_PRICE, bookPrice);
                else
                {
                    conditionalString += Constant.CONDITIONAL_STRING_AND;
                    conditionalString += string.Format(Constant.CONDITIONAL_STRING_COMPARE_BY_INT, Constant.BOOK_FILED_PRICE, bookPrice);
                }
            if (bookQuantity != "" && bookQuantity != Constant.INPUT_ESCAPE.ToString())
                if (conditionalString == "")
                    conditionalString += string.Format(Constant.CONDITIONAL_STRING_COMPARE_BY_INT, Constant.BOOK_FILED_QUANTITY, bookQuantity);
                else
                {
                    conditionalString += Constant.CONDITIONAL_STRING_AND;
                    conditionalString += string.Format(Constant.CONDITIONAL_STRING_COMPARE_BY_INT, Constant.BOOK_FILED_QUANTITY, bookQuantity);
                }

            return conditionalString;
        }

        public bool IsExit(Message message)
        {
            Console.CursorVisible = false;
            message.PrintMessage(Constant.TEXT_IS_EXIST, Constant.EXCEPTION_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, ConsoleColor.Yellow);
            message.PrintMessage(Constant.TEXT_YES_OR_NO, Constant.YES_OR_NO_MESSAGE_CURSOR_POS_X, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, ConsoleColor.Yellow);

            if (GetEnterOrEscape() == Constant.INPUT_ENTER) // enter 눌리면 true 반환
            {
                Console.CursorVisible = true;
                return true;
            }
            Console.CursorVisible = true;
            return false;
        }
    }
}
