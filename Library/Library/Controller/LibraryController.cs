using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.View;


namespace Library.Controller
{
    class LibraryController
    {
        public void Start()
        {
            Drawing drawer = new Drawing();
            TableFuntions tableFuntions = new TableFuntions("Server=localhost;Port=3307;Database=library;Uid=root;Pwd=0000;");
            //tableFuntions.BookSelect("*", "book");

            drawer.LibraryTitleScreen();
            drawer.MainMenuScreen();
        }
    }
}
