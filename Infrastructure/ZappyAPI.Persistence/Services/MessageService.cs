using Microsoft.AspNetCore.Components.Forms;
using System.Runtime.CompilerServices;
using ZappyAPI.Application.Abstractions.DTOs.Message;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Application.Repositories;
using ZappyAPI.Application.ViewModels.StarMessage;

namespace ZappyAPI.Persistence.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageWriteRepository _messageWriteRepository;
        private readonly IMessageReadRepository _messageReadRepository;
        private readonly IUserContext _userContext;
        private readonly IChatHubService _chatHubService;
        private readonly IGroupReadStatusWriteRepository _groupReadStatusWriteRepository;
        private readonly IStarredMessageWriteRepository _starredMessageWriteRepository;
        private readonly IStarredMessageReadRepository _starredMessageReadRepository;

        public MessageService(
            IMessageWriteRepository messageWriteRepository,
            IMessageReadRepository messageReadRepository,
            IUserContext userContext,
            IChatHubService chatHubService,
            IStarredMessageWriteRepository starredMessageWriteRepository,
            IStarredMessageReadRepository starredMessageReadRepository,
            IGroupReadStatusWriteRepository groupReadStatusWriteRepository)
        {
            _messageReadRepository = messageReadRepository;
            _messageWriteRepository = messageWriteRepository;
            _userContext = userContext;
            _chatHubService = chatHubService;
            _starredMessageReadRepository = starredMessageReadRepository;
            _starredMessageWriteRepository = starredMessageWriteRepository;
            _groupReadStatusWriteRepository = groupReadStatusWriteRepository;
        }

        public async Task<bool> CreateMessage(CreateMessage model)
        {
            var userId = _userContext.UserId;
            if (userId == null || model.SenderId != userId)
                return false;

            var messageEntity = new Domain.Entities.Message
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
            };

            await _messageWriteRepository.AddAsync(messageEntity);
            int affected_rows = await _messageWriteRepository.SaveAsync();

            if (affected_rows > 0)
            {
                await _chatHubService.SendMessageToGroup(
                    model.GroupId,
                    model.EncryptedContent,
                    sender: userId.ToString()
                );
                return true;
            }

            return false;
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
            if (affected_rows > 0)
            {
                await _chatHubService.SendMessageToGroup(
                    message.GroupId,
                    $"Mesaj silindi (ID: {message.Id})",
                    sender: userId.ToString()
                );
                return true;
            }
            return false ;
        }

        public async Task<StarMessageResponse> GetStarredMessages()
        {
            var userId = _userContext.UserId;
            if (userId == null) return new StarMessageResponse
            {
                Succeeded = false,
            };

            var starredMessages = await _starredMessageReadRepository.GetUsersStarredMessagesAsync((Guid)userId);
            return new StarMessageResponse
            {
                StarredMessageViewModel = starredMessages,
                Succeeded = true
            };
        }

        public async Task<bool> ReadMessages(Guid groupId)
        {
            var userId = _userContext.UserId;
            if (userId == null) return false;
            await _groupReadStatusWriteRepository.ReadMessagesAsync(groupId, (Guid)userId);

            return await _groupReadStatusWriteRepository.SaveAsync() > 1;
        }

        public async Task<bool> StarMessage(StarMessageRequest model)
        {
            var userId = _userContext.UserId;

            if (userId != null)
            {
                return false;
            }

            await _starredMessageWriteRepository.AddAsync(new Domain.Entities.StarredMessage
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                MessageId = model.MessageId,
                UserId = (Guid)userId,
            });
            return await _starredMessageWriteRepository.SaveAsync() > 0;
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
            if (affected_rows > 0)
            {
                await _chatHubService.SendMessageToGroup(
                    message.GroupId,
                    message.EncryptedContent,
                    sender: userId.ToString()
                );
                return true;
            }
            return false;
        }
    }
}
