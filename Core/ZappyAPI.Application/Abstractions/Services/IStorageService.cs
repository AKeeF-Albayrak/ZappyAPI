using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.Storage;

namespace ZappyAPI.Application.Abstractions.Services
{
    public interface IStorageService
    {
        Task<UploadResult> UploadAsync(Upload upload);
        Task<byte[]> GetAsync(string filePath);
        Task<bool> DeleteAsync(string filePath);
    }
}
