using System.Collections.Concurrent;
using Application.Interfaces.Services;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services;

public class StringLocalizer : IStringLocalizer
{
    private const string DefaultLocalization = "unknown message";
    private static ConcurrentDictionary<string, string> _localizations = new();
    private readonly IServiceProvider _serviceProvider;

    public StringLocalizer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _localizations = LoadLocalizations();
    }

    private ConcurrentDictionary<string, string> LoadLocalizations()
    {
        using var serviceScope = _serviceProvider.CreateScope();

        var personReferenceDbContext = serviceScope.ServiceProvider.GetRequiredService<PersonReferenceDbContext>();
        var localizations = personReferenceDbContext.Localizations.ToList();

        var result = new ConcurrentDictionary<string, string>();
        foreach (var localization in localizations)
        {
            result.TryAdd($"{localization.ResourceKey}_{localization.LanguageCode}",
                localization.LocalizedValue);
        }
        
        return result;
    }
    
    public string? Get(string key, string languageCode)
    {
        _localizations.TryGetValue($"{key}{languageCode}", out var value);

        return value ?? DefaultLocalization;
    }
    
    public void RefreshCache()
    {
        var localizations = LoadLocalizations();
        _localizations = localizations;
    }
}