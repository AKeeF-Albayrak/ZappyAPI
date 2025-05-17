using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZappyAPI.Application.Features.Command.UserStatus.UpdateConnectionId;
using ZappyAPI.Application.Features.Command.UserStatus.UpdateUserStatus;

namespace ZappyAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserStatusController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateConnectionId([FromBody] UpdateConnectionIdCommandRequest request)
        {
            UpdateConnectionIdCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateUserStatus([FromBody] UpdateUserStatusCommandRequest request)
        {
            UpdateUserStatusCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
