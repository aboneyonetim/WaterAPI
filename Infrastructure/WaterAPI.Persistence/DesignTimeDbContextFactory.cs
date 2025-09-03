using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Persistence.Contexts;

namespace WaterAPI.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WaterAPIDbContext>
    {
        public WaterAPIDbContext CreateDbContext(string[] args)
        {
           

            DbContextOptionsBuilder<WaterAPIDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
            //return new WaterAPIDbContext(dbContextOptionsBuilder.Options);
            return new (dbContextOptionsBuilder.Options);
        }
    }
}
