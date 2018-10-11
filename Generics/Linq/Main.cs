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
            LeftOuterJoin();
            //InnerJoinWithMethods();
            InnerJoin();
            //SelectMany();
            //GruopBy();
            //GruopingWithVariables();
            //GruopingAndNestedObjects();
            //Totals();

            Console.ReadKey();
        }


        //static void LeftOuterJoinWithMethod()
        //{
        //    var racers = Formula1.GetChampions()
        //                .SelectMany(r => r.Years, (r1, year) =>
        //                new
        //                {
        //                    Year = year,
        //                    Name = $"{r1.FirstName}{r1.LastName}"
        //                });


        //    var teams = Formula1.GetConstructorChampions()
        //                .SelectMany(t => t.Years, (t1, year) =>
        //                new
        //                {
        //                    Year = year,
        //                    Name = $"{t1.Name}"
        //                });

        //    var racersAndteams = racers.GroupJoin(
        //            teams,
        //            r => r.Year,
        //            t => t.Year,
        //            (r, ts) => new
        //            {
        //                r.Year,
        //                Champion = r.Name,
        //                Construtors = ts
        //            })
        //            .SelectMany(
        //            rt => 
                
        //        );



        //}

        static void LeftOuterJoin()
        {
            var racers = from r in Formula1.GetChampions()
                         from y in r.Years
                         select new
                         {
                             Year = y,
                             Name = r.FirstName + " " + r.LastName
                         };


            var teams = from t in Formula1.GetConstructorChampions()
                        from y in t.Years
                        select new
                        {
                            Year = y,
                            Name = t.Name
                        };


            var racersAndTeams = from r in racers
                                 join t in teams on r.Year equals t.Year into rt
                                 from t in rt.DefaultIfEmpty()
                                 orderby r.Year
                                 select new
                                 {
                                     r.Year,
                                     Champion = r.Name,
                                     Construtor = t == null ? "no constructor championship" : t.Name
                                 };


            foreach (var i in racersAndTeams)
            {
                Console.WriteLine($"{ i.Year,-5}{ i.Champion,-15}{i.Construtor}");
            }
            Console.WriteLine();

        }


        static void InnerJoinWithMethods()
        {
            //var racers = Formula1.GetChampions()
            //            .SelectMany(r => r.Years, (r1, year) =>
            //            new 
            //            {
            //                Year = year,
            //                Name = $"{r1.FirstName}{r1.LastName}"
            //            });


            //var teams = Formula1.GetConstructorChampions()
            //            .SelectMany(t => t.Years, (t1, year) =>
            //            new
            //            {
            //                Year = year,
            //                Name = $"{t1.Name}"
            //            });

            //var racersAndTeams = (racers.Join(
            //        teams, 
            //        r => r.Year, 
            //        t => t.Year, 
            //        (r, t) => new
            //        {
            //            r.Year,
            //            Champion = r.Name,
            //            Construtor = t.Name
            //        })).Take(10);


            //---- In just one query-----

            var racersAndTeams1 = (Formula1.GetChampions()
                       .SelectMany(r => r.Years, (r1, year) =>
                       new
                       {
                           Year = year,
                           Name = $"{r1.FirstName}{r1.LastName}"
                       })
                       .Join(
                        Formula1.GetConstructorChampions()
                        .SelectMany(t => t.Years, (t1, year) =>
                        new
                        {
                            Year = year,
                            Name = $"{t1.Name}"
                        }),
                        r => r.Year,
                        t => t.Year,
                        (r, t) => new
                        {
                            r.Year,
                            Champion = r.Name,
                            Construtor = t.Name
                        })).Take(10);


            foreach (var i in racersAndTeams1)
            {
                Console.WriteLine($"{ i.Year,-5}{ i.Champion,-15}{i.Construtor}");
            }
            Console.WriteLine();

        }

        static void InnerJoin()
        {
            //var racers = from r in Formula1.GetChampions()
            //             from y in r.Years
            //             select new
            //             {
            //                 Year = y,
            //                 Name =  r.FirstName + " " + r.LastName
            //             };

            //var teams = from t in Formula1.GetConstructorChampions()
            //            from y in t.Years
            //            select new
            //            {
            //                Year = y,
            //                Name = t.Name
            //            };


            //var RacersAndTeams = (from r in racers
            //                     join t in teams on r.Year equals t.Year
            //                     orderby r.Year
            //                     select new 
            //                     {
            //                         r.Year,
            //                         Champion = r.Name,
            //                         Construtor = t.Name
            //                     }).Take(10);


            //foreach (var i in RacersAndTeams)
            //{
            //    Console.WriteLine($"{ i.Year,-5}{ i.Champion,-15}{i.Construtor}");
            //}
            //Console.WriteLine();


            //---- In just one query-----
            var RacersAndTeams1 = from r in

                                       from r1 in Formula1.GetChampions()
                                       from yr in r1.Years
                                       select new
                                       {
                                           Year = yr,
                                           Name = r1.FirstName + " " + r1.LastName
                                       }

                                   join t in

                                   from t1 in Formula1.GetConstructorChampions()
                                   from yt in t1.Years
                                   select new
                                   {
                                       Year = yt,
                                       Name = t1.Name
                                   }

                                   on r.Year equals t.Year
                                   orderby r.Year
                                   select new
                                   {
                                       Year = r.Year,
                                       Racer = r.Name,
                                       Team = t.Name
                                   };



            foreach (var i in RacersAndTeams1)
            {
                Console.WriteLine($"{ i.Year,-5}{ i.Racer,-15}{i.Team}");
            }
            Console.WriteLine();


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