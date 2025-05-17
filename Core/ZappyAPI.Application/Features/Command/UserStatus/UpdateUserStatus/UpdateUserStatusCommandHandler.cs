using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Application.Features.Command.UserStatus.UpdateUserStatus
{
    public class UpdateUserStatusCommandHandler : IRequestHandler<UpdateUserStatusCommandRequest, UpdateUserStatusCommandResponse>
    {
        private readonly IUserStatusService _userStatusService;
        public UpdateUserStatusCommandHandler(IUserStatusService userStatusService)
        {
            _userStatusService = userStatusService;
        }
        public async Task<UpdateUserStatusCommandResponse> Handle(UpdateUserStatusCommandRequest request, CancellationToken cancellationToken)
        {
            return new UpdateUserStatusCommandResponse
            {
                Succeeded = await _userStatusService.UpdateUserStatusAsync(request.UserId, request.Status)
            }; 
        }
    }
}
