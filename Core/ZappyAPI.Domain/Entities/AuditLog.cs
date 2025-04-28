using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities.Common;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Domain.Entities
{
    public class AuditLog : BaseEntity
    {
        public required Guid UserId { get; set; }
        public required AuditAction Action { get; set; } //Maybe ENUM
        public required string TargetId { get; set; }
        public required AuditLog_TargetType TargetType { get; set; }

        public User User { get; set; }
    }
}
