using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.Friendship;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Application.Repositories;

namespace ZappyAPI.Persistence.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipWriteRepository _friendshipWriteRepository;
        private readonly IFriendshipReadRepository _friendshipReadRepository;
        private readonly IUserContext _userContext;
        public FriendshipService(IFriendshipWriteRepository friendshipWriteRepository, IFriendshipReadRepository friendshipReadRepository, IUserContext userContext)
        {
            _friendshipWriteRepository = friendshipWriteRepository;
            _friendshipReadRepository = friendshipReadRepository;
            _userContext = userContext;
        }

        public async Task<bool> CreateFriendship(CreateFriendship model)
        {
            if (_userContext.UserId == null || !(_userContext.UserId == model.UserId_1 || _userContext.UserId == model.UserId_2))
            {
                return false;
            }
            await _friendshipWriteRepository.AddAsync(new Domain.Entities.Friendship
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                Status = Domain.Enums.FriendshipStatus.Pending,
                UserId_1 = model.UserId_1,
                UserId_2 = model.UserId_2,
            });

            int affected_rows = await _friendshipWriteRepository.SaveAsync();

            return affected_rows > 0;
        }

        public async Task<bool> UpdateFriendship(UpdateFriendship model)
        {
            var friendship = await _friendshipReadRepository.GetByIdAsync(model.Id);

            if (friendship == null)
            {
                return false;
            }

            if (_userContext.UserId == null || !(_userContext.UserId == friendship.UserId_1 || _userContext.UserId == friendship.UserId_2))
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
