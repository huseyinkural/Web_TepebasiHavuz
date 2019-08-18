using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Web_TepebasiHavuz.Models
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> opts) : base(opts) { }

        public DbSet<Users> Users{ get; set; }
        public DbSet<PoolDB> PoolDB { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<OnKayit> OnKayit { get; set; }
    }
}
