using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Features.Command.Message.DeleteMessage
{
    public class DeleteMessageCommandRequest : IRequest<DeleteMessageCommandResponse>   
    {
        public Guid MessageId { get; set; }
    }
}
