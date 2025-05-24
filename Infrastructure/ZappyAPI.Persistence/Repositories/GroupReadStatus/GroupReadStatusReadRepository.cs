using Microsoft.EntityFrameworkCore;
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
    public class GroupReadStatusReadRepository : ReadRepository<GroupReadStatus>, IGroupReadStatusReadRepository
    {
        private readonly ZappyAPIDbContext _context;
        public GroupReadStatusReadRepository(ZappyAPIDbContext context) : base(context)
        {
            _context = context;
        }
        public DbSet<GroupReadStatus> Table => _context.Set<GroupReadStatus>();

        public Task<List<GroupReadStatus>> GetReadMessagesAsync(Guid groupId)
        {
            return Table
                .Where(grs => grs.GroupId == groupId)
                .ToListAsync();
        }
    }
}
