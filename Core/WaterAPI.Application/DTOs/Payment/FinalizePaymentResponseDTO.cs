using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Application.DTOs.Payment
{
    public class FinalizePaymentResponseDTO
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
        public string PaymentStatus { get; set; }
        public string BasketId { get; set; }
        public string PaymentId { get; set; }
    }
}
