using Microsoft.AspNetCore.Mvc;

namespace VayCayPlanner.Web.Controllers
{
    public class LodgingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
