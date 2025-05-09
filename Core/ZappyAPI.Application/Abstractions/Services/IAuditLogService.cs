using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.AuditLog;
using ZappyAPI.Application.Abstractions.DTOs.User;

namespace ZappyAPI.Application.Abstractions.Services
{
    public interface IAuditLogService
    {
        Task<bool> CreateAsync(bool isSucceeded,CreateAuditLog model);
    }
}
