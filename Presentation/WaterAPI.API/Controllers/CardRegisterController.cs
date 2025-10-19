using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WaterAPI.Application.Features.Commands.CardRegister.CreateCardRegister;
using WaterAPI.Application.Features.Commands.Product.CreateProduct;

namespace WaterAPI.API.Controllers
{
    [Route("api/[Controller]")]

    public class CardRegisterController : Controller
    {

        readonly IMediator _mediator;

        public CardRegisterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateCardRegisterCommandRequest createCardRegisterCommandRequest)
        {

            //if (ModelState.IsValid){}
            CreateCardRegisterCommandResponse response = await _mediator.Send(createCardRegisterCommandRequest);
            return Ok(response);
            //return StatusCode((int)HttpStatusCode.Created);
        }

      


    }


}
