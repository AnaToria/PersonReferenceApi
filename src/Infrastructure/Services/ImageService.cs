using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class ImageService : IImageService
{
    private readonly string _multimediaGetEndpoint;
    private readonly string _folderPath;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ImageService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _multimediaGetEndpoint = configuration["Multimedia:GetEndpoint"]!;
        _folderPath = configuration["Multimedia:FolderPath"]!;
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        var fileName = file.FileName;
        
        var filePath = GetFilePath(fileName);

        await using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return GetImageUrl(fileName);
    }

    public Task<byte[]> GetAsync(string fileName)
    {
        var filePath = GetFilePath(fileName);

        return File.ReadAllBytesAsync(filePath);
    }

    public Task RemoveAsync(string fileName)
    {
        var filePath = GetFilePath(fileName);
        
        File.Delete(filePath);
        
        return Task.CompletedTask;
    }
    
    private string GetFilePath(string imageName)
    {
        if (!Directory.Exists(_folderPath))
            Directory.CreateDirectory(_folderPath);

        var filePath = Path.Combine(_folderPath, $"{imageName}");
        return filePath;
    }

    private string GetImageUrl(string imageName)
    {
        var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
        return $"{baseUrl}/{_multimediaGetEndpoint}{imageName}";
    }
}