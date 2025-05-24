using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Repositories;
using ZappyAPI.Domain.Entities;
using ZappyAPI.Persistence.Contexts;

namespace ZappyAPI.Persistence.Repositories
{
    public class StarredMessageWriteRepository : WriteRepository<StarredMessage>, IStarredMessageWriteRepository
    {
        readonly ZappyAPIDbContext _context;
        public StarredMessageWriteRepository(ZappyAPIDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
