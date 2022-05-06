using Library.Controller;

namespace Library
{
    class LibraryStarter
    {
        static void Main(string[] args)
        {
            LibraryController libraryController = new LibraryController();
            libraryController.Start();
        }
    }
}
