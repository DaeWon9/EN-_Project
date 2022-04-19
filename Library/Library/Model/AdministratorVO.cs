using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    class AdministratorVO
    {
        private static AdministratorVO instance = null;
        private AdministratorVO()
        {
        }

        public static AdministratorVO Get()
        {
            if (instance == null)
                instance = new AdministratorVO();
            return instance;
        }


        private string id;
        private string password;

        public string Id
        {
            get { return id; }
            set { id = value; } 
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
