using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test1.EF_Core
{
    public class BooksContext : DbContext
    {
        private const string conn = "server=LAPTOP-NMRO955A; database = WroxBooks;trusted_connection=true";

        public DbSet<Book> Books { get; set; }

        // this constructor is needed to injected string connection using dependency injection 
        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options) { }


        // This constructor is needed if the previous constructor is defined anf if we will use string connection via the defualt constructor(OnConfiguring should be implemented)
        public BooksContext()
        {

        }

        // Create DB passing string connection
        // The method OnConfiguring of DbContext should be overridden (see the implementation)
        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {
            base.OnConfiguring(ob);
            ob.UseSqlServer(conn);
        }



    }
}
