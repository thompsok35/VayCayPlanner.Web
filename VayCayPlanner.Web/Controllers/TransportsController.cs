using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VayCayPlanner.Data.Models;
using VayCayPlanner.Data;
using VayCayPlanner.Common.ViewModels.Destination;
using VayCayPlanner.Data.Repositories;
using VayCayPlanner.Data.Repositories.Contracts;
using VayCayPlanner.Common.ViewModels.Transports;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VayCayPlanner.Web.Controllers
{
 
    public class TransportsController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly ITransportRepository _transportRepository;
        private readonly ITravelerRepository _travelerRepository;
        private readonly IDestinationRepository _destinationRepository;
        private readonly ITripRepository _tripRepository;

        public TransportsController(ApplicationDbContext context,
            IDestinationRepository destinationRepository,
            ITripRepository tripRepository,
            ITravelerRepository travelerRepository,
            ITransportRepository transportRepository)
        {
            _dbcontext = context;
            _transportRepository = transportRepository;
            _travelerRepository = travelerRepository;
            _destinationRepository = destinationRepository;
            _tripRepository = tripRepository;
        }

        // GET: Transports
        public async Task<IActionResult> Index(int? id)
        {
            var trip = await _dbcontext.Transports.Where(x => x.TripId == id.Value).FirstOrDefaultAsync();
            //var transports = _mapper.Map<TransportVM>(trip);

            var model = await _transportRepository.GetTransportsByTripId(id);
            
            return View(model);
        }

        // GET: Transports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _dbcontext.Transports == null)
            {
                return NotFound();
            }

            var transport = await _transportRepository.GetTravelerTransportDetails(id.Value);
            if (transport == null)
            {
                return NotFound();
            }
            //ViewData["Travelers"] = new SelectList(await _travelerRepository.GetTravelersByGroupId(transport.TravelGroupId), "Id", "FullName");
            return View(transport);
        }

        // GET: Transports/AddTravelerTransports
        public async Task<IActionResult> AddTravelerTransports(int? id)
        {
            var destination = await _destinationRepository.GetDestinationDetailById(id.Value);
            var transportModel = await _transportRepository.CreateTransportToFirstDestination(destination);
            return View(transportModel);
        }

        public async Task<IActionResult> AddTransport(int? id)
        {
            var model = await _transportRepository.GetTransportViewModel(id.Value);
            //ViewData["TransportTypes"] = new SelectList(await _dbcontext.TransportTypes.ToListAsync(), "Id", "Name");
            return View(model);
        }

        public async Task<IActionResult> AddTransportDates(AddTransportVM model)
        {
            var detailModel = await _transportRepository.GetTransportDetails(model);
            return View(detailModel);
        }

        // GET: Transports/Create
        public async Task<IActionResult> Create(int? id)
        {
            var nextDestination = await _destinationRepository.GetNextDestination(id.Value);
            var destination = await _destinationRepository.GetDestinationDetailById(nextDestination.Id);
            var transportModel = await _transportRepository.CreateTransportToFirstDestination(destination);
            return View(transportModel);
        }

        // POST: Transports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransportToFirstDestinationVM transport)
        {
            //if (ModelState.IsValid)
            //{
            await _transportRepository.AddTransport(transport);
            return RedirectToAction("Index", "Transports", new { Id = transport.TripId });
            //}
            return View();
        }

        // GET: Transports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _dbcontext.Transports == null)
            {
                return NotFound();
            }

            var transport = await _dbcontext.Transports.FindAsync(id);
            if (transport == null)
            {
                return NotFound();
            }
            return View(transport);
        }

        // POST: Transports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Transport transport)
        {
            if (id != transport.Id)
            {
                return NotFound();
            }

            try
            {
                await _transportRepository.EditTransport(transport);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransportExists(transport.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Index", "Transports", new { Id = transport.TripId });
        }

        // GET: Transports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _dbcontext.Transports == null)
            {
                return NotFound();
            }

            var transport = await _dbcontext.Transports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transport == null)
            {
                return NotFound();
            }

            return View(transport);
        }

        // POST: Transports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_dbcontext.Transports == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transports'  is null.");
            }
            var transport = await _dbcontext.Transports.FindAsync(id);
            if (transport != null)
            {
                _dbcontext.Transports.Remove(transport);
            }

            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransportExists(int id)
        {
            return _dbcontext.Transports.Any(e => e.Id == id);
        }
    }

}
