using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities.Common;

namespace ZappyAPI.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public required Guid UserId { get; set; }
        public required string Token { get; set; }
        public required DateTime ExpireDate { get; set; }
        public required bool Revoked { get; set; } // false --> Usable

        public User User { get; set; }
    }
}
