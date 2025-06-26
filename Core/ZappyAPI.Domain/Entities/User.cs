using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities.Common;

namespace ZappyAPI.Domain.Entities
{
    public class User : BaseEntity
    {
        public required string Name { get; set; }
        public required string Username { get; set; }
        public required string HashedPassword { get; set; }
        public required string Mail { get; set; }
        public string? ProfilePicPath { get; set; }
        public string? Description { get; set; }
        public DateOnly BirthDate { get; set; }

        public ICollection<Message> Messages { get; set; }
        public ICollection<Friendship> FriendshipsA { get; set; } = new List<Friendship>();
        public ICollection<Friendship> FriendshipsB { get; set; } = new List<Friendship>();

        public IEnumerable<User> Friends =>
            FriendshipsA.Select(f => f.UserB).Concat(FriendshipsB.Select(f => f.UserA));
        public ICollection<FriendshipRequest> FriendshipRequestsSent { get; set; } = new List<FriendshipRequest>();
        public ICollection<FriendshipRequest> FriendshipRequestsReceived { get; set; } = new List<FriendshipRequest>();
        public ICollection<Session> Sessions { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<LoginHistory> LoginHistories { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Participant> Participants { get; set; }
        public ICollection<Report> ReportsMade { get; set; }
        public ICollection<Report> ReportsReceived { get; set; }
        public UserStatus UserStatus { get; set; }
        public ICollection<StarredMessage> starredMessages { get; set; }
        public ICollection<GroupReadStatus> groupReadStatuses { get; set; }
    }
}
