using Application.Common.Wrappers.Command;
using Microsoft.AspNetCore.Http;

namespace Application.Persons.UploadImage;

public class UploadImageCommand : Command<string?>
{
    public IFormFile Image { get; set; }
    public int PersonId { get; set; }
}