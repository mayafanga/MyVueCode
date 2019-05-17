using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Movie.Common
{
    public static class Regularhelper
    {
        public static bool IsPhone(this string phone)
        {
            //return System.Text.RegularExpressions.Regex.IsMatch(phone, @"^1(3|4|5|7|8)\d{9}$");
            Regex regex = new Regex(@"^1(3|4|5|7|8)\d{9}$");
            return regex.IsMatch(phone);

        }
    }
}
