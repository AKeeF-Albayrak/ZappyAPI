using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Application.Repositories;
using ZappyAPI.Application.ViewModels.Group;
using ZappyAPI.Persistence.Repositories;

namespace ZappyAPI.Persistence.Services
{
    public class GroupService : IGroupService
    {
        private readonly IParticipantReadRepository _participantReadRepository;
        private readonly IMessageReadRepository _messageReadRepository;
        private readonly IStorageService _storageService;
        public GroupService(IParticipantReadRepository participantReadRepository,IMessageReadRepository messageReadRepository, IStorageService storageService)
        {
            _participantReadRepository = participantReadRepository;
            _messageReadRepository = messageReadRepository;
            _storageService = storageService;
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
