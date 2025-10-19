using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.Abstractions.Services;
using WaterAPI.Application.DTOs.Payment;
using WaterAPI.Application.Repositories;
using WaterAPI.Domain.Entities;

namespace WaterAPI.Application.Features.Commands.Payment.FinalizePayment
{
    public class FinalizePaymentCommandHandler : IRequestHandler<FinalizePaymentCommandRequest, FinalizePaymentCommandResponse>
    {

        private readonly IPaymentService _paymentService;
        private readonly IPaymentReadRepository _paymentReadRepository;
        private readonly IPaymentWriteRepository _paymentWriteRepository;
        private readonly ICardPayloadWriteRepository _cardPayloadWriteRepository;
        public FinalizePaymentCommandHandler(IPaymentService paymentService, ICardPayloadWriteRepository cardPayloadWriteRepository, IPaymentReadRepository paymentReadRepository, IPaymentWriteRepository paymentWriteRepository)
        {
            _paymentService = paymentService;
            _paymentReadRepository = paymentReadRepository;
            _paymentWriteRepository = paymentWriteRepository;
            _cardPayloadWriteRepository = cardPayloadWriteRepository;
        }

        public async Task<FinalizePaymentCommandResponse> Handle(FinalizePaymentCommandRequest request, CancellationToken cancellationToken)
        {
            // 1. Iyzico'dan ödeme sonucunu doğrula
            FinalizePaymentResponseDTO finalizeResponse = await _paymentService.FinalizePaymentAsync(request.Token);

            // 2. Iyzico'dan dönen BasketId (bizim PaymentId'miz) ile ilgili kaydı bul
            // --- DÜZELTME BURADA ---
            // Guid.Parse işlemini sorgudan ÖNCE yapıp bir değişkene atıyoruz.
            Guid paymentIdToFind;
            try
            {
                paymentIdToFind = Guid.Parse(finalizeResponse.BasketId);
            }
            catch (Exception)
            {
                // Iyzico'dan geçersiz bir BasketId dönerse diye güvenlik önlemi.
                return new() { Succeeded = false, Message = "Geçersiz işlem ID'si." };
            }

            // Sorgu içinde artık sadece bu basit değişkeni kullanıyoruz.
            var payment = await _paymentReadRepository.Table
                                     .Include(p => p.CardRegister)
                                     .FirstOrDefaultAsync(p => p.Id == paymentIdToFind, cancellationToken);
      
            //var payment = await _paymentReadRepository.Table
            //                         .Include(p => p.CardRegister)
            //                         .FirstOrDefaultAsync(p => p.Id == Guid.Parse(finalizeResponse.BasketId), cancellationToken);

            if (payment == null || payment.Status != "Pending")
            {
                // Hatalı veya daha önce işlenmiş bir istekse işlemi sonlandır.
                return new() { Succeeded = false, Message = "İlgili işlem kaydı bulunamadı veya zaten işlenmiş." };
            }
            // 3. Iyzico'dan gelen sonuca göre veritabanını güncelle
            if (finalizeResponse.Succeeded)
            {
                payment.Status = "Completed";
                payment.IyzicoPaymentId = finalizeResponse.PaymentId;

                // Başarılı ödeme için CardPayload kaydı oluştur
                CardPayload payload = new()
                {
                    CardRegisterId = payment.CardRegisterId,
                    PaymentId = payment.Id,
                    PreviousBalance = payment.CardRegister.Balance,
                    LoadedBalance = payment.Amount,
                    TotalBalance = payment.CardRegister.Balance + payment.Amount
                };
                await _cardPayloadWriteRepository.AddAsync(payload);

                // İlgili kartın bakiyesini artır
                payment.CardRegister.Balance += payment.Amount;

                await _paymentWriteRepository.SaveAsync();

                return new() { Succeeded = true, Message = "Ödeme başarıyla tamamlandı ve bakiye güncellendi." };
            }
            else // Ödeme başarısız ise
            {
                payment.Status = "Failed";
                await _paymentWriteRepository.SaveAsync();
                return new() { Succeeded = false, Message = finalizeResponse.ErrorMessage ?? "Ödeme Iyzico tarafından onaylanmadı." };
            }

            throw new NotImplementedException();
        }
    }
}
