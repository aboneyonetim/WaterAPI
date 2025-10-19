using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Domain.Entities.Common;
using WaterAPI.Domain.Entities.Identity;

namespace WaterAPI.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid CardRegisterId { get; set; } // Ödemeyapılan kartın ID'si
        public virtual CardRegister CardRegister { get; set; }

        // Her başarılı ödemenin bir yükleme kaydı olacağını belirtir.
        public virtual CardPayload CardPayload { get; set; }

        public decimal Amount { get; set; } // Yüklenen tutar
        public string Status { get; set; } // Ör: "Pending", "Completed", "Failed"

        // Iyzico'dan gelen ve saklamamız gereken referans numaraları
        public string? IyzicoPaymentId { get; set; } // Başarılı ödeme sonrası Iyzico'nun verdiği ID
        public string? ConversationId { get; set; } // İşlem boyunca kullanılan konuşma ID'si

    }
}
