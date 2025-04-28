using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities.Common;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Domain.Entities
{
    public class GroupInvite : BaseEntity
    {
        public required Guid GroupId { get; set; }
        public required Guid InviterUserId { get; set; }
        public required Guid InvitedUserId { get; set; }
        public required GroupInviteStatus Status { get; set; }
        public required DateTime RespondedDate { get; set; }

        public Group Group { get; set; }
        public User InviterUser { get; set; }
        public User InvitedUser { get; set; }
    }
}
