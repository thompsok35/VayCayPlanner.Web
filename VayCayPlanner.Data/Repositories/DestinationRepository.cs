using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly ITripRepository _tripRepository;
        private readonly ITravelerRepository _travelerRepository;
        private readonly UserManager<Subscriber> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<DestinationRepository> _logger;

        public DestinationRepository(ApplicationDbContext dbContext,
                    IHttpContextAccessor httpContextAccessor,
                    UserManager<Subscriber> userManager,
                    ITravelGroupRepository travelGroupRepository,
                    ITripRepository tripRepository,
                    ITravelerRepository travelerRepository,
                    ILogger<DestinationRepository> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _travelGroupRepository = travelGroupRepository;
            _tripRepository = tripRepository;
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
                if (createNewTripVM.DestinationName.Contains(','))
                {
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
                }
                else
                {
                    var newDestination = new Destination
                    {
                        City = createNewTripVM.DestinationName,
                        Country = createNewTripVM.DestinationName,
                        TravelGroupId = createNewTripVM.GroupId,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        ArrivalDate = createNewTripVM.DestinationArrivalDate,
                        DepartureDate = createNewTripVM.DestinationDepartureDate,
                        TripId = createNewTripVM.TripId
                    };
                    _dbContext.Add(newDestination);
                    await _dbContext.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Adding destination [{ex.StackTrace}]");
                return false;                
            }
        }

        public async Task<DateTime> AddDestinationToTrip(AddDestinationVM model)
        {
            var createdDate = DateTime.Now;
            try
            {
                var thisTrip = await _dbContext.Trips.Where(x => x.Id == model.TripId).FirstOrDefaultAsync();
                var Destination = new Destination
                {
                    TripId = model.TripId,
                    TravelGroupId = thisTrip.TravelGroupId,
                    City = model.City,
                    Country = model.Country,
                    ArrivalDate = model.ArrivalDate,
                    DepartureDate = model.DepartureDate,
                    CreatedDate = createdDate,
                    ModifiedDate = DateTime.Now
                };
                _dbContext.Add(Destination);

                await _dbContext.SaveChangesAsync();
                await _tripRepository.UpdateTripEndDate(model.TripId, model.DepartureDate.Value);
                return createdDate;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return createdDate;
            }
        }

        public async Task<bool> AddTravelerToDestination(int travelerId, int destinationId, int tripId)
        {
            try
            {
                var _travelerDestination = new TravelerDestination
                {
                    DestinationId = destinationId,
                    TravelerId = travelerId,
                    TripId = tripId
                };
                _dbContext.Add(_travelerDestination);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }

        public async Task<Destination> EditDestination (Destination model)
        {
            var thisDestination = new Destination
            { 
                Id = model.Id,
                City = model.City,
                Country = model.Country,
                TravelGroupId = model.TravelGroupId,
                CreatedDate = model.CreatedDate,
                ModifiedDate = DateTime.Now,
                ArrivalDate = model.ArrivalDate,
                DepartureDate = model.DepartureDate,
                TripId = model.TripId
            };
            _dbContext.Update(thisDestination);
            await _dbContext.SaveChangesAsync();
            return thisDestination;
        }

        public async Task<Destination> GetDestinationById(int Id)
        {
            var result = await _dbContext.Destinations.Where(x => x.Id == Id).FirstOrDefaultAsync();
            return result;
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
                    TravelGroupId = trip.TravelGroupId,
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

        public async Task<DestinationDetailVM> GetDestinationDetailById (int id)
        {
            var model = new DestinationDetailVM();

            var destination = await _dbContext.Destinations.Where(x => x.Id == id).FirstOrDefaultAsync();
            var trip = await _dbContext.Trips.Where(x => x.Id == destination.TripId).FirstOrDefaultAsync();
            var travelerList = new SelectList(await _dbContext.Travelers.Where(x => x.TravelGroupId == trip.TravelGroupId).ToListAsync(), "Id", "FullName");
            var travelerDestinations = await _dbContext.TravelerDestinations.Where(x => x.DestinationId == id).ToListAsync();
            var travelerDestinationVm = await GetDestinationTravelers(travelerDestinations);

            model.Id = destination.Id;
            model.DestinationId = destination.Id;
            model.ArrivalDate = destination.ArrivalDate;
            model.City = destination.City;
            model.Country = destination.Country;
            model.DepartureDate = destination.DepartureDate;
            model.TripId = destination.TripId;
            model.TripName = trip.TripName;
            model.Travelers = travelerList;
            model.TravelGroupId = destination.TravelGroupId.Value;
            model.DestinationTravelers = travelerDestinationVm;
            return model;
        }

        public async Task<Destination> GetNextDestination(int id)
        {
            Destination result = new Destination();
            var thisDestination = await _dbContext.Destinations.Where(x => x.Id == id).FirstOrDefaultAsync();
            var nextDestination = await _dbContext.Destinations.Where(x => x.ArrivalDate >= thisDestination.DepartureDate).OrderBy(o => o.ArrivalDate).FirstOrDefaultAsync();
            if (thisDestination != null)
            {
                return nextDestination; 
            }
            return result;
        }

        public async Task<Destination> GetFirstDestination(int tripId)
        {
            var thisDestination = await _dbContext.Destinations.Where(x => x.TripId == tripId).OrderBy(o => o.DepartureDate).FirstOrDefaultAsync();
            //var nextDestination = await _dbContext.Destinations.Where(x => x.DepartureDate >= destination.ArrivalDate).OrderBy(o => o.ArrivalDate).FirstOrDefaultAsync();
            return thisDestination;
        }

        public async Task<List<Destination>> GetDestinationsByDate(int Id)
        {
            var thisDestination = await _dbContext.Destinations.Where(x => x.Id == Id).FirstOrDefaultAsync();
            var destinationList = await _dbContext.Destinations.Where(x => x.TripId == thisDestination.TripId).OrderBy(o => o.DepartureDate).ToListAsync();
            return destinationList;
        }

        private async Task<List<TravelersVM>> GetDestinationTravelers(List<TravelerDestination> travelers)
        {
            List<TravelersVM> result = new List<TravelersVM>();
            foreach (var traveler in travelers)
            {
                var t = await _dbContext.Travelers.Where(x => x.Id == traveler.TravelerId).FirstOrDefaultAsync();
                if (t != null)
                {
                    var thisTraveler = new TravelersVM
                    {
                        Id = t.Id,
                        FullName = t.FullName,
                        EmailAddress = t.EmailAddress
                    };
                    result.Add(thisTraveler); 
                }
            }
            return result;
        }


    }
}
