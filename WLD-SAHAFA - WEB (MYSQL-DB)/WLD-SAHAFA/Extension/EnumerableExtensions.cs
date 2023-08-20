using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WLD_SAHAFA.Extension
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }
    }
}