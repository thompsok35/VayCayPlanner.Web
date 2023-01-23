using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VayCayPlanner.Common.ViewModels.Destination;
using VayCayPlanner.Common.ViewModels.Trip;
using VayCayPlanner.Data.Models;

namespace VayCayPlanner.Data.Repositories.Contracts
{
    public interface IDestinationRepository
    {
        Task<bool> AddFirstDestination(CreateNewTripVM createNewTripVM);
        Task<Destination> GetFirstDestinationByTripId(int tripId);        
        Task<TripWithDestinationsVM> GetDestinationsByTripId(int tripId);
        Task<bool> AddDestinationToTrip(AddDestinationVM model);
        Task<DestinationDetailVM> GetDestinationDetailById(int id);
    }
}
