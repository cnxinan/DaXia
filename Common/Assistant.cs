using System;
using System.Configuration;
using System.Text;
using System.Data;

namespace DaXia.Common
{
	/// <summary>
	/// Assistant 的摘要说明。
	/// </summary>
	public sealed class Assistant
    {

        #region 获取GUID和随机数字字符串

        #region 从字符串里随机得到，规定个数的字符串.
        /// <summary>
        /// 从字符串里随机得到，规定个数的字符串.
		/// </summary>
		/// <param name="allChar"></param>
		/// <param name="CodeCount"></param>
		/// <returns></returns>
        public static string GetRandomCode(string allChar, int CodeCount)
        {
            //string allChar = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,i,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z"; 
            string[] allCharArray = allChar.Split(',');
            string RandomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < CodeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(temp * i * ((int)DateTime.Now.Ticks));
                }

                int t = rand.Next(allCharArray.Length - 1);

                while (temp == t)
                {
                    t = rand.Next(allCharArray.Length - 1);
                }

                temp = t;
                RandomCode += allCharArray[t];
            }
            return RandomCode;
        }
		#endregion

        #region 根据GUID生成16位字符串唯一序列
        public static string GenerateStringID()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
        #endregion

        #region 根据GUID生成19位数字唯一序列
        public static long GenerateIntID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }
        #endregion

        #region 创建订单编号
        public static string GetOrderID()
        {
            string orderID = DateTime.Now.ToString("yyyyMMddHHmmss");
            string charStr = "1,2,3,4,5,6,7,8,9,0";
            return orderID + GetRandomCode(charStr, 6);
        }
        #endregion

        #region 使用.NET自带Random生成6位随机数
        public static Int32 GenerateSixRandom()
        {
            Random ran = new Random();
            int seed = ran.Next(1, 9999);
            Random ran1 = new Random(seed);
            return ran1.Next(100000, 999999);
        }
        #endregion

        #endregion
    }
}
