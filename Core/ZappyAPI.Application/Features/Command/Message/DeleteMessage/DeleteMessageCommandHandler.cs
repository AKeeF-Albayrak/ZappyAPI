using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Application.Features.Command.Message.DeleteMessage
{
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommandRequest, DeleteMessageCommandResponse>
    {
        private readonly IMessageService _messageService;
        public DeleteMessageCommandHandler(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public async Task<DeleteMessageCommandResponse> Handle(DeleteMessageCommandRequest request, CancellationToken cancellationToken)
        {
            return new DeleteMessageCommandResponse
            {
                Succeeded = await _messageService.DeleteMessage(request.MessageId)
            };
        }
    }
}
