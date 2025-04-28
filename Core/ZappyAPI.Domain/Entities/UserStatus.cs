using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities.Common;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Domain.Entities
{
    public class UserStatus : BaseEntity
    {
        public required Guid UserId { get; set; }
        public required User_Status Status { get; set; }

        public User User { get; set; }

    }
}
