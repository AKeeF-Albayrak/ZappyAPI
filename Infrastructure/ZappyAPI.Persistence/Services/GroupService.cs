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
        public GroupService(IParticipantReadRepository participantReadRepository,IMessageReadRepository messageReadRepository, IStorageService storageService, IGroupWriteRepository groupWriteRepository, IGroupReadRepository groupReadRepository, IUserReadRepository userReadRepository)
        {
            _participantReadRepository = participantReadRepository;
            _messageReadRepository = messageReadRepository;
            _storageService = storageService;
            _groupWriteRepository = groupWriteRepository;
            _groupReadRepository = groupReadRepository;
        }

        public async Task<bool> CreateGroup(CreateGroup createGroup)
        {
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
            var group = await _groupReadRepository.GetByIdAsync(groupId);

            if (group == null)
            {
                return new GetGroupResponse{
                    Succeeded = false,
                };
            }

            List<UserViewModel> users = new List<UserViewModel>();
            List<MessageViewModel> messages = new List<MessageViewModel>();

            foreach(var message in group.Messages)
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

            foreach(var participant in group.Participants)
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
            // TODO: Change IsOnline With Signlr Hub

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

        public async Task<List<GroupViewModel>> GetGroups(Guid userId)
        {
            
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

            return groupViewModels;
        }
        // TODO: Change GroupViewModel TO DTO   
    }
}
