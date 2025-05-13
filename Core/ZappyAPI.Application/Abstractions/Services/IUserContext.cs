using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Abstractions.Services
{
    public interface IUserContext
    {
        Guid? UserId { get; set; }
    }
}
