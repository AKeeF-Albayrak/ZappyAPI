using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities.Common;

namespace ZappyAPI.Domain.Entities
{
    public class LoginHistory : BaseEntity
    {
        public required Guid UserId { get; set; }
        public required string IpAdress { get; set; }
        public required string UserAgent { get; set; } // TODO : Add Auto User Agent Later
        public required bool Succeeded { get; set; }

        public User User { get; set; }
    }
}
