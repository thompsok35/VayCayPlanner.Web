using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VayCayPlanner.Common.ViewModels.Destination;
using VayCayPlanner.Common.ViewModels.Transports;
using VayCayPlanner.Data.Models;
using VayCayPlanner.Data.Repositories.Contracts;

namespace VayCayPlanner.Data.Repositories
{
    public class TransportRepository : ITransportRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITravelGroupRepository _travelGroupRepository;
        
        private readonly ITravelerRepository _travelerRepository;
        private readonly UserManager<Subscriber> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<TripRepository> _logger;

        public TransportRepository(ApplicationDbContext dbContext,
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

        async public Task<TransportToFirstDestinationVM> CreateTransportToFirstDestination(DestinationDetailVM model)
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

        public async Task<bool> AddNextDestinationTransport(TransportToFirstDestinationVM model)
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

        public async Task<bool> AddTransport(TransportToFirstDestinationVM model)
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

    }
}
