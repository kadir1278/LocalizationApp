using LocalizationApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocalizationApp.Controllers
{
    public class LocalizationController : Controller
    {
        private readonly SystemContext _systemContext;

        public LocalizationController(SystemContext systemContext)
        {
            _systemContext = systemContext;
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(LocalizationModel model)
        {
            _systemContext.Add(model);
            _systemContext.SaveChanges();
            return View();
        }
    }
}
