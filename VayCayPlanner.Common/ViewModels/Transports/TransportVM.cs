using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VayCayPlanner.Common.ViewModels.Transports
{
    public class TransportVM
    {
        public int Id { get; set; }
        public int? TripId { get; set; }
        public int? DestinationId { get; set; }
        public int? DepartureDestinationId { get; set; }
        public string? FromAddress { get; set; }
        public string? ToAddress { get; set; }
        public DateTime DepartureDatetime { get; set; }
        public string? PreferredAirport { get; set; }
        public int? ArrivalDestinationId { get; set; }
        public DateTime ArrivalDatetime { get; set; }
        public string? TransportType { get; set; }
        public string? Description { get; set; }

        [Column(TypeName = "money")]
        public decimal? CostPerTraveler { get; set; }
    }
}
