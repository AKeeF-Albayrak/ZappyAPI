using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Repositories;
using ZappyAPI.Domain.Entities;
using ZappyAPI.Domain.Enums;
using ZappyAPI.Persistence.Contexts;

namespace ZappyAPI.Persistence.Repositories
{
    public class FriendshipReadRepository : ReadRepository<Friendship>, IFriendshipReadRepository
    {
        private readonly ZappyAPIDbContext _context;
        public FriendshipReadRepository(ZappyAPIDbContext context) : base(context)
        {
            _context = context;
        }

        public DbSet<Friendship> Table => _context.Set<Friendship>();
        public Task<List<Friendship>> GetUsersFriendsAsync(Guid userId, FriendshipStatus status)
        {
            return Table
                .Where(f => (f.UserId_1 == userId || f.UserId_2 == userId) && f.Status == status)
                .ToListAsync();
        }
    }
}
