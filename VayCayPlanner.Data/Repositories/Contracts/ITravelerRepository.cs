using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VayCayPlanner.Data.Models;
using VayCayPlanner.Common.Constants;
using VayCayPlanner.Common.ViewModels;
using VayCayPlanner.Common.Traveler.ViewModels;
using VayCayPlanner.Common.ViewModels.Traveler;

namespace VayCayPlanner.Data.Repositories.Contracts
{
    public interface ITravelerRepository
    {
        Task<bool> AddTravelerToGroup(CreateTravelerVM model);
        Task<bool> AddTravelerToGroup(int groupId);
        Task<bool> AddTravelerToTrip(AddTravelerToTripVM viewModel);
        Task<List<Traveler>> GetTravelersByGroupId(int id);
        Task<bool> EditTraveler(int id, TravelerEditVM viewModel);
        Task<TravelerDetailVM> GetTravelerDetail(int id);

        Task<bool> AddTraveler(TravelerAddVM viewModel);
    }
}
