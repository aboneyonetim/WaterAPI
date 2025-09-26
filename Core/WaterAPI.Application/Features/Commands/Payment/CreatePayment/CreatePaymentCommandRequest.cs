using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Application.Features.Commands.Payment.CreatePayment
{
    public class CreatePaymentCommandRequest : IRequest<CreatePaymentCommandResponse>
    {
        // Mobil app'ten gelecek bilgiler
        public Guid CardRegisterId { get; set; }
        public decimal Amount { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public string Cvc { get; set; }

        // Bu bilgi JWT Token'dan okunup Handler içinde atanabilir veya
        // güvenlik açısından risk oluşturmayacaksa request ile gönderilebilir.
        // Biz şimdilik request'te olduğunu varsayıyoruz.
        public string UserId { get; set; }
    }
}
