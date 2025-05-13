using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.Message;

namespace ZappyAPI.Application.Abstractions.Services
{
    public interface IMessageService
    {
        public Task<bool> CreateMessage(CreateMessage model);
        public Task<bool> DeleteMessage(Guid messageId);
        public Task<bool> UpdateMessage(UpdateMessage model);
    }
}
