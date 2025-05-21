using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Application.Features.Command.Group.CreateGroup
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommandRequest, CreateGroupCommandResponse>
    {
        private readonly IGroupService _groupService;
        public CreateGroupCommandHandler(IGroupService groupService)
        {
            _groupService = groupService;   
        }
        public async Task<CreateGroupCommandResponse> Handle(CreateGroupCommandRequest request, CancellationToken cancellationToken)
        {
            return new CreateGroupCommandResponse
            {
                Succeeded = await _groupService.CreateGroup(new Abstractions.DTOs.Group.CreateGroup
                {
                    UserId = request.UserId,
                    Description = request.Description,
                    ContentType = request.ContentType,
                    GroupName = request.GroupName,
                    GroupPicture = request.GroupPicture,
                    Usernames = request.Usernames,
                })
            };
        }
    }
}
