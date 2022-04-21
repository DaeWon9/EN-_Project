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
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;//resize

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        public void Start()
        {
            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);//resize
            }

            Console.SetWindowSize(Constant.WINDOW_WIDTH, Constant.WINDOW_HEIGHT); // 기능 완성 후에 각 UI별로 크기조정하기

            ModeScreen modeScreen = new ModeScreen();
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
                menuValue = menuSelection.UserModeSelect(modeScreen, dataProcessing);

                switch (menuValue)
                {
                    case Constant.MODE_MEMBER:
                        memberFuntions.LoginOrSignUpSelect(menuSelection, message, memberScreen, dataProcessing);
                        break;
                    case Constant.MODE_ADMINISTRATOR:
                        administratorFuntions.Login(administratorScreen, message, dataProcessing, menuSelection);
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
