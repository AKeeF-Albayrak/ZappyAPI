using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Application.Abstractions.DTOs.GroupInvite
{
    public class RespondGroupInvite
    {
        public Guid Id { get; set; }
        public required GroupInviteStatus Status { get; set; }
    }
}
