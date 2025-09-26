using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.DTOs.Payment;

namespace WaterAPI.Application.Abstractions.Services
{
    public interface IPaymentService
    {
        Task<ProcessPaymentResponseDTO> ProcessPayment(ProcessPaymentRequestDTO request);

        // Bu metot artık 3D Secure başlatma işini yapacak
        Task<CompletePaymentResponseDTO> CompletePayment(CompletePaymentRequestDTO request);


    }
}
