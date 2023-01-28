using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VayCayPlanner.Data.Models
{
    public class Transport : BaseEntity
    {
        public int DestinationId { get; set; }
        public int? DepartureDestinationId { get; set; }
        public string? FromAddress { get; set; }
        public string? ToAddress { get; set; }
        public DateTime DepartureDatetime { get; set; }
        public string? PreferredAirport { get; set; }
        public int ArrivalDestinationId { get; set; }
        public DateTime ArrivalDatetime { get; set; }
        public string? TransportType { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
    }
}
