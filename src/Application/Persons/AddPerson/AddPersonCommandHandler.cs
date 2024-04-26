using Application.Common;
using Application.Common.Models;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Persons.AddPerson;

internal class AddPersonCommandHandler : IRequestHandler<AddPersonCommand, OperationResult<int>>
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
            var phoneNumbers = new List<PhoneNumber>();
            request.PhoneNumbers.ForEach(p => phoneNumbers.Add(new PhoneNumber
            {
                Number = p.Number,
                Type = p.Type
            }));

            var person = new Person
            {
                Name = request.Name,
                Surname = request.Surname,
                Gender = request.Gender,
                Pin = request.Pin,
                BirthDate = request.BirthDate,
                CityId = request.CityId,
                PhoneNumbers = phoneNumbers
            };

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