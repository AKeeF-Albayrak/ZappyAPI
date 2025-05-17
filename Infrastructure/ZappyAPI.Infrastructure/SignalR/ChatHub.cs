using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Infrastructure.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IUserStatusService _userStatusService;

        public ChatHub(IUserStatusService userStatusService)
        {
            _userStatusService = userStatusService;
        }

        public override async Task OnConnectedAsync()
        {
            var userIdClaim = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(userIdClaim, out var userId))
            {
                var connectionId = Context.ConnectionId;
                await _userStatusService.UpdateUserStatusAsync(userId, Domain.Enums.User_Status.Online);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userIdClaim = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(userIdClaim, out var userId))
            {
                await _userStatusService.UpdateUserStatusAsync(userId, Domain.Enums.User_Status.Offline);
            }

            await base.OnDisconnectedAsync(exception);
        }

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
