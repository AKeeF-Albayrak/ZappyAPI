using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.Message;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Application.Repositories;

namespace ZappyAPI.Persistence.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageWriteRepository _messageWriteRepository;
        private readonly IMessageReadRepository _messageReadRepository;
        public MessageService(IMessageWriteRepository messageWriteRepository, IMessageReadRepository messageReadRepository)
        {
            _messageReadRepository = messageReadRepository;
            _messageWriteRepository = messageWriteRepository;
        }
        public async Task<bool> CreateMessage(CreateMessage model)
        {
            // TODO: Add Encryp, Decrypt
            await _messageWriteRepository.AddAsync(new Domain.Entities.Message
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                GroupId = model.GroupId,
                SenderId = model.SenderId,
                ContentType = model.ContentType,
                EncryptedContent = model.EncryptedContent,
                IsDeleted = false,
                IsPinned = model.IsPinned,
                RepliedMessageId = model.RepliedMessageId,
            });

            int affecterd_rows = await _messageWriteRepository.SaveAsync();

            return affecterd_rows > 0;
        }

        public async Task<bool> DeleteMessage(Guid messageId)
        {
            var message = await _messageReadRepository.GetByIdAsync(messageId);
            if (message == null) return false;

            message.IsDeleted = true;
            _messageWriteRepository.Update(message);

            int affected_rows = await _messageWriteRepository.SaveAsync();
            return affected_rows > 0;
        }

        public async Task<bool> UpdateMessage(UpdateMessage model)
        {
            var message = await _messageReadRepository.GetByIdAsync(model.Id);
            
            if (message == null) return false;

            if(model.ContentType == Domain.Enums.MessageContentType.Text) message.EncryptedContent = model.EncryptedContent;
            if (message.RepliedMessage == null || model.RepliedMessageId == message.RepliedMessageId)
            {
                message.RepliedMessageId = model.RepliedMessageId;
                message.IsPinned = model.IsPinned;
            }
            else return false;
            

            _messageWriteRepository.Update(message);
            int affected_rows = await _messageWriteRepository.SaveAsync();

            return affected_rows > 0;
        }
    }
}
