using WaterAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Domain.Entities.Common;

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
        public DbSet<Customer> Customers  { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)//savechangesasyn ı oveerride ettik ne zaman savechangesasyn çağrılırsa bu kodlar çalışsın diye
        {                                                                                        //
            //ChangeTracker: Entytyler üzerinden veritabanına yapılan değişiklikleri veya yeni eklenen verileri izler.
            //Update operasyonlarında Track edilen verileri yakalayıp  elde etmemizi sağlar.
            ChangeTracker.Entries<BaseEntity>().ToList().ForEach(entry =>
            {
                //EntityState.Added: Yeni eklenen verileri temsil eder.
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedDate = DateTime.UtcNow;
                }
            });
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
