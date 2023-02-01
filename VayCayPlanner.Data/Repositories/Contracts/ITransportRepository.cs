using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VayCayPlanner.Common.ViewModels.Destination;
using VayCayPlanner.Common.ViewModels.Transports;

namespace VayCayPlanner.Data.Repositories.Contracts
{
    public interface ITransportRepository
    {
        Task<TransportToFirstDestinationVM> CreateTransportToFirstDestination(DestinationDetailVM model);
        Task<bool> AddTransport(TransportToFirstDestinationVM model);
    }
}
