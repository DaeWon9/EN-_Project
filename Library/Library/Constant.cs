using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//reader["name"], reader["id"], reader["pw"], reader["age"], reader["address"], reader["phonenumber"]);
namespace Library
{
    class Constant
    {
        //window size
        public const int WINDOW_WIDTH = 95;
        public const int WINDOW_HEIGHT = 50;

        // SQL
        public const string DATABASE_CONNECTION_INFORMATION = "Server=localhost;Port=3307;Database=library;Uid=root;Pwd=0000;";
        // QUERY String
        public const string QUERY_STRING_SELECT = "SELECT {0} FROM {1}";
        public const string QUERY_STRING_CONDITIONAL_SELECT = "SELECT {0} FROM {1} WHERE {2}";
        public const string QUERY_STRING_INSERT = "INSERT INTO book VALUES ({1}, {2}, {3}, {4}, {5}, {6})";
        public const string QUERY_STRING_CONDITIONAL_DELETE = "DELETE FROM {0} WHERE {1}";


        public const bool IS_NOT_CONSOLE_CLEAR = false;
        public const bool IS_CONSOLE_CLEAR = true;

        public const bool IS_NOT_PASSWORD = false;
        public const bool IS_PASSWORD = true;

        // cursor pos
        public const int CURSOR_POS_LEFT = 0;
        public const int CURSOR_POS_RIGHT = 94;
        public const int CURSOR_POS_NONE = -1;

        public const int EXCEPTION_MESSAGE_MAX_POS_Y = 90;
        //Max length
        public const int MAX_LENGTH_ID = 10;
        public const int MAX_LENGTH_PASSWORD = 10;
        //exception type
        public const int EXCEPTION_TYPE_ANY = 1; // -> enum으로 하거나 or 갈아엎기 
        public const int EXCEPTION_TYPE_NUMBER = 2;
        public const int EXCEPTION_TYPE_KOREA = 3;
        public const int EXCEPTION_TYPE_ENGLISH = 4;
        public const int EXCEPTION_TYPE_ENGLISH_NUMBER = 5;
        public const int EXCEPTION_TYPE_ID = 6;
        public const int EXCEPTION_TYPE_PASSWORD = 7;

        // menu cursor pos
        public const int MENU_CURSOR_POS_X = 37;
        public const int MENU_CURSOR_MIN_POS_Y = 12;
        public const int FIRST_MENU_CURSOR_MAX_POS_Y = 13;
        public const int ADMINISTRATOR_MENU_CURSOR_MAX_POS_Y = 17;

        //exception cursor pos
        public const int EXCEPTION_CURSOR_POS_X = 37;
        public const int EXCEPTION_CURSOR_POS_Y = 10;

        //check message cursor pos
        public const int CHECK_MESSAGE_CURSOR_POS_X = 50;

        // set constant keyValue 
        public const int INPUT_ESCAPE_IN_ARROW_KEY = -1;
        public const int INPUT_NONE = -1;

        // member and administrator mode value
        public const int MODE_MEMBER = 12;
        public const int MODE_ADMINISTRATOR = 13;

        // member main menu value -> 로그인 or 회원가입
        public const int MODE_MEMBER_LOGIN = 12;
        public const int MODE_MEMBER_SIGN_UP = 13;

        // login pos
        public const int LOGIN_POS_X = 38;
        public const int LOGIN_ID_POS_Y = 12;
        public const int LOGIN_PASSWORD_POS_Y = 13;

        // administrator menu value //이것도 1씩증가하는거라 바꿀 수 있을 듯??
        public const int MENU_BOOK_SEARCH = 12;
        public const int MENU_BOOK_ADD = 13;
        public const int MENU_BOOK_REMOVE = 14;
        public const int MENU_BOOK_REVISE = 15;
        public const int MENU_MEMBER_MANAGEMENT = 16;
        public const int MENU_RENTAL_STATUS = 17;
    }
}
