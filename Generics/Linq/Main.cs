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
            SelectMany();
            //GruopBy();
            //GruopingWithVariables();
            GruopingAndNestedObjects();

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
            var Countries1 = from r in Formula1.GetChampions()
                             group r by r.Country into g
                             orderby g.Count() descending, g.Key
                             where g.Count() >= 2
                             select new
                             {
                                 Country = g.Key,
                                 Count = g.Count()
                             };


            foreach (var i in Countries1)
            {
                Console.WriteLine($"{ i.Country,-50} {i.Count}");
            }


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
                Console.WriteLine($"{ i.Country,-50} {i.Count}");
            }
        }

        static void GruopingWithVariables()
        {
            var countries = from r in Formula1.GetChampions()
                            group r by r.Country into g
                            let count = g.Count()
                            orderby count descending, g.Key
                            where count >= 2
                            select new
                            {
                                Country = g.Key,
                                Count = count
                            };
            foreach (var i in countries)
            {
                Console.WriteLine($"{ i.Country,-50} {i.Count}");
            }

            var Countries1 = Formula1.GetChampions()
                .GroupBy(r => r.Country)
                .Select(g => new {Group = g, Count = g.Count() })
                .OrderByDescending(q => q.Count)
                .ThenBy(g => g.Group.Key)
                .Where(g => g.Count >= 2)
                .Select(g => new
                {
                    Country = g.Group.Key,
                    Count = g.Count
                });

            foreach (var i in Countries1)
            {
                Console.WriteLine($"{ i.Country,-50} {i.Count}");
            }

        }

        static void GruopingAndNestedObjects()
        {
            var countries = from r in Formula1.GetChampions()
                            group r by r.Country into g
                            let count = g.Count()
                            orderby count descending, g.Key
                            where count >= 2
                            select new
                            {
                                Country = g.Key,
                                Count = count,
                                Racers = from r1 in g
                                         orderby r1.LastName
                                         select new { r1.FirstName, r1.LastName }
                            };


            foreach (var i in countries)
            {
                Console.WriteLine($"{ i.Country,-5} {i.Count}");


                foreach (var j in i.Racers)
                {
                    Console.WriteLine($"{ j.FirstName,-5} {j.LastName}");
                }

            }

            //-------------------

            var countries1 = Formula1.GetChampions()
                .GroupBy(r => r.Country)
                .Select(g => new
                {
                    Group = g,
                    Key = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(g => g.Count)
                .ThenBy(g => g.Key)
                .Where(g => g.Count >= 2)
                .Select(g => new
                {
                    Country = g.Key,
                    Count = g.Count,
                    Racers = g.Group.OrderBy(r => r.LastName)
                    .Select(r => new { r.FirstName, r.LastName })
                });
                            

            foreach (var i in countries1)
            {
                Console.WriteLine($"{ i.Country,-5} {i.Count}");


                foreach (var j in i.Racers)
                {
                    Console.WriteLine($"{ j.FirstName,-5} {j.LastName}");
                }

            }



        }

    }
}