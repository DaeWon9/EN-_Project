using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.View
{
    class Message
    {
        public void Draw(string message, int posX = -1, int posY = -1, bool isClear = true, bool colorChangeRed = false)
        {
            if (posX == -1)
                posX = Console.CursorLeft;
            if (posY == -1)
                posY = Console.CursorTop;
            if (colorChangeRed)
                Console.ForegroundColor = ConsoleColor.Red;
            if (isClear)
                Console.Clear();

            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
