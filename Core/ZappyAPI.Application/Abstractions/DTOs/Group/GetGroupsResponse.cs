using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.ViewModels.Group;

namespace ZappyAPI.Application.Abstractions.DTOs.Group
{
    public class GetGroupsResponse
    {
        public bool Succeeded { get; set; }
        public List<GroupsViewModel> Groups { get; set; }
    }
}
