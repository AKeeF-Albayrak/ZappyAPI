using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities;

namespace ZappyAPI.Application.Repositories
{
    public interface IUserStatusWriteRepository : IWriteRepository<UserStatus>
    {
        Task<bool> OnlineAsync(Guid userId);
        Task<bool> OfflineAsync(Guid userId);
    }
}
