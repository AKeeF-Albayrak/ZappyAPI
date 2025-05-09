using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.LoginHistory;
using ZappyAPI.Application.Abstractions.Services;
using ZappyAPI.Application.Repositories;

namespace ZappyAPI.Persistence.Services
{
    public class LoginHistoryService : ILoginHistoryService
    {
        readonly ILoginHistoryWriteRepository _loginHistoryWriteRepository;
        public LoginHistoryService(ILoginHistoryWriteRepository loginHistoryWriteRepository)
        {
            _loginHistoryWriteRepository = loginHistoryWriteRepository;
        }
        public async Task<CreateLoginHistoryResponse> CreateAsync(CreateLoginHistory model)
        {
            Guid id = Guid.NewGuid();
            var var = await _loginHistoryWriteRepository.AddAsync(new Domain.Entities.LoginHistory
            {
                Id = id,
                UserId = model.UserId,
                Succeeded = model.Succeeded,
                IpAdress = model.IpAdress,
                UserAgent = model.UserAgent,
                CreatedDate = DateTime.UtcNow,
            });
            int affected_rows = await _loginHistoryWriteRepository.SaveAsync();
            return new CreateLoginHistoryResponse
            {
                Id = id,
                Succeeded = affected_rows == 1,
            };
        }
    }
}
