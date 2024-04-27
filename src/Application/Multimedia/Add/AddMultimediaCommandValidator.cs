using Application.Common.Constants.MessageKeys;
using FluentValidation;

namespace Application.Multimedia.Add;

public class AddMultimediaCommandValidator : AbstractValidator<AddMultimediaCommand>
{
    private static readonly string ValidFormat = "image/jpeg";

    public AddMultimediaCommandValidator()
    {
        RuleFor(command =>  command.File)
            .NotNull()
            .WithMessage(MessageKeys.General.NonEmpty);
        
        RuleFor(command =>  command.File.ContentType)
            .NotNull()
            .Must(contentType=> ValidFormat == contentType)
            .WithMessage(MessageKeys.File.TypeNotAllowed);
    }
}