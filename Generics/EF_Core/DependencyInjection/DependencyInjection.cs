using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test1.EF_Core.DependencyInjection;

namespace Test1.EF_Core
{
    class Main5
    {
        static async Task Main()
        {
            var p = new Main5();
            p.InitializeServices();
            p.ConfigureLogging();
            var service = p.Container.GetService<BooksService>();
            //await service.AddBooksAsync();
            await service.ReadBooksAsync();
            p.Container.Dispose();

            Console.ReadKey();
        }


        //static void Main(string[] args)
        //{

        //}

        private void InitializeServices()
        {
            const string ConnectionString = "server=LAPTOP-NMRO955A; database = WroxBooks;trusted_connection=true";
            var services = new ServiceCollection();

            services.AddTransient<BooksService>()
                .AddEntityFrameworkSqlServer()
                .AddDbContext<BooksContext>(options => options.UseSqlServer(ConnectionString));

            services.AddLogging();

            Container = services.BuildServiceProvider();
        }
        public ServiceProvider Container { get; private set; }

        private void ConfigureLogging()
        {
            ILoggerFactory loggerFactory = Container.GetService<ILoggerFactory>();
            loggerFactory.AddConsole(LogLevel.Information);
        }

    }
}