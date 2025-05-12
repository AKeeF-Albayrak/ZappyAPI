using System;
using System.IO;
using System.Threading.Tasks;
using ZappyAPI.Application.Abstractions.DTOs.Storage;
using ZappyAPI.Application.Abstractions.Services;

namespace ZappyAPI.Infrastructure.Storage
{
    public class StorageService : IStorageService
    {
        private readonly string _mediaRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "media");

        public StorageService()
        {
            if (!Directory.Exists(_mediaRootPath))
                Directory.CreateDirectory(_mediaRootPath);
        }

        public async Task<UploadResult> UploadAsync(Upload upload)
        {
            byte[] fileBytes;

            try
            {
                fileBytes = Convert.FromBase64String(upload.FileBytes);
            }
            catch (FormatException)
            {
                throw new ArgumentException("Geçersiz base64 dosya verisi.");
            }

            string subFolder = GetMediaSubFolder(upload.ContentType);
            string fullFolderPath = Path.Combine(_mediaRootPath, subFolder);

            if (!Directory.Exists(fullFolderPath))
                Directory.CreateDirectory(fullFolderPath);

            string filePath = Path.Combine(fullFolderPath, upload.FileName);

            await File.WriteAllBytesAsync(filePath, fileBytes);

            return new UploadResult
            {
                FilePath = $"/media/{subFolder}/{upload.FileName}",
                FileName = upload.FileName,
                ContentType = upload.ContentType,
                FileSize = fileBytes.Length
            };
        }


        public async Task<byte[]> GetAsync(string filePath)
        {
            var sanitizedPath = filePath.Replace("/", Path.DirectorySeparatorChar.ToString());
            var fullPath = Path.Combine(_mediaRootPath, sanitizedPath.TrimStart(Path.DirectorySeparatorChar));

            if (!File.Exists(fullPath))
                throw new FileNotFoundException("Dosya bulunamadı.", filePath);

            return await File.ReadAllBytesAsync(fullPath);
        }

        public Task<bool> DeleteAsync(string filePath)
        {
            var sanitizedPath = filePath.Replace("/", Path.DirectorySeparatorChar.ToString());
            var fullPath = Path.Combine(_mediaRootPath, sanitizedPath.TrimStart(Path.DirectorySeparatorChar));

            if (!fullPath.StartsWith(_mediaRootPath))
                throw new UnauthorizedAccessException("Geçersiz dosya yolu.");

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }


        private string GetMediaSubFolder(string contentType)
        {
            if (contentType.StartsWith("image/"))
                return "photos";
            else if (contentType.StartsWith("video/"))
                return "videos";
            else if (contentType.StartsWith("audio/"))
                return "audios";
            else
                throw new NotSupportedException($"Desteklenmeyen içerik türü: {contentType}");
        }
    }
}
