using Application.Common.Constants.MessageKeys;
using FluentValidation;

namespace Application.Multimedia.Get;

public class GetMultimediaQueryValidator : AbstractValidator<GetMultimediaQuery>
{
    public GetMultimediaQueryValidator()
    {
        RuleFor(command =>  command.FileName)
            .NotNull()
            .WithMessage(MessageKeys.General.NonEmpty)
            .NotEmpty()
            .WithMessage(MessageKeys.General.NonEmpty);
    }
}