using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Application.Features.Query.Friendship.GetFriends
{
    public class GetFriendsQueryRequest : IRequest<GetFriendsQueryResponse>
    {
        public FriendshipStatus Status { get; set; }
    }
}
