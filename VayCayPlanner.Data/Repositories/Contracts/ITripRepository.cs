using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VayCayPlanner.Common.ViewModels.Trip;
using VayCayPlanner.Data.Models;

namespace VayCayPlanner.Data.Repositories.Contracts
{
    public interface ITripRepository
    {
        Task<CreateNewTripVM> CreateNewTrip(string tripName);
        Task<bool> UpdateTripEndDate(int id, DateTime departureDate);
        Task<DeleteTripVM> GetAllTripObjects(int tripId);
        Task<Trip> GetTripById(int Id);
        Task<int?> GetNextTrip();
        Task<bool> DeleteAllTripObjects(int tripId);
        Task<Trip> GetTripByGroupId(int groupId);
        Task<TripDetailVM> GetTripDetail(int Id);
        Task<List<Trip>> GetUpcomingTripsAsync();
        Task<List<Trip>> GetPastTripsAsync();
    }
}
