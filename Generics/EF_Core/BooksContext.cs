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

        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {
            base.OnConfiguring(ob);
            ob.UseSqlServer(conn);
        }

    }
}
