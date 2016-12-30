using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DaXia.Common
{
    public class FileLogHelper
    {
        private readonly static object lockOjb = new object();
        /// <summary>
        /// 文件名每日自动生成一个新的文件，放到专用目录下
        /// </summary>
        /// <param name="msg"></param>
        public static void LogToCSVFile(string msg)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "/logs/";
            string fileName = "DataBaseLog.csv";
            string fileFullName = filePath + fileName;
            
            if(!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            if (!File.Exists(fileFullName))
            {
                File.Create(fileFullName);
            }

            lock (lockOjb)
            {
                //这里写入到CSV文件，消息需要以逗号隔开。
                FileStream fs = new FileStream(fileFullName, System.IO.FileMode.Append, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                sw.WriteLine(msg);
                sw.Close();
                fs.Close();
            }
        }
    }
}
