using Application.Common.Models;
using Application.Common.Wrappers.Command;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.Persons.Add;

internal class AddPersonCommandHandler : ICommandHandler<AddPersonCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    public AddPersonCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<OperationResult<int>> Handle(AddPersonCommand request, CancellationToken cancellationToken)
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

        await _unitOfWork.Persons.AddAsync(person, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new OperationResult<int>(ResultCode.Created, person.Id);
    }
}