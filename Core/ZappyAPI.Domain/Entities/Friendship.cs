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
        public Guid UserId_1 { get; set; }
        public Guid UserId_2 { get; set; }
        public FriendshipStatus Status { get; set; }

        public User User_1 { get; set; }
        public User User_2 { get; set; }
    }
}
