using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Application.Features.Commands.Payment.CreatePayment
{
    public class CreatePaymentCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public bool IsRedirectHtml { get; set; }
        public string RedirectHtmlContent { get; set; }
    }
}
