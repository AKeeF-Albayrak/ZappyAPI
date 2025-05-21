using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.Message;
using ZappyAPI.Application.ViewModels.Message;
using ZappyAPI.Application.ViewModels.User;
using ZappyAPI.Domain.Entities;

namespace ZappyAPI.Application.ViewModels.Group
{
    public class GroupsViewModel
    {
        public Guid GroupId { get; set; }
        public byte[] GroupPhoto { get; set; }
        public LastMessageViewModel LastMessage { get; set; }
        public string GroupName { get; set; } 
    }
}
