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
        /// <summary>
        /// Ödeme denemesinin tutarı. Para birimiyle ilgili olduğu için 'decimal' kullanmak en iyi pratiktir.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Ödeme işleminin mevcut durumu. Örn: "Pending", "Succeeded", "Failed".
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Ödemeyi başlatan kullanıcı.
        /// </summary>
        public string AppUserId { get; set; }

        /// <summary>
        /// Ödemenin hangi kayıtlı su kartı için yapıldığı.
        /// </summary>
        public Guid CardRegisterId { get; set; }

        /// <summary>
        /// Ödeme sağlayıcısından (Iyzico) dönen, işleme özel benzersiz ID. Raporlama ve karşılaştırma için kullanılır.
        /// </summary>
        public string? GatewayTransactionId { get; set; }

        /// <summary>
        /// İşlem başarısız olursa, sağlayıcıdan dönen hata mesajı.
        /// </summary>
        public string? ErrorMessage { get; set; }

        // --- Navigation Properties ---

        public virtual AppUser AppUser { get; set; }
        public virtual CardRegister CardRegister { get; set; }
    }
}
