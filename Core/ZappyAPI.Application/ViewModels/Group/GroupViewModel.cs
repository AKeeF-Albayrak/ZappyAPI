using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.Message;
using ZappyAPI.Domain.Entities;

namespace ZappyAPI.Application.ViewModels.Group
{
    public class GroupViewModel
    {
        public Guid GroupId { get; set; }
        public byte[] GroupPhoto { get; set; }
        public MessageDto LastMessage { get; set; }
        public string GroupName { get; set; }
    }
}
