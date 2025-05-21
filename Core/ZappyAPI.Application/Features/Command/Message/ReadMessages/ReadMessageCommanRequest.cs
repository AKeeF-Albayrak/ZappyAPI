using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Features.Command.Message.ReadMessages
{
    public class ReadMessageCommanRequest : IRequest<ReadMessageCommandResponse>
    {
        public Guid GroupId { get; set; }
    }
}
