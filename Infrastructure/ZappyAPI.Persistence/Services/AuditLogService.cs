using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.AuditLog;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Application.Repositories;
using ZappyAPI.Domain.Entities;

namespace ZappyAPI.Persistence.Services
{
    public class AuditLogService : IAuditLogService
    {
        readonly IAuditLogWriteRepository _auditLogWriteRepository;
        public AuditLogService(IAuditLogWriteRepository auditLogWriteRepository)
        {
            _auditLogWriteRepository = auditLogWriteRepository;
        }

        public async Task<bool> CreateAsync(bool isSucceeded, CreateAuditLog model)
        {
            if (isSucceeded )
            {
                await _auditLogWriteRepository.AddAsync(new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = model.UserId,
                    Action = model.Action,
                    TargetId = model.TargetId.ToString(),
                    TargetType = model.TargetType,
                    CreatedDate = DateTime.UtcNow,
                });
            }

            return await _auditLogWriteRepository.SaveAsync() == 1;
        }
    }
}
