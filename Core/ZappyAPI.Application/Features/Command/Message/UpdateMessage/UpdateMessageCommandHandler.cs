using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Application.Features.Command.Message.UpdateMessage
{
    public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommandRequest, UpdateMessageCommandResponse>
    {
        private readonly IMessageService _messageService;
        public UpdateMessageCommandHandler(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public async Task<UpdateMessageCommandResponse> Handle(UpdateMessageCommandRequest request, CancellationToken cancellationToken)
        {
            return new UpdateMessageCommandResponse
            {
                Succeeded = await _messageService.UpdateMessage(new Abstractions.DTOs.Message.UpdateMessage
                {
                    Id = request.Id,
                    ContentType = request.ContentType,
                    EncryptedContent = request.EncryptedContent,
                    IsPinned = request.IsPinned,
                    RepliedMessageId = request.RepliedMessageId,
                })
            };
        }
    }
}
