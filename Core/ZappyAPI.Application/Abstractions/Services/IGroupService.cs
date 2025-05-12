using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.Group;
using ZappyAPI.Application.ViewModels.Group;
using ZappyAPI.Domain.Entities;

namespace ZappyAPI.Application.Abstractions.Services
{
    public interface IGroupService
    {
        Task<List<GroupViewModel>> GetGroups(Guid userId); 
        Task<bool> CreateGroup(CreateGroup createGroup);
    }
}
