using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Services;

public interface IImageService
{
    Task<string> UploadImageAsync(IFormFile file);
    Task<byte[]> GetAsync(string fileName);
    Task RemoveAsync(string fileName);
    string GetImageUrl(string imageName);
}