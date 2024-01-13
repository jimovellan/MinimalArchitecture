using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Common.Extensions
{
    public static class StringExtensions
    {
        public static string CleanBreakLines(this string input) 
        {
            return input.Replace("\n", "").Replace("\r", "");
        }
    }
}
