using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Application.Abstractions.DTOs.Message
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public required MessageContentType ContentType { get; set; }
        public required string EncryptedContent { get; set; }
        public string SenderName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
