using LocalizationApp.LocalizationService;
using LocalizationApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

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
        [HttpGet, Route("/{key}")]
        public IActionResult Index(string key)
        {
            ViewData["culturedata"] = _localizationService[key, this.HttpContext];
            return View("Index");
        }
    }
}
