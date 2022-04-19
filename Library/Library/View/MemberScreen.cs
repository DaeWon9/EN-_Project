using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.View
{
    class MemberScreen : Label
    {

        public void MeberMainScreenDraw(bool isClear = false)
        {
            LibraryLabelDraw(isClear);
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                                                              뒤로가기 : ESC    ");
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                      ▶  로그인                                                ");
            Console.WriteLine("                                      ▶  회원가입                                              ");
            Console.WriteLine("                                                                                                ");
        }
        public void MemberLoginScreenDraw(bool isClear = false)
        {
            ExplainMemberLoginLabelDraw(isClear);
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                                                              뒤로가기 : ESC    ");
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                 ID :                                                           ");
            Console.WriteLine("                                 PW :                                                           ");
            Console.WriteLine("                                                                                                ");
        }
    }
}
