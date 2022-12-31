using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VayCayPlanner.Data.Models;
using VayCayPlanner.Data;
using VayCayPlanner.Data.Repositories;
using VayCayPlanner.Data.Repositories.Contracts;
using VayCayPlanner.Common.ViewModels;
using AutoMapper;

namespace VayCayPlanner.Web.Controllers
{
    public class TravelGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITravelGroupRepository _travelGroupRepository;
        private readonly IMapper _mapper;

        public TravelGroupsController(ApplicationDbContext context,
            IMapper mapper,
            ITravelGroupRepository travelGroupRepository)
        {
            _context = context;
            _travelGroupRepository = travelGroupRepository;
            _mapper = mapper;
        }

        // GET: TravelGroups
        public async Task<IActionResult> Index()
        {
            return View(await _travelGroupRepository.MyTravelGroupMemberships());
        }

        public async Task<IActionResult> MyTravelGroups()
        {
            return View(await _travelGroupRepository.MyTravelGroups());
        }

        // GET: TravelGroups/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var travelGroup = await _travelGroupRepository.GetTravelGroupDetails(id);
           
            if (travelGroup == null)
            {
                return NotFound();
            }

            return View(travelGroup);
        }

        // GET: TravelGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TravelGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TravelGroup travelGroup)
        {
            //if (ModelState.IsValid)
            //{                
                await _travelGroupRepository.CreateTravelGroup(travelGroup);
                return RedirectToAction(nameof(Index));
            //}
            return View(travelGroup);
        }

        // GET: TravelGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TravelGroups == null)
            {
                return NotFound();
            }

            var travelGroup = await _context.TravelGroups.FindAsync(id);
            var travelGroupVM = _mapper.Map<TravelGroupEditVM>(travelGroup);
            if (travelGroup == null)
            {
                return NotFound();
            }
            return View(travelGroupVM);
        }

        // POST: TravelGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TravelGroupEditVM travelGroupVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _travelGroupRepository.EditTravelGroup(id, travelGroupVM);
                    if (!result)
                    {
                        return NotFound();
                    }                    
                }
                catch (Exception)
                {
                    //log the error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(travelGroupVM);
        }

        // GET: TravelGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TravelGroups == null)
            {
                return NotFound();
            }

            var travelGroup = await _context.TravelGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travelGroup == null)
            {
                return NotFound();
            }

            return View(travelGroup);
        }

        // POST: TravelGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TravelGroups == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TravelGroups'  is null.");
            }
            var travelGroup = await _context.TravelGroups.FindAsync(id);
            if (travelGroup != null)
            {
                _context.TravelGroups.Remove(travelGroup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravelGroupExists(int id)
        {
            return _context.TravelGroups.Any(e => e.Id == id);
        }
    }
}
