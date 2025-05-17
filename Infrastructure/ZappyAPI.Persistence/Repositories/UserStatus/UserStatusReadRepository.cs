using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Repositories;
using ZappyAPI.Domain.Entities;
using ZappyAPI.Persistence.Contexts;

namespace ZappyAPI.Persistence.Repositories
{
    public class UserStatusReadRepository : ReadRepository<UserStatus>, IUserStatusReadRepository
    {
        private readonly ZappyAPIDbContext _context;

        public UserStatusReadRepository(ZappyAPIDbContext context) : base(context)
        {
            _context = context;
        }

        public DbSet<UserStatus> Table => _context.Set<UserStatus>();

        public async Task<UserStatus?> GetByUserIdAsync(Guid userId)
        {
            return await Table.FirstOrDefaultAsync(us => us.UserId == userId);
        }
    }

}
