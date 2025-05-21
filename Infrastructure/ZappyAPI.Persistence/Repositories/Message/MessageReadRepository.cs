using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.Message;
using ZappyAPI.Application.Repositories;
using ZappyAPI.Domain.Entities;
using ZappyAPI.Persistence.Contexts;

namespace ZappyAPI.Persistence.Repositories
{
    public class MessageReadRepository : ReadRepository<Message>, IMessageReadRepository
    {
        private readonly ZappyAPIDbContext _context;
        public MessageReadRepository(ZappyAPIDbContext context) : base(context)
        {
            _context = context;
        }
        
        private DbSet<Message> Table => _context.Set<Message>();
        public async Task<MessageDto> GetLastMessageByGroupIdAsync(Guid groupId)
        {
            return await Table
                .Where(m => m.GroupId == groupId && !m.IsDeleted)
                .OrderByDescending(m => m.CreatedDate)
                .Select(m => new MessageDto
                {
                    Id = m.Id,
                    SenderId = m.SenderId,
                    ContentType = m.ContentType,
                    EncryptedContent = m.EncryptedContent,
                    SenderName = m.Sender.Name,
                    CreatedDate = m.CreatedDate,
                })
                .FirstOrDefaultAsync();
        }


    }
}
