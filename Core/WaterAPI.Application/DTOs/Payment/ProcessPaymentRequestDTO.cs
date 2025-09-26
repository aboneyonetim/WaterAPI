using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Application.DTOs.Payment
{
    public class ProcessPaymentRequestDTO
    {
        // Ödeme ve Kart Bilgileri
        public decimal Amount { get; set; }
        public Guid CardRegisterId { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public string Cvc { get; set; }

        // Alıcı (Buyer) Bilgileri
        public string UserId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerSurname { get; set; }
        public string BuyerGsmNumber { get; set; }
        public string BuyerEmail { get; set; }

        // Referans ID'ler
        public string OrderId { get; set; } // Bizim sistemimizdeki Payment.Id olacak

    }
}
