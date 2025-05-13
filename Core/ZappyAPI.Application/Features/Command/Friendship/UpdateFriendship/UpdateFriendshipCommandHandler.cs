using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Application.Features.Command.Friendship.UpdateFriendship
{
    public class UpdateFriendshipCommandHandler : IRequestHandler<UpdateFriendshipCommandRequest, UpdateFriendshipCommandResponse>
    {
        private readonly IFriendshipService _friendshipService;
        public UpdateFriendshipCommandHandler(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }
        public async Task<UpdateFriendshipCommandResponse> Handle(UpdateFriendshipCommandRequest request, CancellationToken cancellationToken)
        {
            return new UpdateFriendshipCommandResponse
            {
                Succeeded = await _friendshipService.UpdateFriendship(new Abstractions.DTOs.Friendship.UpdateFriendship
                {
                    Id = request.Id,
                    Status = request.Status,
                })
            };
        }
    }
}
