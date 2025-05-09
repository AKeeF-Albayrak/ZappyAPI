using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.User;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Application.Repositories;
using ZappyAPI.Domain.Entities;
using ZappyAPI.Infrastructure.Services;

namespace ZappyAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly IUserWriteRepository _userWriteRepository;
        readonly IUserReadRepository _userReadRepository;
        readonly IUserStatusWriteRepository _userStatusWriteRepository;
        readonly HashPassword _hashPassword;
        public UserService(IUserWriteRepository userWriteRepository, IUserStatusWriteRepository userStatusWriteRepository, IUserReadRepository userReadRepository, HashPassword hashPassword)
        {
            _userWriteRepository = userWriteRepository;
            _userStatusWriteRepository = userStatusWriteRepository;
            _userReadRepository = userReadRepository;
            _hashPassword = hashPassword;
        }
        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            Guid userId = Guid.NewGuid();
            var hashedPassword = await _hashPassword.HashPasswordAsync(model.Password);
            await _userWriteRepository.AddAsync(new User
            {
                Id = userId,
                Name = model.Name,
                Username = model.Username,
                HashedPassword = hashedPassword,
                Mail = model.Mail,
                ProfilePicPath = model.ProfilePicturePath,
                Description = model.Description,
                Age = model.Age,
                CreatedDate = DateTime.UtcNow,
            });

            int affectedRows = await _userWriteRepository.SaveAsync();

            if(affectedRows == 1)
            {
                await _userStatusWriteRepository.AddAsync(new UserStatus
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Status = Domain.Enums.User_Status.Offline,
                    CreatedDate = DateTime.UtcNow
                });

                await _userStatusWriteRepository.SaveAsync();
                return new CreateUserResponse
                {
                    Id = userId,
                    Succeeded = true,
                    Message = "User Successfully Added!"
                };
            }
            return new CreateUserResponse
            {
                Succeeded = false,
                Message = "An error occurred while adding the user!"
            };
        }

        public async Task<LoginUserResponse> LoginUserAsync(string userName, string password)
        {
            // TODO: Add UserStatus Online and Add Logout
            var user = await _userReadRepository.GetUserByUsernameAsync(userName);

            if(user != null && await _hashPassword.VerifyPasswordAsync(password, user.HashedPassword))
            {
                return new LoginUserResponse
                {
                    Succeeded = true,
                    UserId = user.Id,
                };
            }
            return new LoginUserResponse
            {
                Succeeded = false,
            };
        }
    }
}
