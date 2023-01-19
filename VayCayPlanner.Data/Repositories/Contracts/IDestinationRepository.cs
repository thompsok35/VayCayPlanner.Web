using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VayCayPlanner.Common.ViewModels.Trip;
using VayCayPlanner.Data.Models;

namespace VayCayPlanner.Data.Repositories.Contracts
{
    public interface IDestinationRepository
    {
        Task<bool> AddFirstDestination(CreateNewTripVM createNewTripVM);
        Task<Destination> GetFirstDestinationByTripId(int tripId);
    }
}
