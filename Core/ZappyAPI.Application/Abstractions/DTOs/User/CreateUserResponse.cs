using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Abstractions.DTOs.User
{
    public class CreateUserResponse
    {
        public Guid Id { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
