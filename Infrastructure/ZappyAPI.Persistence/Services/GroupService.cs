using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.Group;
using ZappyAPI.Application.Abstractions.DTOs.GroupInvite;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Application.Repositories;
using ZappyAPI.Application.ViewModels.Group;
using ZappyAPI.Application.ViewModels.Message;
using ZappyAPI.Application.ViewModels.User;
using ZappyAPI.Domain.Entities;
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
        private readonly IGroupInviteWriteRepository _groupInviteWriteRepository;
        private readonly IGroupInviteReadRepository _groupInviteReadRepository;
        public GroupService(IParticipantReadRepository participantReadRepository,IMessageReadRepository messageReadRepository, IStorageService storageService, IGroupWriteRepository groupWriteRepository, IGroupReadRepository groupReadRepository, IUserReadRepository userReadRepository, IUserContext userContext, IGroupInviteWriteRepository groupInviteWriteRepository, IGroupInviteReadRepository groupInviteReadRepository)
        {
            _participantReadRepository = participantReadRepository;
            _messageReadRepository = messageReadRepository;
            _storageService = storageService;
            _groupWriteRepository = groupWriteRepository;
            _groupReadRepository = groupReadRepository;
            _userContext = userContext;
            _groupInviteWriteRepository = groupInviteWriteRepository;
            _groupInviteReadRepository = groupInviteReadRepository;
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

            foreach(var userId in createGroup.UserIds)
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
                    SenderId = message.SenderId,
                    ContentType = message.ContentType,
                    CreatedDate = message.CreatedDate,
                    EncryptedContent = message.EncryptedContent,
                    IsPinned = message.IsPinned,
                    RepliedMessageId = message.RepliedMessageId,
                });
            }

            foreach (var participant in group.Participants)
            {
                var user = await _userReadRepository.GetByIdAsync(participant.UserId);
                users.Add(new UserViewModel
                {
                    Id = user.Id,
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
                return new GetGroupsResponse
                {
                    Succeeded = false,
                };
            }
            var userGroups = await _participantReadRepository.GetUsersGroupsAsync(userId);

            var groupViewModels = new List<GroupViewModel>();

            foreach (var group in userGroups)
            {
                var lastMessage = await _messageReadRepository.GetLastMessageByGroupIdAsync(group.Id);

                var viewModel = new GroupViewModel
                {
                    GroupId = group.Id,
                    GroupPhoto = await _storageService.GetAsync(group.GroupPicPath),
                    GroupName = group.GroupName,
                    LastMessage = lastMessage
                };

                groupViewModels.Add(viewModel);
            }

            return new GetGroupsResponse
            {
                Succeeded = true,
                Groups = groupViewModels
            };
        }

        public async Task<bool> InviteGroup(CreateGroupInvite model)
        {
            var currentUserId = _userContext.UserId;
            if (currentUserId == null || currentUserId != model.InviterUserId)
                return false;

            var group = await _groupReadRepository.GetByIdAsync(model.GroupId);
            if (group == null || !group.Participants.Any(p => p.UserId == currentUserId))
                return false;

            await _groupInviteWriteRepository.AddAsync(new GroupInvite
            {
                Id = Guid.NewGuid(),
                GroupId = model.GroupId,
                Status = Domain.Enums.GroupInviteStatus.Pending,
                CreatedDate = DateTime.UtcNow,
                InvitedUserId = model.InvitedUserId,
                InviterUserId = model.InviterUserId,
            });

            int affected_rows = await _groupInviteWriteRepository.SaveAsync();
            return affected_rows > 0;
        }


        public async Task<bool> RespondGroupInvite(RespondGroupInvite model)
        {
            var currentUserId = _userContext.UserId;
            if (currentUserId == null)
                return false;

            var invite = await _groupInviteReadRepository.GetByIdAsync(model.Id);
            if (invite == null || invite.InvitedUserId != currentUserId)
                return false;

            if (model.Status != Domain.Enums.GroupInviteStatus.Pending && invite.Status == Domain.Enums.GroupInviteStatus.Pending)
            {
                invite.Status = model.Status;
                invite.RespondedDate = DateTime.UtcNow;
                _groupInviteWriteRepository.Update(invite);
                await _groupInviteWriteRepository.SaveAsync();
                return true;
            }

            return false;
        }

    }
}
