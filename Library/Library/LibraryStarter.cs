using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Controller;

namespace Library
{
    class LibraryStarter
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(Constant.WINDOW_WIDTH, Constant.WINDOW_HEIGHT);

            LibraryController libraryController = new LibraryController();
            libraryController.Start(); 
        }
    }
}
