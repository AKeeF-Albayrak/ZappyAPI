using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities.Common;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Domain.Entities
{
    public class Message : BaseEntity
    {
        public required Guid GroupId { get; set; }
        public required Guid SenderId { get; set; }
        public required MessageContentType ContentType { get; set; }
        public required string EncryptedContent { get; set; }
        public required bool IsDeleted { get; set; }
        public required bool IsPinned { get; set; }
        public Guid? RepliedMessageId { get; set; }

        public Group Group { get; set; }
        public User Sender { get; set; }
        public Message? RepliedMessage { get; set; }
        public ICollection<MessageRead> MessageReads { get; set; }
        public List<StarredMessage> StarredMessages { get; set; }
    }

}
