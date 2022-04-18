using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Utility
{
    internal class ScreenProcessing
    {
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
