using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VayCayPlanner.Data.Models;
using VayCayPlanner.Common.ViewModels;
using VayCayPlanner.Data.Repositories.Contracts;
using VayCayPlanner.Data;
using VayCayPlanner.Common.ViewModels.Lodgings;
using VayCayPlanner.Common.ViewModels.Destination;
using VayCayPlanner.Data.Repositories;

namespace VayCayPlanner.Web.Controllers
{
    public class LodgingsController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly ILodgingRepository _lodgingRepository;
        private readonly ITravelerRepository _travelerRepository;
        private readonly IDestinationRepository _destinationRepository;
        private readonly ITripRepository _tripRepository;

        public LodgingsController(ApplicationDbContext context,
            IDestinationRepository destinationRepository,
            ITripRepository tripRepository,
            ITravelerRepository travelerRepository,
            ILodgingRepository lodgingRepository)
        {
            _dbcontext = context;
            _lodgingRepository = lodgingRepository;
            _travelerRepository = travelerRepository;
            _destinationRepository = destinationRepository;
            _tripRepository = tripRepository;
        }

        // GET: Lodgings
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                var thisTrip = await _tripRepository.GetNextTrip();                
                var model = await _lodgingRepository.GetLodgingsByTripId(thisTrip); 
                var trip = await _tripRepository.GetTripById(thisTrip.Value);
                ViewData["Trip"] = trip;
                return View(model);
            }
            else
            {
                var trip = await _tripRepository.GetTripById(id.Value);
                ViewData["Trip"] = trip;
                var model = await _lodgingRepository.GetLodgingsByTripId(id.Value);
                return View(model);
            }
        }

        // GET: Lodgings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _dbcontext.Lodgings == null)
            {
                return NotFound();
            }

            var lodging = await _lodgingRepository.GetTravelerLodgingDetails(id.Value);
            var thisTrip = await _tripRepository.GetTripById(lodging.TripId);
            ViewData["Trip"] = thisTrip;
            return View(lodging);
        }

        // GET: Lodgings/Create
        public async Task<IActionResult> Create(int? id)
        {
            var newLodging = new AddLodgingVM();
            newLodging.DestinationId = id.Value;
            var model = await _lodgingRepository.LoadAddLodgingVM(newLodging);
            
            return View(model);
        }

        // POST: Lodgings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, AddLodgingVM model)
        {
            model.DestinationId = id.Value;

            var newLodging = await _lodgingRepository.AddLodging(model);
            var thisLodging = await _dbcontext.Lodgings.Where(x => x.CreatedDate == newLodging).FirstOrDefaultAsync();
            return RedirectToAction("Details", "Lodgings", new { Id = thisLodging.Id });
            //return View(model);
        }

        // GET: Lodgings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _dbcontext.Lodgings == null)
            {
                return NotFound();
            }
            var lodging = await _dbcontext.Lodgings.FindAsync(id);
            if (lodging != null)
            {
                var model = await _lodgingRepository.GetEditLodgingVM(lodging);
                if (model == null)
                {
                    return NotFound();
                }
                return View(model); 
            }
            return NotFound();
        }

        // POST: Lodgings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditLodgingVM lodging)
        {
            if (id != lodging.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    await _lodgingRepository.EditLodging(lodging);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LodgingExists(lodging.Id))
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
            return View(lodging);
        }

        // GET: Lodgings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _dbcontext.Lodgings == null)
            {
                return NotFound();
            }

            var lodging = await _dbcontext.Lodgings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lodging == null)
            {
                return NotFound();
            }

            return View(lodging);
        }

        // POST: Lodgings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_dbcontext.Lodgings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Lodgings'  is null.");
            }
            var lodging = await _dbcontext.Lodgings.FindAsync(id);
            if (lodging != null)
            {
                _dbcontext.Lodgings.Remove(lodging);
            }

            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Lodging/AddTraveler
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTraveler(int TravelerId, TravelerDestinationVM viewModel)
        {
            try
            {
                await _lodgingRepository.AddTravelerToLodging(TravelerId, viewModel.Id, viewModel.TripId);
                return RedirectToAction("Details", "Lodgings", new { Id = viewModel.Id });
            }
            catch (Exception)
            {

                throw;
            }

            return View(viewModel);
        }

        public async Task<IActionResult> RemoveTraveler(int? id, int? id2, int? id3)
        {
            if (id == null || _dbcontext.TravelerLodgings == null)
            {
                return NotFound();
            }

            var travelerLodging = await _dbcontext.TravelerLodgings
                .FirstOrDefaultAsync(m => m.TravelerId == id && m.LodgingId == id2 && m.TripId == id3);
            if (travelerLodging != null)
            {
                //delete the record here
                _dbcontext.TravelerLodgings.Remove(travelerLodging);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction("Details", "Lodgings", new { Id = id2 });
            }
            else
            {
                return NotFound();
            }
        }

        private bool LodgingExists(int id)
        {
            return _dbcontext.Lodgings.Any(e => e.Id == id);
        }
    }
}

