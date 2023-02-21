using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VayCayPlanner.Common.ViewModels.Destination;
using VayCayPlanner.Common.ViewModels.Trip;

namespace VayCayPlanner.Common.ViewModels.Transports
{
    public class TripTransportsVM
    {
        public TripTransportsVM(TripVM trip)
        {
            TripName = trip.TripName;            
        }

        public TripTransportsVM(List<TransportVM> transports, TripVM trip)
        {
            TripTransports = transports.OrderBy(x => x.DepartureDatetime).ToList();
            TripName = trip.TripName;
        }

        public List<TransportVM> TripTransports { get; set; }

        public int? Id { get; set; }
        public int? TripId { get; set; }

        [Display(Name = "Trip Name")]
        public string? TripName { get; set; }

        [Display(Name = "Destination Name")]
        public string? DestinationName { get; set; }

        [Display(Name = "Transport Type")]
        public string? TransportType { get; set; }
        public string? Description { get; set; }
        public int? DestinationId { get; set; }
        //public int? DepartureDestinationId { get; set; }
        [Display(Name = "Departing from")]
        public string? FromAddress { get; set; }
        [Display(Name = "Arriving in")]
        public string? ToAddress { get; set; }

        [Display(Name = "Departing Time")]
        public DateTime DepartureDatetime { get; set; }
        //public string? PreferredAirport { get; set; }
        //public int? ArrivalDestinationId { get; set; }
        [Display(Name = "Arrival Time")]
        public DateTime ArrivalDatetime { get; set; }

        [Display(Name = "Cost per Person")]
        public decimal? CostPerTraveler { get; set; }
    }
}
