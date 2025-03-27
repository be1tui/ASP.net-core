using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace LMS_WEBSITE.Utilities
{
    public class Functions
    {
        public static int _UserID = 0;
        public static string _UserName = string.Empty;
        public static string _Email = string.Empty;
        public static string _Message = string.Empty;
        public static bool islogin()
        {
            if ((Functions._UserID <= 0) || (string.IsNullOrEmpty(Functions._UserName) || string.IsNullOrEmpty(Functions._Email)))
            {
                return false;
            }
            return true;
        }
        public static string Md5hash(string Text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
           md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Text));
            byte[] reasult = md5.Hash;
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < reasult.Length; i++)
            {
                str.Append(reasult[i].ToString("x2"));
            }
            return str.ToString();
        }
        public static string Md5Password(string? Text)
        {
            string str = Md5hash(Text);
            for (int i = 0; i < 5; i++)
            {
                str = Md5hash(str+str);
            }
            return str;
        }
    }
}