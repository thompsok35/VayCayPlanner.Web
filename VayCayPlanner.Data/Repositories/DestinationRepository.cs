using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VayCayPlanner.Common.ViewModels;
using VayCayPlanner.Common.ViewModels.Destination;
using VayCayPlanner.Common.ViewModels.Trip;
using VayCayPlanner.Data.Models;
using VayCayPlanner.Data.Repositories.Contracts;

namespace VayCayPlanner.Data.Repositories
{
    public class DestinationRepository : IDestinationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITravelGroupRepository _travelGroupRepository;
        private readonly ITravelerRepository _travelerRepository;
        private readonly UserManager<Subscriber> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<DestinationRepository> _logger;

        public DestinationRepository(ApplicationDbContext dbContext,
                    IHttpContextAccessor httpContextAccessor,
                    UserManager<Subscriber> userManager,
                    ITravelGroupRepository travelGroupRepository,
                    ITravelerRepository travelerRepository,
                    ILogger<DestinationRepository> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _travelGroupRepository = travelGroupRepository;
            _travelerRepository = travelerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> AddFirstDestination(CreateNewTripVM createNewTripVM)
        {
            try
            {
                var newTrip = await _dbContext.Trips.Where(x => x.Id == createNewTripVM.TripId).FirstOrDefaultAsync();
                newTrip.StartDate = createNewTripVM.DestinationArrivalDate;
                newTrip.EndDate = createNewTripVM.DestinationDepartureDate;
                _dbContext.Update(newTrip);
                await _dbContext.SaveChangesAsync();
                var newDestination = new Destination
                {
                    City = createNewTripVM.DestinationName.Split(',')[0],
                    Country = createNewTripVM.DestinationName.Split(',')[1],
                    TravelGroupId = createNewTripVM.GroupId,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ArrivalDate = createNewTripVM.DestinationArrivalDate,
                    DepartureDate = createNewTripVM.DestinationDepartureDate,
                    TripId = createNewTripVM.TripId
                };
                _dbContext.Add(newDestination);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Adding destination [{ex.StackTrace}]");
                return false;                
            }
        }

        public async Task<bool> AddDestinationToTrip(AddDestinationVM model)
        {
            try
            {
                var Destination = new Destination
                {
                    TripId = model.TripId,
                    City = model.City,
                    Country = model.Country,
                    ArrivalDate = model.ArrivalDate,
                    DepartureDate = model.DepartureDate,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                _dbContext.Add(Destination);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return false;
            }
        }

        public async Task<Destination> GetFirstDestinationByTripId(int tripId)
        {
            var result = await _dbContext.Destinations.Where(x => x.TripId == tripId).FirstOrDefaultAsync();
            return result;
        }

        public async Task<TripWithDestinationsVM> GetDestinationsByTripId(int tripId)
        {
            var trip = _mapper.Map<TripVM>(await _dbContext.Trips.Where(x => x.Id == tripId).FirstOrDefaultAsync());
            //var tripVM = _mapper.Map<TripVM>,trip;
            var destinations = await _dbContext.Destinations.Where(x => x.TripId == tripId).ToListAsync();
            var destinationVMs = _mapper.Map<List<DestinationVM>>(destinations);
            if (destinationVMs != null)
            {
                var destinationsWithTrip = new TripWithDestinationsVM(destinationVMs)
                {
                    tripId = trip.Id,
                    TripName = trip.TripName
                };
                return destinationsWithTrip;
            }
            else
            {
                List<DestinationVM> des = new List<DestinationVM>();
                var destinationsWithTrip = new TripWithDestinationsVM(des)
                {
                    TripName = trip.TripName
                };
                return destinationsWithTrip;
            }   
        }
    }
}
