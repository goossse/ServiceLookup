using Microsoft.AspNetCore.Mvc;

namespace ServiceLookup.Controllers
{
    public class ModerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
