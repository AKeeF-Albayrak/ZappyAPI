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
    public class MessageReadReadRepository : ReadRepository<MessageRead>, IMessageReadReadRepository
    {
        private readonly ZappyAPIDbContext _context;
        public MessageReadReadRepository(ZappyAPIDbContext context) : base(context)
        {
            _context = context;
        }
        public DbSet<MessageRead> Table => _context.Set<MessageRead>();

        public Task<List<MessageRead>> GetReadMessagesAsync(Guid groupId)
        {
            return Table
                .Where(mr => mr.Message.GroupId == groupId)
                .ToListAsync();
        }
    }
}
