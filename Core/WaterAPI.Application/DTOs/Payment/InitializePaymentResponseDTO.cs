using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Application.DTOs.Payment
{
    public class InitializePaymentResponseDTO
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public string CheckoutFormContent { get; set; }
    }
}
