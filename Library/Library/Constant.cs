using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//reader["name"], reader["id"], reader["pw"], reader["age"], reader["address"], reader["PHONE_NUMBER"]);
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
        public const string QUERY_STRING_INSERT = "INSERT INTO {0} VALUES ('{1}', '{2}', '{3}', {4}, '{5}', '{6}')";
        public const string QUERY_STRING_CONDITIONAL_DELETE = "DELETE FROM {0} WHERE {1}";

        // Mysql Table 
        public const string TABLE_NAME_ADMINISTRATOR = "administrator";
        public const string TABLE_NAME_BOOK = "book";
        public const string TABLE_NAME_MEMBER = "member";

        // Administrator Filed
        public const string ADMINISTRATOR_FILED_ID = "id";
        public const string ADMINISTRATOR_FILED_PASSWORD = "pw";

        // Member Filed
        public const string MEMBER_FILED_NAME = "name";
        public const string MEMBER_FILED_ID = "id";
        public const string MEMBER_FILED_PASSWORD = "pw";
        public const string MEMBER_FILED_AGE = "age";
        public const string MEMBER_FILED_ADDRESS = "address";
        public const string MEMBER_FILED_PHONE_NUMBER = "phonenumber";

        // Book Filed
        public const string FILED_ALL = "*";
        public const string BOOK_FILED_ID = "id";
        public const string BOOK_FILED_NAME = "name";
        public const string BOOK_FILED_PUBLISHER = "publisher";
        public const string BOOK_FILED_AUTHOR = "author";
        public const string BOOK_FILED_PRICE = "price";
        public const string BOOK_FILED_QUANTITY = "quantity";



        public const bool IS_NOT_CONSOLE_CLEAR = false;
        public const bool IS_CONSOLE_CLEAR = true;

        public const bool IS_NOT_PASSWORD = false;
        public const bool IS_PASSWORD = true;

        // cursor pos
        public const int CURSOR_POS_LEFT = 0;
        public const int CURSOR_POS_RIGHT = 94;
        public const int CURSOR_POS_NONE = -1;

        // message
        public const int EXCEPTION_MESSAGE_MAX_POS_X = 39;

        //Max length
        public const int MAX_LENGTH_ID = 10;
        public const int MAX_LENGTH_PASSWORD = 10;
        public const int MAX_LENGTH_NAME = 5;
        public const int MAX_LENGTH_PHONE_NUMBER = 11;
        public const int MAX_LENGTH_AGE = 3;
        public const int MAX_LENGTH_ADDRESS = 20;
        public const int MAX_LENGTH_BOOK_ID = 3;
        public const int MAX_LENGTH_BOOK_NAME = 100;
        public const int MAX_LENGTH_BOOK_PUBLISHER = 20;
        public const int MAX_LENGTH_BOOK_AUTHOR = 20;
        public const int MAX_LENGTH_BOOK_PRICE = 7;
        public const int MAX_LENGTH_BOOK_QUANTITY = 2;

        //exception type
        public const string EXCEPTION_TYPE_ANY = @"^[a-zA-Z0-9ㄱ-ㅎ가-힣\s!@#$%^&*()-=_+]*$";
        public const string EXCEPTION_TYPE_NUMBER = @"^[0-9]*$";
        public const string EXCEPTION_TYPE_KOREA = @"^[가-힣]*$";
        public const string EXCEPTION_TYPE_KOREA_NUMBER = @"^[가-힣-0-9]*$";
        public const string EXCEPTION_TYPE_KOREA_NUMBER_SPACE = @"^[가-힣-0-9\s]*$";
        public const string EXCEPTION_TYPE_ENGLISH = @"^[a-zA-Z]*$";
        public const string EXCEPTION_TYPE_ENGLISH_NUMBER = @"^[0-9a-zA-Z]*$";
        public const string EXCEPTION_TYPE_ENGLISH_NUMBER_KOREA = @"^[a-zA-Z0-9가-힣\s!@#$%^&*()-=_+]*$";

        public const string EXCEPTION_TYPE_ID = @"^[0-9a-zA-Z]{6,10}$";
        public const string EXCEPTION_TYPE_PASSWORD = @"^[0-9a-zA-Z]{6,10}$";
        public const string EXCEPTION_TYPE_NAME = @"^[가-힣]{2,5}$";
        public const string EXCEPTION_TYPE_AGE = @"^[0-9]{1,3}$";
        public const string EXCEPTION_TYPE_ADDRESS = @"^[가-힣]\w*(시|도)\s[가-힣]\w*(시|군|구)\s[가-힣]\w*(읍|면|동)[가-힣-0-9\s]*$";
        public const string EXCEPTION_TYPE_PHONE_NUMBER = @"^01([0|1|6|7|8|9])([0-9]{3,4})([0-9]{4})$";

        public const string EXCEPTION_TYPE_BOOK_ID = @"^[0-9]{1,3}$";
        public const string EXCEPTION_TYPE_BOOK_NAME = @"^[a-zA-Z0-9ㄱ-ㅎ가-힣\s!@#$%^&*()-=_+]{2,100}$";
        public const string EXCEPTION_TYPE_BOOK_PUBLISHER = @"^[a-zA-Z0-9ㄱ-ㅎ가-힣\s!@#$%^&*()-=_+]{2,20}$";
        public const string EXCEPTION_TYPE_BOOK_AUTHOR = @"^[a-zA-Z0-9가-힣\s!@#$%^&*()-=_+]{2,20}$";
        public const string EXCEPTION_TYPE_BOOK_PRICE = @"^[0-9]{4,7}$";
        public const string EXCEPTION_TYPE_BOOK_QUANTITY = @"^[0-9]{1,2}$";

        // menu cursor pos
        public const int MENU_CURSOR_POS_X = 37;
        public const int MENU_CURSOR_MIN_POS_Y = 12;
        public const int FIRST_MENU_CURSOR_MAX_POS_Y = 13;
        public const int ADMINISTRATOR_MENU_CURSOR_MAX_POS_Y = 17;
        public const int MEMBER_MENU_CURSOR_MAX_POS_Y = 15;

        //message
        public const int EXCEPTION_MESSAGE_CURSOR_POS_X = 37;
        public const int EXCEPTION_MESSAGE_CURSOR_POS_Y = 10;
        public const int YES_OR_NO_MESSAGE_CURSOR_POS_X = 33;

        //check message cursor pos
        public const int CHECK_MESSAGE_CURSOR_POS_X = 80;

        // set constant keyValue 
        public const int INPUT_ESCAPE_IN_ARROW_KEY = -1;
        public const int INPUT_NONE = -1;

        public const int INPUT_DOWN_ARROW_KEY = -2;
        public const int INPUT_UP_ARROW_KEY = -3;
            
            
        // member and administrator mode value
        public const int MODE_MEMBER = 12;
        public const int MODE_ADMINISTRATOR = 13;

        // member main menu value -> 로그인 or 회원가입
        public const int MODE_MEMBER_LOGIN = 12;
        public const int MODE_MEMBER_SIGN_UP = 13;

        // login pos
        public const int LOGIN_POS_X = 41;
        public const int LOGIN_ID_POS_Y = 12;
        public const int LOGIN_PASSWORD_POS_Y = 13;

        // yes or no
        public const int INPUT_ESCAPE = -1;
        public const int INPUT_ENTER = 1;

        // search pos
        public const int SEARCH_POS_X = 47;
        public const int SEARCH_SELECT_OPTION_POS_X = 29;
        public enum BookSearchPosY : int { ID = 12, NAME, PUBLISHER, AUTHOR, PRICE, QUANTITY, SEARCH}

        // signup pos
        public const int SIGNUP_POS_X = 47;

        public enum SignUpPosY : int { NAME = 12, ID, PASSWORD, PASSWORD_CHECK, AGE, ADDRESS, PHONE_NUMBER}

        public enum AdministratorMenu : int { BOOK_SEARCH = 12, BOOK_ADD, BOOK_REMOVE, BOOK_REVISE, MEMBER_MANAGEMENT, RENTAL_STATUS} 

        public enum MemberMenu : int { BOOK_SEARCH = 12, BOOK_RENTAL, BOOK_CHECK, MODIFICATION_MEMBER_INFORMATION }
    }
}