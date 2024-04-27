using Application.Common.Models;
using Application.Common.Wrappers.Command;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.Persons.Update;

internal class UpdatePersonCommandHandler : ICommandHandler<UpdatePersonCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;

    public UpdatePersonCommandHandler(IUnitOfWork unitOfWork, IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
    }

    public async Task<OperationResult<int>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var city = await _unitOfWork.Cities.GetByIdAsync(request.CityId, cancellationToken);
        var phoneNumbers = request.PhoneNumbers
            .Select(p => PhoneNumber.Create(p.Type, p.Number))
            .ToList();

        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        var existingPerson = await _unitOfWork.Persons.GetByIdAsync(request.Id, cancellationToken);

        if (existingPerson!.Image != request.Image)
            await _imageService.RemoveAsync(existingPerson.Image!);

        var person = Person.Create(
            request.Name,
            request.Surname,
            request.Gender,
            request.Pin,
            request.BirthDate,
            existingPerson.Image,
            city!,
            phoneNumbers
        );

        _unitOfWork.Persons.Update(person);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new OperationResult<int>(ResultCode.Created, person.Id);
    }
}