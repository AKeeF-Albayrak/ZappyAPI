using ZappyAPI.Application.Abstractions.DTOs.Message;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Application.Repositories;

namespace ZappyAPI.Persistence.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageWriteRepository _messageWriteRepository;
        private readonly IMessageReadRepository _messageReadRepository;
        private readonly IUserContext _userContext;

        public MessageService(
            IMessageWriteRepository messageWriteRepository,
            IMessageReadRepository messageReadRepository,
            IUserContext userContext)
        {
            _messageReadRepository = messageReadRepository;
            _messageWriteRepository = messageWriteRepository;
            _userContext = userContext;
        }

        public async Task<bool> CreateMessage(CreateMessage model)
        {
            var userId = _userContext.UserId;
            if (userId == null || model.SenderId != userId)
                return false;

            await _messageWriteRepository.AddAsync(new Domain.Entities.Message
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                GroupId = model.GroupId,
                SenderId = userId.Value,
                ContentType = model.ContentType,
                EncryptedContent = model.EncryptedContent,
                IsDeleted = false,
                IsPinned = model.IsPinned,
                RepliedMessageId = model.RepliedMessageId,
            });

            int affected_rows = await _messageWriteRepository.SaveAsync();
            return affected_rows > 0;
        }

        public async Task<bool> DeleteMessage(Guid messageId)
        {
            var userId = _userContext.UserId;
            if (userId == null) return false;

            var message = await _messageReadRepository.GetByIdAsync(messageId);
            if (message == null || message.SenderId != userId)
                return false;

            message.IsDeleted = true;
            _messageWriteRepository.Update(message);

            int affected_rows = await _messageWriteRepository.SaveAsync();
            return affected_rows > 0;
        }

        public async Task<bool> UpdateMessage(UpdateMessage model)
        {
            var userId = _userContext.UserId;
            if (userId == null) return false;

            var message = await _messageReadRepository.GetByIdAsync(model.Id);
            if (message == null || message.SenderId != userId)
                return false;

            if (model.ContentType == Domain.Enums.MessageContentType.Text)
                message.EncryptedContent = model.EncryptedContent;

            if (message.RepliedMessage == null || model.RepliedMessageId == message.RepliedMessageId)
            {
                message.RepliedMessageId = model.RepliedMessageId;
                message.IsPinned = model.IsPinned;
            }
            else
            {
                return false;
            }

            _messageWriteRepository.Update(message);
            int affected_rows = await _messageWriteRepository.SaveAsync();
            return affected_rows > 0;
        }
    }
}
