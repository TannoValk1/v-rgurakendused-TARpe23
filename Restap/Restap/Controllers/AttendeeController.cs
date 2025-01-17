using Microsoft.AspNetCore.Mvc;

namespace Restap.Controllers
{
    public class AttendeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
