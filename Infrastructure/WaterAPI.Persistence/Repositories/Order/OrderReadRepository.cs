using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.Repositories;
using WaterAPI.Domain.Entities;
using WaterAPI.Persistence.Contexts;

namespace WaterAPI.Persistence.Repositories
{
    public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository
    {
        public OrderReadRepository(WaterAPIDbContext context) : base(context)
        {
        }
    }
}
