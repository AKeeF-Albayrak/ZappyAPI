using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Features.Command.UserStatus.UpdateConnectionId
{
    public class UpdateConnectionIdCommandRequest : IRequest<UpdateConnectionIdCommandResponse>
    {
        public Guid UserId { get; set; }
        public string ConnectionId { get; set; }
    }
}
