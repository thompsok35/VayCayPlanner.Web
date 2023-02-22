using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VayCayPlanner.Data.Models;
using VayCayPlanner.Data;
using VayCayPlanner.Data.Repositories.Contracts;
using VayCayPlanner.Common.ViewModels.Destination;
using VayCayPlanner.Common.ViewModels;

namespace VayCayPlanner.Web.Controllers
{
    public class DestinationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDestinationRepository _destinationRepository;
        private readonly ITravelerRepository _travelerRepository;
        private readonly IMapper _mapper;

        public DestinationsController(ApplicationDbContext context,
                IDestinationRepository destinationRepository,
                ITravelerRepository travelerRepository,
                IMapper mapper)
        {
            _context = context;
            _destinationRepository = destinationRepository;
            _travelerRepository = travelerRepository;
            _mapper = mapper;
        }

        // GET: Destinations
        public async Task<IActionResult> Index(int Id)
        {
            //var applicationDbContext = _context.Destinations.Where(d => d.TripId == Id);
            //var destinations = _mapper.Map<List<Destination>>(await applicationDbContext.ToListAsync());
            var destinations = await _destinationRepository.GetDestinationsByTripId(Id);
            return View(destinations);
        }

        // GET: Destinations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Destinations == null)
            {
                return NotFound();
            }

            var destination = await _destinationRepository.GetDestinationDetailById(id.Value);

            if (destination == null)
            {
                return NotFound();
            }
            ViewData["Travelers"] = new SelectList(await _travelerRepository.GetTravelersByGroupId(destination.TravelGroupId), "Id", "FullName");
            return View(destination);
        }

        // GET: Destinations/Create
        public IActionResult Create(int? id)
        {
            var thisTrip = _context.Trips.Where(x => x.Id == id.Value).FirstOrDefault();
            var model = new AddDestinationVM
            {
                TripId = thisTrip.Id,
                TripName = thisTrip.TripName
            };
            //ViewData["TripId"] = new SelectList(_context.Trips, "TripId", "TrapName");
            return View(model);
        }

        // POST: Destinations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddDestinationVM destination)
        {
            try
            {
                var createdDate = await _destinationRepository.AddDestinationToTrip(destination);
                var newDestination = await _context.Destinations.Where(x => x.CreatedDate == createdDate).FirstOrDefaultAsync();
                return RedirectToAction("Details", "Destinations", new { Id = newDestination.Id });                
            }
            catch (Exception ex)
            {
                //add log
            }
            
            //if (ModelState.IsValid)
            //{
            //    _context.Add(destination);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["TripId"] = new SelectList(_context.Trips, "Id", "Id", destination.TripId);
            return View(destination);
        }

        // POST: Destinations/AddTraveler
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTraveler(int TravelerId, TravelerDestinationVM viewModel)
        {
            try
            {
                await _destinationRepository.AddTravelerToDestination(TravelerId, viewModel.Id, viewModel.TripId);
                return RedirectToAction("Details", "Destinations", new { Id = viewModel.Id });
            }
            catch (Exception)
            {

                throw;
            }

            return View(viewModel);
        }

        public async Task<IActionResult> RemoveTraveler(int? id, int? id2, int? id3)
        {
            if (id == null || _context.TravelerDestinations == null)
            {
                return NotFound();
            }

            var travelerDestination = await _context.TravelerDestinations
                .FirstOrDefaultAsync(m => m.TravelerId == id && m.DestinationId == id2 && m.TripId == id3);
            if (travelerDestination != null)
            {
                //delete the record here
                _context.TravelerDestinations.Remove(travelerDestination);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Destinations", new { Id = id2 });
            }
            else
            {
                return NotFound();
            }
            //return View(travelerDestination);
        }

        // GET: Destinations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Destinations == null)
            {
                return NotFound();
            }

            var destination = await _context.Destinations.FindAsync(id);

            //ViewData["TripId"] = new SelectList(_context.Trips, "Id", "Id", destination.TripId);
            return View(destination);
        }

        // POST: Destinations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Destination destination)
        {
            if (id != destination.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _destinationRepository.EditDestination(destination);
                    //_context.Update(destination);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DestinationExists(destination.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Destinations", new { Id = destination.TripId });
            }
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "Id", destination.TripId);
            return View(destination);
        }

        // GET: Destinations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Destinations == null)
            {
                return NotFound();
            }

            var destination = await _context.Destinations.Where(d => d.Id == id).FirstOrDefaultAsync();
                
            if (destination == null)
            {
                return NotFound();
            }

            return View(destination);
        }

        // POST: Destinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Destinations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Destinations'  is null.");
            }
            var destination = await _context.Destinations.FindAsync(id);
            if (destination != null)
            {
                _context.Destinations.Remove(destination);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Destinations", new { Id = destination.TripId });
        }

        private bool DestinationExists(int id)
        {
            return _context.Destinations.Any(e => e.Id == id);
        }
    }
}
