using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.Abstractions.Services;
using WaterAPI.Application.DTOs.Payment;

namespace WaterAPI.Infrastructure.Services
{
    class IyzicoPaymentService : IPaymentService
    {

        private readonly Options _options;
        private readonly IConfiguration _configuration;

        public IyzicoPaymentService(Options options, IConfiguration configuration)
        {
            _configuration = configuration;
            _options = new Options
            {
                ApiKey = configuration["IyzicoSettings:ApiKey"],
                SecretKey = configuration["IyzicoSettings:SecretKey"],
                BaseUrl = configuration["IyzicoSettings:BaseUrl"]
            };
        }




        public Task<CompletePaymentResponseDTO> CompletePayment(CompletePaymentRequestDTO request)
        {
            throw new NotImplementedException();
        }

        public async Task<ProcessPaymentResponseDTO> ProcessPayment(ProcessPaymentRequestDTO request)
        {
            string callbackUrl = _configuration["IyzicoSettings:CallbackUrl"];

            var iyzicoRequest = new CreatePaymentRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = request.OrderId,
                Price = request.Amount.ToString("0.00", CultureInfo.InvariantCulture),
                PaidPrice = request.Amount.ToString("0.00", CultureInfo.InvariantCulture),
                Currency = Currency.TRY.ToString(),
                Installment = 1,
                BasketId = request.CardRegisterId.ToString(),
                PaymentChannel = PaymentChannel.WEB.ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString(),
                CallbackUrl = callbackUrl
            };

            PaymentCard paymentCard = new PaymentCard
            {
                CardHolderName = request.CardHolderName,
                CardNumber = request.CardNumber.Replace(" ", ""),
                ExpireMonth = request.ExpireMonth,
                ExpireYear = request.ExpireYear,
                Cvc = request.Cvc,
                RegisterCard = 0
            };
            iyzicoRequest.PaymentCard = paymentCard;

            Buyer buyer = new Buyer
            {
                Id = request.UserId,
                Name = request.BuyerName,
                Surname = request.BuyerSurname,
                GsmNumber = request.BuyerGsmNumber,
                Email = request.BuyerEmail,
                IdentityNumber = "11111111111",
                Ip = "85.34.78.112"
            };
            iyzicoRequest.Buyer = buyer;

            Address billingAddress = new Address
            {
                ContactName = $"{request.BuyerName} {request.BuyerSurname}",
                City = "Sivas",
                Country = "Turkey",
                Description = "Test Adres"
            };
            iyzicoRequest.BillingAddress = billingAddress;
            iyzicoRequest.ShippingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem
            {
                Id = request.CardRegisterId.ToString(),
                Name = "Su Kartı Bakiye Yükleme",
                Category1 = "Dijital Hizmet",
                ItemType = BasketItemType.VIRTUAL.ToString(),
                Price = request.Amount.ToString("0.00", CultureInfo.InvariantCulture)
            };
            basketItems.Add(firstBasketItem);
            iyzicoRequest.BasketItems = basketItems;
            
            
            // ...

            //Payment payment = await Task.Run(() => Payment.Create(iyzicoRequest, _options));

            //if (payment.Status == "failure")
            //{
            //    return new ProcessPaymentResponseDTO { IsSuccess = false, ErrorMessage = payment.ErrorMessage };
            //}

            //if (!string.IsNullOrEmpty(payment.ThreeDSHtmlContent))
            //{
            //    return new ProcessPaymentResponseDTO
            //    {
            //        IsSuccess = true,
            //        IsRedirectHtml = true,
            //        RedirectHtmlContent = payment.ThreeDSHtmlContent
            //    };
            //}

            return new ProcessPaymentResponseDTO { IsSuccess = true };
        }
    }
}
