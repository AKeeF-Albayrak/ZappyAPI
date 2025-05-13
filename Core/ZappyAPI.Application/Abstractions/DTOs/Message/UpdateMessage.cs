using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Application.Abstractions.DTOs.Message
{
    public class UpdateMessage
    {
        public Guid Id { get; set; }
        public MessageContentType ContentType { get; set; }
        public string EncryptedContent { get; set; }
        public bool IsPinned { get; set; }
        public Guid? RepliedMessageId { get; set; }
    }
}
