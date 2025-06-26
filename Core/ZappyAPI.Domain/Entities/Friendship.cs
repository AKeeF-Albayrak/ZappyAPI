using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities.Common;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Domain.Entities
{
    public class Friendship : BaseEntity
    {
        public required Guid UserAId { get; set; }
        public required Guid UserBId { get; set; }
        public required FriendshipStatus Status { get; set; }

        public User UserA { get; set; }
        public User UserB { get; set; }
    }
}
