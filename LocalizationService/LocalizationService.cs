using LocalizationApp.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;

namespace LocalizationApp.LocalizationService
{
    public class LocalizationService : ILocalizationService
    {
        private readonly IMemoryCache _memoryCache;

        public LocalizationService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public string? this[string name, HttpContext httpContext] => _memoryCache.Get<List<LocalizationModel>>($"{CookieConstants.CookieName}{httpContext.Items["CultureInfo"] ?? "tr-TR"}")?.FirstOrDefault(x => x.Name == name)?.Value;

    }
}
