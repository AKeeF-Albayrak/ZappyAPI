using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.ViewModels.StarMessage
{
    public class StarredMessageViewModel
    {
        public Guid MessageId { get; set; }
        public Guid GroupId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string SenderUsernam { get; set; }

    }
}
