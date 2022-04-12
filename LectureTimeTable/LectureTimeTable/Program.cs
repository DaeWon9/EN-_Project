using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LectureTimeTable.View;
using LectureTimeTable.Controller;


namespace LectureTimeTable
{
    class Program
    {
        static void Main(string[] args)
        {
            UI ui = new UI();
            User user = new User(Constant.USER_ID, Constant.USER_PASSWORD);

            ui.DrawFirstScreen();
            Console.WriteLine();
            Console.WriteLine(user.GetInputData());
        }


        public void login(string id, string password)
        {

        }
    }
}
