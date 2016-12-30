using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DaXia.WebFrameWork
{
    public class CookieHelper
    {
        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        public static void ClearCookie(string cookiename)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        /// <summary>
        /// 获取指定Cookie值
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        /// <returns></returns>
        public static string GetCookieValue(string cookiename)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            string str = string.Empty;
            if (cookie != null)
            {
                str = cookie.Value;
            }
            return str;
        }
        /// <summary>
        /// 添加一个Cookie（24小时过期）
        /// </summary>
        /// <param name="cookiename"></param>
        /// <param name="cookievalue"></param>
        public static void SetCookie(string cookiename, string cookievalue)
        {
            SetCookie(cookiename, cookievalue, DateTime.Now.AddDays(1.0));
        }
        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookiename">cookie名</param>
        /// <param name="cookievalue">cookie值</param>
        /// <param name="expires">过期时间 DateTime</param>
        public static void SetCookie(string cookiename, string cookievalue, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(cookiename)
            {
                Value = cookievalue,
                Expires = expires
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void DeleteCookie(string strName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie != null)
            {
                TimeSpan span = new TimeSpan(-1, 0, 0, 0);
                cookie.Expires = DateTime.Now.Add(span);
                HttpContext.Current.Response.AppendCookie(cookie);
            }
        }

        public static string GetCookie(string strName)
        {
            if ((HttpContext.Current.Request.Cookies != null) && (HttpContext.Current.Request.Cookies[strName] != null))
            {
                return UrlOperate.UrlDecode(HttpContext.Current.Request.Cookies[strName].Value.ToString());
            }
            return "";
        }

        public static string GetCookie(string strName, string key)
        {
            if (((HttpContext.Current.Request.Cookies != null) && (HttpContext.Current.Request.Cookies[strName] != null)) && (HttpContext.Current.Request.Cookies[strName][key] != null))
            {
                return UrlOperate.UrlDecode(HttpContext.Current.Request.Cookies[strName][key].ToString());
            }
            return "";
        }

        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = UrlOperate.UrlEncode(strValue);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = UrlOperate.UrlEncode(strValue);
            cookie.Expires = DateTime.Now.AddMinutes((double)expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        public static void WriteCookie(string strName, string key, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = UrlOperate.UrlEncode(strValue);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        public static void WriteCookie(string strName, string key, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = UrlOperate.UrlEncode(strValue);
            cookie.Expires = DateTime.Now.AddMinutes((double)expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
    }
}
