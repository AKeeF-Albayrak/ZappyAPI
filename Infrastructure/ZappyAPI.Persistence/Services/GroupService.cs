using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.Group;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Application.Repositories;
using ZappyAPI.Application.ViewModels.Group;
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
        public GroupService(IParticipantReadRepository participantReadRepository,IMessageReadRepository messageReadRepository, IStorageService storageService, IGroupWriteRepository groupWriteRepository)
        {
            _participantReadRepository = participantReadRepository;
            _messageReadRepository = messageReadRepository;
            _storageService = storageService;
            _groupWriteRepository = groupWriteRepository;
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

            return new CreateGroupResponse
            {
                Succeeded = affected_rows > 1,
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

    }
}
