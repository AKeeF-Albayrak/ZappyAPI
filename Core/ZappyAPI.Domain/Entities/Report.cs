using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities.Common;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Domain.Entities
{
    public class Report : BaseEntity
    {
        public required Guid ReporterUserId { get; set; }
        public required Guid TargetUserId { get; set; }
        public required string TargetContentId { get; set; }
        public required Report_TargetType TargetType { get; set; }
        public required ReportReason Reason { get; set; }

        public string? TargetContentSnapshot { get; set; }

        public User ReporterUser { get; set; }
        public User TargetUser { get; set; }
    }
}
///??????????