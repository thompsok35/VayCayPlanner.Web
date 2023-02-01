using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VayCayPlanner.Data.Models;
using VayCayPlanner.Data;
using VayCayPlanner.Common.ViewModels.Destination;
using VayCayPlanner.Data.Repositories;
using VayCayPlanner.Data.Repositories.Contracts;
using VayCayPlanner.Common.ViewModels.Transports;

namespace VayCayPlanner.Web.Controllers
{
 
    public class TransportsController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly ITransportRepository _transportRepository;
        private readonly IDestinationRepository _destinationRepository;

        public TransportsController(ApplicationDbContext context,
            IDestinationRepository destinationRepository,
                    ITransportRepository transportRepository)
        {
            _dbcontext = context;
            _transportRepository = transportRepository;
            _destinationRepository = destinationRepository;
        }

        // GET: Transports
        public async Task<IActionResult> Index()
        {
            return View(await _dbcontext.Transports.ToListAsync());
        }

        // GET: Transports/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Transports/AddTravelerTransports
        public async Task<IActionResult> AddTravelerTransports(int? id)
        {
            var destination = await _destinationRepository.GetDestinationDetailById(id.Value);
            var transportModel = await _transportRepository.CreateTransportToFirstDestination(destination);
            return View(transportModel);
        }

        // GET: Transports/Create
        public async Task<IActionResult> Create(int? id)
        {
            var destination = await _destinationRepository.GetDestinationDetailById(id.Value);
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
            //return RedirectToAction("Index", "Destinations", new { Id = transport.TripId });
            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("DestinationId,DepartureDestinationId,FromAddress,ToAddress,DepartureDatetime,PreferredAirport,ArrivalDestinationId,ArrivalDatetime,TransportType,Description,Quantity,Id,TravelGroupId,CreatedDate,ModifiedDate")] Transport transport)
        {
            if (id != transport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbcontext.Update(transport);
                    await _dbcontext.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(transport);
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
