using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public LoginUserCommandHandler(IUserService userService, ILoginHistoryService loginHistoryService, IRefreshTokenService refreshTokenService, ISessionService sessionService, IAuditLogService auditLogService)
        {
            _userService = userService;
            _loginHistoryService = loginHistoryService;
            _refreshTokenService = refreshTokenService;
            _sessionService = sessionService;
            _auditLogService = auditLogService;
        }
        public async Task<LoginUserCommandRepsonse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _userService.LoginUserAsync(request.UserName, request.Password);
            if (response.Succeeded)
            {
                var res1 = await _loginHistoryService.CreateAsync(new Abstractions.DTOs.LoginHistory.CreateLoginHistory
                {
                    Succeeded = true,
                    IpAdress = request.IpAdress,
                    UserAgent = request.UserAgent,
                    UserId = response.UserId,
                });

                var res2 = await _refreshTokenService.CreateAsync(new Abstractions.DTOs.Token.CreateRefreshToken
                {
                    UserId = response.UserId
                });

                var res3 = await _sessionService.CreateAsync(new Abstractions.DTOs.Session.CreateSession
                {
                    UserId = response.UserId,
                    DeviceInfo = request.DeviceInfo,
                    IpAdress = request.IpAdress,
                    RefreshTokenId = res2.TokenId
                });

                await _auditLogService.CreateAsync(res1.Succeeded,new Abstractions.DTOs.AuditLog.CreateAuditLog
                {
                    Action = Domain.Enums.AuditAction.Create,
                    TargetId = res1.Id,
                    TargetType = Domain.Enums.AuditLog_TargetType.LoginHistory,
                    UserId = response.UserId,
                });
                await _auditLogService.CreateAsync(res2.Succeeded, new Abstractions.DTOs.AuditLog.CreateAuditLog
                {
                    Action = Domain.Enums.AuditAction.Create,
                    TargetId = res2.TokenId,
                    TargetType = Domain.Enums.AuditLog_TargetType.RefreshToken,
                    UserId = response.UserId,
                });
                await _auditLogService.CreateAsync(res3.Succeeded, new Abstractions.DTOs.AuditLog.CreateAuditLog
                {
                    Action = Domain.Enums.AuditAction.Create,
                    TargetId = res3.Id,
                    TargetType = Domain.Enums.AuditLog_TargetType.Session,
                    UserId = response.UserId,
                });

                return new LoginUserCommandRepsonse
                {
                    Succeeded = res1.Succeeded && res2.Succeeded && res3.Succeeded,
                    Message = res1.Succeeded && res2.Succeeded && res3.Succeeded ? "Succeeded" : "Something Went Wrong!" 
                };
            }
            return new LoginUserCommandRepsonse
            {
                Succeeded = false,
                Message = "Wrong Password or Username"
            };
        }
    }
}
