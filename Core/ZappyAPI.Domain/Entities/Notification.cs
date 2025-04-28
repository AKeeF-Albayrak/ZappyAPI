using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities.Common;

namespace ZappyAPI.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public required Guid UserId { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required bool IsRead { get; set; }

        public User User { get; set; }
    }
}