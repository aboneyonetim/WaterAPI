using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.Repositories;
using WaterAPI.Domain.Entities.Common;
using WaterAPI.Persistence.Contexts;

namespace WaterAPI.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        readonly private WaterAPIDbContext _context;
        public WriteRepository(WaterAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();
        
        public async Task<bool> AddAsync(T model)
        {
             EntityEntry<T> entityEntry =  await  Table.AddAsync(model);  
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> data)
        {
            await Table.AddRangeAsync(data);
            return true;
        }

        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }

        public bool RemoveRange(List<T> data)
        {
            Table.RemoveRange(data);
            return true;
        }

        public async Task <bool> RemoveAsync(string id)
        {
         T model  = await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
         return Remove(model);
        }
        public  bool Update(T model)
        {
           EntityEntry<T> entityEntry=  Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync()
        {
          return await _context.SaveChangesAsync();
        }

    }
}
