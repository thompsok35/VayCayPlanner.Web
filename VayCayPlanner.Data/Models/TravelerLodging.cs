using System.ComponentModel.DataAnnotations;

namespace VayCayPlanner.Data.Models
{
    public class TravelerLodging
    { 
        public int Id { get; set; }
        public int TravelerId { get; set; }
        public int TripId { get; set; }       
        public int LodgingId { get; set; }
    }
}
