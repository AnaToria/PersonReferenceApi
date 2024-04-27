namespace Infrastructure.Persistence.Models;

public class Localization
{
    public string ResourceKey { get; set; }
    public string LanguageCode { get; set; }
    public string LocalizedValue { get; set; }
}