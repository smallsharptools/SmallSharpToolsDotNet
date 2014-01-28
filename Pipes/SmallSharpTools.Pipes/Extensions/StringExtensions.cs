using System;

namespace SmallSharpTools.Pipes.Extensions
{

    public static class StringExtensions
    {

        public static bool Contains(this String str1, String str2, StringComparison comparison)
        {
            return str1.IndexOf(str2, comparison) >= 0;
        }

    }

}
