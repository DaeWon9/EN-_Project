using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Library
{
    class Constant
    {
        // SQL connection
        public const string DATABASE_CONNECTION_INFORMATION = "Server=localhost;Port=3307;Database=daewonLibrary;Uid=root;Pwd=0000;";

        // naver api
        public const string CLIENT_ID = "RS8gQbYXLAarLLZv26GN";
        public const string CLIENT_SECRET = "_QKwOfvRUZ";

        //window size
        public const int WINDOW_WIDTH = 100;
        public const int WINDOW_WIDTH_CENTER = 50;
        public const int WINDOW_HEIGHT = 50;

        public const int MF_BYCOMMAND = 0x00000000; 
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;

        //exception type
        public const string EXCEPTION_TYPE_ANY = @"^[a-zA-Z0-9ㄱ-ㅎ가-힣\s!@#$%^&*()-=_+]*$";
        public const string EXCEPTION_TYPE_NUMBER = @"^[0-9]*$";
        public const string EXCEPTION_TYPE_NUMBER_SPACE_PYPHEN = @"^[0-9\s-]*$";
        public const string EXCEPTION_TYPE_KOREAN = @"^[가-힣]*$";
        public const string EXCEPTION_TYPE_KOREAN_ENGLISH = @"^[가-힣a-zA-Z]*$";
        public const string EXCEPTION_TYPE_KOREAN_NUMBER = @"^[가-힣-0-9]*$";
        public const string EXCEPTION_TYPE_KOREAN_NUMBER_SPACE = @"^[가-힣-0-9\s]*$";
        public const string EXCEPTION_TYPE_ENGLISH = @"^[a-zA-Z]*$";
        public const string EXCEPTION_TYPE_ENGLISH_NUMBER = @"^[0-9a-zA-Z]*$";
        public const string EXCEPTION_TYPE_ENGLISH_NUMBER_KOREA = @"^[a-zA-Z0-9가-힣\s!@#$%^&*()-=_+]*$";

        public const string EXCEPTION_TYPE_MEMBER_ID = @"^[0-9a-zA-Z]{6,10}$";
        public const string EXCEPTION_TYPE_MEMBER_PASSWORD = @"^[0-9a-zA-Z]{6,10}$";
        public const string EXCEPTION_TYPE_MEMBER_NAME = @"^([가-힣]{2,5}|[a-zA-Z]{3,20})$";
        public const string EXCEPTION_TYPE_MEMBER_ADDRESS = @"^([가-힣]\w*(시)\s[가-힣]\w*(군|구)\s|[가-힣]\w*(도)\s[가-힣]\w*(시)\s[가-힣]\w*(구)\s)([가-힣]\w*(읍|면|동)|[가-힣0-9]\w*(로|길))[가-힣-0-9\s]*$";
        public const string EXCEPTION_TYPE_MEMBER_PHONE_NUMBER = @"^01([0|1|6|7|8|9])([0-9]{3,4})([0-9]{4})$"; 

        public const string EXCEPTION_TYPE_BOOK_ID = @"^[0-9]{1,3}$";
        public const string EXCEPTION_TYPE_BOOK_NAME = @"^[a-zA-Z0-9ㄱ-ㅎ가-힣\s!@#$%^&*()-=_+]{2,100}$";
        public const string EXCEPTION_TYPE_BOOK_PUBLISHER = @"^[a-zA-Z0-9ㄱ-ㅎ가-힣\s!@#$%^&*()-=_+]{2,20}$";
        public const string EXCEPTION_TYPE_BOOK_AUTHOR = @"^[a-zA-Z0-9가-힣\s!@#$%^&*()-=_+]{2,20}$";
        public const string EXCEPTION_TYPE_BOOK_PRICE = @"^[0-9]{4,7}$";
        public const string EXCEPTION_TYPE_BOOK_QUANTITY = @"^[0-9]{1,2}$";
        public const string EXCEPTION_TYPE_DATE = @"^([1-2][0-9]{3})(0[1-9]|1[0-2])(0[1-9]|[1-2][0-9]|3[0-1])$";

        // TEXT
        public const string TEXT_NONE = "";
        public const string TEXT_OK = "[OK]";
        public const string TEXT_WELCOME = "< {0}님 환영합니다 >";
        public const string TEXT_ADMINISTRATOR_MODE = "< 관리자모드 >";
        public const string TEXT_YES_OR_NO = "< YES : ENTER | NO : ESC >";
        public const string TEXT_IS_EXIST = "종료하시겠습니까??";
        public const string TEXT_IS_LOGOUT = "로그아웃하시겠습니까??";
        public const string TEXT_IS_SEARCH = "검색하시겠습니까??";
        public const string TEXT_IS_SIGN_UP = "가입하시겠습니까??";
        public const string TEXT_IS_BORROW = "대여하시겠습니까??";
        public const string TEXT_IS_RETURN = "반납하시겠습니까??";
        public const string TEXT_IS_MODIFY = "수정하시겠습니까??";
        public const string TEXT_IS_ADD = "추가하시겠습니까??";
        public const string TEXT_IS_MANAGEMENT = "선택한 회원의 정보를 수정하시겠습니까??";
        public const string TEXT_IS_WITHDRAWAL = "< 주의 > 모든정보가 삭제됩니다. 정말로 탈퇴하시겠습니까??";
        public const string TEXT_IS_DELETE = "< 주의 > 모든정보가 삭제됩니다. 정말로 삭제하시겠습니까??";
        public const string TEXT_IS_RESET_LOG = "< 주의 > 모든로그가 삭제됩니다. 정말로 삭제하시겠습니까??";

        public const string TEXT_UNABLE_WITHDRAWAL = "대여중인 도서가 존재하여 탈퇴가 불가능합니다";
        public const string TEXT_UNABLE_DELETE = "해당도서를 대여중인 회원이 존재하여 삭제가 불가능합니다";

        public const string TEXT_PLEASE_INPUT_OPTION = "옵션을 입력해주세요";
        public const string TEXT_PLEASE_INPUT_NUMBER = "숫자만 입력하세요";
        public const string TEXT_PLEASE_INPUT_KOREAN_OR_NUMBER = "한글 & 숫자만 입력하세요";
        public const string TEXT_PLEASE_INPUT_ENGLISH_OR_NUMBER = "영어 & 숫자만 입력하세요";
        public const string TEXT_PLEASE_INPUT_CORRECT_STRING = "올바른 글자만 입력하세요";
        public const string TEXT_PLEASE_INPUT_CORRECT_LENGTH = "올바른 범위로 입력하세요";

        public const string TEXT_ALREADY_REGISTERED_ID = "이미 등록되어있는 ID입니다.";
        public const string TEXT_ALREADY_REGISTERED_BOOK = "이미 등록되어있는 도서입니다.";
        public const string TEXT_IS_NOT_CORRECT_PASSWORD = "비밀번호가 일치하지 않습니다.";
        public const string TEXT_IS_NOT_CORRECT_ID_PASSWORD = "ID & PASSWORD 가 틀립니다";

        public const string TEXT_IS_NOT_EXIST_IN_LIBRARY_OR_NOT_SEARCHED = "도서관에 없는 도서이거나, 검색된 도서가 아닙니다.";
        public const string TEXT_IS_NOT_EXIST_IN_LIBRARY = "도서관에 없는 도서입니다.";
        public const string TEXT_THIS_BOOK_IS_NOT_SEARCHED= "검색되지 않은 도서입니다.";
        public const string TEXT_THIS_NUMBER_IS_NOT_SEARCHED = "검색되지 않은 번호입니다.";
        public const string TEXT_IS_ALREADY_BORROWED = "이미 대여중인 도서입니다.";
        public const string TEXT_IS_NOT_ENOUGH_QUANTITY = "대여가능한 수량이 부족합니다.";
        public const string TEXT_IS_NOT_BORROWED_BOOK = "대여중인 도서가 아닙니다.";
        public const string TEXT_IS_NOT_REGISTERED_MEMBER_ID = "등록되지않은 회원ID입니다.";

        public const string TEXT_SUCCESS_RESET_LOG = "로그초기화에 성공하였습니다!";
        public const string TEXT_SUCCESS_SAVE_LOG = "로그저장에 성공하였습니다!";
        public const string TEXT_SUCCESS_BORROW = "도서대여에 성공했습니다! 계속해서 대여하시겠습니까??";
        public const string TEXT_SUCCESS_SIGN_UP = "회원가입에 성공하였습니다!";
        public const string TEXT_SUCCESS_MODIFY = "정보 변경에 성공하였습니다! 계속해서 변경하시겠습니까??";
        public const string TEXT_SUCCESS_RETURN_BOOK = "도서반납에 성공하였습니다! 계속해서 반납하시겠습니까??";
        public const string TEXT_SUCCESS_ADD_BOOK = "도서추가에 성공하였습니다! 계속해서 추가하시겠습니까??";


        // QUERY String
        public const string QUERY_STRING_CREATE_TABLE_BY_USER_ID = "CREATE TABLE {0} ( id INT NOT NULL, name VARCHAR(100), publisher VARCHAR(20), author VARCHAR(20), price INT, quantity INT, pubdate DATE, isbn varchar(30), borrowDate DATETIME, returnDate DATETIME, PRIMARY KEY(id) ) ENGINE = InnoDB DEFAULT CHARSET = utf8";
        public const string QUERY_STRING_GET_ALL_TABLES = "SHOW tables";

        public const string QUERY_STRING_SELECT = "SELECT {0} FROM {1}";
        public const string QUERY_STRING_CONDITIONAL_SELECT = "SELECT {0} FROM {1} WHERE {2}";
        public const string QUERY_STRING_ORDER_BY_SELECT = "SELECT {0} FROM {1} ORDER BY {2}";
        public const string QUERY_STRING_INSERT_MEMBER = "INSERT INTO {0} VALUES ('{1}', '{2}', '{3}', '{4}', '{5}', '{6}')";
        public const string QUERY_STRING_INSERT_BORROW_BOOK = "INSERT INTO {0} VALUES ({1}, '{2}', '{3}', '{4}', {5}, {6}, '{7}', '{8}', '{9}', '{10}')";
        public const string QUERY_STRING_INSERT_ADD_BOOK = "INSERT INTO {0} (name, publisher, author, price, quantity, pubdate, isbn) VALUES ('{1}', '{2}', '{3}', {4}, {5}, '{6}', '{7}')";
        public const string QUERY_STRING_INSERT_LOG = "INSERT INTO log(date, member, activity) VALUES (now(), '{0}', '{1}')";
        public const string QUERY_STRING_LOG_RESET = "DELETE FROM log;ALTER TABLE log AUTO_INCREMENT=1;";

        public const string QUERY_STRING_CONDITIONAL_DELETE = "DELETE FROM {0} WHERE {1}";
        public const string QUERY_STRING_UPDATE = "UPDATE {0} SET {1}";
        public const string QUERY_STRING_CONDITIONAL_UPDATE = "UPDATE {0} SET {1} WHERE {2}";
        public const string QUERY_STRING_UPDATE_BOOK_QUANTITY_BY_BORROWED = "UPDATE book SET quantity = book.quantity - 1 where id = {0}";
        public const string QUERY_STRING_UPDATE_BOOK_QUANTITY_BY_RETURN = "UPDATE book SET quantity = book.quantity + 1 where id = {0}";
        public const string QUERY_STRING_DROP = "DROP TABLE {0}";
        // set string by delete query
        public const string SET_STRING_EQUAL_BY_STRING = "{0} = '{1}'";
        public const string SET_STRING_EQUAL_BY_INT = "{0} = {1}";

        // conditional string 
        public const string CONDITIONAL_STRING_AND = " AND ";
        public const string CONDITIONAL_STRING_OR = " OR ";
        public const string CONDITIONAL_STRING_COMPARE_EQUAL_BY_STRING = "{0} = '{1}'";
        public const string CONDITIONAL_STRING_COMPARE_EQUAL_BY_INT = "{0} = {1}";
        public const string CONDITIONAL_STRING_COMPARE_BELOW_BY_INT = "{0} <= {1}";
        public const string CONDITIONAL_STRING_COMPARE_OVER_BY_INT = "{0} >= {1}";
        public const string CONDITIONAL_STRING_LIKE = "({0} LIKE '{1}%' OR {0} LIKE '%{1}' OR {0} LIKE '%{1}%')";
        // Mysql Table 
        public const string TABLE_NAME_ADMINISTRATOR = "administrator";
        public const string TABLE_NAME_BOOK = "book";
        public const string TABLE_NAME_MEMBER = "member";
        public const string TABLE_NAME_LOG = "log";

        // Administrator Filed
        public const string ADMINISTRATOR_FILED_ID = "id";
        public const string ADMINISTRATOR_FILED_PASSWORD = "pw";

        //대여일자 filed
        public const string FILED_BORROW_DATE = "borrowDate";

        // Log
        public const string LOG_FILE_NAME = "Log.txt";
        public const string LOG_FILED_NUMBER = "number";
        public const string LOG_FILED_DATE = "date";
        public const string LOG_FILED_MEMBER = "member";
        public const string LOG_FILED_ACTIVITY = "activity";
        public const string LOG_MEMBER_TEXT_FORM = "{0}({1})";

        public const string LOG_ADMINISTRATOR_TEXT_FROM = "< 관리자 >";
        public const string LOG_TEXT_LOGIN = "로그인";
        public const string LOG_TEXT_SIGN_UP = "회원가입";
        public const string LOG_TEXT_WITHDRAWL = "회원탈퇴";
        public const string LOG_TEXT_CHECK_BORROWED_BOOK_STATUS = "대여현황확인";
        public const string LOG_TEXT_SEARCH_BOOK = "도서검색";
        public const string LOG_TEXT_SEARCH_BOOK_BY_NABER = "네이버 도서검색";
        public const string LOG_TEXT_BORROW_BOOK = "도서대여";
        public const string LOG_TEXT_RETURN_BOOK = "도서반납";
        public const string LOG_TEXT_ADD_BOOK = "도서추가";
        public const string LOG_TEXT_DELETE_BOOK = "도서삭제";
        public const string LOG_TEXT_DELETE_MEMBER = "회원삭제";
        public const string LOG_TEXT_MODIFY_MEMBER_NAME = "이름수정";
        public const string LOG_TEXT_MODIFY_MEMBER_PASSWORD = "비밀번호수정";
        public const string LOG_TEXT_MODIFY_MEMBER_AGE = "나이수정";
        public const string LOG_TEXT_MODIFY_MEMBER_BIRTH_DATE = "생년월일수정";
        public const string LOG_TEXT_MODIFY_MEMBER_ADDRESS = "주소수정";
        public const string LOG_TEXT_MODIFY_MEMBER_PHONE_NUMBER = "번호수정";
        public const string LOG_TEXT_MODIFY_BOOK_NAME = "도서명수정";
        public const string LOG_TEXT_MODIFY_BOOK_PUBLISHER = "출판사수정";
        public const string LOG_TEXT_MODIFY_BOOK_AUTHOR = "저자수정";
        public const string LOG_TEXT_MODIFY_BOOK_PRICE = "가격수정";
        public const string LOG_TEXT_MODIFY_BOOK_QUANTITY = "수량수정";

        public const string LOG_STRING_FORM_CONTAIN_ID = "{0}(id:{1}) {2}";
        public const string LOG_STRING_SEARCH_BOOK_BY_NAVER = "< 검색어: {0}, 권수: {1} > {2}";
        public const string LOG_STRING_MODIFY_MEMBER_BY_ADMINISTRATOR = "<id:{0} {1}> {2} -> {3}";
        public const string LOG_STRING_MODIFY_MEMBER_PASSWORD_BY_ADMINISTRATOR = "<id:{0} {1}>";
        public const string LOG_STRING_MODIFY_MEMBER_ADDRESS_BY_ADMINISTRATOR = "<id:{0} {1}> {2} -> {3}";
        public const string LOG_STRING_MODIFY_BOOK_NAME_BY_ADMINISTRATOR = "<id:{0} {1}> {2} -> {3}";

        public const string LOG_STRING_MODIFY_MEMBER = "<{0}> {1} -> {2}";
        public const string LOG_STRING_MODIFY_MEMBER_PASSWORD = "<{0}>";
        public const string LOG_STRING_MODIFY_MEMBER_ADDRESS = "<{0}> {1} -> {2}";

        // Member Filed
        public const string FILED_ALL = "*";
        public const string MEMBER_FILED_NAME = "name";
        public const string MEMBER_FILED_ID = "id";
        public const string MEMBER_FILED_PASSWORD = "pw";
        public const string MEMBER_FILED_AGE = "age";
        public const string MEMBER_FILED_BIRTH_DATE = "birthdate";
        public const string MEMBER_FILED_ADDRESS = "address";
        public const string MEMBER_FILED_PHONE_NUMBER = "phonenumber";

        // Book Filed
        public const string BOOK_FILED_ID = "id";
        public const string BOOK_FILED_NAME = "name";
        public const string BOOK_FILED_PUBLISHER = "publisher";
        public const string BOOK_FILED_AUTHOR = "author";
        public const string BOOK_FILED_PRICE = "price";
        public const string BOOK_FILED_QUANTITY = "quantity";
        public const string BOOK_FILED_PUBLICATION_DATE = "pubdate";
        public const string BOOK_FILED_ISBN = "isbn";

        // BorrowedBook Filed
        public const string BORROWED_BOOK_FILED_ID = "id";
        public const string BORROWED_BOOK_FILED_NAME = "name";
        public const string BORROWED_BOOK_FILED_PUBLISHER = "publisher";
        public const string BORROWED_BOOK_FILED_AUTHOR = "author";
        public const string BORROWED_BOOK_FILED_PRICE = "price";
        public const string BORROWED_BOOK_FILED_QUANTITY = "quantity";
        public const string BORROWED_BOOK_FILED_BORROW_DATE = "borrowDate";
        public const string BORROWED_BOOK_FILED_RETURN_DATE = "returnDate";

        // is LoginCompleted
        public const bool IS_LOGIN_SUCCESS = true;
        public const bool IS_LOGIN_FAIL = true;

        // is Console clear
        public const bool IS_NOT_CONSOLE_CLEAR = false;
        public const bool IS_CONSOLE_CLEAR = true;

        // is password
        public const bool IS_NOT_PASSWORD = false;
        public const bool IS_PASSWORD = true;

        // isSearchAndExtra
        public const bool IS_SEARCH_AND_MODIFY = true;
        public const bool IS_SEARCH_AND_BORROW = true;
        public const bool IS_ONLY_SEARCH = false;

        // administrator mode
        public const bool IS_ADMINISTRATOR_MODE = true;
        public const bool IS_TITLE_OPTION = true;

        // cursor pos
        public const int CURSOR_POS_LEFT = 0;
        public const int CURSOR_POS_TOP = 0;
        public const int CURSOR_POS_RIGHT = 98;
        public const int CURSOR_POS_NONE = -1;

        // log length
        public const int LOG_LENGTH_DATE = 25;
        public const int LOG_LENGTH_MEMBER = 23;
        public const int LOG_LENGTH_ACTIVITY = 31;


        //Max length
        public const int MAX_LENGTH_MEMBER_ID = 10;
        public const int MAX_LENGTH_MEMBER_PASSWORD = 10;
        public const int MAX_LENGTH_MEMBER_NAME = 20;
        public const int MAX_LENGTH_MEMBER_PHONE_NUMBER = 11;
        public const int MAX_LENGTH_MEMBER_AGE = 3;
        public const int MAX_LENGTH_MEMBER_ADDRESS = 35;
        public const int MAX_LENGTH_BOOK_ID = 3;
        public const int MAX_LENGTH_BOOK_NAME = 100;
        public const int MAX_LENGTH_BOOK_PUBLISHER = 20;
        public const int MAX_LENGTH_BOOK_AUTHOR = 20;
        public const int MAX_LENGTH_BOOK_PRICE = 7;
        public const int MAX_LENGTH_BOOK_QUANTITY = 2;
        public const int MAX_LENGTH_DATE = 8;
        public const int MAX_LENGTH_BOOK_ISBN = 30;

        // menu cursor pos
        public const int MENU_CURSOR_POS_X = 40;
        public const int MENU_CURSOR_MIN_POS_Y = 12;
        public const int FIRST_MENU_CURSOR_MAX_POS_Y = 13;
        public const int BORROW_MODE_CURSOR_MAX_POS_Y = 13;
        public const int ADMINISTRATOR_MENU_CURSOR_MAX_POS_Y = 17;
        public const int MEMBER_MENU_CURSOR_MAX_POS_Y = 15;

        //message
        public const int WELCOME_MESSAGE_CURSOR_POS_X = 3;
        public const int WELCOME_MESSAGE_CURSOR_POS_Y = 10;
        public const int EXCEPTION_MESSAGE_CURSOR_MAX_POS_X = 79;
        public const int EXCEPTION_MESSAGE_CURSOR_POS_Y = 10;


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
        public const int LOGIN_POS_X = 44;
        public const int LOGIN_ID_POS_Y = 12;
        public const int LOGIN_PASSWORD_POS_Y = 13;

        // yes or no
        public const int INPUT_ESCAPE = -1;
        public const int INPUT_ENTER = 1;

        // search pos
        public const int SEARCH_POS_X = 50;
        public const int SEARCH_SELECT_OPTION_POS_X = 32;
        public const int SEARCH_BY_NAVER_SELECT_OPTION_POS_X = 35;

        //Modify pos
        public const int SELECT_MANAGEMENT_MEMBER_ID_POS_X = 50;
        public const int SELECT_MANAGEMENT_MEMBER_ID_OPTION_POS_X = 35;


        public const int SELECT_MODIFY_BOOK_ID_POS_X = 50;
        public const int SELECT_MODIFY_BOOK_ID_OPTION_POS_X = 35;

        public const int MODIFY_SELECT_OPTION_POS_X = 0;
        public const int MODIFY_BOOK_INPUT_POS_X = 16;
        public const int MODIFY_MEMBER_INPUT_POS_X = 20;

        // borrow pos
        public const int BORROW_BOOK_POS_X = 50;
        public const int BORROW_BOOK_SELECT_OPTION_POS_X = 32;

        // add pos
        public const int ADD_BOOK_SELECT_OPTION_POS_X = 32;
        public const int ADD_BOOK_INPUT_POS_X = 47;

        // signup pos
        public const int SIGNUP_POS_X = 50;

        public enum SignUpPosY : int { NAME = 12, ID, PASSWORD, PASSWORD_CHECK, BIRTH_DATE, ADDRESS, PHONE_NUMBER, SIGN_UP }

        public enum BookSearchPosY : int { ID = 12, NAME, PUBLISHER, AUTHOR, ISBN, PRICE, QUANTITY, SEARCH }

        public enum MemberSearchPosY : int { NAME = 12, ID, BIRTHDATE, ADDRESS, PHONE_NUMBER, SEARCH }

        public enum BookBorrowModePosY : int { IMMEDIATE = 12, SEARCH }

        public enum BookBorrowPosY : int { ID = 12, BORROW }

        public enum BookReturnPosY : int { ID = 12, RETURN }

        public enum AdministratorMenu : int { BOOK_SEARCH = 13, BOOK_ADD, BOOK_MODIFY, MEMBER_MANAGEMENT, BORROW_BOOK_STATUS, SEARCH_BY_NAVER, SHOW_LOG, SAVE_LOG, RESET_LOG}

        public enum MemberMenu : int { BOOK_SEARCH = 13, BOOK_BORROW, BOOK_RETURN, BOOK_CHECK, MODIFY_MEMBER_INFORMATION }

        public enum CheckInsertBorrowedBook : int { NOT_EXIST_BOOK = 1, DUPLICATE_BOOK_ID, SHORTAGE_BOOK_QUANTITY, SUCCESS }

        public enum MemberModifyModePosY : int { NAME = 24, PASSWORD, BIRTH_DATE, ADDRESS, PHONE_NUMBER, WITHDRAWAL }

        public enum BookAddPosY : int { NAME = 12, PUBLISHER, AUTHOR, PRICE, PUBLICATION_DATE, ISBN, QUANTITY, ADD }

        public enum SelectBookIdPosY : int { ID = 12, MODIFY_BOOK }

        public enum BookModifyModePosY : int { IMMEDIATE = 12, SEARCH }
        
        public enum BookModifyPosY : int { NAME = 27, PUBLISHER, AUTHOR, PRICE, QUANTITY, DELETE }

        public enum SelectMemberIdPosY : int { ID = 12, MANAGEMEMT_MEMBER }

        public enum MemberManagementModePosY : int { SEARCH = 12, MODIFY }
    
        public enum CheckBorrowedBookModePosY : int { BOOK_ID = 12, MEMBER_ID }

        public enum NaverBookPosY : int { NAME = 12, DISPLAY, SEARCH }

        public enum AddBookByNaverPosY : int { NUMBER = 13, ADD }
    }
}