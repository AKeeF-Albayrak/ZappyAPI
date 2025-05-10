using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Features.Command.User.LogoutUser
{
    public class LogoutUserCommandRequest : IRequest<LogoutUserCommandResponse>
    {
        public Guid UserId { get; set; }
    }
}
