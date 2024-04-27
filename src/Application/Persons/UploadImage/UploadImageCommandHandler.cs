using Application.Common.Models;
using Application.Common.Wrappers.Command;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Persons.AddPerson;
using Domain.Entities;

namespace Application.Persons.UploadImage;

internal class UploadImageCommandHandler : ICommandHandler<UploadImageCommand, string?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;

    public UploadImageCommandHandler(IUnitOfWork unitOfWork, IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
    }

    public async Task<OperationResult<string?>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var person = await _unitOfWork.Persons.GetByIdAsync(request.PersonId, cancellationToken);
            person!.Image = await _imageService.UploadImageAsync(request.Image,$"{request.PersonId}_image");

            await _unitOfWork.CommitAsync();
            
            return new OperationResult<string?>(ResultCode.Ok, person!.Image);
        }
        catch (Exception)
        {
            return new OperationResult<string?>(ResultCode.InternalError, null);
        }
    }
}