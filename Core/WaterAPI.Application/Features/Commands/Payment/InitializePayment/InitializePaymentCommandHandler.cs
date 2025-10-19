    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WaterAPI.Application.Abstractions.Services;
    using WaterAPI.Application.DTOs.Payment;
    using WaterAPI.Application.DTOs.Payment.Buyer;
    using WaterAPI.Application.Repositories;

    namespace WaterAPI.Application.Features.Commands.Payment.CreatePayment
    {
        public class InitializePaymentCommandHandler : IRequestHandler<InitializePaymentCommandRequest, InitializePaymentCommandResponse>
        {
            readonly IPaymentService _paymentService;
            //readonly IHttpContextAccessor _httpContextAccessor; // Aktif kullanıcıyı bulmak için
            readonly ICardRegisterReadRepository _cardRegisterReadRepository;
            readonly IPaymentWriteRepository _paymentWriteRepository;
            // ...gerekli diğer servisler/repository'ler
            public InitializePaymentCommandHandler(IPaymentService paymentService,
                IHttpContextAccessor httpContextAccessor,
                ICardRegisterReadRepository cardRegisterReadRepository,
                IPaymentWriteRepository paymentWriteRepository)
            {
                _paymentService = paymentService;
                //_httpContextAccessor = httpContextAccessor;
                _cardRegisterReadRepository = cardRegisterReadRepository;
                _paymentWriteRepository = paymentWriteRepository;
            }



            public async Task<InitializePaymentCommandResponse> Handle(InitializePaymentCommandRequest request, CancellationToken cancellationToken)
            {
                // string userId = ... // Aktif kullanıcı ID'sini al

                // Handler, veritabanı ve domain'e ait işleri yapar.
                var card = await _cardRegisterReadRepository.Table
                    .Include(c => c.AppUser) // Buyer bilgilerini doldurmak için AppUser'ı da çekiyoruz.
                    .FirstOrDefaultAsync(c => c.Id == Guid.Parse(request.CardRegisterId));

                // if (card == null || card.AppUserId != userId)
                //     throw new Exception("Geçersiz kart veya yetkisiz işlem.");

                WaterAPI.Domain.Entities.Payment payment = new()
                {

                    CardRegisterId = Guid.Parse(request.CardRegisterId),
                    Amount = request.Amount,
                    Status = "Pending"
                };
                await _paymentWriteRepository.AddAsync(payment);
                await _paymentWriteRepository.SaveAsync();

            // ÖNEMLİ: Handler artık Iyzico'nun istediği Buyer, BasketItem gibi şeyleri bilmiyor.
            // Sadece kendi domain nesnesi olan 'payment'ı servise yolluyor.
            // Geri kalan tüm işi (Iyzico nesnelerini oluşturma vs.) servis yapacak.
            payment.CardRegister = card;
            InitializePaymentResponseDTO responseDto = await _paymentService.InitializePaymentAsync(payment);

                return new InitializePaymentCommandResponse
                {
                    Succeeded = responseDto.Succeeded,
                    Token = responseDto.Token,
                    CheckoutFormContent = responseDto.CheckoutFormContent,
                    Message = responseDto.Succeeded ? "Ödeme formu başarıyla oluşturuldu." : responseDto.ErrorMessage
                };
            }
        }
    }
