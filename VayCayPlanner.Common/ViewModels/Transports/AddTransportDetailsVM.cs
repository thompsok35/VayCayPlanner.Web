using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VayCayPlanner.Common.ViewModels.Transports
{
    public class AddTransportDetailsVM
    {
        [Display(Name = "Transport Type")]
        public string? TransportType { get; set; }
        public string? Description { get; set; }

        //public string DestinationName { get; set; }
        public string TripName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        [Display(Name = "Departure Airport")]
        public string? PreferredAirport { get; set; }
        [Required]
        [Display(Name = "Departure Date")]
        public DateTime DepartureDatetime { get; set; }

        [Display(Name = "Arrival Date")]
        public DateTime ArrivalDatetime { get; set; }

        [Display(Name = "Enter the cost per person")]
        public decimal? CostPerTraveler { get; set; }

        public SelectList? Travelers { get; set; }

        // Reference data
        public List<TravelersVM> TransportTravelers { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public int TripId { get; set; }
        public int TravelGroupId { get; set; }
        public int DestinationId { get; set; }
        public int ArrivalDestinationId { get; set; }
        public int? DepartureDestinationId { get; set; }

        [Display(Name = "From")]
        public string? FromAddress { get; set; }

        [Display(Name = "To")]
        public string? ToAddress { get; set; }
    }
}
