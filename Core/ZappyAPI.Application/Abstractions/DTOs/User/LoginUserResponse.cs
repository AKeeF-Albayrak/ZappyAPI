using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Abstractions.DTOs.User
{
    public class LoginUserResponse
    {
        public bool Succeeded { get; set; }
        public Guid UserId { get; set; }
    }
}
