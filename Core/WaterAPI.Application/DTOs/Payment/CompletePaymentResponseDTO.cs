using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Application.DTOs.Payment
{
    public class CompletePaymentResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}
