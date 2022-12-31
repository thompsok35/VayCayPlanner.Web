using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VayCayPlanner.Common.ViewModels
{
    public class CreateTravelerVM
    {
        [Display(Name = "Select Travel Group")]
        public int TravelGroupId { get; set; }
        //Source: TripRepository.GetUpcomingTrips()
        public SelectList? TravelGroups { get; set; }

        [Display(Name = "Travelers Name")]
        public string? FullName { get; set; }

        [Display(Name = "Email Address")]
        public string? EmailAddress { get; set; }

    }
}
