using System;

namespace Delegate
{
    class Program
    {
        // Multicast Delegate ---------------------------

        static int Sum1(int a, int b)
        {
            var c = a + b;
            Console.WriteLine($"Sum: {c}");
            return c;
        }

        static int Mul1(int a, int b)
        {
            var c = a * b;
            Console.WriteLine($"Mul: {c}");
            return c;
        }

        static void Main(string[] args)
        {
            Func<int, int, int> operations = Sum1;
            operations += Mul1;

            // Way1. Execute all functions. Get the last value.
            //var x = operations(5, 2);
            // Console.WriteLine($"Final: {x}");

            // Way 2. Execute all functions. Get returned value one by one
            var collection = operations.GetInvocationList();

            foreach (var item in collection)
            {
                var x = item.DynamicInvoke(5, 2);
                Console.WriteLine($"Final: {x}");
            }

            Console.ReadKey();

        }
        //-----------------------------------------------

        // Delegate generic -----------------------------
        static int Sum(int a, int b) => a + b;
        static string SumS(Func<int, int, int> action, int v1, int v2) => action(v1, v2).ToString();

        static void Main2(string[] args)
        {
            // First way
            //Func<int, int, int> Suma = Sum;
            //var y = SumS(Suma, 5, 6);
            //-------------------


            // Second way
            var y = SumS(Sum, 5, 6);

            Console.WriteLine($"{y}");

            Console.ReadKey();
        }
        //-----------------------------------------------


        // Delegate -------------------------------------
        delegate double MathOper(double v);
        static double Square(double n) => n * n;

        static void Main1(string[] args)
        {
            // Delegate
            MathOper d = Square;
            var z = d(8);

            Console.WriteLine($"{z}");
            Console.ReadKey();
        }
        //-----------------------------------------------
    }
}
