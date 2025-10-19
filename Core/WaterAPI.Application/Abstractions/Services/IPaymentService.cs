using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.DTOs.Payment;
using WaterAPI.Domain.Entities;

namespace WaterAPI.Application.Abstractions.Services
{
    public interface IPaymentService
    {
        Task<InitializePaymentResponseDTO> InitializePaymentAsync(Payment payment);
        Task<FinalizePaymentResponseDTO> FinalizePaymentAsync(string token);


    }
}
