using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Domain.Entities.Common;

namespace WaterAPI.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public String Name { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
