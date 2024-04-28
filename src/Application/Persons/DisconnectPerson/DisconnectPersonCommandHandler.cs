using Application.Common.Constants.MessageKeys;
using Application.Common.Models;
using Application.Common.Wrappers.Command;
using Application.Interfaces.Repositories;

namespace Application.Persons.DisconnectPerson;

public class DisconnectPersonCommandHandler : ICommandHandler<DisconnectPersonCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public DisconnectPersonCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<OperationResult> Handle(DisconnectPersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _unitOfWork.Persons.GetByIdAsync(request.PersonId, cancellationToken);
        
        var personToDisconnect = await _unitOfWork.Persons.GetByIdAsync(request.PersonIdToRemoveConnectionWith, cancellationToken);
        
        if (!person.IsConnectedTo(personToDisconnect))
            return OperationResult.BadRequest(MessageKeys.Person.PersonNotConnected);

        person.Disconnect(personToDisconnect);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return OperationResult.Ok();

    }
}