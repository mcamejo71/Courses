using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test1.Asynch
{
    class Main3
    {
        static void Main(string[] args)
        {
            //Syncronous();
            //AsyncronousPattern();
            //EvenBasedAsyncPattern();
            //TaskBasedAsyncPattern();
            CallerWithAsync();
            Console.ReadKey();
        }


        static async Task Mainx()
        {
            Console.WriteLine("1");
            await TaskBasedAsyncPattern();
            Console.WriteLine("2");
            Console.ReadKey();
        }


        static void Syncronous()
        {
            var Url = "https://solishealthplans.com";

            using (var client = new System.Net.WebClient())
            {
                var content = client.DownloadString(Url);
                Console.WriteLine(content.Substring(0, 100));
            }
        }

        static void AsyncronousPattern()
        {
            var Url = "https://solishealthplans.com";

            System.Net.WebRequest req = System.Net.WebRequest.Create(Url);

            var res = req.BeginGetResponse(ReadResponse, null);

            void ReadResponse(IAsyncResult ar)
            {
                using (var response = req.EndGetResponse(ar))
                {
                    System.IO.Stream stream = response.GetResponseStream();
                    var reader = new System.IO.StreamReader(stream);

                    var content = reader.ReadToEnd();
                    Console.WriteLine(content.Substring(0, 100));
                }
            }
        }

        static void EvenBasedAsyncPattern()
        {
            var Url = "https://solishealthplans.com";

            using (var client = new System.Net.WebClient())
            {
                client.DownloadStringCompleted += (sender, e) =>
                {
                    Console.WriteLine(e.Result.Substring(0, 100));
                };
                client.DownloadStringAsync(new Uri(Url));
            }
        }

        static async Task TaskBasedAsyncPattern()
        {
            var Url = "https://solishealthplans.com";

            using (var client = new System.Net.WebClient())
            {
                Console.WriteLine("A");
                var content = await client.DownloadStringTaskAsync(Url);
                Console.WriteLine("B");
                Console.WriteLine(content.Substring(0, 100));
            }
        }


        static void TraceThreadAndTask(string info)
        {
            string taskInfo = Task.CurrentId == null ? "No task" : "task " + Task.CurrentId;

            Console.WriteLine($"{info} in thread {Thread.CurrentThread.ManagedThreadId} and {taskInfo}");
        }

        static string Greeting(string name)
        {
            TraceThreadAndTask($"running {nameof(Greeting)}");
            return $"Hello, {name}";
        }




        static Task<string> GreetingAsync(string name) =>
            Task.Run<string>(() =>
            {
                TraceThreadAndTask($"running {nameof(GreetingAsync)}");
                return Greeting(name);
            });

        private async static void CallerWithAsync()
        {
            TraceThreadAndTask($"running {nameof(CallerWithAsync)}");
            string result = await GreetingAsync("juan de los palotes");
            Console.WriteLine(result);
            TraceThreadAndTask($"ended {nameof(CallerWithAsync)}");
        }




    }
}