using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.View;
using Library.Utility;
using System.Runtime.InteropServices;


namespace Library.Controller
{
    class LibraryController
    {

        public void Start()
        {
            Console.SetWindowSize(Constant.WINDOW_WIDTH, Constant.WINDOW_HEIGHT);
         
            BothScreen bothScreen = new BothScreen();
            MemberScreen memberScreen = new MemberScreen();
            AdministratorScreen administratorScreen = new AdministratorScreen();
            MenuSelection menuSelection = new MenuSelection();
            Member memberFuntions = new Member();
            Administrator administratorFuntions = new Administrator();

            int menuValue;
            bool isExit = false;
            while (!isExit)
            {
                menuValue = menuSelection.GetUserMode(bothScreen);

                switch (menuValue)
                {
                    case Constant.MODE_MEMBER:
                        memberFuntions.SelectLoginOrSignUp(memberScreen);
                        break;
                    case Constant.MODE_ADMINISTRATOR:
                        administratorFuntions.Login(administratorScreen);
                        break;
                    case Constant.INPUT_ESCAPE_IN_ARROW_KEY:
                        isExit = DataProcessing.GetDataProcessing().IsExit(bothScreen);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
