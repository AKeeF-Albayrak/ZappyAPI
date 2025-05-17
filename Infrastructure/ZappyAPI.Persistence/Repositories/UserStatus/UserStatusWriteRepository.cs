using Microsoft.EntityFrameworkCore;
using ZappyAPI.Application.Repositories;
using ZappyAPI.Domain.Entities;
using ZappyAPI.Domain.Enums;
using ZappyAPI.Persistence.Contexts;
using ZappyAPI.Persistence.Repositories;

public class UserStatusWriteRepository : WriteRepository<UserStatus>, IUserStatusWriteRepository
{
    private readonly ZappyAPIDbContext _context;

    public UserStatusWriteRepository(ZappyAPIDbContext context) : base(context)
    {
        _context = context;
    }

    public DbSet<UserStatus> Table => _context.Set<UserStatus>();

    public async Task<bool> OfflineAsync(Guid userId)
    {
        var userStatus = await Table.FirstOrDefaultAsync(us => us.UserId == userId);

        if (userStatus == null)
        {
            userStatus = new UserStatus
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Status = User_Status.Offline,
                CreatedDate = DateTime.UtcNow,
                LastSeen = DateTime.UtcNow,
                ConnectionId = ""
            };
            await Table.AddAsync(userStatus);
        }
        else
        {
            userStatus.Status = User_Status.Offline;
        }

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> OnlineAsync(Guid userId)
    {
        var userStatus = await Table.FirstOrDefaultAsync(us => us.UserId == userId);

        if (userStatus == null)
        {
            userStatus = new UserStatus
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Status = User_Status.Online,
                CreatedDate = DateTime.UtcNow,
                LastSeen = DateTime.UtcNow,
            };
            await Table.AddAsync(userStatus);
        }
        else
        {
            userStatus.Status = User_Status.Online;
            userStatus.LastSeen = DateTime.UtcNow;
        }

        return await _context.SaveChangesAsync() > 0;
    }
}
