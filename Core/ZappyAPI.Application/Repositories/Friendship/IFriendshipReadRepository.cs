using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Application.Repositories
{
    public interface IFriendshipReadRepository : IReadRepository<Friendship>
    {
        public Task<List<Friendship>> GetUsersFriendsAsync(Guid userId, FriendshipStatus status);
    }
}
