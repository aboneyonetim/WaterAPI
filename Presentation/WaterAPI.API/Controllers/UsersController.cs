using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WaterAPI.Application.Features.Commands.AppUser.CreateUser;
using WaterAPI.Application.Features.Commands.AppUser.GoogleLogin;
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
        public async Task<IActionResult> Login([FromBody]LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody]GoogleLoginCommandRequest googleLoginCommandRequest) 
        {
            GoogleLoginCommandResponse response= await _mediator.Send(googleLoginCommandRequest);
            return Ok(response);
        }
      
        
    }
}
