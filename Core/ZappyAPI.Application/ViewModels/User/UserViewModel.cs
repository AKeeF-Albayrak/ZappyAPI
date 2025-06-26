using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Application.ViewModels.User
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public byte[] ProfilePicture { get; set; }
        public User_Status Status { get; set; }
    }
}