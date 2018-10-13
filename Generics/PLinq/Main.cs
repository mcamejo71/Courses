using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.PLinq
{
    public class Main2
    {
        static IEnumerable<int> SampleData()
        {
            const int arraySize = 50000000;

            var r = new Random();

            return Enumerable.Range(0, arraySize).Select(x => r.Next(140)).ToList(); 
        }

        static void Main(string[] args)
        {
            var x = SampleData();


            //var res = (from y in x
            //           where Math.Log(y) < 4
            //           select x).Sum();

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }


    }
}
