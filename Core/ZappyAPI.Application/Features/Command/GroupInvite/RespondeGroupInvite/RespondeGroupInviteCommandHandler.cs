using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Application.Features.Command.GroupInvite.RespondeGroupInvite
{
    public class RespondeGroupInviteCommandHandler : IRequestHandler<RespondeGroupInviteCommandRequest, RespondeGroupInviteCommandResponse>
    {
        private readonly IGroupService _groupService;
        public RespondeGroupInviteCommandHandler(IGroupService groupService)
        {
            _groupService = groupService;
        }
        public async Task<RespondeGroupInviteCommandResponse> Handle(RespondeGroupInviteCommandRequest request, CancellationToken cancellationToken)
        {
            return new RespondeGroupInviteCommandResponse
            {
                Succeeded = await _groupService.RespondGroupInvite(new Abstractions.DTOs.GroupInvite.RespondGroupInvite
                {
                    Id = request.InviteId,
                    Status = request.Status,
                })
            };
        }
    }
}
