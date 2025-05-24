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
    public class GroupReadStatusWriteRepository : WriteRepository<GroupReadStatus>, IGroupReadStatusWriteRepository
    {
        private readonly ZappyAPIDbContext _context;
        public GroupReadStatusWriteRepository(ZappyAPIDbContext context) : base(context)
        {
            _context = context;
        }
        public DbSet<GroupReadStatus> Table => _context.Set<GroupReadStatus>();

        public async Task<bool> ReadMessagesAsync(Guid groupId, Guid userId)
        {
            var latestMessage = await _context.Messages
                .Where(m => m.GroupId == groupId)
                .OrderByDescending(m => m.CreatedDate)
                .FirstOrDefaultAsync();

            if (latestMessage == null)
                return false;

            var existingStatus = await _context.GroupReadStatuses
                .FirstOrDefaultAsync(grs => grs.UserId == userId && grs.GroupId == groupId);

            if (existingStatus != null)
            {
                existingStatus.LastReadAt = latestMessage.CreatedDate;
                _context.GroupReadStatuses.Update(existingStatus);
            }
            else
            {
                var newStatus = new GroupReadStatus
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    GroupId = groupId,
                    LastReadAt = latestMessage.CreatedDate,
                    CreatedDate = DateTime.UtcNow
                };
                await _context.GroupReadStatuses.AddAsync(newStatus);
            }

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
