using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Configuration;
using Tunynet;
using Tunynet.Utilities;
using DaXia.Common;
using DaXia.BLL;
using Newtonsoft.Json;

namespace DaXia.WebFrameWork
{
    public class Utility
    {       
        #region 分页变量
        //默认分页实体，给展示页面用
        public readonly static Pager pagedefault = new Pager() { RecordAllCount = 1, PageIndex = 1, PageAllCount = 1 };

        public readonly static int pageIndex = 1;
        public readonly static long itemsPrePage = 20;
        #endregion

        #region 加密与解密
        /// <summary>
        /// 加密AdiminCookie
        /// </summary>
        /// <param name="timeliness">加密有效期</param>
        /// <param name="userId">要加密的用户Id</param>
        /// <returns>加密令牌</returns>
        public static string EncryptTokenForAdminCookie(string encryptString)
        {
            string key = System.Configuration.ConfigurationManager.AppSettings["TokenKeyForAdminLogin"];
            string iv = System.Configuration.ConfigurationManager.AppSettings["TokenIvForAdminLogin"];
            return EncryptTokenForCookie(encryptString, key, iv);
        }

        /// <summary>
        /// 解密AdiminCookie
        /// </summary>
        /// <param name="token">要解密的令牌</param>
        /// <param name="isTimeout">输出参数：令牌是否过期</param>
        /// <returns>解密后的用户Id</returns>
        public static string DecryptTokenForAdminCookie(string token)
        {
            string key = System.Configuration.ConfigurationManager.AppSettings["TokenKeyForAdminLogin"];
            string iv = System.Configuration.ConfigurationManager.AppSettings["TokenIvForAdminLogin"];
            return DecryptTokenForCookie(token, key, iv);
        }

        /// <summary>
        /// 加密的操作类
        /// </summary>
        /// <param name="timeliness">时限</param>
        /// <param name="encryptString">encryptString</param>
        /// <param name="key">key</param>
        /// <param name="iv">向量</param>
        /// <returns></returns>
        private static string EncryptTokenForCookie(string encryptString, string key, string iv)
        {
            string tonkenStr = encryptString;
            return EncryptionUtility.SymmetricEncrypt(SymmetricEncryptType.DES, tonkenStr, iv, key);
        }

        /// <summary>
        /// 解密操作类
        /// </summary>
        /// <param name="token">网络令牌</param>
        /// <param name="isTimeout">是否失效</param>
        /// <param name="key">key</param>
        /// <param name="iv">向量</param>
        /// <returns></returns>
        private static string DecryptTokenForCookie(string token, string key, string iv)
        {
            string encryptString = string.Empty;
            try
            {
                token = token.Replace(" ", "+");
                encryptString = EncryptionUtility.SymmetricDncrypt(SymmetricEncryptType.DES, token, iv, key);
            }
            catch (Exception ex)
            {
                throw new ExceptionFacade("解密操作的时候发生错误", ex);
            }
            return encryptString;
        }

        /// <summary>
        /// DES加密类
        /// </summary>
        /// <param name="targetStr">加密字符串</param>
        /// <param name="key">密钥盐</param>
        /// <returns></returns>
        public static string EncryptDES(string targetStr,string key)
        {
            return Common.DEncrypt.DESEncrypt.Encrypt(targetStr, key);
        }

        /// <summary>
        /// DES解密类
        /// </summary>
        /// <param name="targetStr"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DecryptDES(string targetStr, string key)
        {
            return Common.DEncrypt.DESEncrypt.Decrypt(targetStr, key);
        }

        #endregion

        #region 安全

        /// <summary>
        /// 获取密钥盐
        /// </summary>
        /// <returns></returns>
        public static string GetSalt()
        {
            int saltInt = Assistant.GenerateSixRandom();

            return saltInt.ToString();
        }

        /// <summary>
        /// 是不是合法的请求
        /// </summary>
        /// <remarks>
        /// 用于防盗链的检测、防洪攻击
        /// </remarks>
        /// <returns></returns>
        public static bool IsAllowableReferrer(HttpRequestBase httpRequest)
        {
            if (httpRequest == null || httpRequest.UrlReferrer == null)
                return false;
            string[] domainRules = { };

            string urlReferrerDomain = WebUtility.GetServerDomain(httpRequest.UrlReferrer, domainRules);
            string urlDomain = WebUtility.GetServerDomain(httpRequest.Url, domainRules);

            return urlReferrerDomain.Equals(urlDomain, StringComparison.CurrentCultureIgnoreCase);
        }

        #endregion

        #region 日期、数字、金额等格式化

        /// <summary>
        /// 格式化系统内时间,默认格式2015:06:09 12:23:43
        /// </summary>
        /// <param name="targetDT"></param>
        /// <returns></returns>
        public static string DTDefaultFormat(DateTime targetDT)
        {
            return targetDT.ToString("yyyy:MM:dd HH:mm:ss");
        }

        /// <summary>
        /// 利用Int.TryParse(),出错则返回-1
        /// </summary>
        /// <param name="targetStr"></param>
        /// <returns></returns>
        public static int StrToInt(string targetStr)
        {
            int result;

            if (!int.TryParse(targetStr, out result))
            {
                result = -1;
            }

            return result;
        }

        /// <summary>
        /// 金钱格式化
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static string MoneyDefaultFormat(decimal money)
        {
            return string.Format("{0:0}", money);
        }
        
        #endregion

        #region 枚举相关

        /// <summary>
        /// 根据枚举类型获取枚举描述
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription<TEnum>(TEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            var attribute = fi.GetCustomAttributes(
                  typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false)
                   .Cast<System.ComponentModel.DataAnnotations.DisplayAttribute>()
                   .FirstOrDefault();
            if (attribute != null)
                return attribute.Name;
            return value.ToString();
        }        

        #endregion
                
        #region 读取配置文件

        public static string GetConfigValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static string GetXMLValue(string key)
        {
            return string.Empty;
        }

        #endregion

        #region Json格式化

        public static string ObjectToJson(object result)
        {
            return JsonConvert.SerializeObject(result);
        }

        public static T JsonToObject<T>(string jsonString)
        {
            return (T)JsonConvert.DeserializeObject(jsonString, typeof(T));
        }
        
        #endregion

        #region 路径相关

        #region 获得当前绝对路径
        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (strPath.ToLower().StartsWith("http://"))
            {
                return strPath;
            }
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }
        #endregion

        #endregion

        #region 随机数
        /// <summary>
        /// 使用.NET自带Random生成8位
        /// </summary>
        /// <returns></returns>
        public static Int32 GenerateEightRandom()
        {
            Random ran = new Random();
            int seed = ran.Next(1, 9999);
            Random ran1 = new Random(seed);
            return ran1.Next(10000000, 99999999);
        }

        public static Int32 GenerateSixRandom()
        {
            Random ran = new Random();
            int seed = ran.Next(1, 9999);
            Random ran1 = new Random(seed);
            return ran1.Next(100000, 999999);
        }
        #endregion
        
        #region 数字 0-9 转 零-九

        public static string ConvertLowwerNumberToUpper(int number)
        {
            string result = string.Empty;

            string numStr = number.ToString();
            for (int i = 0; i < numStr.Length; i++)
            {
                string nowChar = numStr.Substring(i,1);
                switch (nowChar)
                {
                    case "0":
                        result += "零";
                        break;
                    case "1":
                        result += "一";
                        break;
                    case "2":
                        result += "二";
                        break;
                    case "3":
                        result += "三";
                        break;
                    case "4":
                        result += "四";
                        break;
                    case "5":
                        result += "五";
                        break;
                    case "6":
                        result += "六";
                        break;
                    case "7":
                        result += "七";
                        break;
                    case "8":
                        result += "八";
                        break;
                    case "9":
                        result += "九";
                        break;
                }
            }

            return result;
        }

        #endregion

        #region 获取订单号
        /// <summary>
        /// 获取随机订单号
        /// </summary>
        /// <returns></returns>
        public static string GetRandomOrderNo()
        {
            return DateTime.Now.ToString("yyyyMMdd") + GenerateSixRandom().ToString();
        }

        //public string 

        //public 

        #endregion
    }
}
