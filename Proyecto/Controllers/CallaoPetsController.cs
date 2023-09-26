using Microsoft.AspNetCore.Mvc;

namespace Proyecto.Controllers
{
    public class CallaoPetsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
