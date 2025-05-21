using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities;

namespace ZappyAPI.Application.Repositories
{
    public interface IUserReadRepository : IReadRepository<User>
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<List<Guid>> GetUserIdsAsync(List<string> usernames);
    }

}
