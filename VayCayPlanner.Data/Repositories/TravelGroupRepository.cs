using AutoMapper;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using VayCayPlanner.Common.ViewModels;
using VayCayPlanner.Common.ViewModels.Trip;
using VayCayPlanner.Data.Models;
using VayCayPlanner.Data.Repositories.Contracts;

namespace VayCayPlanner.Data.Repositories
{
    public  class TravelGroupRepository : ITravelGroupRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Subscriber> _userManager;
        private readonly IMapper _mapper;
        private readonly ITravelerRepository _travelerRepository;

        public TravelGroupRepository(ApplicationDbContext dbContext,
                    IHttpContextAccessor httpContextAccessor,
                    UserManager<Subscriber> userManager,
                    ITravelerRepository travelerRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _travelerRepository = travelerRepository;
            _mapper = mapper;
        }

        public async Task<List<TravelGroup>> MyTravelGroups()
        {
            var user = await CurrentUser();
            
            try
            {
                var myTravelGroups = await _dbContext.TravelGroups.Where(x => x.OwnerId == user.Id).ToListAsync();
                return myTravelGroups;
            }
            catch (Exception)
            {
                //log this error
                List<TravelGroup> myTravelGroups = new();
                return myTravelGroups;
            }
        }

        public async Task<List<TravelGroup>> MyTravelGroupMemberships()
        {
            var user = await CurrentUser();            
            List<TravelGroup> myTravelGroups = new();
            try
            {
                var myTravelGroupIds = await _dbContext.Travelers.Where(x => x.EmailAddress == user.TravelerEmail).ToListAsync();
                foreach (var group in myTravelGroupIds)
                {
                    var thisGroup = _dbContext.TravelGroups.Where(x => x.Id == group.TravelGroupId).FirstOrDefault();
                    if (thisGroup != null)
                    {
                        myTravelGroups.Add(thisGroup);
                    }                    
                }
                return myTravelGroups;
            }
            catch (Exception)
            {
                //log this error
                return myTravelGroups;
            }
        }

        public async Task<TravelGroupDetailVM> GetTravelGroupDetails(int groupId)
        {
            var thisGroup = await _dbContext.TravelGroups.Where(x => x.Id == groupId).FirstOrDefaultAsync();
            var groupMembers = _mapper.Map<List<TravelersVM>>(await _dbContext.Travelers.Where(x => x.TravelGroupId == thisGroup.Id).ToListAsync());
            var user = await _userManager.FindByIdAsync(thisGroup.OwnerId);
            var model = new TravelGroupDetailVM(groupMembers)
            {
                Id = thisGroup.Id,
                GroupName = thisGroup.GroupName,
                GroupType = GetGroupType(thisGroup.TypeId),
                OwnerName = user.Email,
                InvitationKey = thisGroup.InvitationKey
            };
            return model;            
        }

        public async Task<bool> CreateTravelGroup(TravelGroup travelGroup)
        {
            try
            {
                var user = await CurrentUser();
                var thisTravelGroup = new TravelGroup
                {
                    GroupName = travelGroup.GroupName,
                    InvitationKey = GenerateKey(),
                    OwnerId = user.Id,
                    TypeId = 0
                };

                _dbContext.Add(thisTravelGroup);
                await _dbContext.SaveChangesAsync();
                await AddTravelerToNewGroup(thisTravelGroup);
            }
            catch (Exception)
            {
                //log error
                return false;                
            }
            return true;
        }

        public async Task<int> CreateTripTravelGroup(string newTripName)
        {
            int result = 0;
            var _key = GenerateKey();
            try
            {
                var user = await CurrentUser();
                var thisTravelGroup = new TravelGroup
                {
                    GroupName = newTripName,
                    InvitationKey = _key,
                    OwnerId = user.Id,
                    TypeId = 0
                };

                _dbContext.Add(thisTravelGroup);
                await _dbContext.SaveChangesAsync();
                var newGroup = await _dbContext.TravelGroups.Where(x => x.InvitationKey == _key).FirstOrDefaultAsync();
                if (newGroup != null)
                {
                    result = newGroup.Id; 
                }
            }
            catch (Exception)
            {
                //log error
                return result;
            }
            return result;
        }

        private async Task<bool> AddTravelerToNewGroup(TravelGroup travelGroup)
        {
            var newGroup = await _dbContext.TravelGroups.Where(x => x.GroupName.ToLower() == travelGroup.GroupName.ToLower()).FirstOrDefaultAsync();
            if (newGroup != null)
            {
                return await _travelerRepository.AddTravelerToGroup(newGroup.Id); 
            }
            return false;
        }

        public async Task<bool> EditTravelGroup(int id, TravelGroupEditVM travelGroupVM)
        {
            var travelGroup = await _dbContext.TravelGroups.Where(x => x.Id == id).FirstOrDefaultAsync();
            try
            {
                var record = _mapper.Map(travelGroupVM, travelGroup);
                if (record != null)
                {
                    _dbContext.Update(record);
                    await _dbContext.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {
                // log error
                return false;
            }
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

        private string GenerateKey()
        {
            var thiskey = System.Guid.NewGuid().ToString("D");
            return thiskey;
        }

        private string GetGroupType(int Id)
        {
            if (Id == 0)
            {
                return "Private";
            }
            else
            {
                return "Public";
            }
        }
    }
}
