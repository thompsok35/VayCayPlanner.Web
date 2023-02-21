using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VayCayPlanner.Common.ViewModels.Destination;
using VayCayPlanner.Common.ViewModels.Transports;
using VayCayPlanner.Data.Models;

namespace VayCayPlanner.Data.Repositories.Contracts
{
    public interface ITransportRepository
    {
        Task<TransportToFirstDestinationVM> CreateTransportToFirstDestination(DestinationDetailVM model);
        Task<bool> EditTransport(Transport model);
        //Task<bool> AddTransport(TransportToFirstDestinationVM model);
        Task<DateTime> AddTransport(TransportToFirstDestinationVM model);
        Task<bool> AddTravelerToTransport(int travelerId, int transportId, int tripId);
        Task<bool> AddNextDestinationTransport(AddTransportDetailsVM model);
        Task<List<TripTransportsVM>> GetTripTransportsByTripId(int? id);
        Task<TripTransportsVM> GetTransportsByTripId(int? id);
        Task<AddTransportDetailsVM> GetTransportDetails(AddTransportVM model);
        Task<AddTransportVM> GetNextTransportViewModel(int desId);
        Task<AddTransportVM> GetFirstTransportViewModel(int desId);
        Task<AddTransportVM> GetLastTransportViewModel(int desId);
        Task<TravelerTransportDetailsVM> GetTravelerTransportDetails(int transportId);
    }
}
