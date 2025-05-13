using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZappyAPI.Application.Features.Command.Group.CreateGroup;
using ZappyAPI.Application.Features.Command.GroupInvite.AddGroupInvite;
using ZappyAPI.Application.Features.Command.GroupInvite.RespondeGroupInvite;
using ZappyAPI.Application.Features.Query.Group.GetGroup;
using ZappyAPI.Application.Features.Query.Group.GetGroups;

namespace ZappyAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupCommandRequest request)
        {
            CreateGroupCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetGroup([FromQuery] GetGroupQueryRequest request)
        {
            GetGroupQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserGroups([FromQuery] GetGroupsQueryRequest request)
        {
            GetGroupsQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InviteGroup([FromBody] AddGroupInviteCommandRequest request)
        {
            AddGroupInviteCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RespondeInvite([FromBody] RespondeGroupInviteCommandRequest request)
        {
            RespondeGroupInviteCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
