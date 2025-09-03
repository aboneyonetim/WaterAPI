using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.Repositories;
using WaterAPI.Domain.Entities;
using WaterAPI.Persistence.Contexts;

namespace WaterAPI.Persistence.Repositories { 
class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
{
    public OrderWriteRepository(WaterAPIDbContext context) : base(context)
    {
    }
}
}
