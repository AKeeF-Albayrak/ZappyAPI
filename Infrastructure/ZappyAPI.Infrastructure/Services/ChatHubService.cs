using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Infrastructure.SignalR;

namespace ZappyAPI.Infrastructure.Services
{
    public class ChatHubService : IChatHubService
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatHubService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessageToGroup(Guid groupId, string message, string? sender = "Sunucu")
        {
            var timestamp = DateTime.UtcNow.ToString("HH:mm:ss");
            await _hubContext.Clients.Group(groupId.ToString()).SendAsync("ReceiveMessage", new
            {
                user = sender,
                message,
                time = timestamp
            });
        }

        public async Task JoinGroup(Guid groupId, string connectionId, string? username = "Sistem")
        {
            await _hubContext.Groups.AddToGroupAsync(connectionId, groupId.ToString());
            await _hubContext.Clients.Group(groupId.ToString()).SendAsync("ReceiveMessage", $"📢 {username} (sunucu tarafından) gruba katıldı.");
        }

        public async Task LeaveGroup(Guid groupId, string connectionId, string? username = "Sistem")
        {
            await _hubContext.Groups.RemoveFromGroupAsync(connectionId, groupId.ToString());
            await _hubContext.Clients.Group(groupId.ToString()).SendAsync("ReceiveMessage", $"👋 {username} (sunucu tarafından) gruptan ayrıldı.");
        }
    }
}
