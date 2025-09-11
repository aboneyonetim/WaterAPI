using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.Repositories;
using WaterAPI.Domain.Entities;
using WaterAPI.Persistence.Contexts;

namespace WaterAPI.Persistence.Repositories.File
{
    public class FileWriteRepository : WriteRepository<WaterAPI.Domain.Entities.File> ,IFileWriteRepository 
    {
        public FileWriteRepository(WaterAPIDbContext context) : base(context) 
        {
            
        }
    }
}
