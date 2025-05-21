using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.ViewModels.User;

namespace ZappyAPI.Application.Abstractions.DTOs.User
{
    public class GetUserResponse
    {
        public UserViewModel User { get; set; }
        public bool Succeeded { get; set; }
    }
}
