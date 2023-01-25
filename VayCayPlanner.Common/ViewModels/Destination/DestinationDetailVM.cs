using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VayCayPlanner.Common.Traveler.ViewModels;

namespace VayCayPlanner.Common.ViewModels.Destination
{
    public class DestinationDetailVM
    {
        public int Id { get; set; }
        public int DestinationId { get; set; }
        public int TripId { get; set; }
        public int TravelGroupId { get; set; }

        [Display(Name = "Trip Name")]
        public string TripName { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Arrival Date")]
        public DateTime? ArrivalDate { get; set; }

        [Display(Name = "Departure Date")]
        public DateTime? DepartureDate { get; set; }

        //
        public SelectList? Travelers { get; set; }

        public List<TravelersVM>? DestinationTravelers { get; set; }

        public int TravelerId { get; set; }

        [Display(Name = "Traveler Name")]
        public string? FullName { get; set; }

        [Display(Name = "Email Address")]
        public string? EmailAddress { get; set; }

        //public int Transports { get; set; }
        //public string Lodging { get; set; }
        //public int Activities { get; set; }
        //public int Meals { get; set; }

    }
}
