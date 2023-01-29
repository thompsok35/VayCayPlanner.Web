using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VayCayPlanner.Data.Models;
using VayCayPlanner.Data;

namespace VayCayPlanner.Web.Controllers
{
    public class TravelerDestinationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TravelerDestinationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TravelerDestinations
        public async Task<IActionResult> Index()
        {
            return View(await _context.TravelerDestinations.ToListAsync());
        }

        // GET: TravelerDestinations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TravelerDestinations == null)
            {
                return NotFound();
            }

            var travelerDestination = await _context.TravelerDestinations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travelerDestination == null)
            {
                return NotFound();
            }

            return View(travelerDestination);
        }

        // GET: TravelerDestinations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TravelerDestinations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TravelerId,TripId,DestinationId")] TravelerDestination travelerDestination)
        {
            if (ModelState.IsValid)
            {
                _context.Add(travelerDestination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(travelerDestination);
        }

        // GET: TravelerDestinations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TravelerDestinations == null)
            {
                return NotFound();
            }

            var travelerDestination = await _context.TravelerDestinations.FindAsync(id);
            if (travelerDestination == null)
            {
                return NotFound();
            }
            return View(travelerDestination);
        }

        // POST: TravelerDestinations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TravelerId,TripId,DestinationId")] TravelerDestination travelerDestination)
        {
            if (id != travelerDestination.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(travelerDestination);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelerDestinationExists(travelerDestination.Id))
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
            return View(travelerDestination);
        }

        // GET: TravelerDestinations/Delete/5
        public async Task<IActionResult> Delete(int? id, int? id2, int? id3)
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

                return RedirectToAction("Details", "Destinations", new { Id = id2 });
            }
            else
            {
                return NotFound();
            }
            //return View(travelerDestination);
        }

        // POST: TravelerDestinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TravelerDestinations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TravelerDestinations'  is null.");
            }
            var travelerDestination = await _context.TravelerDestinations.FindAsync(id);
            if (travelerDestination != null)
            {
                _context.TravelerDestinations.Remove(travelerDestination);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravelerDestinationExists(int id)
        {
            return _context.TravelerDestinations.Any(e => e.Id == id);
        }
    }
}
