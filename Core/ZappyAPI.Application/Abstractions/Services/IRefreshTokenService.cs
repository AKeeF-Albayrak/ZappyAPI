using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.LoginHistory;
using ZappyAPI.Application.Abstractions.DTOs.Token;

namespace ZappyAPI.Application.Abstractions.Services
{
    public interface IRefreshTokenService
    {
        Task<CreateTokenResponse> CreateAsync(CreateRefreshToken model);
        Task<bool> DisableOldTokensAsync(Guid id);
    }
}
