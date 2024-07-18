using Microsoft.Extensions.Localization;

namespace LocalizationApp.LocalizationService
{
    public interface ILocalizationService
    {
        string? this[string name, HttpContext httpContext] { get; }
    }
}
