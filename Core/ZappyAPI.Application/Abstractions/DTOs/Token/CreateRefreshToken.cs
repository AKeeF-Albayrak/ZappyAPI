using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Abstractions.DTOs.Token
{
    public class CreateRefreshToken
    {
        public required Guid UserId { get; set; }
    }
}
