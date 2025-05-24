using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.Group;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Application.Repositories;
using ZappyAPI.Application.ViewModels.Group;
using ZappyAPI.Application.ViewModels.Message;
using ZappyAPI.Application.ViewModels.User;
using ZappyAPI.Domain.Entities;
using ZappyAPI.Domain.Enums;
using ZappyAPI.Persistence.Repositories;

namespace ZappyAPI.Persistence.Services
{
    public class GroupService : IGroupService
    {
        private readonly IParticipantReadRepository _participantReadRepository;
        private readonly IMessageReadRepository _messageReadRepository;
        private readonly IStorageService _storageService;
        private readonly IGroupWriteRepository _groupWriteRepository;
        private readonly IGroupReadRepository _groupReadRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserContext _userContext;
        private readonly IUserStatusReadRepository _userStatusReadRepository;
        private readonly IChatHubService _chatHubService;
        private readonly IMessageReadReadRepository _messageReadReadRepository;
        private readonly IStarredMessageReadRepository _starredMessageReadRepository;
        public GroupService(IParticipantReadRepository participantReadRepository,IMessageReadRepository messageReadRepository, IStorageService storageService, IGroupWriteRepository groupWriteRepository, IGroupReadRepository groupReadRepository, IUserReadRepository userReadRepository, IUserContext userContext,  IUserStatusReadRepository userStatusReadRepository, IChatHubService chatHubService, IMessageReadReadRepository messageReadReadRepository, IStarredMessageReadRepository starredMessageReadRepository)
        {
            _participantReadRepository = participantReadRepository;
            _messageReadRepository = messageReadRepository;
            _storageService = storageService;
            _groupWriteRepository = groupWriteRepository;
            _groupReadRepository = groupReadRepository;
            _userContext = userContext;
            _userReadRepository = userReadRepository;
            _chatHubService = chatHubService;
            _messageReadReadRepository = messageReadReadRepository;
            _starredMessageReadRepository = starredMessageReadRepository;
        }

        public async Task<bool> CreateGroup(CreateGroup createGroup)
        {
            if(_userContext.UserId == null || createGroup.UserId != _userContext.UserId)
            {
                return false;
            }
            List<Participant> participants = new List<Participant>();
            Guid groupId = Guid.NewGuid();

            participants.Add(new Participant
            {
                CreatedDate = DateTime.UtcNow,
                UserId = createGroup.UserId,
                GroupId = groupId,
                Role = Domain.Enums.ParticipantRole.Admin,
                Id = Guid.NewGuid(),
                IsDeleted = false,
            });

            var userIds = await _userReadRepository.GetUserIdsAsync(createGroup.Usernames);

            foreach(var userId in userIds)
            {
                participants.Add(new Participant
                {
                    CreatedDate = DateTime.UtcNow,
                    UserId = userId,
                    GroupId = groupId,
                    Role = Domain.Enums.ParticipantRole.Normal,
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                });
            }

            var res = await _storageService.UploadAsync(new Application.Abstractions.DTOs.Storage.Upload
            {
                FileBytes = createGroup.GroupPicture.ToString(),
                FileName = "0_1",
                ContentType = createGroup.ContentType,
            });

            await _groupWriteRepository.AddAsync(new Group
            {
                Id = groupId,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
                GroupName = createGroup.GroupName,
                Description = createGroup.Description,
                GroupPicPath = res.FilePath,
                Participants = participants
            });

            var affected_rows = await _groupWriteRepository.SaveAsync();
            return affected_rows > 1;
        }

        public async Task<GetGroupResponse> GetGroup(Guid groupId)
        {
            var userId = _userContext.UserId;

            if (userId == null)
            {
                return new GetGroupResponse
                {
                    Succeeded = false
                };
            }

            var group = await _groupReadRepository.GetByIdAsync(groupId);

            if (group == null)
            {
                return new GetGroupResponse
                {
                    Succeeded = false
                };
            }

            var isParticipant = group.Participants.Any(p => p.UserId == userId);
            if (!isParticipant)
            {
                return new GetGroupResponse
                {
                    Succeeded = false
                };
            }

            List<UserViewModel> users = new List<UserViewModel>();
            List<MessageViewModel> messages = new List<MessageViewModel>();

            foreach (var message in group.Messages)
            {
                messages.Add(new MessageViewModel
                {
                    Id = message.Id,
                    ContentType = message.ContentType,
                    CreatedDate = message.CreatedDate,
                    EncryptedContent = message.EncryptedContent,
                    IsPinned = message.IsPinned,
                    IsStarreed = message.StarredMessages.Any(sm => sm.UserId == userId),
                    RepliedMessageId = message.RepliedMessageId,
                    SenderName = message.Sender.Username,
                    IsUser = message.SenderId == userId,
                });
            }

            foreach (var participant in group.Participants)
            {
                var user = await _userReadRepository.GetByIdAsync(participant.UserId);
                users.Add(new UserViewModel
                {
                    isOnline = false,
                    Username = user.Username,
                    ProfilePicture = await _storageService.GetAsync(user.ProfilePicPath)
                });
            }

            // TODO: Change IsOnline With SignalR Hub

            return new GetGroupResponse
            {
                Succeeded = true,
                Id = groupId,
                GroupPicture = await _storageService.GetAsync(group.GroupPicPath),
                Name = group.GroupName,
                Messages = messages,
                Users = users
            };
        }


        public async Task<GetGroupsResponse> GetGroups(Guid userId)
        {
            if (_userContext.UserId == null || userId != _userContext.UserId)
            {
                return new GetGroupsResponse { Succeeded = false };
            }

            var userGroups = await _participantReadRepository.GetUsersGroupsAsync(userId);
            var userStatus = await _userStatusReadRepository.GetByUserIdAsync(userId);
            var connectionId = userStatus?.ConnectionId;

            var groupViewModels = new List<GroupsViewModel>();

            foreach (var group in userGroups)
            {
                var messageDto = await _messageReadRepository.GetLastMessageByGroupIdAsync(group.Id);

                string content = "";

                switch (messageDto.ContentType)
                {
                    case MessageContentType.Audio:
                        content = "Audio";
                        break;
                    
                    case MessageContentType.Picture:
                        content = "Picture";
                        break;

                    default:
                        break;
                }
                    
                var viewModel = new GroupsViewModel
                {
                    GroupId = group.Id,
                    GroupPhoto = await _storageService.GetAsync(group.GroupPicPath),
                    GroupName = group.GroupName,
                    LastMessage = new LastMessageViewModel
                    {
                        SenderName = messageDto.SenderName,
                        Id = messageDto.Id,
                        CreatedDate = messageDto.CreatedDate,
                        Content = content,
                        IsUser = messageDto.SenderId == userId,
                    }
                };

                groupViewModels.Add(viewModel);

                if (!string.IsNullOrWhiteSpace(connectionId))
                    await _chatHubService.JoinGroup(group.Id, connectionId);
            }

            return new GetGroupsResponse
            {
                Succeeded = true,
                Groups = groupViewModels
            };
        }

    }
}
