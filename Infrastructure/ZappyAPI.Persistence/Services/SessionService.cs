using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.Session;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Application.Repositories;

namespace ZappyAPI.Persistence.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionWriteRepository _sessionWriteRepository;
        public SessionService(ISessionWriteRepository sessionWriteRepository)
        {
            _sessionWriteRepository = sessionWriteRepository;
        }
        public async Task<CreateSessionResponse> CreateAsync(CreateSession model)
        {
            Guid id = Guid.NewGuid();
            var res = await _sessionWriteRepository.AddAsync(new Domain.Entities.Session
            {
                Id = id,
                CreatedDate = DateTime.UtcNow,
                DeviceInfo = model.DeviceInfo,
                IpAdress = model.IpAdress,
                IsActive = true,
                LastActivityDate = DateTime.UtcNow,
                RefreshTokenId = model.RefreshTokenId,
                UserId = model.UserId,
            });
            int affected_rows = await _sessionWriteRepository.SaveAsync();
            return new CreateSessionResponse
            {
                Id = id,
                Succeeded = affected_rows == 1,
            };
        }
    }
}
