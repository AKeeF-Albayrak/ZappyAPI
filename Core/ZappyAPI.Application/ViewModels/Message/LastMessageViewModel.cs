using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Application.ViewModels.Message
{
    public class LastMessageViewModel
    {
        public Guid Id { get; set; }
        public required string Content { get; set; }
        public string SenderName { get; set; }
        public bool IsUser { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
