using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Application.Features.Command.User.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, UpdateUserCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly IStorageService _storageService;
        public UpdateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            return new UpdateUserCommandResponse
            {
                Succeeded = await _userService.UpdateUserAsync(new Abstractions.DTOs.User.UpdateUser
                {
                    Name = request.Name,
                    ProfilePicPath = _storageService.UploadAsync(new Abstractions.DTOs.Storage.Upload
                    {
                        FileBytes = request.ProfilePic,
                        FileName = "0_0",
                        ContentType = request.ContentType
                    }).Result.FilePath,
                    Mail = request.Mail,
                    Description = request.Description,
                    Username = request.Username,
                })
            };
        }
    }
}
