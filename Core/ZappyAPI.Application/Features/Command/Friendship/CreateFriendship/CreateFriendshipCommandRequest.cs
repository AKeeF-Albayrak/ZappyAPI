using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Features.Command.Friendship.CreateFriendship
{
    public class CreateFriendshipCommandRequest : IRequest<CreateFriendshipCommandResponse> 
    {
        // TODO: Change to userid, username
        public Guid UserId_1 { get; set; }
        public Guid UserId_2 { get; set; }

    }
}
