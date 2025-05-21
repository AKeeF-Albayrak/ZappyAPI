using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Features.Command.Group.CreateGroup
{
    public class CreateGroupCommandRequest : IRequest<CreateGroupCommandResponse>
    {
        public Guid UserId { get; set; }
        public string GroupName { get; set; }
        public byte[] GroupPicture { get; set; }
        public string ContentType { get; set; }
        public string Description { get; set; }
        public List<string> Usernames { get; set; }
    }
}
