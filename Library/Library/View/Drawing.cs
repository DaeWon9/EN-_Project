using System;

namespace Library.View
{
    class Drawing
    {

        public void LibraryTitleScreen(bool isClear = false)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *               ");
            Console.WriteLine("           *                                                                     *              ");
            Console.WriteLine("          *     ■     ■■■  ■■■   ■■■      ■■    ■■■   ■    ■     *             ");
            Console.WriteLine("         *      ■       ■    ■   ■  ■   ■    ■  ■   ■   ■   ■  ■       *            ");
            Console.WriteLine("        *       ■       ■    ■■■   ■■■    ■■■■  ■■■      ■          *           ");
            Console.WriteLine("         *      ■       ■    ■   ■  ■   ■   ■    ■  ■   ■     ■         *            ");
            Console.WriteLine("          *     ■■■ ■■■  ■■■   ■    ■  ■    ■  ■    ■    ■        *             ");
            Console.WriteLine("           *                                                                     *              ");
            Console.WriteLine("            *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *               ");
        }

        public void MainMenuScreen()
        {
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                      ▶  회원모드                                              ");
            Console.WriteLine("                                      ▶  관리자모드                                            ");
            Console.WriteLine("                                                                                                ");
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
