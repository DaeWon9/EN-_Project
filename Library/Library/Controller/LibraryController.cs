using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.View;
using Library.Utility;


namespace Library.Controller
{
    class LibraryController
    {
        public void Start()
        {
            ModeScreen modeScreen = new ModeScreen();
            LoginScreen loginScreen = new LoginScreen();
            TableFuntions tableFuntions = new TableFuntions("Server=localhost;Port=3307;Database=library;Uid=root;Pwd=0000;");
            MenuSelection menuSelection = new MenuSelection();
            Arrow arrow = new Arrow();
            Message message = new Message();
            ScreenProcessing screenProcessing = new ScreenProcessing();
            
            //tableFuntions.BookSelect("*", "book");

            int menuValue;
            menuValue = menuSelection.ModeSelect(modeScreen, arrow);
            
            switch(menuValue)
            {
                case Constant.MODE_MEMBER:
                    loginScreen.MemberLoginScreenDraw(true);
                    Console.ReadKey();
                    break;
                case Constant.MODE_ADMINISTRATOR:
                    loginScreen.AdimistratorLoginScreenDraw(true);
                    Console.ReadKey();
                    break;
                default:
                    break;
            }


        }
    }
}
