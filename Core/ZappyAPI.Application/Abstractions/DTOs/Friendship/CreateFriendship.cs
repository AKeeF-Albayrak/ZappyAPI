using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Abstractions.DTOs.Friendship
{
    public class CreateFriendship
    {
        public required Guid UserId_1 { get; set; }
        public required Guid UserId_2 { get; set; }
    }
}
