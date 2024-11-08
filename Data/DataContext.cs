using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using secure_online_bookstore.Models;


namespace secure_online_bookstore.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Book> Books => Set<Book>();
        public DbSet<RegisterUser> RegisterUsers => Set<RegisterUser>();
    }
}