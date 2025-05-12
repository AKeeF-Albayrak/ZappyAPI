using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Repositories;
using ZappyAPI.Domain.Entities;
using ZappyAPI.Persistence.Contexts;
using Group = ZappyAPI.Domain.Entities.Group;

namespace ZappyAPI.Persistence.Repositories
{
    public class GroupReadRepository : ReadRepository<Group>, IGroupReadRepository
    {
        private readonly ZappyAPIDbContext _context;
        public GroupReadRepository(ZappyAPIDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
