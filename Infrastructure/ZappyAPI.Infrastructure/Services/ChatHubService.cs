using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Application.Repositories;
using ZappyAPI.Infrastructure.SignalR;

namespace ZappyAPI.Infrastructure.Services
{
    public class ChatHubService : IChatHubService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IParticipantReadRepository _participantReadRepository;
        private readonly IUserContext _userContext;

        public ChatHubService(IHubContext<ChatHub> hubContext, IParticipantReadRepository participantReadRepository, IUserContext userContext)
        {
            _hubContext = hubContext;
            _participantReadRepository = participantReadRepository;
            _userContext = userContext;
        }

        public async Task<bool> SendMessageToGroup(Guid groupId, string message, string? sender = "Sunucu")
        {
            var timestamp = DateTime.UtcNow.ToString("HH:mm:ss");
            await _hubContext.Clients.Group(groupId.ToString()).SendAsync("ReceiveMessage", new
            {
                user = sender,
                message,
                time = timestamp
            });
            return true;
        }

        public async Task<bool> JoinGroup(Guid groupId, string connectionId, string? username = "Sistem")
        {
            var userId = _userContext.UserId;

            if (userId == null) return false;

            var isMember = await _participantReadRepository.CheckUserGroupAsync(userId, groupId);
            if (!isMember)
                return false;

            await _hubContext.Groups.AddToGroupAsync(connectionId, groupId.ToString());
            await _hubContext.Clients.Group(groupId.ToString())
                .SendAsync("ReceiveMessage", $"📢 {username} (sunucu tarafından) gruba katıldı.");
            
            return true;
        }

        public async Task<bool> LeaveGroup(Guid groupId, string connectionId, string? username = "Sistem")
        {
            var userId = _userContext.UserId;

            if (userId == null) return false;

            var isMember = await _participantReadRepository.CheckUserGroupAsync(userId, groupId);
            if (!isMember)
                return false;

            await _hubContext.Groups.RemoveFromGroupAsync(connectionId, groupId.ToString());
            await _hubContext.Clients.Group(groupId.ToString())
                .SendAsync("ReceiveMessage", $"👋 {username} (sunucu tarafından) gruptan ayrıldı.");

            return true;
        }
    }

}
