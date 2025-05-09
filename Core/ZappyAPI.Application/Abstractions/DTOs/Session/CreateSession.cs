using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Abstractions.DTOs.Session
{
    public class CreateSession
    {
        public required Guid UserId { get; set; }
        public required Guid RefreshTokenId { get; set; }
        public string IpAdress { get; set; }
        public string DeviceInfo { get; set; }
    }
}
