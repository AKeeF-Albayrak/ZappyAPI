﻿using Microsoft.EntityFrameworkCore;
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
    public class ParticipantReadRepository : ReadRepository<Participant>, IParticipantReadRepository
    {
        private readonly ZappyAPIDbContext _context;
        public ParticipantReadRepository(ZappyAPIDbContext context) : base(context)
        {
            _context = context;
        }

        private DbSet<Participant> Table => _context.Set<Participant>();

        public async Task<bool> CheckUserGroupAsync(Guid? userId, Guid groupId)
        {
            return await Table.AnyAsync(p => p.UserId == userId && p.GroupId == groupId);
        }

        public async Task<List<Group>> GetUsersGroupsAsync(Guid userId)
        {
            return await Table
                .Include(p => p.Group)
                .Where(p => p.UserId == userId && !p.IsDeleted && !p.Group.IsDeleted)
                .Select(p => p.Group)
                .ToListAsync();
        }
    }
}
