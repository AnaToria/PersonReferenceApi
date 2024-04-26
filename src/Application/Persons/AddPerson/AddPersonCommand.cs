using System.Text.RegularExpressions;
using Application.Common.Wrappers.Command;
using Domain.Enums;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Persons.AddPerson;

public class AddPersonCommand : ICommand<int>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public string Pin { get; set; }
    public DateTime BirthDate { get; set; }
    public int CityId { get; set; }
    public List<PhoneNumberRequest> PhoneNumbers { get; set; }
}

public class AddPersonCommandValidator : AbstractValidator<AddPersonCommand>
{
    public AddPersonCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Name must not be empty.")
            .NotNull()
            .WithMessage("Name must not be null.")
            .Length(2, 50)
            .WithMessage("Name must not be between 2-50 character.")
            .Matches(@"^((\p{IsGeorgian}+)|([a-zA-Z]+))$")
            .WithMessage("Name must contain only Latin or Georgian characters.")
            .Custom((name, context) =>
            {
                var surname = context.InstanceToValidate.Surname;
                if (!string.IsNullOrEmpty(surname))
                {
                    var nameIsGeorgian = Regex.IsMatch(name, @"\p{IsGeorgian}+");
                    var surnameIsGeorgian = Regex.IsMatch(surname, @"\p{IsGeorgian}+");

                    if (nameIsGeorgian != surnameIsGeorgian)
                    {
                        context.AddFailure("Name and surname must be in the same language.");
                    }
                }
            });
        
        RuleFor(command => command.Surname)
            .NotEmpty()
            .WithMessage("Surname must not be empty.")
            .NotNull()
            .WithMessage("Surname must not be null.")
            .Length(2, 50)
            .WithMessage("Surname must not be between 2-50 character.")
            .Matches(@"^((\p{IsGeorgian}+)|([a-zA-Z]+))$")
            .WithMessage("Surname must contain only Latin or Georgian characters.");
        
        RuleFor(command => command.Pin)
            .NotEmpty()
            .WithMessage("Pin must not be empty.")
            .NotNull()
            .WithMessage("Pin must not be null.")
            .Length(11)
            .WithMessage("Pin must not be exactly 11 character.")
            .Matches(@"^\d+$")
            .WithMessage("Pin must contain only numeric characters.")
            .MustAsync(async (pin, cancellationToken) =>
                !await unitOfWork.Persons.ExistsWithPinAsync(pin, cancellationToken: cancellationToken))
            .WithMessage("Person with this pin already exists.");
        
        RuleFor(command => command.BirthDate)
            .NotNull()
            .WithMessage("BirthDate must not be null.")
            .Must(date => DateTime.Today.AddYears(-18) >= date)
            .WithMessage("Person must be 18 years or older to proceed.");
        
        RuleFor(command => command.CityId)
            .MustAsync(async (cityId, cancellationToken) =>
                await unitOfWork.Cities.ExistsWithIdAsync(cityId, cancellationToken))
            .WithMessage("City with this id does not exist.");

        RuleFor(command => command.PhoneNumbers)
            .Must(phoneNumbers => phoneNumbers != null && phoneNumbers.Any())
            .WithMessage("Phone numbers must be included.");
        
        RuleForEach(command => command.PhoneNumbers)
            .ChildRules(phoneNumber =>
            {
                phoneNumber.RuleFor(p => p.Number)
                    .NotNull()
                    .WithMessage("Phone number should not be null.")
                    .NotEmpty()
                    .WithMessage("Phone number should not be empty.")
                    .MustAsync(async (p, cancellationToken) =>
                        !await unitOfWork.PhoneNumbers.ExistsWithNumberAsync(p, cancellationToken))
                    .WithMessage("Phone number - '{PropertyValue}' is already registered.");
            });
    }
}