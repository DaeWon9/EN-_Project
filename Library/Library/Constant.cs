using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class Constant
    {
        //window size
        public const int WINDOW_WIDTH = 95;
        public const int WINDOW_HEIGHT = 50;
        //exception type
        public const int EXCEPTION_TYPE_ANY = 1;
        public const int EXCEPTION_TYPE_NUMBER = 2;
        public const int EXCEPTION_TYPE_KOREA = 3;
        public const int EXCEPTION_TYPE_ENGLISH = 4;

        // first menu cursor pos
        public const int FIRST_MENU_CURSOR_POS_X = 37;
        public const int FIRST_MENU_CURSOR_MIN_POS_Y = 12;
        public const int FIRST_MENU_CURSOR_MAX_POS_Y = 13;

        //exception cursor pos
        public const int EXCEPTION_CURSOR_POS_X = 80;

        // set constant Escape keyValue in arrow move
        public const int INPUT_ESCAPE_IN_ARROW_KEY = -1;

        // member and administrator mode value
        public const int MODE_MEMBER = 11;
        public const int MODE_ADMINISTRATOR = 12;



    }
}
