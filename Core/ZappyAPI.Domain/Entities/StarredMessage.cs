using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities.Common;

namespace ZappyAPI.Domain.Entities
{
    public class StarredMessage : BaseEntity
    {
        public Guid MessageId { get; set; }
        public Guid UserId { get; set; }
        
        public Message Message { get; set; }
        public User User { get; set; }
    }
}
