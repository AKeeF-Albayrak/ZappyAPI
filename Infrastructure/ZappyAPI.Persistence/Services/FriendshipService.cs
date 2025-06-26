using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.Friendship;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Application.Repositories;
using ZappyAPI.Application.ViewModels.Friendship;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Persistence.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipWriteRepository _friendshipWriteRepository;
        private readonly IFriendshipReadRepository _friendshipReadRepository;
        private readonly IUserContext _userContext;
        private readonly IUserReadRepository _userReadRepository;
        public FriendshipService(IFriendshipWriteRepository friendshipWriteRepository, IFriendshipReadRepository friendshipReadRepository, IUserContext userContext, IUserReadRepository userReadRepository)
        {
            _friendshipWriteRepository = friendshipWriteRepository;
            _friendshipReadRepository = friendshipReadRepository;
            _userContext = userContext;
            _userReadRepository = userReadRepository;
        }

        public async Task<bool> CreateFriendship(CreateFriendship model)
        {
            var userId = _userContext.UserId;

            var user2Id = (await _userReadRepository.GetUserByUsernameAsync(model.Username)).Id;
            
            if (userId == null || user2Id == null)
            {
                return false;
            }
            await _friendshipWriteRepository.AddAsync(new Domain.Entities.Friendship
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                Status = Domain.Enums.FriendshipStatus.Accepted,
                UserAId = (Guid)userId,
                UserBId = user2Id,
            });

            int affected_rows = await _friendshipWriteRepository.SaveAsync();

            return affected_rows > 0;
        }

        public async Task<GetFriendResponse> GetFriends(FriendshipStatus status)
        {
            var userId = _userContext.UserId;
            if(userId == null) return new GetFriendResponse {
                Succeeded = false,
            };

            var friendships = await _friendshipReadRepository.GetUsersFriendsAsync((Guid)userId, status);
            if(friendships == null) return new GetFriendResponse { Succeeded = false };

            List<FriendViewModel> friends = new List<FriendViewModel>();

            foreach(var friendship in friendships)
            {
                if(friendship.UserAId == userId)
                {
                    friends.Add(new FriendViewModel
                    {
                        Username = friendship.UserB.Username,
                        Status = friendship.UserB.UserStatus.Status,
                    });
                }
                else
                {
                    friends.Add(new FriendViewModel
                    {
                        Username = friendship.UserA.Username,
                        Status = friendship.UserA.UserStatus.Status,
                    });
                }
            }

            return new GetFriendResponse
            {
                Succeeded = true,
                Friends = friends
            };
        }

        public async Task<bool> UpdateFriendship(UpdateFriendship model)
        {
            var friendship = await _friendshipReadRepository.GetByIdAsync(model.Id);

            if (friendship == null)
            {
                return false;
            }

            if (_userContext.UserId == null || !(_userContext.UserId == friendship.UserAId || _userContext.UserId == friendship.UserBId))
            {
                return false;
            }

            friendship.Status = model.Status;

            _friendshipWriteRepository.Update(friendship);

            int affected_rows = await _friendshipWriteRepository.SaveAsync();
            return affected_rows > 0;
        }

    }
}
