using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Abstractions.Services
{
    public interface IChatHubService
    {
        Task SendMessageToGroup(Guid groupId, string message, string? sender = "Sunucu");
        Task JoinGroup(Guid groupId, string connectionId, string? username = null);
        Task LeaveGroup(Guid groupId, string connectionId, string? username = null);
    }
}
