using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Domain.Entities
{
   public class ProductImageFile : File
    {
        public ICollection<Product> Product { get; set; }
        public int Width { get; set; }

    }
}
