using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.AuditLog;
using ZappyAPI.Application.Abstractions.DTOs.LoginHistory;

namespace ZappyAPI.Application.Abstractions.Services
{
    public interface ILoginHistoryService
    {
        Task<CreateLoginHistoryResponse> CreateAsync(CreateLoginHistory model);
    }
}
