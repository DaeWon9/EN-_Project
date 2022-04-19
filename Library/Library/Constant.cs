using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Constant
    {
        //window size
        public const int WINDOW_WIDTH = 95;
        public const int WINDOW_HEIGHT = 50;
        //exception type
        public const int EXCEPTION_TYPE_ANY = 1;
        public const int EXCEPTION_TYPE_NUMBER = 2;
        public const int EXCEPTION_TYPE_KOREA = 3;
        public const int EXCEPTION_TYPE_ENGLISH = 4;
        public const int EXCEPTION_TYPE_ENGLISH_NUMBER = 5;

        // menu cursor pos
        public const int MENU_CURSOR_POS_X = 37;
        public const int MENU_CURSOR_MIN_POS_Y = 12;
        public const int FIRST_MENU_CURSOR_MAX_POS_Y = 13;
        public const int ADMINISTRATOR_MENU_CURSOR_MAX_POS_Y = 17;

        //exception cursor pos
        public const int EXCEPTION_CURSOR_POS_X = 37;
        public const int EXCEPTION_CURSOR_POS_Y = 10;

        // set constant Escape keyValue in arrow move
        public const int INPUT_ESCAPE_IN_ARROW_KEY = -1;

        // member and administrator mode value
        public const int MODE_MEMBER = 12;
        public const int MODE_ADMINISTRATOR = 13;

        // member main menu value -> 로그인 or 회원가입
        public const int MODE_MEMBER_LOGIN = 12;
        public const int MODE_MEMBER_SIGN_UP = 13;

        // login pos
        public const int LOGIN_POS_X = 35;
        public const int LOGIN_ID_POS_Y = 12;
        public const int LOGIN_PASSWORD_POS_Y = 13;

        // administrator menu value
        public const int MENU_BOOK_SEARCH = 12;
        public const int MENU_BOOK_ADD = 13;
        public const int MENU_BOOK_REMOVE = 14;
        public const int MENU_BOOK_REVISE = 15;
        public const int MENU_MEMBER_MANAGEMENT = 16;
        public const int MENU_RENTAL_STATUS = 17;
    }
}
