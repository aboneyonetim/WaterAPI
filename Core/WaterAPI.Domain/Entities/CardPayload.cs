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
        public decimal PreviousBalance { get; set; }
        public decimal LoadedBalance { get; set; }
        public virtual Payment Payment { get; set; }
        public decimal TotalBalance { get; set; }
        public virtual CardRegister CardRegister { get; set; }

        // Hangi ödeme sonucunda bu kaydın oluştuğunu belirten foreign key.
        public Guid PaymentId { get; set; }
    }
}
