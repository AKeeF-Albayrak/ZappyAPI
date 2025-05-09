using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Application.Abstractions.DTOs.AuditLog
{
    public class CreateAuditLog
    {
        public required Guid UserId { get; set; }
        public required AuditAction Action { get; set; }
        public required Guid TargetId { get; set; }
        public required AuditLog_TargetType TargetType { get; set; }
    }
}
