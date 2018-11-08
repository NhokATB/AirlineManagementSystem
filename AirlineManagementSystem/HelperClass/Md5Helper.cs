using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AirportManagerSystem.HelperClass
{
    class Md5Helper
    {
        public static string GetMd5(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            var buffer = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            return BitConverter.ToString(buffer).Replace("-", "");
        }
    }
}
