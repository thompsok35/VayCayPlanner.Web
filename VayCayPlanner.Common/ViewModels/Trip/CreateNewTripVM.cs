using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VayCayPlanner.Common.ViewModels.Trip
{
    public class CreateNewTripVM
    {
        public CreateNewTripVM(List<TravelersVM> travelers)
        {
            Travelers = travelers;
        }
        public int Id { get; set; }
        public int TripId { get; set; }
        public int GroupId { get; set; }

        [Display(Name = "Trip Name")]
        public string TripName { get; set; }

        [Display(Name = "Created By")]
        public string OwnerName { get; set; }

        [Display(Name = "City, Country")]
        public string DestinationName { get; set; }

        [Display(Name = "Destination arrival date")]
        public DateTime? DestinationArrivalDate { get; set; }

        [Display(Name = "Departure date for your first destination")]
        public DateTime? DestinationDepartureDate { get; set; }

        [Display(Name = "Trip start date")]
        public DateTime? TripStartDate { get; set; }

        [Display(Name = "Count down")]
        public int DaysUntilDeparture { get; set; }

        [Display(Name = "Trip end date")]
        public DateTime? TripEndDate { get; set; }
        
        public List<TravelersVM>? Travelers { get; set; }

        [Display(Name = "Traveler Name")]
        public string? FullName { get; set; }

        [Display(Name = "Email Address")]
        public string? EmailAddress { get; set; }
    }
}
