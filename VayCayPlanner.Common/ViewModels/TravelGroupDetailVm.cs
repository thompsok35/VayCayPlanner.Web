using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VayCayPlanner.Common.ViewModels
{
    public class TravelGroupDetailVm
    {
        public int Id { get; set; }

        [Display(Name = "Group Name")]
        public string GroupName { get; set; }

        [Display(Name = "Traveler Group Type")]
        public int TypeId { get; set; }
        public SelectList? GroupTypes { get; set; }

        [Display(Name = "Traveler Group Owner")]        
        public string OwnerId { get; set; }
        public string FullName { get; set; }
        //UUID used to verify a new member 
        //This UUID will be generated when the group is first created
        [Display(Name = "Share Traveler Group")]
        public string? InvitationKey { get; set; }

    }
}
