using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Infrastructure.Services
{
    public class UserContext : IUserContext
    {
        public Guid? UserId { get; set; }
    }
}
