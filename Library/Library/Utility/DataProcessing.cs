using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Library.Utility
{
    internal class DataProcessing
    {
        public bool IsExceptionCheck(string stringValue, int exceptionType = Constant.EXCEPTION_TYPE_ANY)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9가-힣]*$", RegexOptions.None);
            if (stringValue == null || stringValue.Length < 1)
                return false;

            switch (exceptionType)
            {
                case Constant.EXCEPTION_TYPE_ANY:
                    regex = new Regex(@"^[a-zA-Z0-9가-힣]*$", RegexOptions.None);
                    break;
                case Constant.EXCEPTION_TYPE_NUMBER:
                    regex = new Regex(@"^[0-9]*$", RegexOptions.None);
                    break;
                case Constant.EXCEPTION_TYPE_KOREA:
                    regex = new Regex(@"^[가-힣]*$", RegexOptions.None);
                    break;
                case Constant.EXCEPTION_TYPE_ENGLISH:
                    regex = new Regex(@"^[a-zA-Z]*$", RegexOptions.None);
                    break;
                default:
                    break;
            }

            return regex.IsMatch(stringValue);
        }
    }
}
