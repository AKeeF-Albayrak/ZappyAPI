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
    public class MessageReadWriteRepository : WriteRepository<MessageRead>, IMessageReadWriteRepository
    {
        private readonly ZappyAPIDbContext _context;
        public MessageReadWriteRepository(ZappyAPIDbContext context) : base(context)
        {
            _context = context;
        }
        public DbSet<MessageRead> Table => _context.Set<MessageRead>();

        public async Task<bool> ReadMessagesAsync(Guid groupId, Guid userId)
        {
            var groupMessages = await _context.Messages
                .Where(m => m.GroupId == groupId)
                .OrderByDescending(m => m.CreatedDate)
                .ToListAsync();

            foreach (var message in groupMessages)
            {
                bool alreadyRead = await _context.MessageReads
                    .AnyAsync(mr => mr.MessageId == message.Id && mr.UserId == userId);

                if (alreadyRead)
                {
                    break;
                }

                var readRecord = new MessageRead
                {
                    Id = Guid.NewGuid(),
                    MessageId = message.Id,
                    UserId = userId,
                    CreatedDate = DateTime.UtcNow
                };

                await _context.MessageReads.AddAsync(readRecord);
            }
            return true;
        }
    }
}
