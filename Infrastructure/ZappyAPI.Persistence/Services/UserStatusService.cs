using System;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Application.Repositories;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Persistence.Services
{
    public class UserStatusService : IUserStatusService
    {
        private readonly IUserStatusWriteRepository _userStatusWriteRepository;
        private readonly IUserStatusReadRepository _userStatusReadRepository;
        private readonly IUserContext _userContext;

        public UserStatusService(IUserStatusWriteRepository userStatusWriteRepository, IUserContext userContext, IUserStatusReadRepository userStatusReadRepository)
        {
            _userStatusWriteRepository = userStatusWriteRepository;
            _userContext = userContext;
            _userStatusReadRepository = userStatusReadRepository;
        }

        public async Task<bool> UpdateConnectionIdAsync(Guid userId, string ConnectionId)
        {
            Guid? _userId = _userContext?.UserId;
            if (_userId == null && _userId != userId) return false;


            var user_status = await _userStatusReadRepository.GetByUserIdAsync(userId);

            user_status.ConnectionId = ConnectionId;

            _userStatusWriteRepository.Update(user_status);
            var affected_rows = await _userStatusWriteRepository.SaveAsync();
            return affected_rows > 0;
        }

        public async Task<bool> UpdateUserStatusAsync(Guid userId, User_Status status)
        {
            Guid? _userId = _userContext?.UserId;
            if (_userId == null && _userId != userId) return false;

            var user_status = await _userStatusReadRepository.GetByUserIdAsync(userId);

            user_status.Status = status;
            _userStatusWriteRepository.Update(user_status);
            var affected_rows = await _userStatusWriteRepository.SaveAsync();
            return affected_rows > 0;
        }
    }
}
