using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.Group;
using ZappyAPI.Application.Abstractions.DTOs.GroupInvite;
using ZappyAPI.Application.ViewModels.Group;
using ZappyAPI.Domain.Entities;

namespace ZappyAPI.Application.Abstractions.Services
{
    public interface IGroupService
    {
        Task<GetGroupsResponse> GetGroups(Guid userId); 
        Task<GetGroupResponse> GetGroup(Guid groupId);
        Task<bool> CreateGroup(CreateGroup createGroup);
        Task<bool> InviteGroup(CreateGroupInvite model);
        Task<bool> RespondGroupInvite(RespondGroupInvite model);
    }
}
