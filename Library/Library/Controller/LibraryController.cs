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
            Console.SetWindowSize(Constant.WINDOW_WIDTH, Constant.WINDOW_HEIGHT); // 기능 완성 후에 각 UI별로 크기조정하기

            BothScreen bothScreen = new BothScreen();
            MemberScreen memberScreen = new MemberScreen();
            AdministratorScreen administratorScreen = new AdministratorScreen();
            MenuSelection menuSelection = new MenuSelection();
            Message message = new Message();
            DataProcessing dataProcessing = new DataProcessing();
            MemberFuntions memberFuntions = new MemberFuntions();
            AdministratorFuntions administratorFuntions = new AdministratorFuntions();
            
            int menuValue;
            bool isExit = false;
            while (!isExit)
            {
                menuValue = menuSelection.UserModeSelect(bothScreen, dataProcessing);

                switch (menuValue)
                {
                    case Constant.MODE_MEMBER:
                        memberFuntions.LoginOrSignUpSelect(menuSelection, message, memberScreen, bothScreen, dataProcessing);
                        break;
                    case Constant.MODE_ADMINISTRATOR:
                        administratorFuntions.Login(administratorScreen, bothScreen, message, dataProcessing, menuSelection);
                        break;
                    case Constant.INPUT_ESCAPE_IN_ARROW_KEY:
                        isExit = true;
                        break;
                    default:
                        break;
                }
            }


        }
    }
}
