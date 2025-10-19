using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Domain.Entities.Common;
using WaterAPI.Domain.Entities.Identity;

namespace WaterAPI.Domain.Entities
{
    public class CardRegister : BaseEntity
    {

        public string Number { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<CardPayload> CardPayloads { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
