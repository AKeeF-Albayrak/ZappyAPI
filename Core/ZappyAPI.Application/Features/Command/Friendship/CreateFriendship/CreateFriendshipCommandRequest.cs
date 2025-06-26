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
        public string Username { get; set; }

    }
}
