using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Application.Features.Command.Message.AddMessage
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommandRequest, CreateMessageCommandResponse>
    {
        private readonly IMessageService _messageService;
        public CreateMessageCommandHandler(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public async Task<CreateMessageCommandResponse> Handle(CreateMessageCommandRequest request, CancellationToken cancellationToken)
        {
            return new CreateMessageCommandResponse
            {
                Succeeded = await _messageService.CreateMessage(new Abstractions.DTOs.Message.CreateMessage
                {
                    GroupId = request.GroupId,
                    SenderId = request.SenderId,
                    ContentType = request.ContentType,
                    EncryptedContent = request.EncryptedContent,
                    IsPinned = request.IsPinned,
                    RepliedMessageId = request.RepliedMessageId,
                })
            };
        }
    }
}
