using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.ViewModels.Group;

namespace ZappyAPI.Application.Features.Query.Group.GetGroups
{
    public class GetGroupsQueryResponse
    {
        public List<GroupViewModel> Groups { get; set; }
        public bool Succeeded { get; set; }
    }
}
