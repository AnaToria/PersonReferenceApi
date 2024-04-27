using Application.Common.Wrappers.Command;
using Microsoft.AspNetCore.Http;

namespace Application.Multimedia.Add;

public class AddMultimediaCommand : Command<string>
{
    public IFormFile File { get; set; }
}