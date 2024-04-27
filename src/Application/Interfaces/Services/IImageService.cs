using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Services;

public interface IImageService
{
    Task<string> UploadImageAsync(IFormFile imageFile, string imageName);
    string GetImageUrl(string fileName);
}