using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Features.Command.User.LoginUser
{
    public class LoginUserCommandRequest : IRequest<LoginUserCommandRepsonse>
    {
        public required string Password { get; set; }
        public required string UserName { get; set; }
        public required string IpAdress { get; set; }
        public  required string DeviceInfo { get; set; }
        public required string UserAgent { get; set; }
    }                                         
}
