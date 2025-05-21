using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Features.Command.User.UpdateUser
{
    public class UpdateUserCommandRequest : IRequest<UpdateUserCommandResponse>
    {
        public required string Name { get; set; }
        public required string Username { get; set; }
        public required string Mail { get; set; }
        public string? ProfilePic { get; set; }
        public string? ContentType { get; set; }
        public string? Description { get; set; }
    }
}
