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
            if (!IsPasswordValid(request.Password, out string passwordError))
            {
                return new CreateUserCommandResponse
                {
                    Succeeded = false,
                    Message = $"Parola geçerli değil: {passwordError}"
                };
            }

            UploadResult? result = null;

            if (request.ProfilePicture != null)
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

        private bool IsPasswordValid(string password, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (password.Length < 8)
            {
                errorMessage = "En az 8 karakter olmalıdır.";
                return false;
            }

            if (!password.Any(char.IsUpper))
            {
                errorMessage = "En az bir büyük harf içermelidir.";
                return false;
            }

            if (!password.Any(char.IsLower))
            {
                errorMessage = "En az bir küçük harf içermelidir.";
                return false;
            }

            if (!password.Any(char.IsDigit))
            {
                errorMessage = "En az bir rakam içermelidir.";
                return false;
            }

            if (!password.Any(ch => "!@#$%^&*()_+-=[]{}|;:',.<>/?".Contains(ch)))
            {
                errorMessage = "En az bir özel karakter içermelidir.";
                return false;
            }

            return true;
        }

    }
}
