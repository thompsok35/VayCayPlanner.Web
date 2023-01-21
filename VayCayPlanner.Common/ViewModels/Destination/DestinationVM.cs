using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using VayCayPlanner.Common.ViewModels.Trip;

namespace VayCayPlanner.Common.ViewModels.Destination
{
    public class DestinationVM
    {
        //public DestinationVM (TripVM trip)
        //{
        //    TripName = trip.TripName;
        //}
        public int Id { get; set; }
        //public int TripId { get; set; }

        //[Display(Name = "Trip Name")]
        //public string TripName { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Arrival Date")]
        public DateTime? ArrivalDate { get; set; }

        [Display(Name = "Departure Date")]
        public DateTime? DepartureDate { get; set; }

        public int Travelers { get; set; }
        //public int Transports { get; set; }
        //public string Lodging { get; set; }
        //public int Activities { get; set; }
        //public int Meals { get; set; }

    }
}
