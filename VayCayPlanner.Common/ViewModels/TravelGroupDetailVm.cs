using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VayCayPlanner.Common.ViewModels
{
    public class TravelGroupDetailVM
    {
        public TravelGroupDetailVM(List<TravelersVM> travelers)
        {
            Travelers = travelers;
        }
        public int Id { get; set; }

        public int TripId { get; set; }

        [Display(Name = "Group Name")]
        public string GroupName { get; set; }

        [Display(Name = "Traveler Group Type")]
        public string GroupType { get; set; }

        [Display(Name = "Traveler Name")]
        public string FullName { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        public List<TravelersVM>? Travelers { get; set; }

        [Display(Name = "Travel Group Owner")]        
        public string OwnerId { get; set; }
        public string OwnerName { get; set; }
        //UUID used to verify a new member 
        //This UUID will be generated when the group is first created
        [Display(Name = "Share Traveler Group")]
        public string? InvitationKey { get; set; }

    }
}
