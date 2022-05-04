using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.View;
using Library.Utility;
using System.Runtime.InteropServices;


namespace Library.Controller // 소멸자로 객체 소멸시켜주기생각, 함수는 최대한 해당 기능만 
{
    class LibraryController
    {

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        public void Start()
        {
            Console.SetWindowSize(Constant.WINDOW_WIDTH, Constant.WINDOW_HEIGHT);
            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);
            
            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, Constant.SC_MINIMIZE, Constant.MF_BYCOMMAND);
                DeleteMenu(sysMenu, Constant.SC_MAXIMIZE, Constant.MF_BYCOMMAND);
                DeleteMenu(sysMenu, Constant.SC_SIZE, Constant.MF_BYCOMMAND);
            }

         
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
