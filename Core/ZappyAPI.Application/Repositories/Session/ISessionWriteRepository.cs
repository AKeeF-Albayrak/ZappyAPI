using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities;

namespace ZappyAPI.Application.Repositories
{
    public interface ISessionWriteRepository : IWriteRepository<Session>
    {
        public Task<Guid?> OfflineOthersAsync(Guid userId);
    }
}
