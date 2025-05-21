using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Abstractions.DTOs.User
{
    public class CreateUser
    {
        public required string Name { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Mail { get; set; }
        public string? ProfilePicturePath { get; set; }
        public string? Description { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
