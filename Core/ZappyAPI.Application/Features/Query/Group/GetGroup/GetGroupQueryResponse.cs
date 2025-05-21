using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.ViewModels.Group;
using ZappyAPI.Application.ViewModels.Message;
using ZappyAPI.Application.ViewModels.User;

namespace ZappyAPI.Application.Features.Query.Group.GetGroup
{
    public class GetGroupQueryResponse
    {
        public bool Succeeded { get; set; }
        public GroupViewModel Group { get; set; }
    }
}
