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

namespace VayCayPlanner.Web.Controllers
{
    public class OnBoardingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITripRepository _tripRepository;
        private readonly IDestinationRepository _destinationRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Subscriber> _userManager;

        public OnBoardingsController(ApplicationDbContext context,
            ITripRepository tripRepository,
            IDestinationRepository destinationRepository,
            IHttpContextAccessor httpContextAccessor,
            UserManager<Subscriber> userManager)
        {
            _context = context;
            _tripRepository = tripRepository;
            _destinationRepository = destinationRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        // GET: Trips
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trips.ToListAsync());
        }

        // GET: Trips/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Trips/AddNewTrip
        public IActionResult AddNewTrip()
        {
            return View();
        }

        // POST: Trips/FirstDestination
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FirstDestination(int id, NewTripTemplate model)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User);
            try
            {
                await _context.SaveChangesAsync();
               var newTrip =  await _tripRepository.CreateNewTrip(model.TripName);

                var newModel = new NewTripTemplate
                {
                    UserId = user.Id,
                    TripName = model.TripName,
                    TripId = newTrip.TripId,
                    TravelGroupId = newTrip.GroupId,
                    isTripComplete = true,
                    isTravelersComplete = false,
                    isComplete = false,
                    isDestinationComplete = false
                };
                _context.Add(newModel);
                await _context.SaveChangesAsync();

                return View(newModel);
            }
            catch (Exception)
            {

                throw;
            }

        }


        // POST: Trips/AddTravelers
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTravelers(int Id, NewTripTemplate model)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User);
            var thisTrip = await _context.NewTripTemplates.Where(x => x.Id == Id).FirstOrDefaultAsync();
            TravelersVM defaultTraveler = new TravelersVM
            {
                FullName = user.FullName,
                EmailAddress = user.Email
            };
            List<TravelersVM> travelers = new List<TravelersVM>();
            travelers.Add(defaultTraveler);
            var newTripVM = new CreateNewTripVM(travelers)
            {
                TripId = thisTrip.TripId.Value,
                TripName = model.TripName,
                GroupId = thisTrip.TravelGroupId.Value,
                DestinationName = model.DestinationName,
                OwnerName = user.Email,
                DestinationArrivalDate = model.ArrivalDate,
                DestinationDepartureDate = model.DepartureDate
            };
            await _destinationRepository.AddFirstDestination(newTripVM);
            return View(newTripVM);
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
        public async Task<IActionResult> Create(CreateNewTripVM trip)
        {
            if (ModelState.IsValid)
            {
                var newTrip = await _tripRepository.CreateNewTrip(trip.TripName);
                await _destinationRepository.AddFirstDestination(newTrip);
                return RedirectToAction(nameof(Index));
            }
            return View(trip);
        }


        // GET: OnBoardings/Edit/5
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
        public async Task<IActionResult> Edit(int id, [Bind("TripName,TripDescription,StartDate,EndDate,Id,CreatedDate,ModifiedDate")] Trip trip)
        {
            if (id != trip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            }
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
