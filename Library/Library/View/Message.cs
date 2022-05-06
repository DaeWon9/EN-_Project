using System;
using System.Text;

namespace Library.View
{
    class Message
    {
        public void PrintMessage(string message, int posX, int posY, ConsoleColor color = ConsoleColor.White, bool isClear = false)
        {
            if (posX == Constant.WINDOW_WIDTH_CENTER)
                posX = Constant.WINDOW_WIDTH_CENTER - (Encoding.Default.GetBytes(message).Length) / 2; // 가운데정렬
            if (posY == Constant.CURSOR_POS_NONE)
                posY = Console.CursorTop;
            if (isClear)
                Console.Clear();

            Console.SetCursorPosition(posX, posY);
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void PrintConfirmationMessage(string message, ConsoleColor color)
        {
            PrintMessage(message, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y - 1, color); //검색하시겠습니까? 확인문구 
            PrintMessage(Constant.TEXT_YES_OR_NO, Constant.WINDOW_WIDTH_CENTER, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y, color);
            Console.SetCursorPosition(Constant.CURSOR_POS_LEFT, Constant.EXCEPTION_MESSAGE_CURSOR_POS_Y); //좌표조정
        }

    }
}
