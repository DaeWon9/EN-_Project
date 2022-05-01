using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Controller;

namespace Library
{
    class LibraryStarter // 나이계산은 알아서 처리되도록, 도서추가시 id, 회원도 id 입력, 주석쓰기, 버튼들 가운데정렬
    {
        static void Main(string[] args)
        {
            LibraryController libraryController = new LibraryController();
            libraryController.Start();
        }
    }
}
