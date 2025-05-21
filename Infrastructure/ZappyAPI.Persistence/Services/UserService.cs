using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.User;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Application.Repositories;
using ZappyAPI.Application.ViewModels.User;
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
        readonly IUserContext _userContext;
        readonly IStorageService _storageService;
        public UserService(IUserWriteRepository userWriteRepository, IUserStatusWriteRepository userStatusWriteRepository, IUserReadRepository userReadRepository, HashPassword hashPassword, IUserContext userContext, IStorageService storageService)
        {
            _userWriteRepository = userWriteRepository;
            _userStatusWriteRepository = userStatusWriteRepository;
            _userReadRepository = userReadRepository;
            _hashPassword = hashPassword;
            _userContext = userContext;
            _storageService = storageService;
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
                BirthDate = model.BirthDate,
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

        public async Task<GetUserResponse> GetUserByIdAsync()
        {
            var userId = _userContext.UserId;
            if (userId == null) return null;
            var user = await _userReadRepository.GetByIdAsync((Guid)_userContext.UserId);

            return new GetUserResponse
            {
                Succeeded = user != null,
                User = new UserViewModel
                {
                    Username = user.Username,
                    isOnline = true,
                    ProfilePicture = await _storageService.GetAsync(user.ProfilePicPath),

                }
            };
        }

        public async Task<LoginUserResponse> LoginUserAsync(string userName, string password)
        {
            var user = await _userReadRepository.GetUserByUsernameAsync(userName);

            if(user != null && await _hashPassword.VerifyPasswordAsync(password, user.HashedPassword))
            {
                await _userStatusWriteRepository.OnlineAsync(user.Id);
                await _userStatusWriteRepository.SaveAsync();
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

        public async Task<bool> UpdateUserAsync(UpdateUser model)
        {
            var userId = _userContext.UserId;
            if (userId == null)
            {
                return false;
            }
            var user = await _userReadRepository.GetByIdAsync((Guid)userId);

            if (user != null)
            {
                return false;
            }

            user.Name = model.Name;
            user.Username = model.Username;
            user.Mail = model.Mail;
            user.ProfilePicPath = model.ProfilePicPath;
            user.Description = model.Description;

            _userWriteRepository.Update(user);
            return await _userStatusWriteRepository.SaveAsync() > 0;
        }
    }
}
