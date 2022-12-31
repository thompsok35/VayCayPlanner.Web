﻿using AutoMapper;
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
using VayCayPlanner.Data.Models;
using VayCayPlanner.Data.Repositories.Contracts;

namespace VayCayPlanner.Data.Repositories
{
    public  class TravelGroupRepository : ITravelGroupRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Subscriber> _userManager;

        public TravelGroupRepository(ApplicationDbContext dbContext,
                    IHttpContextAccessor httpContextAccessor,
                    UserManager<Subscriber> userManager, IMapper mapper)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
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
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                //log error
                return false;                
            }
            return true;
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
    }
}
