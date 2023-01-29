using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VayCayPlanner.Data.Models;
using VayCayPlanner.Data;

namespace VayCayPlanner.Web.Controllers
{
 
    public class TransportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transports
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transports.ToListAsync());
        }

        // GET: Transports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transports == null)
            {
                return NotFound();
            }

            var transport = await _context.Transports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transport == null)
            {
                return NotFound();
            }

            return View(transport);
        }

        // GET: Transports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DestinationId,DepartureDestinationId,FromAddress,ToAddress,DepartureDatetime,PreferredAirport,ArrivalDestinationId,ArrivalDatetime,TransportType,Description,Quantity,Id,TravelGroupId,CreatedDate,ModifiedDate")] Transport transport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transport);
        }

        // GET: Transports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transports == null)
            {
                return NotFound();
            }

            var transport = await _context.Transports.FindAsync(id);
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
                    _context.Update(transport);
                    await _context.SaveChangesAsync();
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
            if (id == null || _context.Transports == null)
            {
                return NotFound();
            }

            var transport = await _context.Transports
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
            if (_context.Transports == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transports'  is null.");
            }
            var transport = await _context.Transports.FindAsync(id);
            if (transport != null)
            {
                _context.Transports.Remove(transport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransportExists(int id)
        {
            return _context.Transports.Any(e => e.Id == id);
        }
    }

}
