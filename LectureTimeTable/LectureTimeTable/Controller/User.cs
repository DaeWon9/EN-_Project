using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureTimeTable.Controller
{
    class User // id : 20003321 , pw:11111111
    {
        private string id;
        private string password;
        public User(string id, string password)
        {
            this.id = id;
            this.password = password;
        }

        public string GetInputData() // id, pw입력받을때 예외처리 필요
        {
            string userInput;
            /*
            ConsoleKeyInfo keyInfo;

            while (true)
            {
                keyInfo = Console.ReadKey();
                userInput = keyInfo.KeyChar.ToString();
                if (keyInfo.Key == ConsoleKey.Enter)
                    break;
            }
            */
            userInput = Console.ReadLine();
            return userInput;
        }

        public bool LoginCheck(User userData, string id, string password)
        {
            if (userData.id == id && userData.password == password)
                return true;
            else
                return false;
        }

    }
}
