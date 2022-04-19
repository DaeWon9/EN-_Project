using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.View;
using Library.Utility;

namespace Library.Controller
{
    class MemberFuntions
    {
        public void MemberMenuSelect(MenuSelection menuSelection, MemberScreen memberScreen, DataProcessing dataProcessing)
        {
            int menuValue;
            menuValue = menuSelection.MemberModeSelect(memberScreen, dataProcessing);
            switch (menuValue)
            {
                case Constant.MODE_MEMBER_LOGIN:
                    break;
                case Constant.MODE_MEMBER_SIGN_UP:
                    break;
                default:
                    break;
            }
        }
    }
}
