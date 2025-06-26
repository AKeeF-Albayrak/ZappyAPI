using System.Security.Cryptography;
using ZappyAPI.Application.Abstractions.DTOs.Token;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Application.Repositories;

namespace ZappyAPI.Persistence.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenWriteRepository _refreshTokenWriteRepository;
        private readonly IRefreshTokenReadRepository _refreshTokenReadRepository;
        private readonly IUserContext _userContext;

        public RefreshTokenService(
            IRefreshTokenWriteRepository refreshTokenWriteRepository,
            IRefreshTokenReadRepository refreshTokenReadRepository,
            IUserContext userContext)
        {
            _refreshTokenWriteRepository = refreshTokenWriteRepository;
            _refreshTokenReadRepository = refreshTokenReadRepository;
            _userContext = userContext;
        }

        public async Task<CreateTokenResponse> CreateAsync(CreateRefreshToken model)
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);
            var token = Convert.ToBase64String(randomBytes);
            var id = Guid.NewGuid();

            await _refreshTokenWriteRepository.AddAsync(new Domain.Entities.RefreshToken
            {
                Id = id,
                Token = token,
                CreatedDate = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddDays(7),
                UserId = model.UserId,
                Revoked = false
            });

            int affectedRows = await _refreshTokenWriteRepository.SaveAsync();

            return new CreateTokenResponse
            {
                Succeeded = affectedRows == 1,
                Token = token,
                TokenId = id
            };
        }

        public async Task<bool> DisableOldTokensAsync(Guid id)
        {
            var userId = _userContext.UserId;
            if (userId == null) return false;

            var token = await _refreshTokenReadRepository.GetByIdAsync(id);
            if (token == null || token.UserId != userId)
                return false;

            token.Revoked = true;
            _refreshTokenWriteRepository.Update(token);
            await _refreshTokenWriteRepository.SaveAsync();

            return true;
        }
    }
}
