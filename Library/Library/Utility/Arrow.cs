using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Utility
{
    class Arrow
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
    }
}
