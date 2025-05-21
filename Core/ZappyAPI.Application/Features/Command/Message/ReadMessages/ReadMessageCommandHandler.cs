using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Application.Features.Command.Message.ReadMessages
{
    public class ReadMessageCommandHandler : IRequestHandler<ReadMessageCommanRequest, ReadMessageCommandResponse>
    {
        private readonly IMessageService _messageService;
        public ReadMessageCommandHandler(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public async Task<ReadMessageCommandResponse> Handle(ReadMessageCommanRequest request, CancellationToken cancellationToken)
        {
            return new ReadMessageCommandResponse
            {
                Succeeded = await _messageService.ReadMessages(request.GroupId)
            };
        }
    }
}
