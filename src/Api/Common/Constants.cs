using Microsoft.Net.Http.Headers;

namespace Api.Common;

internal static class Constants
{
    internal static string LanguageHeaderName => HeaderNames.AcceptLanguage;
    internal const string DefaultLanguage = "ka";
}