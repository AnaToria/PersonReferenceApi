using Application.Common.Models;
using Application.Common.Wrappers.Command;
using Application.Interfaces.Repositories;

namespace Application.Persons.Delete;

public class DeletePersonCommandHandler : ICommandHandler<DeletePersonCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePersonCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<bool>> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _unitOfWork.Persons.GetByIdAsync(request.Id, cancellationToken);
        
        person!.Delete();

        _unitOfWork.Persons.Update(person);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new OperationResult<bool>(ResultCode.Ok, true);
    }
}