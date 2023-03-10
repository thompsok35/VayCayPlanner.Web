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
        Task<Destination> EditDestination(Destination model);
        Task<Destination> GetFirstDestinationByTripId(int tripId);
        Task<Destination> GetDestinationById(int Id);
        Task<string> GetDestinationNameById(int Id);
        Task<List<Destination>> GetDestinationsByDate(int Id);
        Task<TripWithDestinationsVM> GetDestinationsByTripId(int tripId);
        Task<Destination> GetNextDestination(int id);
        Task<Destination> GetFirstDestination(int tripId);
        Task<DateTime> AddDestinationToTrip(AddDestinationVM model);
        Task<bool> AddTravelerToDestination(int travelerId, int destinationId, int tripId);
        Task<DestinationDetailVM> GetDestinationDetailById(int id);
    }
}
