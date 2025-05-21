using MediatR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Application.Features.Query.Friendship.GetFriends
{
    public class GetFriendsQueryHandler : IRequestHandler<GetFriendsQueryRequest, GetFriendsQueryResponse>
    {
        private readonly IFriendshipService _friendshipService;
        public GetFriendsQueryHandler(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }
        public async Task<GetFriendsQueryResponse> Handle(GetFriendsQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _friendshipService.GetFriends(request.Status);

            return new GetFriendsQueryResponse
            {
                Friends = response.Friends,
                Succeeded = response.Succeeded,
            };
        }
    }
}
