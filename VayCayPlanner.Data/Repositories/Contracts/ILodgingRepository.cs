using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VayCayPlanner.Common.ViewModels;
using VayCayPlanner.Common.ViewModels.Lodgings;
using VayCayPlanner.Data.Models;

namespace VayCayPlanner.Data.Repositories.Contracts
{
    public interface ILodgingRepository
    {
        Task<AddLodgingVM> LoadAddLodgingVM(AddLodgingVM model);
        Task<List<LodgingsVM>> GetLodgingsByTripId(int? id);
        Task<LodgingDetailVM> GetTravelerLodgingDetails(int lodgingId);
        Task<Lodging> GetLodgingById(int Id);
        Task<EditLodgingVM> GetEditLodgingVM(Lodging lodging);
        Task<DateTime> AddLodging(AddLodgingVM model);
        Task<bool> EditLodging(EditLodgingVM model);
        Task<bool> AddTravelerToLodging(int travelerId, int lodgingId, int tripId);
    }
}
