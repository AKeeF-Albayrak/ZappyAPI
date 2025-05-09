using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Abstractions.DTOs.Session
{
    public class CreateSessionResponse
    {
        public bool Succeeded { get; set; }
        public Guid Id { get; set; }
    }
}
