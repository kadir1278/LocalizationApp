using LocalizationApp.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LocalizationApp.Controllers
{
    public class LocalizationController : Controller
    {
        private readonly SystemContext _systemContext;
        private readonly RequestLocalizationOptions _localizationOptions;


        public LocalizationController(SystemContext systemContext, IOptions<RequestLocalizationOptions> localizationOptions)
        {
            _systemContext = systemContext;
            _localizationOptions = localizationOptions.Value;
           
        }

        public IActionResult Add()
        {
            var allCultures = _localizationOptions.SupportedCultures
                    .Select(culture => new
                    {
                        Name = culture.Name,
                        Text = culture.DisplayName
                    }).ToList();

            ViewBag.AllCultures = allCultures;
            return View(_systemContext.LocalizationModels.ToList());
        }
        [HttpPost]
        public IActionResult Add(LocalizationModel model)
        {
            _systemContext.Add(model);
            _systemContext.SaveChanges();
            var allCultures = _localizationOptions.SupportedCultures
                  .Select(culture => new
                  {
                      Name = culture.Name,
                      Text = culture.DisplayName
                  }).ToList();

            ViewBag.AllCultures = allCultures;
            return View(_systemContext.LocalizationModels.ToList());
        }
    }
}
