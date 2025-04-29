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
        public required Guid UserId_1 { get; set; }
        public required Guid UserId_2 { get; set; }
        public required FriendshipStatus Status { get; set; }

        public User User_1 { get; set; }
        public User User_2 { get; set; }
    }
}
