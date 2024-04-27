namespace Api.Models;

public class UploadImageRequest
{
    public IFormFile Image { get; set; }
    public int PersonId { get; set; }
    
    public static UploadImageRequest Create(IFormFile image, int personId) =>
        new()
        {
            Image = image,
            PersonId = personId
        };
}