using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kinotiki.Web.Helpers
{
    public class AuthHelper
    {
        public static string EncodePassword(string pass)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(pass);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string DecodePassword(string base64)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}