using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Repositories;
using ZappyAPI.Application.ViewModels.StarMessage;
using ZappyAPI.Domain.Entities;
using ZappyAPI.Persistence.Contexts;

namespace ZappyAPI.Persistence.Repositories
{
    public class StarredMessageReadRepository : ReadRepository<StarredMessage>, IStarredMessageReadRepository
    {
        readonly ZappyAPIDbContext _context;
        public StarredMessageReadRepository(ZappyAPIDbContext context) : base(context)
        {
            _context = context;
        }

        private DbSet<StarredMessage> Table => _context.Set<StarredMessage>();

        public async Task<List<StarredMessageViewModel>> GetUsersStarredMessagesAsync(Guid userId)
        {
            var starredMessages = await _context.StarredMessages
                .Where(sm => sm.UserId == userId)
                .Include(sm => sm.Message)
                    .ThenInclude(m => m.Group)
                .Include(sm => sm.Message.Sender)
                .Select(sm => new StarredMessageViewModel
                {
                    MessageId = sm.MessageId,
                    GroupId = sm.Message.Group.Id,
                    Content = sm.Message.EncryptedContent,
                    CreatedDate = sm.Message.CreatedDate,
                    SenderUsernam = sm.Message.Sender.Username
                })
                .ToListAsync();

            return starredMessages;
        }

    }
}
