using Domain.Interfaces;
using FluentValidation;

namespace Application.Persons.UploadImage;

public class UploadImageCommandValidator : AbstractValidator<UploadImageCommand>
{
    public UploadImageCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(command =>  command.Image.ContentType)
            .NotNull()
            .Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
            .WithMessage("File type is not allowed");
        
        RuleFor(command => command.PersonId)
            .MustAsync(async (userId, cancellationToken) =>
                await unitOfWork.Persons.ExistsWithIdAsync(userId, cancellationToken))
            .WithMessage("Person with this id does not exist.");
    }
}