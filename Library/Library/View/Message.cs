using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.View
{
    class Message
    {
        public void Draw(string message, int posX = -1, int posY = -1, bool isClear = true, ConsoleColor color = ConsoleColor.White)
        {
            if (posX == -1)
                posX = Console.CursorLeft;
            if (posY == -1)
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
