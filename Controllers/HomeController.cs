using LocalizationApp.LocalizationService;
using LocalizationApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;

namespace LocalizationApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILocalizationService _localizationService;
        public HomeController(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        public IActionResult Index()
        {
            ViewData["culturedata"] = _localizationService["Test", this.HttpContext];
            return View();
        }
    }
}
