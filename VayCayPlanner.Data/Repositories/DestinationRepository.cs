using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<bool> AddDestination(CreateNewTripVM createNewTripVM)
        {

            try
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
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Adding destination [{ex.StackTrace}]");
                return false;                
            }
        }
    }
}
