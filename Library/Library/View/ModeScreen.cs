using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.View
{
    class ModeScreen : Label
    {
        public void SelectModeScreenDraw()
        {
            LibraryLabelDraw();
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                                              선택 : ENTER        종료 : ESC    ");
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                      ▶  회원모드                                              ");
            Console.WriteLine("                                      ▶  관리자모드                                            ");
            Console.WriteLine("                                                                                                ");
        }
    }
}
