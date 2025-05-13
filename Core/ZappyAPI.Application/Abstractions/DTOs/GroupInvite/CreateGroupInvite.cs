using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Application.Abstractions.DTOs.GroupInvite
{
    public class CreateGroupInvite
    {
        public required Guid GroupId { get; set; }
        public required Guid InviterUserId { get; set; }
        public required Guid InvitedUserId { get; set; }
    }
}
