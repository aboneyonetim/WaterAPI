using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Domain.Entities.Common;

namespace WaterAPI.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table => throw new NotImplementedException();

       Task<bool> AddAsync(T model);// Tek bir veri eklemek için
       Task<bool> AddRangeAsync(List<T> data);// Birden fazla veri eklemek için liste
       bool Remove(T model);// Tek bir veriyi silmek için
       bool RemoveRange(List<T> data);// Birden fazla veriyi silmek için liste
        Task<bool> RemoveAsync(string id);// Tek bir veriyi id sine göre silmek için
       bool Update(T model);// Tek bir veriyi güncellemek için

       Task<int> SaveAsync();

    }
}
