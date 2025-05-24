using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities.Common;

namespace ZappyAPI.Domain.Entities
{
    public class GroupReadStatus : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public DateTime LastReadAt { get; set; }

        public User User { get; set; }
        public Group Group { get; set; }
    }
}
