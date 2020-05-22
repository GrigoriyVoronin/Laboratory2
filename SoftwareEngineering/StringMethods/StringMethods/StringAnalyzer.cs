using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMethods
{
    public class StringAnalyzer
    {
        public static bool IsPalindrome(string text)
        {
            text = string.Join("", text.Split(new[] {' ', '.', ',', '!', '?', ';', '(', ')', '\t', '\r', '\n'},
                StringSplitOptions.RemoveEmptyEntries));

            var len = text.Length;
            var d = len % 2 == 0 ? 0 : 1;
            len /= 2;
            for (var i = 0; i < len; i++)
                if (text[len + i + d] != text[len - 1 - i])
                    return false;

            return true;
        }
    }
}
