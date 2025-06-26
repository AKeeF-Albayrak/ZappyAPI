using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities.Common;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Domain.Entities
{
    public class FriendshipRequest : BaseEntity
    {
        public required Guid SenderId { get; set; }
        public required Guid ReceiverId { get; set; }
        public FriendshipRequestStatus Status { get; set; }

        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}
