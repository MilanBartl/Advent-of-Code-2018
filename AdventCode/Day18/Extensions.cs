using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day18
{
    public static class Extensions
    {
        public static bool Contains<T>(this List<T> data, List<T> otherData)
        {
            var dataLength = data.Count();
            var otherDataLength = otherData.Count();

            if (dataLength < otherDataLength)
                return false;

            return Enumerable.Range(0, dataLength - otherDataLength + 1)
                .Any(skip => data.Skip(skip).Take(otherDataLength).SequenceEqual(otherData));
        }
    }
}
