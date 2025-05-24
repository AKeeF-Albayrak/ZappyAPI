using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities;

namespace ZappyAPI.Persistence.Contexts
{
    public class ZappyAPIDbContext : DbContext
    {
        public ZappyAPIDbContext(DbContextOptions<ZappyAPIDbContext> options) : base(options) { }

        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<LoginHistory> LoginHistories { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<GroupReadStatus> GroupReadStatuses { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserStatus> UserStatuses { get; set; }
        public DbSet<StarredMessage> StarredMessages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuditLog>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.User_1)
                .WithMany(u => u.Friendships_1)
                .HasForeignKey(f => f.UserId_1)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.User_2)
                .WithMany(u => u.Friendships_2)
                .HasForeignKey(f => f.UserId_2)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LoginHistory>()
                .HasOne(l => l.User)
                .WithMany(u => u.LoginHistories)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Group)
                .WithMany(g => g.Messages)
                .HasForeignKey(m => m.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.RepliedMessage)
                .WithMany()
                .HasForeignKey(m => m.RepliedMessageId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Participant>()
                .HasOne(p => p.Group)
                .WithMany(g => g.Participants)
                .HasForeignKey(p => p.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Participant>()
                .HasOne(p => p.User)
                .WithMany(u => u.Participants)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RefreshToken>()
                .HasOne(r => r.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.ReporterUser)
                .WithMany(u => u.ReportsMade)
                .HasForeignKey(r => r.ReporterUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.TargetUser)
                .WithMany(u => u.ReportsReceived)
                .HasForeignKey(r => r.TargetUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.User)
                .WithMany(u => u.Sessions)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.RefreshToken)
                .WithMany()
                .HasForeignKey(s => s.RefreshTokenId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserStatus>()
                .HasOne(us => us.User)
                .WithOne(u => u.UserStatus)
                .HasForeignKey<UserStatus>(us => us.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StarredMessage>()
                .HasOne(sm => sm.Message)
                .WithMany(m => m.StarredMessages)
                .HasForeignKey(sm => sm.MessageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StarredMessage>()
                .HasOne(sm => sm.User)
                .WithMany(u => u.starredMessages)
                .HasForeignKey(sm => sm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupReadStatus>()
                .HasOne(grs => grs.User)
                .WithMany(u => u.groupReadStatuses)
                .HasForeignKey(grs => grs.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupReadStatus>()
                .HasOne(grs => grs.Group)
                .WithMany(g => g.GroupReadStatuses)
                .HasForeignKey(grs => grs.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
