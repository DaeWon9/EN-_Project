﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Controller;

namespace Library
{
    internal class LibraryStarter
    {
        static void Main(string[] args)
        {
            LibraryController libraryController = new LibraryController();
            libraryController.Start(); 
        }
    }
}
