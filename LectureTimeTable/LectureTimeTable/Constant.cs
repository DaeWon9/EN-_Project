using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureTimeTable
{
    class Constant
    {
        // user info
        public const string USER_ID = "20003321";
        public const string USER_PASSWORD = "11111111";

        //exception type
        public const int EXCEPTION_TYPE_ANY = 1;
        public const int EXCEPTION_TYPE_NUMBER = 2;
        public const int EXCEPTION_TYPE_KOREA = 3;
        public const int EXCEPTION_TYPE_ENGLISH = 4;


        //timetable cursor pos
        public const int CURSOR_X_POS_TIME_TABLE_MON = 16;
        public const int CURSOR_X_POS_TIME_TABLE_TUE = 40;
        public const int CURSOR_X_POS_TIME_TABLE_WED = 64;
        public const int CURSOR_X_POS_TIME_TABLE_THR = 88;
        public const int CURSOR_X_POS_TIME_TABLE_FRI = 112;
        public const int CURSOR_Y_POS_TIME_TABLE = 5;
        // cursor pos
        public const int CURSOR_X_POS_ID = 56;
        public const int CURSOR_Y_POS_ID = 3;
        public const int CURSOR_X_POS_PW = 56;
        public const int CURSOR_Y_POS_PW = 4;

        public const int CURSOR_X_POS_SEARCH = 15;
        public const int CURSOR_Y_POS_SEARCH = 1;
        // error number
        public const int ERROR_NUMBER = -1;
        public const int ESC_NUMBER = -1;

        // menu number
        public const int MENU_NUMBER_SEARCH_LECTURE_TIME = 1;
        public const int MENU_NUMBER_BASKET = 2;
        public const int MENU_NUMBER_APPLY = 3;
        public const int MENU_NUMBER_SEARCH_APPLY = 4;
        public const int MENU_NUMBER_BACK = 5;

        //attention&applying menu number
        public const int MENU_NUMBER_SEARCH = 1;
        public const int MENU_NUMBER_HISTORY = 2;
        public const int MENU_NUMBER_TIME_TABLE = 3;
        public const int MENU_NUMBER_REMOVE = 4;

        //contents number
        public const int CONTENT_NUMBER_DEPARTMENT = 1;
        public const int CONTENT_NUMBER_HAGSU_NUMBER = 2;
        public const int CONTENT_NUMBER_LECTURE_NAME = 3;
        public const int CONTENT_NUMBER_PROFESSOR_NAME = 4;
        public const int CONTENT_NUMBER_GRADE = 5;
        public const int CONTENT_NUMBER_ATTENTION = 6;
        public const int CONTENT_NUMBER_BACK = 7;

        //
        public const int USER_INPUT_OPTOIN_INDEX_DEPARTMENT = 0;
        public const int USER_INPUT_OPTOIN_INDEX_DIVISION = 1;
        public const int USER_INPUT_OPTION_INDEX_HAGU_CLASS_NUMBER = 1;
        public const int USER_INPUT_OPTOIN_INDEX_LECTURE_NAME = 2;
        public const int USER_INPUT_OPTOIN_INDEX_PROFESSOR_NAME = 3;
        public const int USER_INPUT_OPTOIN_INDEX_GRADE = 4;

        //
        public const int DATA_NO = 0;
        public const int DATA_DEPARTMENT = 1;  
        public const int DATA_HAGSU_NUMBER = 2;   
        public const int DATA_CLASS_NUMBER = 3;
        public const int DATA_LECTURE_NAME = 4;
        public const int DATA_DIVISION = 5;
        public const int DATA_GRADE = 6; // 학년
        public const int DATA_GRADES= 7; // 학점
        public const int DATA_TIME = 8;
        public const int DATA_LECTURE_ROOM = 9;
        public const int DATA_PROFESSOR_NAME = 10;
        public const int DATA_LANGUAGE = 11;

        //
        public const int MAX_GRADES = 24;
        public const int MAX_APPLYING_GRADES = 21;

        public const int EXCEL_POS_X_MON = 15;
        public const int EXCEL_POS_X_TUE = 16;
        public const int EXCEL_POS_X_WED = 17;
        public const int EXCEL_POS_X_THR = 18;
        public const int EXCEL_POS_X_FRI = 19;
        public const int EXCEL_POS_X_NOT = 15;

    }
}
