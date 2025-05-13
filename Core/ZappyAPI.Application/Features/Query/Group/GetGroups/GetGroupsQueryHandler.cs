using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Application.Features.Query.Group.GetGroups
{
    public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQueryRequest, GetGroupsQueryResponse>
    {
        private readonly IGroupService _groupService;
        public GetGroupsQueryHandler(IGroupService groupService)
        {
             _groupService = groupService;
        }
        public async Task<GetGroupsQueryResponse> Handle(GetGroupsQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _groupService.GetGroups(request.UserId);

            return new GetGroupsQueryResponse
            {
                Groups = response.Groups,
                Succeeded = response.Succeeded,
            };
        }
    }
}
