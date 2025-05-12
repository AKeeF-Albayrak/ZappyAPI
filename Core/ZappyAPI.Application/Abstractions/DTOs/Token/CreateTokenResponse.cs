using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Abstractions.DTOs.Token
{
    public class CreateTokenResponse
    {
        public bool Succeeded { get; set; }
        public string Token { get; set; }
        public Guid TokenId { get; set; }
    }
}
