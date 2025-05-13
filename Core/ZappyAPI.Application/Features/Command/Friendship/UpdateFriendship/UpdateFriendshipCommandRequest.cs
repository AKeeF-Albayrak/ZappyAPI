using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Application.Features.Command.Friendship.UpdateFriendship
{
    public class UpdateFriendshipCommandRequest : IRequest<UpdateFriendshipCommandResponse>
    {
        public Guid Id { get; set; }
        public FriendshipStatus Status { get; set; }
    }
}
