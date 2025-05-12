using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Application.ViewModels.Message
{
    public class MessageViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public required Guid SenderId { get; set; }
        public required MessageContentType ContentType { get; set; }
        public required string EncryptedContent { get; set; }
        public required bool IsPinned { get; set; }
        public Guid? RepliedMessageId { get; set; }
    }
}
