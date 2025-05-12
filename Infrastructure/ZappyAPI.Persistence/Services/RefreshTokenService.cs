using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.Token;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Application.Repositories;

namespace ZappyAPI.Persistence.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        readonly IRefreshTokenWriteRepository _refreshTokenWriteRepository;
        readonly IRefreshTokenReadRepository _refreshTokenReadRepository;
        public RefreshTokenService(IRefreshTokenWriteRepository refreshTokenWriteRepository, IRefreshTokenReadRepository refreshTokenReadRepository)
        {
            _refreshTokenWriteRepository = refreshTokenWriteRepository;
            _refreshTokenReadRepository = refreshTokenReadRepository;
        }
        public async Task<CreateTokenResponse> CreateAsync(CreateRefreshToken model)
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);
            var id = Guid.NewGuid();
            var token = Convert.ToBase64String(randomBytes);

            var res = await _refreshTokenWriteRepository.AddAsync(new Domain.Entities.RefreshToken
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
                TokenId = id,
            };
        }

        public async Task<bool> DisableOldTokensAsync(Guid id)
        {
            var token = await _refreshTokenReadRepository.GetByIdAsync(id);
            
            if (token == null) return false;

            token.Revoked = true;

            _refreshTokenWriteRepository.Update(token);
            await _refreshTokenWriteRepository.SaveAsync();
            return true;
        }
    }
}
