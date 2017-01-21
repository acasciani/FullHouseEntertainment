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

        public static string TakeFirstNCharacters(this string raw, int N, bool AddEllipsis = true)
        {
            return (raw.Length < N) ? raw : raw.Substring(0, N) + (AddEllipsis ? "..." : "");
        }
    }
}