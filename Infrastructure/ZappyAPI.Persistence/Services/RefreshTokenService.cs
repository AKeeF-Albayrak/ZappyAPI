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
        public RefreshTokenService(IRefreshTokenWriteRepository refreshTokenWriteRepository)
        {
            _refreshTokenWriteRepository = refreshTokenWriteRepository;
        }
        public async Task<CreateTokenResponse> CreateAsync(CreateRefreshToken model)
        {
            // TODO: Add Disable Old Tokens
            var randomBytes = RandomNumberGenerator.GetBytes(64);
            var id = Guid.NewGuid();

            var res = await _refreshTokenWriteRepository.AddAsync(new Domain.Entities.RefreshToken
            {
                Id = id,
                Token = Convert.ToBase64String(randomBytes),
                CreatedDate = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddDays(7),
                UserId = model.UserId,
                Revoked = false
            });
            int affectedRows = await _refreshTokenWriteRepository.SaveAsync();
            return new CreateTokenResponse
            {
                Succeeded = affectedRows == 1,
                TokenId = id,
            };
        }
    }
}
