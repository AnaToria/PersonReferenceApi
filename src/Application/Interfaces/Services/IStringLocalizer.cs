namespace Application.Interfaces.Services;

public interface IStringLocalizer
{
    string Get(string key, string languageCode);
}