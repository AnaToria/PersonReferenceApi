using Application.Common.Constants.MessageKeys;
using Application.Interfaces.Repositories;
using FluentValidation;

namespace Application.Persons.Delete;

public class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
{
    public DeletePersonCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage(MessageKeys.General.NonEmpty)
            .NotNull()
            .WithMessage(MessageKeys.General.NonEmpty)
            .MustAsync(async (id, cancellationToken) =>
                await unitOfWork.Persons.ExistsWithIdAsync(id, cancellationToken: cancellationToken))
            .WithMessage(MessageKeys.Person.PersonNotExistsWithId);
    }
}