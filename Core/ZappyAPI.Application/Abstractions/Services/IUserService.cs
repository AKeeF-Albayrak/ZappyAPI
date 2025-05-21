using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.User;
using ZappyAPI.Application.ViewModels.User;

namespace ZappyAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);
        Task<LoginUserResponse> LoginUserAsync(string userName, string password);
        Task<GetUserResponse> GetUserByIdAsync();
        Task<bool> UpdateUserAsync(UpdateUser model);
    }
}
