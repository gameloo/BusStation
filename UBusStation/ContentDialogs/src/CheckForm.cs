using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UBusStation.ContentDialogs.src
{
    public class CheckForm
    {
        public static bool IsPasspordId(string str)
        {
            if (Regex.IsMatch(str, @"^\d{10}$", RegexOptions.IgnoreCase))
                return true;
            else return false;
        }

        public static bool IsMail(string str)
        {
            if (Regex.IsMatch(str, @"^\w+@\w+.\w+$", RegexOptions.IgnoreCase))
                return true;
            else return false;
        }
    }
}
