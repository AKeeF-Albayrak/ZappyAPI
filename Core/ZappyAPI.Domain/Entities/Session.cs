using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities.Common;

namespace ZappyAPI.Domain.Entities
{
    public class Session : BaseEntity
    {
        public required Guid UserId { get; set; }
        public required Guid RefreshTokenId { get; set; }
        public string IpAdress { get; set; }
        public string DeviceInfo { get; set; }
        public required bool IsActive { get; set; }
        public required DateTime LastActivityDate { get; set; }

        public User User { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
