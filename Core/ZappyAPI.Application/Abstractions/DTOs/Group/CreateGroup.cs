using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Abstractions.DTOs.Group
{
    public class CreateGroup
    {
        public Guid UserId { get; set; }
        public string GroupName { get; set; }
        public byte[] GroupPicture { get; set; }
        public string ContentType { get; set; }
        public string Description { get; set; }
        public List<Guid> UserIds { get; set; }
    }
}
