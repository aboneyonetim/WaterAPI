using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Domain.Entities.Common;

namespace WaterAPI.Application.Repositories
{
   public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        public DbSet<T> Table => throw new NotImplementedException();

       IQueryable<T> GetAll();// Örneğin Product daki tüm verileri getirmek için  
       IQueryable<T> GetWhere(Expression<Func<T, bool>> method);// Örneğin Product daki miktarı 3 olan verileri getirmek için, şart ifadesi doğru olan veriyi getirir
       Task<T> GetSingleAsync(Expression<Func<T, bool>> method);// Örneğin Product daki id si 5 olan veriyi getirmek için, şart ifadesi doğru olan tek veriyi getirir
       Task<T> GetByIdAsync(string id);// Örneğin Product daki id si 5 olan veriyi getirmek için, id ye göre tek veriyi getirir
    }
}
