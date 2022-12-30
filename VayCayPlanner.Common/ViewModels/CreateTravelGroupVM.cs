using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace VayCayPlanner.Common.ViewModels
{
    public class CreateTravelGroupVM
    {
        [Display(Name = "Enter your Group Name")]
        public string GroupName { get; set; }

        public int TypeId { get; set; }

        public string OwnerId { get; set; }

        public string? InvitationKey { get; set; }
    }
}
