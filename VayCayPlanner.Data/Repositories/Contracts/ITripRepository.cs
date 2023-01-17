using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VayCayPlanner.Common.ViewModels.Trip;

namespace VayCayPlanner.Data.Repositories.Contracts
{
    public interface ITripRepository
    {
        Task<CreateNewTripVM> CreateNewTrip(string tripName);
    }
}
