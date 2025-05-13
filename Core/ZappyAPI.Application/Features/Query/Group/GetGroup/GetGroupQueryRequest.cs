using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Features.Query.Group.GetGroup
{
    public class GetGroupQueryRequest : IRequest<GetGroupQueryResponse>
    {
        public Guid GroupId { get; set; }
    }
}
