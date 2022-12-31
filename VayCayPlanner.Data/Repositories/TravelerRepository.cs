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
using VayCayPlanner.Data.Models;
using VayCayPlanner.Data.Repositories.Contracts;

namespace VayCayPlanner.Data.Repositories
{
    public  class TravelerRepository : ITravelerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Subscriber> _userManager;

        public TravelerRepository(ApplicationDbContext dbContext,
                    IHttpContextAccessor httpContextAccessor,
                    UserManager<Subscriber> userManager, IMapper mapper)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<List<Traveler>> MyTravelGroupIds()
        {
            var user = await CurrentUser();
            List<Traveler> myTravelGroupIds = new();
            try
            {
                var myGroups = await _dbContext.Travelers.Where(x => x.EmailAddress.ToLower() == user.TravelerEmail.ToLower()).ToListAsync();
                return myGroups;
            }
            catch (Exception)
            {
                //log this error
                return myTravelGroupIds;
            }
        }

        public async Task<bool> EditTraveler(int id, TravelerEditVM viewModel)
        {
            var thisRecord = await _dbContext.Travelers.Where(x => x.Id == id).FirstOrDefaultAsync();
            try
            {
                if (thisRecord != null)
                {
                    thisRecord.FullName = viewModel.FullName;
                    thisRecord.EmailAddress = viewModel.EmailAddress;
                    thisRecord.ModifiedDate = DateTime.Now;
                    //var record = _mapper.Map(travelGroupVM, travelGroup);
                    _dbContext.Update(thisRecord);
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

        public async Task<bool> AddTravelerToGroup(CreateTravelerVM viewModel)
        {
            try
            {
                var user = await CurrentUser();
                var dataModel = new Traveler
                {
                    FullName = viewModel.FullName,
                    EmailAddress = viewModel.EmailAddress,
                    TravelGroupId = viewModel.TravelGroupId,
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now
                };
                if (!isEmailInGroup(viewModel.TravelGroupId, viewModel.EmailAddress).Result)
                {
                    _dbContext.Add(dataModel);
                    _dbContext.SaveChanges();
                }

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

        private async Task<bool> isEmailInGroup(int groupId, string emailAddress)
        {
            var _list = await _dbContext.Travelers.Where(x => x.TravelGroupId == groupId && x.EmailAddress == emailAddress).FirstOrDefaultAsync();
            if (_list == null)
            {
                return false;
            }            
            return true;
        }
    }
}
