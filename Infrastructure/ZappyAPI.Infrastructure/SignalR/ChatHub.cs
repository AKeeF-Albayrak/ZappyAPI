using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Infrastructure.SignalR
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task JoinChat(Guid groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId.ToString());
            var username = Context.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Bilinmeyen";
            await Clients.Group(groupId.ToString()).SendAsync("ReceiveMessage", $"📢 {username} sohbete katıldı.");
        }

        public async Task LeaveChat(Guid groupId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId.ToString());
            var username = Context.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Bilinmeyen";
            await Clients.Group(groupId.ToString()).SendAsync("ReceiveMessage", $"👋 {username} sohbetten ayrıldı.");
        }

        public async Task SendMessage(Guid groupId, string message)
        {
            var username = Context.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Bilinmeyen";
            var timestamp = DateTime.UtcNow.ToString("HH:mm:ss");
            await Clients.Group(groupId.ToString()).SendAsync("ReceiveMessage", new
            {
                user = username,
                message,
                time = timestamp
            });
        }
    }
}
