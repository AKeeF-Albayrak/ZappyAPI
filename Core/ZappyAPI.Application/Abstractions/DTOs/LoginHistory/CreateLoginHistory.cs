using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Abstractions.DTOs.LoginHistory
{
    public class CreateLoginHistory
    {
        public required Guid UserId { get; set; }
        public required string IpAdress { get; set; }
        public required string UserAgent { get; set; }
        public required bool Succeeded { get; set; }
    }
}
