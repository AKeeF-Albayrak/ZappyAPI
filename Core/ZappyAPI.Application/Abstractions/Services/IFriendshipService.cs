using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.Friendship;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Application.Abstractions.Services
{
    public interface IFriendshipService
    {
        Task<bool> CreateFriendship(CreateFriendship model);
        Task<bool> UpdateFriendship(UpdateFriendship model);
    }
}
