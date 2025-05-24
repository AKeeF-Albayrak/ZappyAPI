using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Application.Features.Query.StarMessage.GetStarredMessages
{
    public class GetStarredMessagesQueryHandler : IRequestHandler<GetStarredMessagesQueryRequest, GetStarredMessagesQueryReponse>
    {
        private readonly IMessageService _messageService;
        public GetStarredMessagesQueryHandler(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public async Task<GetStarredMessagesQueryReponse> Handle(GetStarredMessagesQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _messageService.GetStarredMessages();
            return new GetStarredMessagesQueryReponse
            {
                StarredMessages = response.StarredMessageViewModel,
                Succeeded = response.Succeeded,
            };
        }
    }
}
