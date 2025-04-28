using System;
using System.Collections.Generic;
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
        public int? Age { get; set; }

        public ICollection<Message> Messages { get; set; }
        public ICollection<Friendship> Friendships_1 { get; set; }
        public ICollection<Friendship> Friendships_2 { get; set; }
        public ICollection<GroupInvite> InvitesSent { get; set; }
        public ICollection<GroupInvite> InvitesReceived { get; set; }
        public ICollection<Session> Sessions { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<LoginHistory> LoginHistories { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Participant> Participants { get; set; }
        public ICollection<Report> ReportsMade { get; set; }
        public ICollection<Report> ReportsReceived { get; set; }
        public UserStatus UserStatus { get; set; }
    }

}
