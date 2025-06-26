using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Application.Repositories;
using ZappyAPI.Persistence.Contexts;
using ZappyAPI.Persistence.Repositories;
using ZappyAPI.Persistence.Services;

namespace ZappyAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<ZappyAPIDbContextFactory>()
                .Build();

            services.AddDbContext<ZappyAPIDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Scoped
            );


            //Repositories

            services.AddScoped<IAuditLogReadRepository, AuditLogReadRepository>();
            services.AddScoped<IAuditLogWriteRepository, AuditLogWriteRepository>();

            services.AddScoped<IFriendshipReadRepository, FriendshipReadRepository>();
            services.AddScoped<IFriendshipWriteRepository, FriendshipWriteRepository>();

            services.AddScoped<IGroupReadRepository, GroupReadRepository>();
            services.AddScoped<IGroupWriteRepository, GroupWriteRepository>();

            services.AddScoped<ILoginHistoryReadRepository, LoginHistoryReadRepository>();
            services.AddScoped<ILoginHistoryWriteRepository, LoginHistoryWriteRepository>();

            services.AddScoped<IMessageReadRepository, MessageReadRepository>();
            services.AddScoped<IMessageWriteRepository, MessageWriteRepository>();

            services.AddScoped<IGroupReadStatusReadRepository, GroupReadStatusReadRepository>();
            services.AddScoped<IGroupReadStatusWriteRepository, GroupReadStatusWriteRepository>();

            services.AddScoped<INotificationReadRepository, NotificationReadRepository>();
            services.AddScoped<INotificationWriteRepository, NotificationWriteRepository>();

            services.AddScoped<IParticipantReadRepository, ParticipantReadRepository>();
            services.AddScoped<IParticipantWriteRepository, ParticipantWriteRepository>();

            services.AddScoped<IRefreshTokenReadRepository, RefreshTokenReadRepository>();
            services.AddScoped<IRefreshTokenWriteRepository, RefreshTokenWriteRepository>();

            services.AddScoped<IReportReadRepository, ReportReadRepository>();
            services.AddScoped<IReportWriteRepository, ReportWriteRepository>();

            services.AddScoped<ISessionReadRepository, SessionReadRepository>();
            services.AddScoped<ISessionWriteRepository, SessionWriteRepository>();

            services.AddScoped<IStarredMessageReadRepository, StarredMessageReadRepository>();
            services.AddScoped<IStarredMessageWriteRepository, StarredMessageWriteRepository>();

            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();

            services.AddScoped<IUserStatusReadRepository, UserStatusReadRepository>();
            services.AddScoped<IUserStatusWriteRepository, UserStatusWriteRepository>();

            services.AddScoped<IFriendshipRequestReadRepository, FriendshipRequestReadRepository>();
            services.AddScoped<IFriendshipRequestWriteRepository, FriendshipRequestWriteRepository>();

            //Services

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuditLogService, AuditLogService>();
            services.AddScoped<ILoginHistoryService, LoginHistoryService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IFriendshipService, FriendshipService>();
            services.AddScoped<IUserStatusService, UserStatusService>();
            services.AddScoped<IMessageService, MessageService>();

        }
    }
}
