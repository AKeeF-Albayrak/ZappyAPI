using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Application.ViewModels.Friendship
{
    public class FriendViewModel
    {
        public string Username { get; set; }
        public User_Status Status { get; set; }
    }
}
