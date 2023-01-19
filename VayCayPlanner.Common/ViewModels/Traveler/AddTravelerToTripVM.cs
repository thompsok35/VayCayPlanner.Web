using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VayCayPlanner.Common.ViewModels.Traveler
{
    public class AddTravelerToTripVM
    {
        public int TripId { get; set; }

        [Display(Name = "Trip Name")]
        public string TripName { get; set; }

        public int TravelGroupId { get; set; }

        [Display(Name = "Traveler Name")]
        public string? FullName { get; set; }

        [Display(Name = "Email Address")]
        public string? EmailAddress { get; set; }
    }
}
