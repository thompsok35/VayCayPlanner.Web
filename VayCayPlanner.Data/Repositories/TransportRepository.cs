using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VayCayPlanner.Common.ViewModels;
using VayCayPlanner.Common.ViewModels.Destination;
using VayCayPlanner.Common.ViewModels.Transports;
using VayCayPlanner.Common.ViewModels.Trip;
using VayCayPlanner.Data.Models;
using VayCayPlanner.Data.Repositories.Contracts;

namespace VayCayPlanner.Data.Repositories
{
    public class TransportRepository : ITransportRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITravelGroupRepository _travelGroupRepository;
        private readonly IDestinationRepository _destinationRepository;
        private readonly ITravelerRepository _travelerRepository;
        private readonly UserManager<Subscriber> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<TripRepository> _logger;

        public TransportRepository(ApplicationDbContext dbContext,
                    IHttpContextAccessor httpContextAccessor,
                    UserManager<Subscriber> userManager,
                    ITravelGroupRepository travelGroupRepository,
                    IDestinationRepository destinationRepository,
                    ITravelerRepository travelerRepository,
                    ILogger<TripRepository> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _travelGroupRepository = travelGroupRepository;
            _travelerRepository = travelerRepository;
            _destinationRepository = destinationRepository;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<TransportToFirstDestinationVM> CreateTransportToFirstDestination(DestinationDetailVM model)
        {
            //query for the first destination

            TransportToFirstDestinationVM returnModel = new TransportToFirstDestinationVM
            {
                TripId = model.TripId,
                City = model.City,
                Country = model.Country,
                DestinationId = model.DestinationId,
                ArrivalDestinationId = model.DestinationId,
                
                ToAddress =$"{model.City}, {model.Country}",
                TripName = model.TripName,
                //TransportType = model. "Flight"
            };

            return returnModel;
        }

        public async Task<List<TripTransportsVM>> GetTripTransportsByTripId(int? id)
        {
            List<TripTransportsVM> result = new List<TripTransportsVM>();
            if (id != null)
            {
                var thisTrip = await _dbContext.Trips.Where(x => x.Id == id).FirstOrDefaultAsync();
                var thisTripVM = _mapper.Map<TripVM>(thisTrip);
                var transports = await _dbContext.Transports.Where(x => x.TripId == id.Value).ToListAsync();
                //var tripTransports = _mapper.Map < List < TripTransportsVM(thisTripVM) >> (transports);
                foreach (var item in transports)
                {
                    TripTransportsVM tran = new TripTransportsVM(thisTripVM)
                    { 
                        Id = item.Id,
                        ArrivalDatetime = item.ArrivalDatetime,
                        //ArrivalDestinationId = item.ArrivalDestinationId,
                        CostPerTraveler = item.CostPerTraveler,
                        DestinationId = item.DestinationId,
                        //DepartureDestinationId = item.DepartureDestinationId,
                        //PreferredAirport = item.PreferredAirport,
                        TripId = item.TripId,                        
                        DepartureDatetime = item.DepartureDatetime,                        
                        Description = item.Description,
                        FromAddress = item.FromAddress,
                        ToAddress = item.ToAddress,
                        TransportType = item.TransportType,
                        //ModifiedDate = DateTime.Now
                    };
                    result.Add(tran);
                }                
            }
            return result;
        }

        public async Task<TripTransportsVM> GetTransportsByTripId(int? id)
        {
            //TripTransportsVM result = new TripTransportsVM();
            if (id != null)
            {
                var thisTrip = await _dbContext.Trips.Where(x => x.Id == id).FirstOrDefaultAsync();
                var thisTripVM = _mapper.Map<TripVM>(thisTrip);
                var transports = await _dbContext.Transports.Where(x => x.TripId == id.Value).ToListAsync();
                var transportVM = _mapper.Map<List<TransportVM>>(transports);

                    TripTransportsVM result = new TripTransportsVM(transportVM, thisTripVM)
                    {
                        //Id = item.Id,
                        //ArrivalDatetime = item.ArrivalDatetime,
                        ////ArrivalDestinationId = item.ArrivalDestinationId,
                        //CostPerTraveler = item.CostPerTraveler,
                        //DestinationId = item.DestinationId,
                        ////DepartureDestinationId = item.DepartureDestinationId,
                        ////PreferredAirport = item.PreferredAirport,
                        //TripId = item.TripId,
                        //DepartureDatetime = item.DepartureDatetime,
                        //Description = item.Description,
                        //FromAddress = item.FromAddress,
                        //ToAddress = item.ToAddress,
                        //TransportType = item.TransportType,
                        ////ModifiedDate = DateTime.Now
                    };
                return result;
            }
            return null;
        }

        public async Task<AddTransportVM> GetTransportViewModel(int desId)
        {            
            var thisDestination = await _dbContext.Destinations.Where(x => x.Id == desId).FirstOrDefaultAsync();
            var nextDestination = await _destinationRepository.GetNextDestination(desId);
            var thisTrip = await _dbContext.Trips.Where(x => x.Id == thisDestination.TripId).FirstOrDefaultAsync();
            var transportType = new SelectList(await _dbContext.TransportTypes.ToListAsync(), "Id", "Name");
            var travelerList = new SelectList(await _dbContext.Travelers.Where(x => x.TravelGroupId == thisTrip.TravelGroupId).ToListAsync(), "Id", "FullName");
            var thisTransport = new AddTransportVM
            {
                TripId = thisTrip.Id,
                TripName = thisTrip.TripName,
                TravelGroupId = thisTrip.TravelGroupId.Value,
                DestinationId = thisDestination.Id,
                DepartFrom = $"{thisDestination.City}, {thisDestination.Country}",
                ArriveIn = $"{nextDestination.City}, {nextDestination.Country}",
                TransportType = transportType
            };
            return thisTransport;
        }

        public async Task<AddTransportDetailsVM> GetTransportDetails(AddTransportVM model)
        {
            var thisTransport = new AddTransportDetailsVM
            {                
                DestinationId = model.DestinationId.Value,
                TripId = model.TripId.Value,
                TripName = model.TripName,
                //TransportType = model.TransportType.,
                FromAddress = model.DepartFrom,
                ToAddress = model.ArriveIn,
                //TransportTravelers = await GetTripTravelers(model.TravelGroupId.Value)
            };
            return thisTransport;
        }

        public async Task<TravelerTransportDetailsVM> GetTravelerTransportDetails(int transportId)
        {
            var nullResult = new TravelerTransportDetailsVM();
            var thisTransport = await _dbContext.Transports.Where(x => x.Id == transportId).FirstOrDefaultAsync();            
            if (thisTransport != null)
            {
                var thisTrip = await _dbContext.Trips.Where(x => x.Id == thisTransport.TripId).FirstOrDefaultAsync();
                var tripTravelers = await GetTripTravelers(thisTrip.TravelGroupId.Value);
                var transportTravelers = await GetTransportTravelers(thisTransport);
                var travelerList = new SelectList(await _dbContext.Travelers.Where(x => x.TravelGroupId == thisTrip.TravelGroupId).ToListAsync(), "Id", "FullName");
                var model = new TravelerTransportDetailsVM
                {
                    Id = thisTransport.Id,
                    TripId = thisTrip.Id,
                    TripName = thisTrip.TripName,
                    TravelGroupId = thisTrip.TravelGroupId.Value,
                    TransportType = thisTransport.TransportType,
                    Description = thisTransport.Description,
                    FromAddress = thisTransport.FromAddress,
                    ToAddress = thisTransport.ToAddress,
                    DepartureDatetime = thisTransport.DepartureDatetime,
                    ArrivalDatetime = thisTransport.ArrivalDatetime,                    
                    CostPerTraveler = thisTransport.CostPerTraveler,
                    TransportTravelers = transportTravelers,   
                    Travelers = travelerList
                };
                return model;
            }
            return nullResult;
        }

        public async Task<bool> EditTransport(Transport model)
        {
            try
            {
                var thisTransport = new Transport
                {
                    Id = model.Id,
                    ArrivalDatetime = model.ArrivalDatetime,
                    ArrivalDestinationId = model.ArrivalDestinationId,
                    CostPerTraveler = model.CostPerTraveler,
                    CreatedDate = model.CreatedDate,
                    DestinationId = model.DestinationId,
                    DepartureDestinationId = model.DepartureDestinationId,
                    DepartureDatetime = model.DepartureDatetime,
                    Description = model.Description,
                    FromAddress = model.FromAddress,
                    ModifiedDate = DateTime.Now,
                    PreferredAirport = model.PreferredAirport,
                    ToAddress = model.ToAddress,
                    TransportType = model.TransportType,
                    TravelGroupId = model.TravelGroupId,
                    TripId = model.TripId
                };
                _dbContext.Update(thisTransport);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<List<TravelersVM>> GetTransportTravelers(Transport transport)
        {
            List<TravelersVM> result = new List<TravelersVM>();
            var travelers = await _dbContext.TravelerTransports.Where(x => x.TransportId == transport.Id && x.TripId == transport.TripId.Value).ToListAsync();
            
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

        private async Task<List<TravelersVM>> GetTripTravelers(int travelGroupId)
        {
            var result = await _dbContext.Travelers.Where(x => x.TravelGroupId == travelGroupId).ToListAsync();
            return _mapper.Map<List<TravelersVM>>(result);
            ////return result;
        }

        public async Task<bool> AddNextDestinationTransport(AddTransportDetailsVM model)
        {
            //var thisTransport = _mapper.Map<Transport>(model);
            Transport thisTransport = new Transport
            {
                ArrivalDatetime = model.ArrivalDatetime,
                ArrivalDestinationId = model.ArrivalDestinationId,
                CostPerTraveler = model.CostPerTraveler,
                Description = model.Description,
                DestinationId = model.DestinationId,
                DepartureDatetime = model.DepartureDatetime,
                DepartureDestinationId = model.DepartureDestinationId,
                FromAddress = model.FromAddress,
                PreferredAirport = model.PreferredAirport,
                ToAddress = model.ToAddress,
                TransportType = model.TransportType,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            try
            {
                _dbContext.Add(thisTransport);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddHomeDestinationTransport(TransportToFirstDestinationVM model)
        {
            //var thisTransport = _mapper.Map<Transport>(model);
            Transport thisTransport = new Transport
            {
                ArrivalDatetime = model.ArrivalDatetime,
                ArrivalDestinationId = model.ArrivalDestinationId,
                CostPerTraveler = model.CostPerTraveler,
                Description = model.Description,
                DestinationId = model.DestinationId,
                DepartureDatetime = model.DepartureDatetime,
                DepartureDestinationId = model.DepartureDestinationId,
                FromAddress = model.FromAddress,
                PreferredAirport = model.PreferredAirport,
                ToAddress = model.ToAddress,
                TransportType = model.TransportType,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            try
            {
                _dbContext.Add(thisTransport);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddTransport( TransportToFirstDestinationVM model)
        {
            //var thisTransport = _mapper.Map<Transport>(model);
            Transport thisTransport = new Transport
            {
                TripId = model.TripId,
                ArrivalDatetime = model.ArrivalDatetime,
                ArrivalDestinationId = model.ArrivalDestinationId,
                CostPerTraveler = model.CostPerTraveler,
                Description = model.Description,
                DestinationId = model.DestinationId,
                DepartureDatetime = model.DepartureDatetime,
                DepartureDestinationId = model.DepartureDestinationId,
                FromAddress = model.FromAddress,
                PreferredAirport = model.PreferredAirport,
                ToAddress = model.ToAddress,
                TransportType = model.TransportType,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            try
            {
                _dbContext.Add(thisTransport);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddTravelerToTransport(int travelerId, int transportId, int tripId)
        {
            try
            {
                var isDup = await _dbContext.TravelerTransports.Where(x => x.TransportId == transportId && x.TravelerId == travelerId && x.TripId == tripId).FirstOrDefaultAsync();
                if (isDup == null)
                {
                    var _travelerTransport = new TravelerTransport
                    {
                        TransportId = transportId,
                        TravelerId = travelerId,
                        TripId = tripId
                    };
                    _dbContext.Add(_travelerTransport);
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

    }
}
