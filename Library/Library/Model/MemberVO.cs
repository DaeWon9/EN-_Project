using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    class MemberVO
    {
        private static MemberVO Instance = null;
        private MemberVO()
        {
        }

        public static MemberVO Get()
        {
            if (Instance == null)
                Instance = new MemberVO();
            return Instance; 
        }
    }
}
