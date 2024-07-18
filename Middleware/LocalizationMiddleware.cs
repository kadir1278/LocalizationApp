using LocalizationApp.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace LocalizationApp.Middleware
{
    public class LocalizationMiddleware : IMiddleware
    {
        private readonly IList<CultureInfo>? _cultureInfos;
        private readonly CultureInfo _defaultCultureInfo;
        private readonly IMemoryCache _memoryCache;
        private readonly SystemContext _systemContext;
        public LocalizationMiddleware(IOptions<RequestLocalizationOptions> requestLocalizationOptions, IMemoryCache memoryCache, SystemContext systemContext)
        {
            _cultureInfos = requestLocalizationOptions.Value.SupportedCultures;
            _defaultCultureInfo = requestLocalizationOptions.Value.DefaultRequestCulture.Culture;
            _memoryCache = memoryCache;
            _systemContext = systemContext;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {


            string cookieName = CookieConstants.CookieName;
            string? selectedCultureInfo = context.Request.Cookies[cookieName];
            string checkCultureInfo = _cultureInfos?.Where(x => x.Name == selectedCultureInfo)
                                                    .FirstOrDefault()?.Name ?? _defaultCultureInfo.Name;
            string cacheKey = cookieName + checkCultureInfo;
            List<LocalizationModel>? cacheValue;
            _memoryCache.TryGetValue<List<LocalizationModel>>(cacheKey, out cacheValue);
            // servis seviyesinde en doğru cultureınfo bilgisine ulaşmak için items eklendi
            context.Items["CultureInfo"] = checkCultureInfo;
            if (cacheValue is not null)
            {
                await next(context);
            }
            else
            {
                List<LocalizationModel> localizationModels = _systemContext.LocalizationModels.Where(x => x.CultureInfo == checkCultureInfo).ToList();
                context.Response.Cookies.Append(cookieName, checkCultureInfo);
                _memoryCache.Set(cacheKey, localizationModels, DateTimeOffset.MaxValue);
                await next(context);
            }

        }
    }
}
