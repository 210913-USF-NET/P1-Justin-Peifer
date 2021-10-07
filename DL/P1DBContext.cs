using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DL
{
    public class P1DBContext : DbContext
    {
        public P1DBContext() : base() { }
        public P1DBContext(DbContextOptions<P1DBContext> options) : base(options) { }

        public DbSet<StoreFront> StoreFronts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }


    }
}
