using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DaXia.WebFrameWork
{
    public class WechatHelper
    {
        /// <summary>
        /// 记录bug，以便调试
        /// </summary>
        /// <returns></returns>
        public static bool Writebug(string str)
        {
            try
            {
                str = DateTime.Now + "::" + str;
                FileStream FileStream = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + "/logs/wxbugLog.txt", FileMode.Append);
                StreamWriter StreamWriter = new StreamWriter(FileStream);
                //开始写入
                StreamWriter.WriteLine(str);
                //清空缓冲区
                StreamWriter.Flush();
                //关闭流
                StreamWriter.Close();
                StreamWriter.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// unix时间转换为datetime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        private static DateTime UnixTimeToTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// datetime转换为unixtime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// 关注处理
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <param name="ToUserName"></param>
        /// <returns></returns>
        public static string GetSubscribe(string FromUserName, string ToUserName)
        {
            //用户添加到数据库


            //回复关注消息
            string reply_text = "欢迎各位大BOSS,加入我们这个大家庭，在以后的日子里我们将共同成长，共同努力。让大家摇出美丽，摇出健康，摇出欢乐。";
            string resXml = "";

            resXml = "<xml><ToUserName><![CDATA[" + FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + ToUserName + "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + reply_text + "]]></Content><FuncFlag>0</FuncFlag></xml>";

            return resXml;
        }

    }
}
