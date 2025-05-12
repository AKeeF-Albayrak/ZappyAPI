using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Features.Query.Group.GetGroups
{
    public class GetGroupsQueryRequest : IRequest<GetGroupsQueryResponse>
    {
        public Guid UserId { get; set; }
    }
}
