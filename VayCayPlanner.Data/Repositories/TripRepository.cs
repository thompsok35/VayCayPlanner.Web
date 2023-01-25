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
using VayCayPlanner.Common.ViewModels.Trip;
using VayCayPlanner.Data.Models;
using VayCayPlanner.Data.Repositories.Contracts;

namespace VayCayPlanner.Data.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITravelGroupRepository _travelGroupRepository;
        private readonly ITravelerRepository _travelerRepository;
        private readonly UserManager<Subscriber> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<TripRepository> _logger;

        public TripRepository(ApplicationDbContext dbContext,
                    IHttpContextAccessor httpContextAccessor,
                    UserManager<Subscriber> userManager,
                    ITravelGroupRepository travelGroupRepository,
                    ITravelerRepository travelerRepository,
                    ILogger<TripRepository> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _travelGroupRepository = travelGroupRepository;
            _travelerRepository = travelerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateNewTripVM> CreateNewTrip(string tripName)
        {
            var _travelers = new List<TravelersVM>();
            var result = new CreateNewTripVM(_travelers);
            try
            {
                var user = await CurrentUser();
                string newTripName = tripName;
                string newTripTravelGroup = $"{tripName}_TravelGroup";
                //Add the TravelGroup to generate an Id
                int groupId = await _travelGroupRepository.CreateTripTravelGroup(newTripTravelGroup);
                if (isDuplicateTripName(groupId, newTripName).Result)
                {
                    newTripName = $"{tripName}_2";
                }
                var newTrip = new Trip
                {
                    TripName = newTripName,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    TravelGroupId = groupId,
                    OwnerId = user.Id,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                //Adding to the Trips table to generate an Id
                _dbContext.Add(newTrip);
                await _dbContext.SaveChangesAsync();

                //the trip id is needed to add a destination
                var thisTrip = await _dbContext.Trips.Where(x => x.TripName.ToLower() == newTripName.ToLower()).FirstOrDefaultAsync();
                var isTravelerAdded = await _travelerRepository.AddTravelerToGroup(groupId);
                result.TripName = newTripName;
                result.GroupId = groupId;
                result.TripId = thisTrip.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating new trip with the wizard [{ex.StackTrace}]");
                return result;
            }
            return result;
        }

        public async Task<bool> UpdateTripEndDate(int id, DateTime departureDate)
        {
            var thisTrip = await _dbContext.Trips.Where(x => x.Id == id).FirstOrDefaultAsync();
            try
            {
                if (thisTrip != null)
                {
                    if (departureDate > thisTrip.EndDate)
                    {
                        thisTrip.EndDate = departureDate;
                        _dbContext.Update(thisTrip);
                        await _dbContext.SaveChangesAsync();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<TripDetailVM> GetTripDetail(int Id)
        {
            var thisTrip = await _dbContext.Trips.Where(x => x.Id == Id).FirstOrDefaultAsync();
            var _Destinations = await _dbContext.Destinations.Where(x => x.TripId == Id).ToListAsync();
            var _Travelers = await _dbContext.Travelers.Where(x => x.TravelGroupId == thisTrip.TravelGroupId).ToListAsync();
            var thisTripDetail = new TripDetailVM
            {
                Id = thisTrip.Id,
                TripName = thisTrip.TripName,
                StartDate = thisTrip.StartDate,
                EndDate = thisTrip.EndDate,
                DaysUntilDeparture = (thisTrip.StartDate.Value - DateTime.Today).Days,
                Duration = (thisTrip.EndDate.Value - thisTrip.StartDate.Value).Days,
                Destinations = _Destinations.Count,
                Travelers = _Travelers.Count
            };
            return thisTripDetail;
        }

        public async Task<Trip> GetTripByGroupId(int groupId)
        {
            var thisTrip = await _dbContext.Trips.Where(x => x.TravelGroupId == groupId).FirstOrDefaultAsync();
            return thisTrip;
        }

        public async Task<List<Trip>> GetUpcomingTripsAsync()
        {
            var result = await _dbContext.Trips.Where(x => x.EndDate >= DateTime.Today).ToListAsync();
            return result;
        }

        public async Task<List<Trip>> GetPastTripsAsync()
        {
            var result = await _dbContext.Trips.Where(x => x.EndDate <= DateTime.Today).ToListAsync();
            return result;
        }

        private async Task<bool> isDuplicateTripName(int groupId, string tripName)
        {
            bool result = false;
            var record = await _dbContext.Trips.Where(x => x.TravelGroupId == groupId && x.TripName.ToLower() == tripName.ToLower()).FirstOrDefaultAsync();

            if (record != null)
            {
                result = true;
            }
            return result;
        }



        private async Task<Subscriber> CurrentUser()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User);
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}
