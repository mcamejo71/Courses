using System;
using System.Collections.Generic;
using System.Text;

namespace Test1
{
    class LambdaExpressions
    {
        static void Main(string[] args)
        {
            int operations(int a, int b)  => a + b;

            var x = operations(1, 2);

            Console.WriteLine($"Sum: {x}");
        }


        static void Main1(string[] args)
        {
            Func<int, int, int> operations = (a, b) => a + b;

            var x = operations(1, 2);

            Console.WriteLine($"Sum: {x}");
        }
    }
}