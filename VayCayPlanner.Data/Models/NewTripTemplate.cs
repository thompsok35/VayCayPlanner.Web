using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VayCayPlanner.Data.Models
{
    public class NewTripTemplate
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? TripId { get; set; }
        public string? TripName { get; set; }
        public int? TravelGroupId { get; set; }
        public int? DestinationId { get; set; }
        public string? DestinationName { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? DepartureDate { get; set; }

        [MaxLength(256)]
        public string? FullName { get; set; }

        [MaxLength(256)]
        public string? EmailAddress { get; set; }
        public bool isTripComplete { get; set; }
        public bool isTravelersComplete { get; set; }
        public bool isDestinationComplete { get; set; }
        public bool? isComplete { get; set; }

    }
}
