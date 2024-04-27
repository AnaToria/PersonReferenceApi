using Application.Common.Models;
using Application.Common.Wrappers.Command;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Persons.AddPerson;

internal class AddPersonCommandHandler : ICommandHandler<AddPersonCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddPersonCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<int>> Handle(AddPersonCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var person = Person.Create(
                request.Name,
                request.Surname,
                request.Gender,
                request.Pin,
                request.BirthDate,
                (await _unitOfWork.Cities.GetByIdAsync(request.CityId, cancellationToken))!,
                request.PhoneNumbers.Select(p => PhoneNumber.Create(p.Type, p.Number)).ToList()
            );

            await _unitOfWork.Persons.AddAsync(person, cancellationToken);
            await _unitOfWork.CommitAsync();
            
            return new OperationResult<int>(ResultCode.Created, person.Id);
        }
        catch (Exception)
        {
            return new OperationResult<int>(ResultCode.InternalError, -1);
        }
    }
}