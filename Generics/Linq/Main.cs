using System;
using System.Collections.Generic;
using System.Text;
using DataLib;
using System.Linq;

namespace Test1.Linq
{
    class Main1
    {

        static void Main(string[] args)
        {
            //SelectMany();
            GruopBy();
            Console.ReadKey();
        }


        static void SelectMany()
        {
            var ferrariDrivers = from r in Formula1.GetChampions()
                                  from c in r.Cars
                                  where c == "Ferrari"
                                  orderby r.LastName
                                  select r;

            foreach (var i in ferrariDrivers)
            {
                Console.WriteLine(i.FirstName + " " + i.LastName);
            }

            Console.WriteLine(" ");

            var ferrariDrivers1 = Formula1.GetChampions()
                    .SelectMany(r => r.Cars, (r, c) => new { Racer = r, Car = c })
                    .Where(r => r.Car == "Ferrari")
                    .OrderBy(r => r.Racer.LastName)
                    .Select(r => r);

            foreach (var i in ferrariDrivers1)
            {
                Console.WriteLine(i.Racer.FirstName + i.Racer.LastName);
            }
        }

        static void GruopBy()
        {
            var Countries = Formula1.GetChampions()
                .GroupBy(r => r.Country)
                .OrderByDescending(q => q.Count())
                .ThenBy(q => q.Key)
                .Where(r => r.Count() > 2)
                .Select(q => new
                {
                    Country = q.Key,
                    Count = q.Count()
                });

            foreach (var i in Countries)
            {
                Console.WriteLine($"{ i.Country, -50} {i.Count}");
            }
        }
    }
}