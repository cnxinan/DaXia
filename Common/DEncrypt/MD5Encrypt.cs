using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DaXia.Common.DEncrypt
{
    public class MD5Encrypt
    {
        public static string GetMD5Hash(String input)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(input, "MD5");
        } 
    }       
}
