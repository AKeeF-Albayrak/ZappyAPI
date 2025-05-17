using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Application.Abstractions.Services
{
    public interface IUserStatusService
    {
        public Task<bool> UpdateUserStatusAsync(Guid userId, User_Status status);
        public Task<bool> UpdateConnectionIdAsync(Guid userId, string ConnectionId);
    }
}
