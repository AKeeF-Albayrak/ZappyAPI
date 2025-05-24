using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Features.Command.StarMessage.AddStarMessage
{
    public class AddStarMessageCommandRequest : IRequest<AddStarMessageCommandResponse>
    {
        public Guid MessageId { get; set; }
    }
}
