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
using System.Runtime;
using WaterAPI.Domain.Entities;

namespace WaterAPI.Infrastructure.Services
{
    class IyzicoPaymentService : IPaymentService
    {
       

        private readonly Options _iyzicoOptions;
        private readonly IConfiguration _configuration;

        public IyzicoPaymentService(IConfiguration configuration)
        {
            _configuration = configuration;
            _iyzicoOptions = new ()
            {
                ApiKey = configuration["IyzicoSettings:ApiKey"],
                SecretKey = configuration["IyzicoSettings:SecretKey"],
                BaseUrl = configuration["IyzicoSettings:BaseUrl"]
            };
        }



        
        public async Task<InitializePaymentResponseDTO> InitializePaymentAsync(WaterAPI.Domain.Entities.Payment  payment)
        {


            //**
            // Iyzico nesneleri oluşturma mantığı Handler'dan buraya taşındı.
            var request = new CreateCheckoutFormInitializeRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = Guid.NewGuid().ToString(),
                Price = payment.Amount.ToString("0.00", CultureInfo.InvariantCulture),
                PaidPrice = payment.Amount.ToString("0.00", CultureInfo.InvariantCulture),
                Currency = Currency.TRY.ToString(),
                BasketId = payment.Id.ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString(),
                CallbackUrl = _configuration["IyzicoSettings:CallbackUrl"],

                Buyer = new Buyer
                {
                    Id = payment.CardRegister.AppUser.Id,
                    Name = payment.CardRegister.AppUser.NameSurname.Split(' ')[0],
                    Surname = payment.CardRegister.AppUser.NameSurname.Split(' ').Last(),
                    Email = payment.CardRegister.AppUser.Email,
                    GsmNumber = "+905350000000", // GEÇİCİ - Zorunlu alan, sonra AppUser'dan alırsın
                    IdentityNumber = "74300864791", // GEÇİCİ - Sandbox için geçerli test TC No
                    Ip = "85.34.78.112",           // GEÇİCİ - Sonra dinamik hale getirilecek
                    City = "Istanbul",             // GEÇİCİ - Zorunlu alan
                    Country = "Turkey",            // GEÇİCİ - Zorunlu alan
                    RegistrationAddress = "Placeholder Mah. No:1" // GEÇİCİ - Zorunlu alan
                },

                // Sanal ürün olduğu için Fatura ve Teslimat Adresi aynı olabilir.
                BillingAddress = new Address
                {
                    ContactName = payment.CardRegister.AppUser.NameSurname,
                    City = "Istanbul",    // GEÇİCİ - Zorunlu alan
                    Country = "Turkey",   // GEÇİCİ - Zorunlu alan
                    Description = "Placeholder Mah. No:1" // GEÇİCİ - Zorunlu alan
                },

                ShippingAddress = new Address
                {
                    ContactName = payment.CardRegister.AppUser.NameSurname,
                    City = "Istanbul",    // GEÇİCİ - Zorunlu alan
                    Country = "Turkey",   // GEÇİCİ - Zorunlu alan
                    Description = "Placeholder Mah. No:1" // GEÇİCİ - Zorunlu alan
                },

                BasketItems = new List<BasketItem>
    {
        new BasketItem
        {
            Id = payment.CardRegisterId.ToString(),
            Name = "Su Kartı Bakiye Yükleme",
            Category1 = "Hizmet",
            ItemType = BasketItemType.VIRTUAL.ToString(),
            Price = payment.Amount.ToString("0.00", CultureInfo.InvariantCulture)
        }
    }
            };

            //**
            // Hata veren senkron metot yerine, artık çalışması gereken ASENKRON metot kullanılıyor.
            CheckoutFormInitialize checkoutFormInitialize = await CheckoutFormInitialize.Create(request, _iyzicoOptions);

            if (checkoutFormInitialize.Status != "success")
            {
                return new InitializePaymentResponseDTO
                {
                    Succeeded = false,
                    ErrorMessage = checkoutFormInitialize.ErrorMessage
                };
            }

            return new InitializePaymentResponseDTO
            {
                Succeeded = true,
                Token = checkoutFormInitialize.Token,
                CheckoutFormContent = checkoutFormInitialize.CheckoutFormContent
            };
        }
        public async Task<FinalizePaymentResponseDTO> FinalizePaymentAsync(string token)
        {
            var retrieveRequest = new RetrieveCheckoutFormRequest
            {
                Locale = Locale.TR.ToString(),
                Token = token
            };

            // Hatalı olan 'RetrieveAsync' metodu, 'Create' gibi 'Retrieve' olarak düzeltildi.
            CheckoutForm retrieveResult = await CheckoutForm.Retrieve(retrieveRequest, _iyzicoOptions);

            if (retrieveResult.Status == "success" && retrieveResult.PaymentStatus == "SUCCESS")
            {
                return new FinalizePaymentResponseDTO
                {
                    Succeeded = true,
                    PaymentStatus = retrieveResult.PaymentStatus,
                    BasketId = retrieveResult.BasketId,
                    PaymentId = retrieveResult.PaymentId
                };
            }

            return new FinalizePaymentResponseDTO
            {
                Succeeded = false,
                ErrorMessage = retrieveResult.ErrorMessage,
                PaymentStatus = retrieveResult.PaymentStatus
            };
        }

    }
    }

