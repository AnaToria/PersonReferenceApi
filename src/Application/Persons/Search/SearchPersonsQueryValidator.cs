using Application.Common.Constants.MessageKeys;
using FluentValidation;

namespace Application.Persons.Search;

public class SearchPersonsQueryValidator : AbstractValidator<SearchPersonsQuery>
{
    public SearchPersonsQueryValidator()
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