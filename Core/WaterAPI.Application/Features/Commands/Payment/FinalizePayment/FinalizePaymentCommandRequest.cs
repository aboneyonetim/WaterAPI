using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Application.Features.Commands.Payment.FinalizePayment
{
    public class FinalizePaymentCommandRequest :IRequest<FinalizePaymentCommandResponse>
    {
        public string Token { get; set; }
    }
}
