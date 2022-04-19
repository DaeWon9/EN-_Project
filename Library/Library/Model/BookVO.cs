using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    class BookVO
    {
        private static BookVO Instance = null;
        private BookVO() { }

        public static BookVO Get()
        {
            if (Instance == null)
                Instance = new BookVO();
            return Instance;
        }
    }
}
