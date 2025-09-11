using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Domain.Entities.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
       virtual public DateTime? UpdatedDate { get; set; } //Tüm entitylerde geçerli olmayabilir.Bazıları migrate etmeyebilir.
        public bool IsActive  { get; set; }
        public bool IsDeleted  { get; set; }
  
    }
}
