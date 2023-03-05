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
using VayCayPlanner.Data.Models;
using VayCayPlanner.Common.ViewModels.Lodgings;
using VayCayPlanner.Data.Repositories.Contracts;
using VayCayPlanner.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using VayCayPlanner.Common.ViewModels.Transports;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VayCayPlanner.Data.Repositories{
    
    public class LodgingRepository : ILodgingRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITravelGroupRepository _travelGroupRepository;
        private readonly IDestinationRepository _destinationRepository;
        private readonly ITravelerRepository _travelerRepository;
        //private readonly UserManager<Subscriber> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<TripRepository> _logger;


        public LodgingRepository(ApplicationDbContext dbContext,
                    IHttpContextAccessor httpContextAccessor,
                    UserManager<Subscriber> userManager,
                    ITravelGroupRepository travelGroupRepository,
                    IDestinationRepository destinationRepository,
                    ITravelerRepository travelerRepository,
                    ILogger<TripRepository> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            //_userManager = userManager;
            _travelGroupRepository = travelGroupRepository;
            _travelerRepository = travelerRepository;
            _destinationRepository = destinationRepository;
            _mapper = mapper;
        }

        public async Task<Lodging> GetLodgingById(int Id)
        {
            var result = await _dbContext.Lodgings.Where(x => x.Id == Id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<LodgingsVM>> GetLodgingsByTripId(int? id)
        {
            List<LodgingsVM> result = new List<LodgingsVM>();
            var thisTrip = await _dbContext.Trips.Where(x => x.Id == id.Value).FirstOrDefaultAsync();
            var lodgings = await _dbContext.Lodgings.Where(x => x.TripId == id.Value).ToListAsync();
            foreach (var item in lodgings)
            {
                var thisLodging = _mapper.Map<LodgingsVM>(item);
                thisLodging.DestinationName = await _destinationRepository.GetDestinationNameById(item.DestinationId);
                thisLodging.TripName = thisTrip.TripName;
                result.Add(thisLodging);
            }
            return result;
        }

        public async Task<LodgingDetailVM> GetTravelerLodgingDetails(int lodgingId)
        {
            var nullResult = new LodgingDetailVM();
            var thisLodging = await _dbContext.Lodgings.Where(x => x.Id == lodgingId).FirstOrDefaultAsync();
            if (thisLodging != null)
            {
                var thisTrip = await _dbContext.Trips.Where(x => x.Id == thisLodging.TripId).FirstOrDefaultAsync();
                var tripTravelers = await GetTripTravelers(thisTrip.TravelGroupId.Value);
                var lodgingTravelers = await GetLodgingTravelers(thisLodging);
                var travelerList = new SelectList(await _dbContext.Travelers.Where(x => x.TravelGroupId == thisTrip.TravelGroupId).ToListAsync(), "Id", "FullName");
                var model = new LodgingDetailVM
                {
                    Id = thisLodging.Id,
                    TripId = thisTrip.Id,
                    TripName = thisTrip.TripName,
                    TravelGroupId = thisTrip.TravelGroupId.Value,
                    LodgingType = thisLodging.LodgingType,
                    Name = thisLodging.Name,
                    CheckInDate = thisLodging.CheckInDate,
                    CheckOutDate = thisLodging.CheckOutDate,
                    CostPerNight = thisLodging.CostPerNight,
                    TotalCost = thisLodging.TotalCost,
                    Nights = thisLodging.Nights,
                    WebLink = thisLodging.WebLink,

                    LodgingTravelers = lodgingTravelers,
                    Travelers = travelerList
                };
                return model;
            }
            return nullResult;
        }

        public async Task<AddLodgingVM> LoadAddLodgingVM(AddLodgingVM model)
        {
            var lodgingTypes = new SelectList(await _dbContext.LodgingTypes.ToListAsync(), "Id", "Name");
            //var lodgingType = await _dbContext.LodgingTypes.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
            var thisDestination = await _dbContext.Destinations.Where(x => x.Id == model.DestinationId).FirstOrDefaultAsync();
            var thisTrip = await _dbContext.Trips.Where(x => x.Id == thisDestination.TripId).FirstOrDefaultAsync();
            var thisLodging = new AddLodgingVM
            {
                Id = model.Id,
                //Name = model.Name,
                TripName = thisTrip.TripName,
                TripId = thisTrip.Id,
                DestinationName = model.DestinationName,
                DestinationId = model.DestinationId,
                LodgingTypes = lodgingTypes,
                //LodgingTypeId = ,
                //CheckInDate = model.CheckInDate,
                //CheckOutDate = model.CheckOutDate,
                //Nights = model.Nights,
                //CostPerNight = model.CostPerNight,
                //TotalCost = model.TotalCost,
                //WebLink = model.WebLink
                //TransportTravelers = await GetTripTravelers(model.TravelGroupId.Value)
            };
            return thisLodging;
        }


        private async Task<List<TravelersVM>> GetLodgingTravelers(Lodging lodging)
        {
            List<TravelersVM> result = new List<TravelersVM>();
            var travelers = await _dbContext.TravelerLodgings.Where(x => x.LodgingId == lodging.Id && x.TripId == lodging.TripId).ToListAsync();

            if (travelers.Count > 0)
            {
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
            return result;
        }

        public async Task<EditLodgingVM> GetEditLodgingVM(Lodging lodging)
        {
            var model = _mapper.Map<EditLodgingVM>(lodging);
            var lodgingTypes = new SelectList(await _dbContext.LodgingTypes.ToListAsync(), "Id", "Name");
            model.LodgingTypes = lodgingTypes;
            return model;
        }

        public async Task<DateTime> AddLodging(AddLodgingVM model)
        {
            var thisDestination = await _dbContext.Destinations.Where(x => x.Id == model.DestinationId).FirstOrDefaultAsync();
            var thisTrip = await _dbContext.Trips.Where(x => x.Id == thisDestination.TripId).FirstOrDefaultAsync();
            var lodgingType = await _dbContext.LodgingTypes.Where(x => x.Id == model.LodgingTypeId).FirstOrDefaultAsync();
            //var thisTrip = await _dbContext.Destinations.Where(x => x.Id == model.DestinationId).FirstOrDefaultAsync();
            DateTime result = DateTime.Now;
            var days = (model.CheckOutDate- model.CheckInDate);
            int nights = int.Parse(days.Value.Days.ToString());
            Lodging thisLodging = new Lodging
            {
                TripId = thisTrip.Id,
                TravelGroupId = thisTrip.TravelGroupId,
                Name = model.Name,
                DestinationId = model.DestinationId,
                //DestinationName = ($"{thisDestination.City}, {thisDestination.Country}"),
                CheckInDate = model.CheckInDate,
                CheckOutDate = model.CheckOutDate,
                MaxOccupancy = model.MaxOccupancy,
                WebLink = model.WebLink,
                Nights = nights,
                CostPerNight = model.CostPerNight,
                LodgingType = lodgingType.Name,
                TotalCost = model.CostPerNight*nights,
                CreatedDate = result,
                ModifiedDate = DateTime.Now
            };

            try
            {
                _dbContext.Add(thisLodging);
                await _dbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }

        }

        private async Task<List<TravelersVM>> GetTripTravelers(int travelGroupId)
        {
            var result = await _dbContext.Travelers.Where(x => x.TravelGroupId == travelGroupId).ToListAsync();
            return _mapper.Map<List<TravelersVM>>(result);
            ////return result;
        }

        public async Task<bool> AddTravelerToLodging(int travelerId, int lodgingId, int tripId)
        {
            try
            {
                var isDup = await _dbContext.TravelerLodgings.Where(x => x.LodgingId == lodgingId && x.TravelerId == travelerId && x.TripId == tripId).FirstOrDefaultAsync();
                if (isDup == null)
                {
                    var _travelerLodging = new TravelerLodging
                    {
                        LodgingId = lodgingId,
                        TravelerId = travelerId,
                        TripId = tripId
                    };
                    _dbContext.Add(_travelerLodging);
                    await _dbContext.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }

        public async Task<bool> EditLodging(EditLodgingVM model)
        {
            try
            {
                var lodgingType = await _dbContext.LodgingTypes.FindAsync(model.LodgingTypeId);
                var thisLodging = new Lodging
                {
                    Id = model.Id,
                    Name = model.Name,
                    DestinationId = model.DestinationId,
                    CheckInDate = model.CheckInDate,
                    CheckOutDate = model.CheckOutDate,
                    MaxOccupancy = model.MaxOccupancy,
                    Nights = model.Nights,
                    WebLink = model.WebLink,
                    TravelGroupId = model.TravelGroupId,
                    CreatedDate = model.CreatedDate,
                    CostPerNight = model.CostPerNight,
                    LodgingType = lodgingType.Name,
                    TotalCost = model.TotalCost,
                    ModifiedDate = DateTime.Now,                     
                    TripId = model.TripId
                };
                _dbContext.Update(thisLodging);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
