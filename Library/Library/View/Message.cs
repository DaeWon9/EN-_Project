using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.View
{
    class Message
    {
        public void PrintMessage(string message, int posX, int posY, bool isClear = true, ConsoleColor color = ConsoleColor.White)
        {
            if (posX == Constant.CURSOR_POS_NONE)
                posX = Console.CursorLeft;
            if (posY == Constant.CURSOR_POS_NONE)
                posY = Console.CursorTop;
            if (isClear)
                Console.Clear();

            Console.SetCursorPosition(posX, posY);
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
