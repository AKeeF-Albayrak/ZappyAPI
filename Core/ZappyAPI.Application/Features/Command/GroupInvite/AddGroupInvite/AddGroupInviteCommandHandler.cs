using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Application.Features.Command.GroupInvite.AddGroupInvite
{
    public class AddGroupInviteCommandHandler : IRequestHandler<AddGroupInviteCommandRequest, AddGroupInviteCommandResponse>
    {
        private readonly IGroupService _groupService;
        public AddGroupInviteCommandHandler(IGroupService groupService)
        {
            _groupService = groupService;
        }
        public async Task<AddGroupInviteCommandResponse> Handle(AddGroupInviteCommandRequest request, CancellationToken cancellationToken)
        {
            return new AddGroupInviteCommandResponse
            {
                Succeeded = await _groupService.InviteGroup(new Abstractions.DTOs.GroupInvite.CreateGroupInvite
                {
                    GroupId = request.GroupId,
                    InvitedUserId = request.InvitedUserId,
                    InviterUserId = request.InviterUserId,
                })
            };
        }
    }
}
