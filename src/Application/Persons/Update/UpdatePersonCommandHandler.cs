using Application.Common.Models;
using Application.Common.Wrappers.Command;
using Application.Persons.AddPerson;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Persons.Update;


internal class UpdatePersonCommandHandler : ICommandHandler<UpdatePersonCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePersonCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<int>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var person = await _unitOfWork.Persons.GetByIdAsync(request.Id);


            _unitOfWork.Persons.Update(person);
            await _unitOfWork.CommitAsync();
            
            return new OperationResult<int>(ResultCode.Ok, person.Id);
        }
        catch (Exception)
        {
            return new OperationResult<int>(ResultCode.InternalError, -1);
        }
    }
}