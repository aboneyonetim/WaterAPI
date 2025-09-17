using MediatR;
using Microsoft.AspNetCore.Mvc;
using WaterAPI.Application.Features.Commands.AppUser.CreateUser;
using WaterAPI.Application.Features.Commands.AppUser.LoginUser;

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
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response=  await _mediator.Send(createUserCommandRequest); 
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }
    }
}
