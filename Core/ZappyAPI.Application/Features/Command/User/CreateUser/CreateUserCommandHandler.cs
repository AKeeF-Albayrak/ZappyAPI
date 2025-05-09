using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.Storage;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Application.Features.Command.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly IStorageService _storageService;
        private readonly IAuditLogService _auditLogService;
        public CreateUserCommandHandler(IUserService userService, IStorageService storageService, IAuditLogService auditLogService)
        {
            _userService = userService;
            _storageService = storageService;
            _auditLogService = auditLogService;
        }
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            // TODO: PASSWORD KISITLAMALARI
            UploadResult? result = null;

            if(request.ProfilePicture != null)
            {
                result = await _storageService.UploadAsync(new Upload
                {
                    FileBytes = request.ProfilePicture,
                    ContentType = request.ContentType,
                    FileName = "0_0"
                });
            }

            var response = await _userService.CreateAsync(new Abstractions.DTOs.User.CreateUser
            {
                Name = request.Name,
                Mail = request.Mail,
                Age = request.Age,
                Description = request.Description,
                Password = request.Password,
                ProfilePicturePath = result?.FilePath,
                Username = request.Username,
            });

            await _auditLogService.CreateAsync(response.Succeeded, new Abstractions.DTOs.AuditLog.CreateAuditLog
            {
                Action = Domain.Enums.AuditAction.Create,
                TargetId = response.Id,
                TargetType = Domain.Enums.AuditLog_TargetType.User,
                UserId = response.Id
            });

            return new CreateUserCommandResponse
            {
                Succeeded = response.Succeeded,
                Message = response.Message
            };
        }
    }
}
