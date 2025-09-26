using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Application.DTOs.Payment
{
    public class ProcessPaymentResponseDTO
    {
        public bool IsSuccess { get; set; }
        public bool IsRedirectHtml { get; set; } // 3D Secure için mi?
        public string RedirectHtmlContent { get; set; } // Mobil app'e gönderilecek HTML
        public string ErrorMessage { get; set; }
    }
}
