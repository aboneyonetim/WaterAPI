using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Domain.Entities.Common;

namespace WaterAPI.Domain.Entities
{
    public class Product : BaseEntity
    {
        public String Name { get; set; }
        public int Stock { get; set; }
        public long Price { get; set; }
        ICollection<Order> Orders { get; set; }
    }
}
