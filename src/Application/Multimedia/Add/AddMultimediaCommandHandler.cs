using Application.Common.Models;
using Application.Common.Wrappers.Command;
using Application.Interfaces.Services;

namespace Application.Multimedia.Add;

public class AddMultimediaCommandHandler : ICommandHandler<AddMultimediaCommand, string>
{
    private readonly IImageService _imageService;

    public AddMultimediaCommandHandler(IImageService imageService)
    {
        _imageService = imageService;
    }
    
    public async Task<OperationResult<string>> Handle(AddMultimediaCommand request, CancellationToken cancellationToken)
    {
        var fileName = $"{Guid.NewGuid().ToString()}_{request.File.Name}";
        
        var image = await _imageService.UploadImageAsync(request.File);
        
        return new OperationResult<string>(ResultCode.Ok, image);
    }
}