using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Globalization;
using WaterAPI.Application.Features.Commands.Payment.CreatePayment;

namespace WaterAPI.API.Controllers
{
    public class IyzipaySettings
    {
        public string ApiKey { get; set; } = "sandbox-huIYh8tne4UbrxAalBZXvHKwIQ4Qn9Vn";
        public string SecretKey { get; set; } = "sandbox-7XBmreNWDPCfUD4o02bQSU9yURFaNoIV";
        public string BaseUrl { get; set; } = "https://sandbox-api.iyzipay.com";
    }

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        readonly IMediator _mediator;
        private readonly Iyzipay.Options _iyziOptions;

        public PaymentController(IOptions<IyzipaySettings> iyzipaySettings, IMediator mediator)
        {
            _mediator = mediator;
            var cfg = iyzipaySettings.Value ?? throw new ArgumentNullException(nameof(iyzipaySettings));
            _iyziOptions = new Iyzipay.Options
            {
                ApiKey = cfg.ApiKey,
                SecretKey = cfg.SecretKey,
                BaseUrl = cfg.BaseUrl
            };
        }

        [HttpPost("payment")]
        public async Task<IActionResult> paymentInt(InitializePaymentCommandRequest initializePaymentCommandRequest) {
            InitializePaymentCommandResponse response =await _mediator.Send(initializePaymentCommandRequest);

            return Ok(response);
                }

        /// <summary>
        /// Checkout Form başlatır (3D Secure).
        /// </summary>
        /// <returns>iyzico tarafından üretilen token ve HTML içerik</returns>
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout()
        {
            var conversationId = Guid.NewGuid().ToString();
            decimal price = 1.00m;
            decimal paidPrice = 1.20m;
            string basketId = "B67832";

            var request = new CreateCheckoutFormInitializeRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = conversationId,
                Price = price.ToString("0.00", CultureInfo.InvariantCulture),
                PaidPrice = paidPrice.ToString("0.00", CultureInfo.InvariantCulture),
                Currency = Currency.TRY.ToString(),
                BasketId = basketId,
                PaymentGroup = PaymentGroup.PRODUCT.ToString(),
                CallbackUrl = $"{Request.Scheme}://{Request.Host}/api/payment/callback"
            };

            request.EnabledInstallments = new List<int> { 2, 3, 6, 9 };

            request.Buyer = new Buyer
            {
                Id = "BY789",
                Name = "John",
                Surname = "Doe",
                GsmNumber = "+905350000000",
                Email = "email@email.com",
                IdentityNumber = "74300864791",
                LastLoginDate = "2015-10-05 12:43:35",
                RegistrationDate = "2013-04-21 15:12:09",
                RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                Ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "85.34.78.112",
                City = "Istanbul",
                Country = "Turkey",
                ZipCode = "34732"
            };

            request.ShippingAddress = new Address
            {
                ContactName = "Jane Doe",
                City = "Istanbul",
                Country = "Turkey",
                Description = "Nidakule Göztepe",
                ZipCode = "34742"
            };

            request.BillingAddress = new Address
            {
                ContactName = "Jane Doe",
                City = "Istanbul",
                Country = "Turkey",
                Description = "Nidakule Göztepe",
                ZipCode = "34742"
            };

            request.BasketItems = new List<BasketItem>
            {
                new BasketItem
                {
                    Id = "BI101",
                    Name = "Binocular",
                    Category1 = "Collectibles",
                    Category2 = "Accessories",
                    ItemType = BasketItemType.PHYSICAL.ToString(),
                    Price = "0.3"
                },
                new BasketItem
                {
                    Id = "BI102",
                    Name = "Game code",
                    Category1 = "Game",
                    Category2 = "Online Game Items",
                    ItemType = BasketItemType.VIRTUAL.ToString(),
                    Price = "0.5"
                },
                new BasketItem
                {
                    Id = "BI103",
                    Name = "Usb",
                    Category1 = "Electronics",
                    Category2 = "Usb / Cable",
                    ItemType = BasketItemType.PHYSICAL.ToString(),
                    Price = "0.2"
                }
            };

            var checkoutFormInitialize = await CheckoutFormInitialize.Create(request, _iyziOptions);

            if (checkoutFormInitialize == null || checkoutFormInitialize.Status != "success")
            {
                return BadRequest(new
                {
                    success = false,
                    error = checkoutFormInitialize?.ErrorMessage ?? "iyzico initialize error"
                });
            }

            return Ok(new
            {
                success = true,
                token = checkoutFormInitialize.Token,
                htmlContent = checkoutFormInitialize.CheckoutFormContent
            });

            //return Content(checkoutFormInitialize.CheckoutFormContent, "text/html");
        }

        /// <summary>
        /// iyzico callback → ödeme sonucu döner.
        /// </summary>
        [HttpPost("callback")]
        public async Task<IActionResult> Callback([FromForm] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { success = false, error = "token missing" });
            }

            var retrieveRequest = new RetrieveCheckoutFormRequest
            {
                Locale = Locale.TR.ToString(),
                Token = token
            };

            var retrieveResult = await CheckoutForm.Retrieve(retrieveRequest, _iyziOptions);

            if (retrieveResult != null && retrieveResult.Status == "success" && retrieveResult.PaymentStatus == "SUCCESS")
            {
                return Ok(new
                {
                    success = true,
                    message = "Ödeme başarılı",
                    data = retrieveResult
                });
            }

            return BadRequest(new
            {
                success = false,
                message = retrieveResult?.ErrorMessage ?? "Ödeme başarısız",
                data = retrieveResult
            });
        }
    }
}
