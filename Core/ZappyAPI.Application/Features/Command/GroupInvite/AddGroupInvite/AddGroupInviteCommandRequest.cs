using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Features.Command.GroupInvite.AddGroupInvite
{
    public class AddGroupInviteCommandRequest : IRequest<AddGroupInviteCommandResponse>
    {
        public required Guid GroupId { get; set; }
        public required Guid InviterUserId { get; set; }
        public required Guid InvitedUserId { get; set; }
    }
}
