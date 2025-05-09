using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.AuditLog;
using ZappyAPI.Application.Abstractions.DTOs.Session;

namespace ZappyAPI.Application.Abstractions.Services
{
    public interface ISessionService
    {
        Task<CreateSessionResponse> CreateAsync(CreateSession model);
    }
}
