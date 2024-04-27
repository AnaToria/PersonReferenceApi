using Application.Common.Models;
using Application.Common.Wrappers.Command;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.Persons.Update;

internal class UpdatePersonCommandHandler : ICommandHandler<UpdatePersonCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdatePersonCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<OperationResult<int>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var city = await _unitOfWork.Cities.GetByIdAsync(request.CityId, cancellationToken);
        var phoneNumbers = request.PhoneNumbers
            .Select(p => PhoneNumber.Create(p.Type, p.Number))
            .ToList();

        var person = Person.Create(
            request.Name,
            request.Surname,
            request.Gender,
            request.Pin,
            request.BirthDate,
            city!,
            phoneNumbers
        );

        _unitOfWork.Persons.Update(person);
        await _unitOfWork.CommitAsync();

        return new OperationResult<int>(ResultCode.Created, person.Id);
    }
}