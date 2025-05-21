using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.ViewModels.User
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public byte[] ProfilePicture { get; set; }
        public bool isOnline { get; set; }
    }
}
