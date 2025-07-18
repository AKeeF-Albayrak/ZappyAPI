﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ZappyAPI.Persistence.Contexts;

#nullable disable

namespace ZappyAPI.Persistence.Migrations
{
    [DbContext(typeof(ZappyAPIDbContext))]
    partial class ZappyAPIDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ZappyAPI.Domain.Entities.AuditLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Action")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TargetId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TargetType")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.Friendship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserAId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserBId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserAId");

                    b.HasIndex("UserBId");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.FriendshipRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("friendshipRequests");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GroupPicPath")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.GroupReadStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastReadAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupReadStatuses");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.LoginHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("IpAdress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Succeeded")
                        .HasColumnType("boolean");

                    b.Property<string>("UserAgent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("LoginHistories");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ContentType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EncryptedContent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPinned")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("RepliedMessageId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("RepliedMessageId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.Participant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Revoked")
                        .HasColumnType("boolean");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.Report", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Reason")
                        .HasColumnType("integer");

                    b.Property<Guid>("ReporterUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("TargetContentId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TargetContentSnapshot")
                        .HasColumnType("text");

                    b.Property<int>("TargetType")
                        .HasColumnType("integer");

                    b.Property<Guid>("TargetUserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ReporterUserId");

                    b.HasIndex("TargetUserId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeviceInfo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IpAdress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastActivityDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("RefreshTokenId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RefreshTokenId");

                    b.HasIndex("UserId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.StarredMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.HasIndex("UserId");

                    b.ToTable("StarredMessages");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfilePicPath")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.UserStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConnectionId")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastSeen")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserStatuses");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.AuditLog", b =>
                {
                    b.HasOne("ZappyAPI.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.Friendship", b =>
                {
                    b.HasOne("ZappyAPI.Domain.Entities.User", "UserA")
                        .WithMany("FriendshipsA")
                        .HasForeignKey("UserAId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ZappyAPI.Domain.Entities.User", "UserB")
                        .WithMany("FriendshipsB")
                        .HasForeignKey("UserBId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UserA");

                    b.Navigation("UserB");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.FriendshipRequest", b =>
                {
                    b.HasOne("ZappyAPI.Domain.Entities.User", "Receiver")
                        .WithMany("FriendshipRequestsReceived")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ZappyAPI.Domain.Entities.User", "Sender")
                        .WithMany("FriendshipRequestsSent")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.GroupReadStatus", b =>
                {
                    b.HasOne("ZappyAPI.Domain.Entities.Group", "Group")
                        .WithMany("GroupReadStatuses")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZappyAPI.Domain.Entities.User", "User")
                        .WithMany("groupReadStatuses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.LoginHistory", b =>
                {
                    b.HasOne("ZappyAPI.Domain.Entities.User", "User")
                        .WithMany("LoginHistories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.Message", b =>
                {
                    b.HasOne("ZappyAPI.Domain.Entities.Group", "Group")
                        .WithMany("Messages")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZappyAPI.Domain.Entities.Message", "RepliedMessage")
                        .WithMany()
                        .HasForeignKey("RepliedMessageId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("ZappyAPI.Domain.Entities.User", "Sender")
                        .WithMany("Messages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("RepliedMessage");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.Notification", b =>
                {
                    b.HasOne("ZappyAPI.Domain.Entities.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.Participant", b =>
                {
                    b.HasOne("ZappyAPI.Domain.Entities.Group", "Group")
                        .WithMany("Participants")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZappyAPI.Domain.Entities.User", "User")
                        .WithMany("Participants")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("ZappyAPI.Domain.Entities.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.Report", b =>
                {
                    b.HasOne("ZappyAPI.Domain.Entities.User", "ReporterUser")
                        .WithMany("ReportsMade")
                        .HasForeignKey("ReporterUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZappyAPI.Domain.Entities.User", "TargetUser")
                        .WithMany("ReportsReceived")
                        .HasForeignKey("TargetUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReporterUser");

                    b.Navigation("TargetUser");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.Session", b =>
                {
                    b.HasOne("ZappyAPI.Domain.Entities.RefreshToken", "RefreshToken")
                        .WithMany()
                        .HasForeignKey("RefreshTokenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZappyAPI.Domain.Entities.User", "User")
                        .WithMany("Sessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RefreshToken");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.StarredMessage", b =>
                {
                    b.HasOne("ZappyAPI.Domain.Entities.Message", "Message")
                        .WithMany("StarredMessages")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZappyAPI.Domain.Entities.User", "User")
                        .WithMany("starredMessages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Message");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.User", b =>
                {
                    b.HasOne("ZappyAPI.Domain.Entities.User", null)
                        .WithMany("Friends")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.UserStatus", b =>
                {
                    b.HasOne("ZappyAPI.Domain.Entities.User", "User")
                        .WithOne("UserStatus")
                        .HasForeignKey("ZappyAPI.Domain.Entities.UserStatus", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.Group", b =>
                {
                    b.Navigation("GroupReadStatuses");

                    b.Navigation("Messages");

                    b.Navigation("Participants");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.Message", b =>
                {
                    b.Navigation("StarredMessages");
                });

            modelBuilder.Entity("ZappyAPI.Domain.Entities.User", b =>
                {
                    b.Navigation("Friends");

                    b.Navigation("FriendshipRequestsReceived");

                    b.Navigation("FriendshipRequestsSent");

                    b.Navigation("FriendshipsA");

                    b.Navigation("FriendshipsB");

                    b.Navigation("LoginHistories");

                    b.Navigation("Messages");

                    b.Navigation("Notifications");

                    b.Navigation("Participants");

                    b.Navigation("RefreshTokens");

                    b.Navigation("ReportsMade");

                    b.Navigation("ReportsReceived");

                    b.Navigation("Sessions");

                    b.Navigation("UserStatus")
                        .IsRequired();

                    b.Navigation("groupReadStatuses");

                    b.Navigation("starredMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
