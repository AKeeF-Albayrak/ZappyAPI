using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities.Common;

namespace ZappyAPI.Domain.Entities
{
    public class Group : BaseEntity
    {
        public required string GroupName { get; set; }
        public string? GroupPicPath { get; set; }
        public required bool IsDeleted { get; set; }
        public required string Description { get; set; }

        public ICollection<Participant> Participants { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<GroupInvite> Invites { get; set; }
    }

}
