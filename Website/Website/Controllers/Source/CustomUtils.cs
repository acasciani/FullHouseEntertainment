using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website
{
    public static class CustomUtils
    {
        public static string GetFirstNameOnly(this string fullName)
        {
            string[] parts = fullName.Split(' ');
            if (parts.Count() == 0)
            {
                return fullName.Trim();
            }
            else
            {
                return parts[0].Trim();
            }
        }
    }
}