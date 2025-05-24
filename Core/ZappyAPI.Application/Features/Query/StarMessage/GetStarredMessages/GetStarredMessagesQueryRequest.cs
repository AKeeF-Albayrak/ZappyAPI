using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Features.Query.StarMessage.GetStarredMessages
{
    public class GetStarredMessagesQueryRequest : IRequest<GetStarredMessagesQueryReponse>
    {
    }
}
