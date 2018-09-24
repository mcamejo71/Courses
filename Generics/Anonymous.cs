using System;
using System.Collections.Generic;
using System.Text;

namespace Anonymous
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int, int, int> operations = delegate (int a, int b)
            {
                var c = a + b;
                Console.WriteLine($"Sum: {c}");
                return c;
            };


            operations += delegate (int a, int b)
            {
                var c = a * b;
                Console.WriteLine($"Mul: {c}");
                return c;
            };

            var x = operations(5,2);

            Console.WriteLine($"Final: {x}");
            Console.ReadKey();
        }
    }
}