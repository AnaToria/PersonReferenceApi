using Application.Common.Models;
using Application.Common.Wrappers.Query;
using Application.Interfaces.Services;

namespace Application.Multimedia.Get;

public class GetMultimediaQueryHandler : IQueryHandler<GetMultimediaQuery, byte[]>
{
    private readonly IImageService _imageService;

    public GetMultimediaQueryHandler(IImageService imageService) => _imageService = imageService;

    public async Task<OperationResult<byte[]>> Handle(GetMultimediaQuery request, CancellationToken cancellationToken)
    {
        var blob = await _imageService.GetAsync(request.FileName);

        return new OperationResult<byte[]>(ResultCode.Ok, blob);
    }
}