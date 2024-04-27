using Application.Common.Constants.MessageKeys;
using Application.Interfaces.Repositories;
using FluentValidation;

namespace Application.Persons.UploadImage;

public class UploadImageCommandValidator : AbstractValidator<UploadImageCommand>
{
    private static readonly List<string> ValidFormats = [ "image/jpeg", "image/jpg", "image/png"];

    public UploadImageCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(command =>  command.Image.ContentType)
            .NotNull()
            .Must(contentType=> ValidFormats.Contains(contentType))
            .WithMessage(MessageKeys.File.TypeNotAllowed);
        
        RuleFor(command => command.PersonId)
            .MustAsync(async (userId, cancellationToken) =>
                await unitOfWork.Persons.ExistsWithIdAsync(userId, cancellationToken))
            .WithMessage(MessageKeys.Person.PersonNotExistsWithId);
    }
}