using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VayCayPlanner.Common.ViewModels.Destination
{
    public class TripWithDestinationsVM
    {
        public TripWithDestinationsVM(List<DestinationVM>? destinations)
        {
            TripDestinations = destinations;
        }
        public int Id { get; set; }

        public int tripId { get; set; }

        [Display(Name = "Trip Name")]
        public string TripName { get; set; }

        public List<DestinationVM> TripDestinations { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Arrival Date")]
        public DateTime? ArrivalDate { get; set; }

        [Display(Name = "Departure Date")]
        public DateTime? DepartureDate { get; set; }

    }
}
