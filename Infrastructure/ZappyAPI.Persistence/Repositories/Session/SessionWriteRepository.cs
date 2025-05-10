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
    public class SessionWriteRepository : WriteRepository<Session>, ISessionWriteRepository
    {
        private readonly ZappyAPIDbContext _context;
        public SessionWriteRepository(ZappyAPIDbContext context) : base(context)
        {
            _context = context;
        }

        public DbSet<Session> Table => _context.Set<Session>();

        public async Task<Guid?> OfflineOthersAsync(Guid userId)
        {
            var now = DateTime.UtcNow;

            var activeSessions = await Table
                .Where(s => s.UserId == userId && s.IsActive)
                .OrderByDescending(s => s.LastActivityDate)
                .ToListAsync();

            if (activeSessions.Count == 0)
                return null;

            var latestSession = activeSessions.First();

            foreach (var session in activeSessions.Skip(1))
            {
                session.IsActive = false;
                session.LastActivityDate = now;
            }

            await _context.SaveChangesAsync();

            return latestSession.RefreshTokenId;
        }
    }
}
