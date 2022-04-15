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


        public const int USER_INPUT_OPTOIN_INDEX_DEPARTMENT = 0;
        public const int USER_INPUT_OPTOIN_INDEX_DIVISION = 1;
        public const int USER_INPUT_OPTOIN_INDEX_LECTURE_NAME = 2;
        public const int USER_INPUT_OPTOIN_INDEX_PROFESSOR_NAME = 3;
        public const int USER_INPUT_OPTOIN_INDEX_GRADE = 4;

        public const int DATA_NO = 0;
        public const int DATA_DEPARTMENT = 1;  
        public const int DATA_HAGSU_NUMBER = 2;   
        public const int DATA_CLASS_NUMBER = 3;
        public const int DATA_LECUTRE_NAME = 4;
        public const int DATA_DIVISION = 5;
        public const int DATA_GRADE = 6; // 학년
        public const int DATA_GRADES= 7; // 학점
        public const int DATA_TIME = 8;
        public const int DATA_LECTURE_ROOM = 9;
        public const int DATA_PROFESSOR_NAME = 10;
        public const int DATA_LANGUAGE = 11;

        public const int MAX_GRADES = 24;
    }
}
