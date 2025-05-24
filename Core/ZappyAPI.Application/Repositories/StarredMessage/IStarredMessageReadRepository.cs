using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.ViewModels.StarMessage;
using ZappyAPI.Domain.Entities;

namespace ZappyAPI.Application.Repositories
{
    public interface IStarredMessageReadRepository : IReadRepository<StarredMessage>
    {
        public Task<List<StarredMessageViewModel>> GetUsersStarredMessagesAsync(Guid userId);
    }
}
