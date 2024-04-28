using Application.Common.Constants.MessageKeys;
using Application.Common.Models;
using Application.Common.Wrappers.Command;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.Persons.Update;

internal class UpdatePersonCommandHandler : ICommandHandler<UpdatePersonCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;

    public UpdatePersonCommandHandler(IUnitOfWork unitOfWork, IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
    }

    public async Task<OperationResult> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var existingPerson = await _unitOfWork.Persons.GetByIdAsync(request.Id, cancellationToken);

        if (existingPerson!.Pin != request.Pin && await _unitOfWork.Persons.ExistsWithPinAsync(request.Pin, cancellationToken))
            return OperationResult.BadRequest(MessageKeys.Person.PersonExistsWithPin);
        
        foreach (var number in request.PhoneNumbers)
        {
            var existingNumber = existingPerson.PhoneNumbers.FirstOrDefault(p => p.Number == number.Number);
            
            if (existingNumber is not null && existingNumber.Type != number.Type) 
                existingNumber.UpdateType(number.Type);

            if (existingNumber is null)
            {
                if (await _unitOfWork.PhoneNumbers.ExistsWithNumberAsync(number.Number, cancellationToken))
                    return OperationResult.BadRequest(MessageKeys.PhoneNumber.PhoneAlreadyRegistered);

                existingPerson.AddNumber(PhoneNumber.Create(number.Type, number.Number));
            }
        }

        var existingNumbers = new List<PhoneNumber>(existingPerson.PhoneNumbers);
        
        foreach (var number in existingNumbers)
        {
            if (request.PhoneNumbers.All(p => p.Number != number.Number))
            {
                existingPerson.RemoveNumber(number);
            }
        }

        if (existingPerson.City.Id != request.CityId)
        {
            var city = await _unitOfWork.Cities.GetByIdAsync(request.CityId, cancellationToken);
            existingPerson.UpdateCity(city!);
        }
        
        try
        {
            if (existingPerson!.Image != request.Image)
                await _imageService.RemoveAsync(existingPerson.Image!);
        }
        catch
        {
            // ignored
        }

        
        existingPerson.Update(
            request.Name,
            request.Surname,
            request.Gender,
            request.Pin,
            request.BirthDate,
            existingPerson.Image
        );

        _unitOfWork.Persons.Update(existingPerson);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return OperationResult.Ok();
    }
}