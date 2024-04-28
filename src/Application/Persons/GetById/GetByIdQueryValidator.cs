using Application.Common.Constants.MessageKeys;
using Application.Interfaces.Repositories;
using FluentValidation;

namespace Application.Persons.GetById;

public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
{
    public GetByIdQueryValidator(IUnitOfWork unitOfWork)
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