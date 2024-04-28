using Application.Common.Constants.MessageKeys;
using Application.Common.Models;
using Application.Common.Wrappers.Command;
using Application.Interfaces.Repositories;

namespace Application.Persons.ConnectPerson;

public class ConnectPersonCommandHandler : ICommandHandler<ConnectPersonCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public ConnectPersonCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<OperationResult> Handle(ConnectPersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _unitOfWork.Persons.GetByIdAsync(request.PersonId, cancellationToken);
        
        var personToConnect = await _unitOfWork.Persons.GetByIdAsync(request.PersonIdToConnectWith, cancellationToken);
        
        if (person.IsConnectedTo(personToConnect))
            return OperationResult.BadRequest(MessageKeys.Person.PersonAlreadyConnected);

        person.Connect(personToConnect, request.RelationshipType);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return OperationResult.Ok();
    }
}