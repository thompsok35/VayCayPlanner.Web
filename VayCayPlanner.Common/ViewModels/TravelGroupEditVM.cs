using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VayCayPlanner.Common.ViewModels
{
    public class TravelGroupEditVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }

        [Display(Name = "Traveler Group Type (0-Private 1-Public)")]
        [Range(0, 1, ErrorMessage = "Invalid Id Entered")]
        public int TypeId { get; set; }

        [Display(Name = "Update Invitation Key")]
        public string? InvitationKey { get; set; }
    }
}
