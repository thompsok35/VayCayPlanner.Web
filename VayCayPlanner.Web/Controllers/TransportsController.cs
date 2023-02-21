using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VayCayPlanner.Data.Models;
using VayCayPlanner.Data;
using VayCayPlanner.Common.ViewModels.Destination;
using VayCayPlanner.Data.Repositories;
using VayCayPlanner.Data.Repositories.Contracts;
using VayCayPlanner.Common.ViewModels.Transports;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography.Xml;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

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
            if (id == null)
            {
                var thisTrip = await _tripRepository.GetNextTrip();
                var model = await _transportRepository.GetTransportsByTripId(thisTrip.Value);
                return View(model);
            }
            else
            {
                var model = await _transportRepository.GetTransportsByTripId(id);
                return View(model);
            }            
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
            var thisTrip = await _tripRepository.GetTripById(transport.TripId);
            ViewData["Trip"] = thisTrip;
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
            AddTransportVM? model = new AddTransportVM();
            var thisDestination = await _destinationRepository.GetDestinationById(id.Value);
            var destinations = await _destinationRepository.GetDestinationsByDate(id.Value);
            var first = destinations.ElementAt(0);
            var last = destinations.ElementAt(destinations.Count-1);
            if (first.Id == id)
            {
                model = await _transportRepository.GetFirstTransportViewModel(id.Value);
                return View(model);
            } else if (last.Id == id)
            {
                model = await _transportRepository.GetLastTransportViewModel(id.Value);
                return View(model);
            }
            else
            {
                model = await _transportRepository.GetNextTransportViewModel(id.Value);
                return View(model);
            }
            var thisTrip = await _tripRepository.GetTripById(thisDestination.TripId);
            ViewData["Trip"] = thisTrip;
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

        // POST: Transports/AddTraveler
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTraveler(int TravelerId, TravelerDestinationVM viewModel)
        {
            try
            {
                await _transportRepository.AddTravelerToTransport(TravelerId, viewModel.Id, viewModel.TripId);
                return RedirectToAction("Details", "Transports", new { Id = viewModel.Id });
            }
            catch (Exception)
            {

                throw;
            }

            return View(viewModel);
        }

        public async Task<IActionResult> RemoveTraveler(int? id, int? id2, int? id3)
        {
            if (id == null || _dbcontext.TravelerTransports == null)
            {
                return NotFound();
            }

            var travelerTransport = await _dbcontext.TravelerTransports
                .FirstOrDefaultAsync(m => m.TravelerId == id && m.TransportId == id2 && m.TripId == id3);
            if (travelerTransport != null)
            {
                //delete the record here
                _dbcontext.TravelerTransports.Remove(travelerTransport);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction("Details", "Transports", new { Id = id2 });
            }
            else
            {
                return NotFound();
            }
            //return View(travelerDestination);
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
            var createdDate = await _transportRepository.AddTransport(transport);
            var newTransport = await _dbcontext.Transports.Where(x => x.CreatedDate == createdDate).FirstOrDefaultAsync();
            return RedirectToAction("Details", "Transports", new { Id = newTransport.Id });
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
