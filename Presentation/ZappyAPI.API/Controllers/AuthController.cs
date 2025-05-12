using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZappyAPI.Application.Features.Command.User.CreateUser;
using ZappyAPI.Application.Features.Command.User.LoginUser;
using ZappyAPI.Application.Features.Command.User.LogoutUser;
using ZappyAPI.Application.Repositories;

namespace ZappyAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] CreateUserCommandRequest request)
        {
            CreateUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest request)
        {
            LoginUserCommandRepsonse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] LogoutUserCommandRequest request)
        {
            LogoutUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
