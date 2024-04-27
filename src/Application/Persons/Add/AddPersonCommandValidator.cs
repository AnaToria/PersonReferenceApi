using System.Text.RegularExpressions;
using Application.Common.Constants.MessageKeys;
using Application.Interfaces.Repositories;
using FluentValidation;

namespace Application.Persons.Add;

public class AddPersonCommandValidator : AbstractValidator<AddPersonCommand>
{
    public AddPersonCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage(MessageKeys.General.NonEmpty)
            .NotNull()
            .WithMessage(MessageKeys.General.NonEmpty)
            .Length(2, 50)
            .WithMessage(MessageKeys.General.Between2And50Character)
            .Matches(@"^((\p{IsGeorgian}+)|([a-zA-Z]+))$")
            .WithMessage(MessageKeys.General.ContainsOnlyGeorgianOrLatinCharacters)
            .Custom((name, context) =>
            {
                var surname = context.InstanceToValidate.Surname;
                if (!string.IsNullOrEmpty(surname))
                {
                    var nameIsGeorgian = Regex.IsMatch(name, @"\p{IsGeorgian}+");
                    var surnameIsGeorgian = Regex.IsMatch(surname, @"\p{IsGeorgian}+");

                    if (nameIsGeorgian != surnameIsGeorgian)
                    {
                        context.AddFailure(MessageKeys.Person.NameAndSurnameSameLanguage);
                    }
                }
            });
        
        RuleFor(command => command.Surname)
            .NotEmpty()
            .WithMessage(MessageKeys.General.NonEmpty)
            .NotNull()
            .WithMessage(MessageKeys.General.NonEmpty)
            .Length(2, 50)
            .WithMessage(MessageKeys.General.Between2And50Character)
            .Matches(@"^((\p{IsGeorgian}+)|([a-zA-Z]+))$")
            .WithMessage(MessageKeys.General.ContainsOnlyGeorgianOrLatinCharacters);
        
        RuleFor(command => command.Pin)
            .NotEmpty()
            .WithMessage(MessageKeys.General.NonEmpty)
            .NotNull()
            .WithMessage(MessageKeys.General.NonEmpty)
            .Length(11)
            .WithMessage(MessageKeys.Person.PinExactly11Character)
            .Matches(@"^\d+$")
            .WithMessage(MessageKeys.General.ContainsOnlyNumericCharacters)
            .MustAsync(async (pin, cancellationToken) =>
                !await unitOfWork.Persons.ExistsWithPinAsync(pin, cancellationToken: cancellationToken))
            .WithMessage(MessageKeys.Person.PersonExistsWithPin);
        
        RuleFor(command => command.BirthDate)
            .NotNull()
            .WithMessage(MessageKeys.General.NonEmpty)
            .Must(date => DateTime.Today.AddYears(-18) >= date)
            .WithMessage(MessageKeys.Person.PersonOlderThan18);
        
        RuleFor(command => command.CityId)
            .MustAsync(async (cityId, cancellationToken) =>
                await unitOfWork.Cities.ExistsWithIdAsync(cityId, cancellationToken))
            .WithMessage(MessageKeys.City.CityDoesNotExists);

        RuleFor(command => command.PhoneNumbers)
            .Must(phoneNumbers => phoneNumbers != null && phoneNumbers.Any())
            .WithMessage(MessageKeys.General.NonEmpty);
        
        RuleForEach(command => command.PhoneNumbers)
            .ChildRules(phoneNumber =>
            {
                phoneNumber.RuleFor(p => p.Number)
                    .NotNull()
                    .WithMessage(MessageKeys.General.NonEmpty)
                    .NotEmpty()
                    .WithMessage(MessageKeys.General.NonEmpty)
                    .MustAsync(async (p, cancellationToken) =>
                        !await unitOfWork.PhoneNumbers.ExistsWithNumberAsync(p, cancellationToken))
                    .WithMessage(MessageKeys.PhoneNumber.PhoneAlreadyRegistered);
            });
    }
}