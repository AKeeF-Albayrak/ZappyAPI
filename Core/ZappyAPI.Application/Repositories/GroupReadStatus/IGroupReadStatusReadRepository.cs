using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities;

namespace ZappyAPI.Application.Repositories
{
    public interface IGroupReadStatusReadRepository : IReadRepository<GroupReadStatus>
    {
        public Task<List<GroupReadStatus>> GetReadMessagesAsync(Guid groupId);
    }
}
