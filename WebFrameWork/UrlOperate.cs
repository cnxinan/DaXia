using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DaXia.WebFrameWork
{
    public class UrlOperate
    {
        public static string UrlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return HttpContext.Current.Server.UrlDecode(str);
        }

        public static string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            str = str.Replace("'", "");
            return HttpContext.Current.Server.UrlEncode(str);
        }
    }
}
