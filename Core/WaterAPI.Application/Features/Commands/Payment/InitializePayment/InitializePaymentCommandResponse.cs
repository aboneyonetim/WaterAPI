using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Application.Features.Commands.Payment.CreatePayment
{
    public class InitializePaymentCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Token { get; set; }
        public string CheckoutFormContent { get; set; }
        public string Message { get; set; } // Hata durumunda mesaj göstermek için

    }
}
