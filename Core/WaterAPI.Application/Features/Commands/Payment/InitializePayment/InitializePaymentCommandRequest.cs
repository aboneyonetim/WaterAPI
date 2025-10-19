using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Application.Features.Commands.Payment.CreatePayment
{
    public class InitializePaymentCommandRequest : IRequest<InitializePaymentCommandResponse>
    {
        public string CardRegisterId { get; set; } // Hangi karta yükleme yapılacağı
        public decimal Amount { get; set; } // Yüklenecek tutar
    }
}
