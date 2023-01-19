using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VayCayPlanner.Data.Models;
using VayCayPlanner.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using VayCayPlanner.Common.ViewModels;
using VayCayPlanner.Data.Repositories.Contracts;
using AutoMapper;
using VayCayPlanner.Common.Traveler.ViewModels;
using VayCayPlanner.Common.ViewModels.Traveler;

namespace VayCayPlanner.Web.Controllers
{
    public class TravelersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITravelGroupRepository _travelGroupRepository;
        private readonly ITravelerRepository _travelerRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IMapper _mapper;

        public TravelersController(ApplicationDbContext context, 
            ITravelGroupRepository travelGroupRepository,
            ITripRepository tripRepository,
            IMapper mapper,
            ITravelerRepository travelerRepository)
        {
            _context = context;
            _travelGroupRepository = travelGroupRepository;
            _travelerRepository = travelerRepository;
            _tripRepository = tripRepository;
            _mapper = mapper;
        }

        // GET: Travelers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Travelers.ToListAsync());
        }

        // GET: Travelers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Travelers == null)
            {
                return NotFound();
            }

            var traveler = await _travelerRepository.GetTravelerDetail(id.Value);

            if (traveler == null)
            {
                return NotFound();
            }

            return View(traveler);
        }

        // GET: Travelers/Create
        public IActionResult Create()
        {
            var model = new CreateTravelerVM
            {
                //You can pre-populate data into the fields of the view model here...
                //The SelectList provides the source data for drop doen
                TravelGroups = new SelectList(_travelGroupRepository.MyTravelGroupMemberships().Result, "Id", "GroupName")
            };
            return View(model);
        }

        // POST: Travelers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTravelerVM traveler)
        {
            if (traveler != null)
            {
                await _travelerRepository.AddTravelerToGroup(traveler);
            }
            return RedirectToAction("Details", "TravelGroups", new { Id = traveler.TravelGroupId });            
        }

        // GET: Travelers/Add/5
        //public async Task<IActionResult> Add(int id)
        //{
        public async Task<IActionResult> Add(int? id)
        { 
            if (id != null)
            {
                var thisGroup = await _travelGroupRepository.GetTravelGroupDetails(id.Value);
                var model = new TravelerAddVM
                {
                    TravelGroupId = id.Value,
                    GroupName = thisGroup.GroupName
                };
                return View(model);
            }

            return View();
        }

        // POST: Travelers/Add
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(TravelerAddVM travelerVM)
        {
   
            if (travelerVM != null)
            {
                await _travelerRepository.AddTraveler(travelerVM);
            }
            //TravelGroups / Details / 1 
            return RedirectToAction("Details", "TravelGroups", new { Id = travelerVM.TravelGroupId });
        }


        // GET: Travelers/Add/5
        //public async Task<IActionResult> Add(int id)
        //{
        public async Task<IActionResult> AddToTrip(int? id)
        {
            if (id != null)
            {
                var thisGroup = await _travelGroupRepository.GetTravelGroupDetails(id.Value);
                var thisTrip = await _tripRepository.GetTripByGroupId(id.Value);
                var model = new AddTravelerToTripVM
                {
                    TravelGroupId = id.Value,
                    TripName = thisTrip.TripName,
                };
                return View(model);
            }

            return View();
        }

        // POST: Travelers/Add
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToTrip(AddTravelerToTripVM travelerVM)
        {

            if (travelerVM != null)
            {
                await _travelerRepository.AddTravelerToTrip(travelerVM);
            }
            //TravelGroups / Details / 1 
            return RedirectToAction("AddTravelers", "OnBoardings", new { Id = travelerVM.TravelGroupId });
        }

        // GET: Travelers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Travelers == null)
            {
                return NotFound();
            }

            var traveler = await _context.Travelers.FindAsync(id);
            var travelerEditVM = _mapper.Map<TravelerEditVM>(traveler);

            if (traveler == null)
            {
                return NotFound();
            }
            return View(travelerEditVM);
        }

        // POST: Travelers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TravelerEditVM travelerVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var traveler = await _context.Travelers.FindAsync(id);

                    if (traveler != null)
                    {
                        travelerVM.TravelGroupId = traveler.TravelGroupId.Value;
                        var result = await _travelerRepository.EditTraveler(id, travelerVM);
                        if (!result)
                        {
                            return NotFound();
                        }
                    }    
                }
                catch (Exception)
                {
                    //log the error
                }
                return RedirectToAction("Details", "TravelGroups", new { Id = travelerVM.TravelGroupId });
            }
            return View(travelerVM);
        }

        // GET: Travelers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var traveler = await _context.Travelers.FirstOrDefaultAsync(m => m.Id == id);
                if (traveler == null)
                {
                    return NotFound();
                }
                var thisGroup = await _travelGroupRepository.GetTravelGroupDetails(traveler.TravelGroupId.Value);
                var model = new TravelerDeleteVM
                {
                    FullName = traveler.FullName,
                    EmailAddress = traveler.EmailAddress,
                    TravelGroupId = thisGroup.Id,
                    GroupName = thisGroup.GroupName
                };
                return View(model);
            }

            return View();
        }

        // POST: Travelers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Travelers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Travelers'  is null.");
            }
            var traveler = await _context.Travelers.FindAsync(id);
            if (traveler != null)
            {
                _context.Travelers.Remove(traveler);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "TravelGroups", new { Id = traveler.TravelGroupId });
        }

        private bool TravelerExists(int id)
        {
            return _context.Travelers.Any(e => e.Id == id);
        }
    }
}
