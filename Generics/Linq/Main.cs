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
            //GruopBy();
            //GruopingWithVariables();
            //GruopingAndNestedObjects();
            Totals();

            Console.ReadKey();
        }

        static void Totals()
        {
            var t = from r in Formula1.GetChampions()
                    where r.Country =="UK" || r.Country == "Brazil"
                    select r;

            foreach (var i in t)
            {
                Console.WriteLine($"{ i.FirstName,-5} {i.LastName,-20} {i.Country,20} {i.Wins}");
            }
            Console.WriteLine();

            //var k = from r in t
            //        group r by r.Country into g
            //        let count = g.Count()
            //        let sum = g.Sum(r => r.Wins)
            //        let max = g.Max(r => r.Wins)
            //        select new
            //        {
            //            Country = g.Key,
            //            Count = count,
            //            Sum = sum,
            //            Max = max

            //        };

            var k = from r in t
                    group r by r.Country into g
                    select new
                    {
                        Country = g.Key,
                        Count = g.Count(),
                        Sum = g.Sum(r => r.Wins),
                        Max = g.Max(r => r.Wins)
                    };


            foreach (var i in k)
            {
                Console.WriteLine($"{ i.Country,-15}{i.Count, -15}{i.Sum,-15}{i.Max,-15}");
            }

            Console.WriteLine();
            //-----------
            var q = t
                .GroupBy(g => g.Country)
                .Select(r => new {
                    Country = r.Key,
                    Count = r.Count(),
                    Sum = r.Sum(r1 => r1.Wins),
                    Max = r.Max(r1 => r1.Wins)
                });


            foreach (var i in q)
            {
                Console.WriteLine($"{ i.Country,-15}{i.Count,-15}{i.Sum,-15}{i.Max,-15}");
            }


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

        static void GroupBy()
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


            //var Countries = Formula1.GetChampions()
            //    .GroupBy(r => r.Country)
            //    .OrderByDescending(q => q.Count())
            //    .ThenBy(q => q.Key)
            //    .Where(r => r.Count() > 2)
            //    .Select(q => new
            //    {
            //        Country = q.Key,
            //        Count = q.Count()
            //    });

            //foreach (var i in Countries)
            //{
            //    Console.WriteLine($"{ i.Country,-50} {i.Count}");
            //}
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