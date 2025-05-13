using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Application.Features.Command.Friendship.CreateFriendship
{
    public class CreateFriendshipCommandHandler : IRequestHandler<CreateFriendshipCommandRequest, CreateFriendshipCommandResponse>
    {
        private readonly IFriendshipService _friendshipService;
        public CreateFriendshipCommandHandler(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }
        public async Task<CreateFriendshipCommandResponse> Handle(CreateFriendshipCommandRequest request, CancellationToken cancellationToken)
        {
            return new CreateFriendshipCommandResponse
            {
                Succeeded = await _friendshipService.CreateFriendship(new Abstractions.DTOs.Friendship.CreateFriendship
                {
                    UserId_1 = request.UserId_1,
                    UserId_2 = request.UserId_2,
                })
            };
        }
    }
}
