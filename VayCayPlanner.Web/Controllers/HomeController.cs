using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VayCayPlanner.Common.Constants;
using VayCayPlanner.Common.ViewModels;
using VayCayPlanner.Data.Repositories.Contracts;

namespace VayCayPlanner.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITripRepository _tripRepository;

        public HomeController(ILogger<HomeController> logger, 
            ITripRepository tripRepository)
        {
            _logger = logger;
            _tripRepository = tripRepository;
        }

        public async Task<IActionResult> Index()
        {
            //var myTrips = await _tripRepository.GetUpcomingTripsAsync();
            //if (myTrips.Count > 0)
            //{
            //    return RedirectToAction("Index", "Trips");
            //}
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}