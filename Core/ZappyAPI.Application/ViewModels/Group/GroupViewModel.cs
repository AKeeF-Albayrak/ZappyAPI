using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.ViewModels.GroupReadStatus;
using ZappyAPI.Application.ViewModels.Message;
using ZappyAPI.Application.ViewModels.User;
using ZappyAPI.Domain.Entities;

namespace ZappyAPI.Application.ViewModels.Group
{
    public class GroupViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] GroupPicture { get; set; }
        public List<MessageViewModel> Messages { get; set; }
        public List<UserViewModel> Users { get; set; }
        public List<GroupReadStatusViewModel> GroupReadStatuses { get; set; }
    }
}
