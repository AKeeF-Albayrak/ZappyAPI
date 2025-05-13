using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Application.Features.Command.GroupInvite.RespondeGroupInvite
{
    public class RespondeGroupInviteCommandRequest : IRequest<RespondeGroupInviteCommandResponse>
    {
        public Guid InviteId { get; set; }
        public GroupInviteStatus Status { get; set; }
    }
}
