using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Application.Abstractions.DTOs.Friendship
{
    public class UpdateFriendship
    {
        public FriendshipStatus Status { get; set; }
        public Guid Id { get; set; }
    }
}
