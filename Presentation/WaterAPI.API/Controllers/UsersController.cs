using MediatR;
using Microsoft.AspNetCore.Mvc;
using WaterAPI.Application.Features.Commands.AppUser.CreateUser;

namespace WaterAPI.API.Controllers
{
    [Route("api/[Controller]")]
        public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //public Task <IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest) 
        //{

        //    return Ok();
        //}
    }
}
