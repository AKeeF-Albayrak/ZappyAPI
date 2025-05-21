using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZappyAPI.Application.Features.Command.Message.AddMessage;
using ZappyAPI.Application.Features.Command.Message.DeleteMessage;
using ZappyAPI.Application.Features.Command.Message.ReadMessages;
using ZappyAPI.Application.Features.Command.Message.UpdateMessage;

namespace ZappyAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MessageController(IMediator mediator)
        {
             _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateMessage([FromBody] CreateMessageCommandRequest request)
        {
            CreateMessageCommandResponse response =  await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteMessage([FromBody] DeleteMessageCommandRequest request)
        {
            DeleteMessageCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateMessage([FromBody] UpdateMessageCommandRequest request)
        {
            UpdateMessageCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ReadMessages([FromBody] ReadMessageCommanRequest request)
        {
            ReadMessageCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
