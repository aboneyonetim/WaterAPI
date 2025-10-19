using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Application.Features.Commands.Payment.FinalizePayment
{
    public class FinalizePaymentCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
