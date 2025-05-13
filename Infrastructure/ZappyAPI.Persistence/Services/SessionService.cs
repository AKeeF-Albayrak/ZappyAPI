using ZappyAPI.Application.Abstractions.DTOs.Session;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Application.Repositories;

namespace ZappyAPI.Persistence.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionWriteRepository _sessionWriteRepository;
        private readonly IUserStatusWriteRepository _userStatusWriteRepository;
        private readonly IUserContext _userContext;

        public SessionService(
            ISessionWriteRepository sessionWriteRepository,
            IUserStatusWriteRepository userStatusWriteRepository,
            IUserContext userContext)
        {
            _sessionWriteRepository = sessionWriteRepository;
            _userStatusWriteRepository = userStatusWriteRepository;
            _userContext = userContext;
        }

        public async Task<CreateSessionResponse> CreateAsync(CreateSession model)
        {
            var userId = _userContext.UserId;

            if (userId == null || userId != model.UserId)
            {
                return new CreateSessionResponse
                {
                    Succeeded = false,
                    Id = Guid.Empty
                };
            }

            Guid id = Guid.NewGuid();

            await _sessionWriteRepository.AddAsync(new Domain.Entities.Session
            {
                Id = id,
                CreatedDate = DateTime.UtcNow,
                DeviceInfo = model.DeviceInfo,
                IpAdress = model.IpAdress,
                IsActive = true,
                LastActivityDate = DateTime.UtcNow,
                RefreshTokenId = model.RefreshTokenId,
                UserId = userId.Value
            });

            int affected_rows = await _sessionWriteRepository.SaveAsync();

            return new CreateSessionResponse
            {
                Id = id,
                Succeeded = affected_rows == 1
            };
        }

        public async Task<OfflineSessionResponse> OfflineSessions(Guid userId)
        {
            var currentUserId = _userContext.UserId;

            if (currentUserId == null || currentUserId != userId)
            {
                return new OfflineSessionResponse
                {
                    Succeeded = false
                };
            }

            var token = await _sessionWriteRepository.OfflineOthersAsync(userId);
            await _sessionWriteRepository.SaveAsync();

            if (token == null)
                return new OfflineSessionResponse { Succeeded = false };

            await _userStatusWriteRepository.OfflineAsync(userId);
            await _userStatusWriteRepository.SaveAsync();

            return new OfflineSessionResponse
            {
                Succeeded = true,
                TokenId = token.Value
            };
        }
    }
}
