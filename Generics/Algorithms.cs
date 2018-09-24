using System;
using System.Collections.Generic;
using System.Text;

namespace Generics
{
    public class Algorithms
    {
        public static T2 Accumulate<T1, T2>(IEnumerable<T1> source, Func<T1, T2, T2> action)
        {
            T2 sum = default(T2);
            foreach (T1 item in source)
                sum = action(item, sum);

            return sum;
        }
    }
}