using F = WaterAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Application.Repositories
{
    public interface IFileWriteRepository : IWriteRepository<F.File>
    {
    }
}
