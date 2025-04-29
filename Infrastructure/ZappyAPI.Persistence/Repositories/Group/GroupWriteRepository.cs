using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Repositories;
using ZappyAPI.Persistence.Contexts;
using Group = ZappyAPI.Domain.Entities.Group;

namespace ZappyAPI.Persistence.Repositories
{
    public class GroupWriteRepository : WriteRepository<Group>, IGroupWriteRepository
    {
        private readonly ZappyAPIDbContext _context;
        public GroupWriteRepository(ZappyAPIDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
