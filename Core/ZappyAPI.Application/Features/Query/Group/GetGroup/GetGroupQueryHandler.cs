using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Application.Features.Query.Group.GetGroup
{
    public class GetGroupQueryHandler : IRequestHandler<GetGroupQueryRequest, GetGroupQueryResponse>
    {
        private readonly IGroupService _groupService;
        public GetGroupQueryHandler(IGroupService groupService)
        {
            _groupService = groupService;
        }
        public async Task<GetGroupQueryResponse> Handle(GetGroupQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _groupService.GetGroup(request.GroupId);

            return new GetGroupQueryResponse
            {
                Succeeded = response.Succeeded,
                Group = new ViewModels.Group.GroupViewModel
                {
                    GroupPicture = response.GroupPicture,
                    Id = response.Id,
                    Messages = response.Messages,
                    Name = response.Name,
                    Users = response.Users,
                    GroupReadStatuses = response.GroupReadStatuses,
                }
            };
        }
    }
}
