using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.ViewModels.Message;
using ZappyAPI.Application.ViewModels.User;

namespace ZappyAPI.Application.Abstractions.DTOs.Group
{
    public class GetGroupResponse
    {
        public bool Succeeded { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] GroupPicture { get; set; }
        public List<MessageViewModel> Messages { get; set; }
        public List<UserViewModel> Users { get; set; }
        public List<Guid> ReadByUserIds { get; set; }
    }
}
