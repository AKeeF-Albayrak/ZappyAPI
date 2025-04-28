using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities.Common;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Domain.Entities
{
    public class Participant : BaseEntity
    {
        public required Guid UserId { get; set; }
        public required Guid GroupId { get; set; }
        public required ParticipantRole Role { get; set; }
        public required bool IsDeleted { get; set; }

        public User User { get; set; }
        public Group Group { get; set; }
    }
}
