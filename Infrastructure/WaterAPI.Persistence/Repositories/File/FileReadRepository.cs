using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.Repositories;
using WaterAPI.Persistence.Contexts;


namespace WaterAPI.Persistence.Repositories.File
{
   public class FileReadRepository : ReadRepository<WaterAPI.Domain.Entities.File>,IFileReadRepository
    {
        public FileReadRepository( WaterAPIDbContext context ) : base(context)
        {
            
        }
    }
}
