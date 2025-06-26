using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using Claim = System.Security.Claims.Claim;
using ClaimTypes = System.Security.Claims.ClaimTypes;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Helper;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Application.Features.Command.User.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandRepsonse>
    {
        private readonly IUserService _userService;
        private readonly ILoginHistoryService _loginHistoryService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly ISessionService _sessionService;
        private readonly IAuditLogService _auditLogService;
        private readonly TokenService _tokenService;
        public LoginUserCommandHandler(IUserService userService, ILoginHistoryService loginHistoryService, IRefreshTokenService refreshTokenService, ISessionService sessionService, IAuditLogService auditLogService, TokenService tokenService)
        {
            _userService = userService;
            _loginHistoryService = loginHistoryService;
            _refreshTokenService = refreshTokenService;
            _sessionService = sessionService;
            _auditLogService = auditLogService;
            _tokenService = tokenService;
        }
        public async Task<LoginUserCommandRepsonse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _userService.LoginUserAsync(request.UserName, request.Password);
            if (!response.Succeeded)
            {
                return new LoginUserCommandRepsonse
                {
                    Succeeded = false,
                    Message = "Wrong Password or Username"
                };
            }

            var loginHistoryResult = await _loginHistoryService.CreateAsync(new Abstractions.DTOs.LoginHistory.CreateLoginHistory
            {
                Succeeded = true,
                IpAdress = request.IpAdress,
                UserAgent = request.UserAgent,
                UserId = response.UserId,
            });

            var refreshTokenResult = await _refreshTokenService.CreateAsync(new Abstractions.DTOs.Token.CreateRefreshToken
            {
                UserId = response.UserId
            });

            var sessionResult = await _sessionService.CreateAsync(new Abstractions.DTOs.Session.CreateSession
            {
                UserId = response.UserId,
                DeviceInfo = request.DeviceInfo,
                IpAdress = request.IpAdress,
                RefreshTokenId = refreshTokenResult.TokenId
            });

            await _auditLogService.CreateAsync(loginHistoryResult.Succeeded, new Abstractions.DTOs.AuditLog.CreateAuditLog
            {
                Action = AuditAction.Create,
                TargetId = loginHistoryResult.Id,
                TargetType = AuditLog_TargetType.LoginHistory,
                UserId = response.UserId,
            });

            await _auditLogService.CreateAsync(refreshTokenResult.Succeeded, new Abstractions.DTOs.AuditLog.CreateAuditLog
            {
                Action = AuditAction.Create,
                TargetId = refreshTokenResult.TokenId,
                TargetType = AuditLog_TargetType.RefreshToken,
                UserId = response.UserId,
            });

            await _auditLogService.CreateAsync(sessionResult.Succeeded, new Abstractions.DTOs.AuditLog.CreateAuditLog
            {
                Action = AuditAction.Create,
                TargetId = sessionResult.Id,
                TargetType = AuditLog_TargetType.Session,
                UserId = response.UserId,
            });

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, response.UserId.ToString()),
                new Claim(ClaimTypes.Name, request.UserName),
                new Claim(ClaimTypes.Role, "User"),
                new Claim("userId", response.UserId.ToString()),
                new Claim("username", request.UserName)
            };


            var accessToken = _tokenService.GenerateAccessToken(claims);
            string result = loginHistoryResult.Succeeded.ToString() + refreshTokenResult.Succeeded.ToString() + sessionResult.Succeeded.ToString();
            return new LoginUserCommandRepsonse
            {
                Succeeded = loginHistoryResult.Succeeded && refreshTokenResult.Succeeded && sessionResult.Succeeded,
                Message = loginHistoryResult.Succeeded && refreshTokenResult.Succeeded && sessionResult.Succeeded
                    ? "Succeeded"
                    : "Something Went Wrong! " + result,
                RefreshToken = accessToken,
                AccessToken = refreshTokenResult.Token
            };
        }
    }
}
