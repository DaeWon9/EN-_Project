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
            MemberScreen memberScreen = new MemberScreen();
            AdministratorScreen administratorScreen = new AdministratorScreen();
            TableFuntions tableFuntions = new TableFuntions("Server=localhost;Port=3307;Database=library;Uid=root;Pwd=0000;");
            MenuSelection menuSelection = new MenuSelection();
            Arrow arrow = new Arrow();
            Message message = new Message();
            DataProcessing dataProcessing = new DataProcessing();
            MemberFuntions memberFuntions = new MemberFuntions();
            AdministratorFuntions administratorFuntions = new AdministratorFuntions();
            
            //tableFuntions.BookSelect("*", "book");

            int menuValue;
            menuValue = menuSelection.UserModeSelect(modeScreen, arrow);
            
            switch(menuValue)
            {
                case Constant.MODE_MEMBER:
                    memberFuntions.MemberMenuSelect(menuSelection, memberScreen, arrow);
                    break;
                case Constant.MODE_ADMINISTRATOR:
                    administratorFuntions.Login(administratorScreen, message, dataProcessing, menuSelection, arrow);
                    break;
                default:
                    break;
            }


        }
    }
}
