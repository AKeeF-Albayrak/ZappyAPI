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
    public class UserReadRepository : ReadRepository<User>, IUserReadRepository
    {
        private readonly ZappyAPIDbContext _context;
        public UserReadRepository(ZappyAPIDbContext context) : base(context)
        {
            _context = context;
        }
        public DbSet<User> Table => _context.Set<User>();

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await Table.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<List<Guid>> GetUserIdsAsync(List<string> usernames)
        {
            return await Table
                .Where(u => usernames.Contains(u.Username))
                .Select(u => u.Id)
                .ToListAsync();
        }
    }
}
