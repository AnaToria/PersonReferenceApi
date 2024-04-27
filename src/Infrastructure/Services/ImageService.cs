using Application.Interfaces;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class ImageService : IImageService
{
    private readonly string _uploadsDirectory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ImageService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor; 
        _uploadsDirectory = "../../Uploads/";
    }

    public async Task<string> UploadImageAsync(IFormFile imageFile, string imageName)
    {
        try
        {
            if (!Directory.Exists(_uploadsDirectory))
                Directory.CreateDirectory(_uploadsDirectory);

            var filePath = Path.Combine(_uploadsDirectory, $"{imageName}.jpeg");

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return $"{imageName}.jpeg";
        }
        catch (Exception ex)
        {
            throw new Exception("Error uploading image", ex);
        }
    }

    public string GetImageUrl(string imageName)
    {
        string baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
        return $"{baseUrl}/{_uploadsDirectory}/{imageName}";
    }
}