using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Features.Command.User.CreateUser
{
    public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
    {
        public required string Name { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Mail { get; set; }
        public string? ProfilePicture { get; set; }
        public string? ContentType { get; set; }
        public string? Description { get; set; }
        public int? Age { get; set; }
    }
}