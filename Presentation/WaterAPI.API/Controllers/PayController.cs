using MediatR;
using Microsoft.AspNetCore.Mvc;
using WaterAPI.Application.Features.Commands.Payment.CreatePayment;
using WaterAPI.Application.Features.Commands.Payment.FinalizePayment;

namespace WaterAPI.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PayController : Controller
    {
        private readonly IMediator _mediator;

        public PayController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("initialize")]
        public async Task<IActionResult> InitializePayment([FromForm] InitializePaymentCommandRequest initializePaymentCommandRequest)
        {

            InitializePaymentCommandResponse response = await _mediator.Send(initializePaymentCommandRequest);
            return Content(response.CheckoutFormContent, "text/html");
            
        }

        [HttpPost("initializer")]
        public async Task<IActionResult> InitializerPayment([FromBody] InitializePaymentCommandRequest initializePaymentCommandRequest)
        {

            InitializePaymentCommandResponse response = await _mediator.Send(initializePaymentCommandRequest);
            return Content(response.CheckoutFormContent, "text/html");

        }
        [HttpPost("finalize")]
        public async Task<IActionResult> FinalizePayment([FromForm] FinalizePaymentCommandRequest finalizePaymentCommandRequest)
        {
            //FinalizePaymentCommandRequest
            var response = await _mediator.Send(finalizePaymentCommandRequest);

            //if (response.Succeeded)
            //{
            //    return Ok(new { response.Message });
            //}
            //return Ok(new { response.Message });
            return Ok(response);
        }

    }
}
