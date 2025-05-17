using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Application.Features.Command.UserStatus.UpdateConnectionId
{
    public class UpdateConnectionIdCommandHandler : IRequestHandler<UpdateConnectionIdCommandRequest, UpdateConnectionIdCommandResponse>
    {
        private readonly IUserStatusService _userStatusService;
        public UpdateConnectionIdCommandHandler(IUserStatusService userStatusService)
        {
            _userStatusService = userStatusService;
        }
        public async Task<UpdateConnectionIdCommandResponse> Handle(UpdateConnectionIdCommandRequest request, CancellationToken cancellationToken)
        {
            return new UpdateConnectionIdCommandResponse
            {
                Succeeded = await _userStatusService.UpdateConnectionIdAsync(request.UserId, request.ConnectionId)
            };
        }
    }
}
