using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Application.Features.Command.StarMessage.AddStarMessage
{
    public class AddStarMessageCommandHandler : IRequestHandler<AddStarMessageCommandRequest, AddStarMessageCommandResponse>
    {
        private readonly IMessageService _messageService;
        public AddStarMessageCommandHandler(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public async Task<AddStarMessageCommandResponse> Handle(AddStarMessageCommandRequest request, CancellationToken cancellationToken)
        {
            return new AddStarMessageCommandResponse
            {
                Succeeded = await _messageService.StarMessage(new Abstractions.DTOs.Message.StarMessageResponse
                {
                    MessageId = request.MessageId,
                })
            };
        }
    }
}
