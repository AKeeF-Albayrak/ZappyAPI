using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.Message;
using ZappyAPI.Domain.Entities;

namespace ZappyAPI.Application.Repositories
{
    public interface IMessageReadRepository : IReadRepository<Message>
    {
        Task<MessageDto> GetLastMessageByGroupIdAsync(Guid groupId);
    }
}
