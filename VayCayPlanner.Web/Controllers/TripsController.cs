using VayCayPlanner.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using VayCayPlanner.Common.ViewModels;
using VayCayPlanner.Data.Repositories.Contracts;
using AutoMapper;
using VayCayPlanner.Common.Traveler.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VayCayPlanner.Data.Models;
using VayCayPlanner.Common.ViewModels.Trip;
using VayCayPlanner.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using VayCayPlanner.Web.Services;

namespace VayCayPlanner.Web.Controllers
{
    public class TripsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITripRepository _tripRepository;
        private readonly ITravelerRepository _travelerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Subscriber> _userManager;
        private readonly ILogger<TripsController> _logger;

        public TripsController(ApplicationDbContext context,
            ITripRepository tripRepository,
            ITravelerRepository travelerRepository,
            IHttpContextAccessor httpContextAccessor,
            ILogger<TripsController> logger,
            UserManager<Subscriber> userManager)
        {
            _context = context;
            _tripRepository = tripRepository;
            _travelerRepository = travelerRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Trips/Upcoming
        public async Task<IActionResult> Index()
        {
            
            return View(await _tripRepository.GetUpcomingTripsAsync());
        }

        // GET: Trips/Past
        public async Task<IActionResult> PastTrips()
        {

            return View(await _tripRepository.GetPastTripsAsync());
        }

        // GET: Trips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Trips == null)
            {
                return NotFound();
            }

            var trip = await _tripRepository.GetTripDetail(id.Value);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // GET: Trips/AddNewTrip
        public IActionResult AddNewTrip()
        {
            return View();
        }

        // POST: Trips/FirstDestination
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FirstDestination(NewTripTemplate model)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User);
            var newModel = new NewTripTemplate { 
                UserId = user.Id,
                TripName = model.TripName,
                isTripComplete = false,
                isTravelersComplete = false,
                isComplete = false,
                isDestinationComplete = false
            };
            _context.Add(newModel);
            await _context.SaveChangesAsync();

            return View(newModel);
        }

        // GET: Trips/AddTravelers
        /// <summary>
        /// Updates the NewTripTemplate 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult AddTravelers(int Id)
        {
            //var user = await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User);
            var tripTemplate = _context.NewTripTemplates.Where(x => x.Id == Id ).FirstOrDefaultAsync();

            //if (!tripTemplate.isTripComplete)
            //{
            //    CreateNewTripVM newTrip = await _tripRepository.CreateNewTripWizard(model.TripName);
            //    tripTemplate.DestinationName = model.DestinationName;
            //    tripTemplate.TripId = newTrip.TripId;
            //    tripTemplate.TravelGroupId = newTrip.GroupId;
            //    tripTemplate.isTripComplete = true;
            //    tripTemplate.isComplete = false;
            //    _context.Update(tripTemplate);
            //    await _context.SaveChangesAsync();
            //}
            //else
            //{
            //    if (tripTemplate.TravelGroupId != null)
            //    {
            //        var newTraveler = new TravelerAddVM
            //        {
            //            FullName = model.FullName,
            //            EmailAddress = model.EmailAddress,
            //            TravelGroupId = tripTemplate.TravelGroupId.Value
            //        };
            //        await _travelerRepository.AddTraveler(newTraveler); 
            //    }
            //}

            return View();
        }

        // POST: Trips/AddTravelers
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTravelers(int Id, NewTripTemplate model)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User);
            var tripTemplate = await _context.NewTripTemplates.Where(x => x.Id == Id & x.UserId == user.Id).FirstOrDefaultAsync();

            if (!tripTemplate.isTripComplete)
            {
                CreateNewTripVM newTrip = await _tripRepository.CreateNewTrip(model.TripName);
                tripTemplate.DestinationName = model.DestinationName;
                tripTemplate.TripId = newTrip.TripId;
                tripTemplate.TravelGroupId = newTrip.GroupId;
                tripTemplate.isTripComplete = true;
                tripTemplate.isComplete = false;
                _context.Update(tripTemplate);
                await _context.SaveChangesAsync();
            }
            else
            {
                if (tripTemplate.TravelGroupId != null)
                {
                    var newTraveler = new TravelerAddVM
                    {
                        FullName = model.FullName,
                        EmailAddress = model.EmailAddress,
                        TravelGroupId = tripTemplate.TravelGroupId.Value
                    };
                    await _travelerRepository.AddTraveler(newTraveler);
                }
            }
            var NewTripTemplate = new NewTripTemplate { };
            return View(tripTemplate);
        }

        // GET: Trips/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TripName,TripDescription,StartDate,EndDate,Id,CreatedDate,ModifiedDate,OwnerId")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trip);
        }

        // GET: Trips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Trips == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Trip trip)
        {
            if (id != trip.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(trip.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            return View(trip);
        }

        // GET: Trips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Trips == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Trips == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Trips'  is null.");
            }
            var trip = await _context.Trips.FindAsync(id);
            if (trip != null)
            {
                _context.Trips.Remove(trip);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripExists(int id)
        {
            return _context.Trips.Any(e => e.Id == id);
        }
    }

}
