using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Application.Features.Command.User.LogoutUser
{
    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommandRequest, LogoutUserCommandResponse>
    {
        private readonly ISessionService _sessionService;
        private readonly IRefreshTokenService _refreshTokenService;
        public LogoutUserCommandHandler(ISessionService sessionService, IRefreshTokenService refreshTokenService)
        {
            _sessionService = sessionService;
            _refreshTokenService = refreshTokenService;
        }
        public async Task<LogoutUserCommandResponse> Handle(LogoutUserCommandRequest request, CancellationToken cancellationToken)
        {
            var res = await _sessionService.OfflineSessions(request.UserId);
            await _refreshTokenService.DisableOldTokensAsync(res.TokenId);

            return new LogoutUserCommandResponse
            {
                Succeeded = true,
                Message = "Succeded!"
            };
        }
    }
}
