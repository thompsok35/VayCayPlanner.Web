using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VayCayPlanner.Data.Models;

namespace VayCayPlanner.Data.Repositories.Contracts
{
    public interface ITravelGroupRepository
    {
        Task<List<TravelGroup>> MyTravelGroups();
        Task<bool> CreateTravelGroup(TravelGroup travelGroup);
    }
}
