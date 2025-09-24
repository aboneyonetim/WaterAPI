using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Domain.Entities.Common;

namespace WaterAPI.Domain.Entities
{
    public class CardPayload : BaseEntity
    {
        public Guid  CardRegisterId  { get; set; }
        public float PreviousBalance { get; set; }
        public float LoadedBalance { get; set; }
        public float TotalBalance { get; set; }
        public virtual CardRegister CardRegister { get; set; }
    }
}
