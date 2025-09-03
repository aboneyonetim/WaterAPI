using WaterAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Persistence.Contexts
{
    public class WaterAPIDbContext : DbContext
    {
        //public WaterAPIDbContext(DbContext opttions) : base(opttions)
        //{  }
        public WaterAPIDbContext(DbContextOptions<WaterAPIDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; } 
        public DbSet<Customer> Costomers  { get; set; }
    }
}
