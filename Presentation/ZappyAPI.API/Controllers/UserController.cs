using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZappyAPI.Application.Features.Command.User.UpdateUser;
using ZappyAPI.Application.Features.Query.User.GetUserById;

namespace ZappyAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserCommandRequest request)
        {
            UpdateUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserById([FromQuery] GetUserByIdQueryRequest request)
        {
            GetUserByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

    }
}
