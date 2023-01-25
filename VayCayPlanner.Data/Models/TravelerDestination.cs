using System.ComponentModel.DataAnnotations;

namespace VayCayPlanner.Data.Models
{
    public class TravelerDestination
    { 
        public int Id { get; set; }
        public int TravelerId { get; set; }
        public int TripId { get; set; }       
        public int DestinationId { get; set; }
    }
}
