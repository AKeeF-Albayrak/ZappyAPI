using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZappyAPI.Application.Features.Command.Friendship.CreateFriendship;
using ZappyAPI.Application.Features.Command.Friendship.UpdateFriendship;
using ZappyAPI.Application.Features.Query.Friendship.GetFriends;

namespace ZappyAPI.API.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FriendshipController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateFriendship([FromBody] CreateFriendshipCommandRequest request)
        {
            CreateFriendshipCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateFriendship([FromBody] UpdateFriendshipCommandRequest request)
        {
            UpdateFriendshipCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetFriends([FromQuery] GetFriendsQueryRequest request)
        {
            GetFriendsQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
