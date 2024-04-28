using Application.Common.Constants.MessageKeys;
using Application.Interfaces.Repositories;
using FluentValidation;

namespace Application.Persons.ConnectPerson;

public class ConnectPersonCommandValidator : AbstractValidator<ConnectPersonCommand>
{
    public ConnectPersonCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(command => command.PersonId)
            .NotEmpty()
            .WithMessage(MessageKeys.General.NonEmpty)
            .NotNull()
            .WithMessage(MessageKeys.General.NonEmpty)
            .MustAsync(async (id, cancellationToken) =>
                await unitOfWork.Persons.ExistsWithIdAsync(id, cancellationToken: cancellationToken))
            .WithMessage(MessageKeys.Person.PersonNotExistsWithId);
        
        RuleFor(command => command.PersonIdToConnectWith)
            .NotEmpty()
            .WithMessage(MessageKeys.General.NonEmpty)
            .NotNull()
            .WithMessage(MessageKeys.General.NonEmpty)
            .MustAsync(async (id, cancellationToken) =>
                await unitOfWork.Persons.ExistsWithIdAsync(id, cancellationToken: cancellationToken))
            .WithMessage(MessageKeys.Person.PersonNotExistsWithId);

        RuleFor(command => command.RelationshipType)
            .IsInEnum();
    }
}