using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Group = ZappyAPI.Domain.Entities.Group;

namespace ZappyAPI.Application.Repositories
{
    public interface IGroupReadRepository : IReadRepository<Group>
    {
    }
}
