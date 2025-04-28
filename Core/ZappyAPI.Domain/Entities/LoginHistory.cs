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
        public string IpAdress { get; set; }// ??? REQUIRED ?
        public string UserAgent { get; set; }// ??? REQUIRED ?

        public User User { get; set; }
    }
}
