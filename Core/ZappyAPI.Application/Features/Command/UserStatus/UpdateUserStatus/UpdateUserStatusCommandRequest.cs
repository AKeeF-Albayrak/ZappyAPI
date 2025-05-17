using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Domain.Entities;
using ZappyAPI.Domain.Enums;

namespace ZappyAPI.Application.Features.Command.UserStatus.UpdateUserStatus
{
    public class UpdateUserStatusCommandRequest : IRequest<UpdateUserStatusCommandResponse>
    {
        public Guid UserId { get; set; }
        public User_Status Status { get; set; }
    }
}
