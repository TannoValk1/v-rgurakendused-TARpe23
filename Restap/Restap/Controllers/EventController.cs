using Microsoft.AspNetCore.Mvc;

namespace Restap.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
