using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VayCayPlanner.Data.Models;
using VayCayPlanner.Common.Constants;
using VayCayPlanner.Common.ViewModels;

namespace VayCayPlanner.Data.Repositories.Contracts
{
    public interface ITravelerRepository
    {
        Task<bool> AddTravelerToGroup(CreateTravelerVM model);

        Task<bool> EditTraveler(int id, TravelerEditVM viewModel);

        Task<bool> AddTraveler(TravelerAddVM viewModel);
    }
}
