using Application.Common.Constants.MessageKeys;
using FluentValidation;

namespace Application.Persons.GetAll;

public class GetAllPersonsQueryValidator : AbstractValidator<GetAllPersonsQuery>
{
    public GetAllPersonsQueryValidator()
    {
        RuleFor(command => command.PageNumber)
            .NotEmpty()
            .WithMessage(MessageKeys.General.NonEmpty)
            .NotNull()
            .WithMessage(MessageKeys.General.NonEmpty);
        
        RuleFor(command => command.PageSize)
            .NotEmpty()
            .WithMessage(MessageKeys.General.NonEmpty)
            .NotNull()
            .WithMessage(MessageKeys.General.NonEmpty);

    }
}