using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
