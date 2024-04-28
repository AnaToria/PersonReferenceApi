using Domain.Enums;

namespace Application.Interfaces.Services;

public interface IStringLocalizer
{
    string Get(string key, Language language);
}